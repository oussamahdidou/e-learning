using api.Data;
using api.Dtos.NiveauScolaire;
using api.generique;
using api.interfaces;
using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class NiveauScolaireRepository : INiveauScolaireRepository
    {
        private readonly apiDbContext apiDbContext;
        private readonly IBlobStorageService blobStorageService;
        private string pdfContainer = "pdf-container";
        private string videoContainer = "video-container";
        private string schemaContainer = "schema-container";
        private string syntheseContainer = "synthese-container";
        private string controleContainer = "controle-container";
        private string imageContainer = "image-container";
        private string programContainer = "program-container";

        public NiveauScolaireRepository(apiDbContext apiDbContext, IBlobStorageService blobStorageService)
        {
            this.apiDbContext = apiDbContext;
            this.blobStorageService = blobStorageService;
        }
        public async Task<Result<NiveauScolaire>> CreateNiveauScolaire(CreateNiveauScolaireDto createNiveauScolaireDto)
        {
            try
            {
                NiveauScolaire niveauScolaire = new NiveauScolaire()
                {
                    Nom = createNiveauScolaireDto.Nom,
                    InstitutionId = createNiveauScolaireDto.InstitutionId,
                };



                await apiDbContext.niveauScolaires.AddAsync(niveauScolaire);
                await apiDbContext.SaveChangesAsync();
                return Result<NiveauScolaire>.Success(niveauScolaire);
            }
            catch (Exception ex)
            {
                return Result<NiveauScolaire>.Failure($"{ex.Message}");
            }
        }

        public async Task<Result<NiveauScolaire>> GetNiveauScolaireById(int id)
        {
            try
            {
                NiveauScolaire? niveauScolaire = await apiDbContext.niveauScolaires.Include(x => x.NiveauScolaireModules).ThenInclude(x => x.Module).FirstOrDefaultAsync(x => x.Id == id);
                if (niveauScolaire == null)
                {
                    return Result<NiveauScolaire>.Failure("niveauScolaire notfound");

                }
                return Result<NiveauScolaire>.Success(niveauScolaire);
            }
            catch (System.Exception ex)
            {

                return Result<NiveauScolaire>.Failure(ex.Message);
            }
        }
        public async Task<Result<NiveauScolaire>> UpdateNiveauScolaire(UpdateNiveauScolaireDto updateNiveauScolaireDto)
        {
            try
            {
                NiveauScolaire? NiveauScolaire = await apiDbContext.niveauScolaires.FirstOrDefaultAsync(x => x.Id == updateNiveauScolaireDto.NiveauScolaireId);
                if (NiveauScolaire == null)
                {
                    return Result<NiveauScolaire>.Failure("NiveauScolaire notfound");

                }
                NiveauScolaire.Nom = updateNiveauScolaireDto.Nom;
                await apiDbContext.SaveChangesAsync();
                return Result<NiveauScolaire>.Success(NiveauScolaire);
            }
            catch (System.Exception ex)
            {

                return Result<NiveauScolaire>.Failure($"{ex.Message}");
            }
        }
        public async Task<bool> DeleteNiveauScolaire(int niveauScolaireId)
        {
            try
            {
                // Retrieve the NiveauScolaire with all related entities
                var niveauScolaire = await apiDbContext.niveauScolaires
                    .Include(ns => ns.NiveauScolaireModules)
                        .ThenInclude(nsm => nsm.Module)
                            .ThenInclude(m => m.ExamFinal)
                                .ThenInclude(ef => ef.ResultExams)
                    .Include(ns => ns.ElementPedagogiques)
                    .Include(ns => ns.NiveauScolaireModules)
                        .ThenInclude(nsm => nsm.Module)
                            .ThenInclude(m => m.Chapitres)
                                .ThenInclude(c => c.Quiz)
                                    .ThenInclude(q => q.Questions)
                                        .ThenInclude(q => q.Options)
                    .Include(ns => ns.NiveauScolaireModules)
                        .ThenInclude(nsm => nsm.Module)
                            .ThenInclude(m => m.Chapitres)
                                .ThenInclude(c => c.Quiz)
                    .Include(ns => ns.NiveauScolaireModules)
                        .ThenInclude(nsm => nsm.Module)
                            .ThenInclude(m => m.Chapitres)
                            .ThenInclude(c => c.Quiz)
                                .ThenInclude(c => c.QuizResults)
                    .Include(ns => ns.NiveauScolaireModules)
                        .ThenInclude(nsm => nsm.Module)
                            .ThenInclude(m => m.TestNiveaus)
                    .Include(ns => ns.NiveauScolaireModules)
                        .ThenInclude(nsm => nsm.Module)
                            .ThenInclude(m => m.ModuleRequirements)
                    .Include(ns => ns.NiveauScolaireModules)
                        .ThenInclude(nsm => nsm.Module)
                            .ThenInclude(m => m.ModulesRequiredIn)
                    .Include(ns => ns.NiveauScolaireModules)
                        .ThenInclude(nsm => nsm.Module)
                            .ThenInclude(m => m.Chapitres)
                                .ThenInclude(c => c.Controle)
                                    .ThenInclude(ctrl => ctrl.ResultControles)
                    .Include(ns => ns.NiveauScolaireModules)
                        .ThenInclude(nsm => nsm.Module)
                            .ThenInclude(m => m.Chapitres)
                                .ThenInclude(c => c.CheckChapters)
                    .FirstOrDefaultAsync(ns => ns.Id == niveauScolaireId);

                if (niveauScolaire != null)
                {
                    // Delete Element Pedagogique Links
                    foreach (var elementPedagogique in niveauScolaire.ElementPedagogiques)
                    {
                        if (!string.IsNullOrEmpty(elementPedagogique.Lien))
                        {
                            var oldLienFileName = Path.GetFileName(new Uri(elementPedagogique.Lien).LocalPath);
                            await blobStorageService.DeleteFileAsync(imageContainer, oldLienFileName);
                        }
                    }

                    // Remove the NiveauScolaire from the database
                    apiDbContext.niveauScolaires.Remove(niveauScolaire);
                    await apiDbContext.SaveChangesAsync();

                    // Identify and delete orphaned modules (modules not linked to any NiveauScolaireModule)
                    var orphanedModules = await apiDbContext.modules
                        .Include(m => m.Chapitres)
                            .ThenInclude(c => c.Cours)
                                .ThenInclude(cours => cours.Paragraphes)
                        .Include(m => m.Chapitres)
                            .ThenInclude(c => c.Controle)
                        .Include(m => m.ExamFinal)
                        .Include(m => m.Chapitres)
                            .ThenInclude(c => c.Quiz)
                                .ThenInclude(q => q.Questions)
                                    .ThenInclude(q => q.Options)
                        .Include(m => m.Chapitres)
                            .ThenInclude(c => c.Quiz)
                                .ThenInclude(q => q.QuizResults)
                        .Include(m => m.Chapitres)
                            .ThenInclude(c => c.CheckChapters)
                        .Where(m => !apiDbContext.niveauScolaireModules.Any(nsm => nsm.ModuleId == m.Id))
                        .ToListAsync();

                    // Delete associated files for each orphaned module
                    foreach (var module in orphanedModules)
                    {
                        if (!string.IsNullOrEmpty(module.ModuleImg))
                        {
                            var Image = Path.GetFileName(new Uri(module.ModuleImg).LocalPath);
                            await blobStorageService.DeleteFileAsync(imageContainer, Image);
                        }
                        if (!string.IsNullOrEmpty(module.CourseProgram))
                        {
                            var CourseProgram = Path.GetFileName(new Uri(module.CourseProgram).LocalPath);
                            await blobStorageService.DeleteFileAsync(programContainer, CourseProgram);
                        }
                        // Delete ExamFinal files
                        if (module.ExamFinal != null)
                        {
                            if (!string.IsNullOrEmpty(module.ExamFinal.Ennonce))
                            {
                                var oldExamFileName = Path.GetFileName(new Uri(module.ExamFinal.Ennonce).LocalPath);
                                await blobStorageService.DeleteFileAsync(controleContainer, oldExamFileName);
                            }
                            if (!string.IsNullOrEmpty(module.ExamFinal.Solution))
                            {
                                var oldSolutionFileName = Path.GetFileName(new Uri(module.ExamFinal.Solution).LocalPath);
                                await blobStorageService.DeleteFileAsync(controleContainer, oldSolutionFileName);
                            }
                        }

                        // Delete Chapitre files
                        foreach (var chapitre in module.Chapitres)
                        {
                            if (!string.IsNullOrEmpty(chapitre.VideoPath))
                            {
                                var oldVideoFileName = Path.GetFileName(new Uri(chapitre.VideoPath).LocalPath);
                                await blobStorageService.DeleteFileAsync(videoContainer, oldVideoFileName);
                            }
                            if (!string.IsNullOrEmpty(chapitre.Schema))
                            {
                                var oldSchemaFileName = Path.GetFileName(new Uri(chapitre.Schema).LocalPath);
                                await blobStorageService.DeleteFileAsync(schemaContainer, oldSchemaFileName);
                            }
                            if (!string.IsNullOrEmpty(chapitre.Synthese))
                            {
                                var oldSyntheseFileName = Path.GetFileName(new Uri(chapitre.Synthese).LocalPath);
                                await blobStorageService.DeleteFileAsync(syntheseContainer, oldSyntheseFileName);
                            }

                            // Delete Cours Paragraphes files
                            foreach (var cours in chapitre.Cours)
                            {
                                foreach (var paragraphe in cours.Paragraphes)
                                {
                                    if (!string.IsNullOrEmpty(paragraphe.Contenu))
                                    {
                                        var oldParagrapheFileName = Path.GetFileName(new Uri(paragraphe.Contenu).LocalPath);
                                        await blobStorageService.DeleteFileAsync(pdfContainer, oldParagrapheFileName);
                                    }
                                }
                            }

                            // Delete Controle files
                            if (chapitre.Controle != null)
                            {
                                if (!string.IsNullOrEmpty(chapitre.Controle.Ennonce))
                                {
                                    var oldControleFileName = Path.GetFileName(new Uri(chapitre.Controle.Ennonce).LocalPath);
                                    await blobStorageService.DeleteFileAsync(controleContainer, oldControleFileName);
                                }
                                if (!string.IsNullOrEmpty(chapitre.Controle.Solution))
                                {
                                    var oldSolutionFileName = Path.GetFileName(new Uri(chapitre.Controle.Solution).LocalPath);
                                    await blobStorageService.DeleteFileAsync(controleContainer, oldSolutionFileName);
                                }
                            }
                        }
                    }

                    // Remove the orphaned modules from the database
                    if (orphanedModules.Any())
                    {
                        apiDbContext.modules.RemoveRange(orphanedModules);
                        await apiDbContext.SaveChangesAsync();
                    }

                    return true;
                }

                return false;
            }
            catch (System.Exception)
            {
                return false;
            }
        }



    }
}