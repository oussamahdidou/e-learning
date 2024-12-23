using api.Data;
using api.Dtos.NiveauScolaire;
using api.generique;
using api.helpers;
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
                            await blobStorageService.DeleteFileAsync(imageContainer, CloudinaryUrlHelper.ExtractFileName(elementPedagogique.Lien));
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
                        .ThenInclude(m => m.Videos)
                        .Include(m => m.Chapitres)
                        .ThenInclude(m => m.Schemas)
                        .Include(m => m.Chapitres)
                        .ThenInclude(m => m.Syntheses)
                        .Include(m => m.Chapitres)
                            .ThenInclude(c => c.CheckChapters)
                        .Where(m => !apiDbContext.niveauScolaireModules.Any(nsm => nsm.ModuleId == m.Id))
                        .ToListAsync();

                    // Delete associated files for each orphaned module
                    foreach (var module in orphanedModules)
                    {
                        if (!string.IsNullOrEmpty(module.ModuleImg))
                        {
                            await blobStorageService.DeleteImageVideoAsync(imageContainer, CloudinaryUrlHelper.ExtractFileName(module.ModuleImg));
                        }
                        if (!string.IsNullOrEmpty(module.CourseProgram))
                        {
                            await blobStorageService.DeleteFileAsync(programContainer, CloudinaryUrlHelper.ExtractFileName(module.CourseProgram));
                        }
                        // Delete ExamFinal files
                        if (module.ExamFinal != null)
                        {
                            if (!string.IsNullOrEmpty(module.ExamFinal.Ennonce))
                            {
                                await blobStorageService.DeleteFileAsync(controleContainer, CloudinaryUrlHelper.ExtractFileName(module.ExamFinal.Ennonce));
                            }
                            if (!string.IsNullOrEmpty(module.ExamFinal.Solution))
                            {
                                await blobStorageService.DeleteFileAsync(controleContainer, CloudinaryUrlHelper.ExtractFileName(module.ExamFinal.Solution));
                            }
                        }

                        // Delete Chapitre files
                        foreach (var chapitre in module.Chapitres)
                        {
                            foreach (var paragraphe in chapitre.Videos)
                            {
                                if (!string.IsNullOrEmpty(paragraphe.Link))
                                {
                                    await blobStorageService.DeleteFileAsync(videoContainer, CloudinaryUrlHelper.ExtractFileName(paragraphe.Link));
                                }
                            }
                            foreach (var paragraphe in chapitre.Syntheses)
                            {
                                if (!string.IsNullOrEmpty(paragraphe.Link))
                                {
                                    await blobStorageService.DeleteFileAsync(syntheseContainer, CloudinaryUrlHelper.ExtractFileName(paragraphe.Link));
                                }
                            }
                            foreach (var paragraphe in chapitre.Schemas)
                            {
                                if (!string.IsNullOrEmpty(paragraphe.Link))
                                {
                                    await blobStorageService.DeleteFileAsync(schemaContainer, CloudinaryUrlHelper.ExtractFileName(paragraphe.Link));
                                }
                            }

                            // Delete Cours Paragraphes files
                            foreach (var cours in chapitre.Cours)
                            {
                                foreach (var paragraphe in cours.Paragraphes)
                                {
                                    if (!string.IsNullOrEmpty(paragraphe.Contenu))
                                    {
                                        await blobStorageService.DeleteFileAsync(pdfContainer, CloudinaryUrlHelper.ExtractFileName(paragraphe.Contenu));
                                    }
                                }
                            }

                            // Delete Controle files
                            if (chapitre.Controle != null)
                            {
                                if (!string.IsNullOrEmpty(chapitre.Controle.Ennonce))
                                {
                                    await blobStorageService.DeleteFileAsync(controleContainer, CloudinaryUrlHelper.ExtractFileName(chapitre.Controle.Ennonce));
                                }
                                if (!string.IsNullOrEmpty(chapitre.Controle.Solution))
                                {
                                    await blobStorageService.DeleteFileAsync(controleContainer, CloudinaryUrlHelper.ExtractFileName(chapitre.Controle.Solution));
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