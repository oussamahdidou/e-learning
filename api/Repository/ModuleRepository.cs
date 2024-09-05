using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Chapitre;
using api.Dtos.Control;
using api.Dtos.Module;
using api.Dtos.NiveauScolaire;
using api.Dtos.Option;
using api.Dtos.Question;
using api.Dtos.Quiz;
using api.extensions;
using api.generique;
using api.helpers;
using api.interfaces;
using api.Mappers;
using api.Model;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace api.Repository
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly apiDbContext apiDbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IBlobStorageService blobStorageService;
        private string pdfContainer = "pdf-container";
        private string videoContainer = "video-container";
        private string schemaContainer = "schema-container";
        private string syntheseContainer = "synthese-container";
        private string controleContainer = "controle-container";
        private string imageContainer = "image-container";
        private string programContainer = "program-container";
        public ModuleRepository(IBlobStorageService blobStorageService, apiDbContext apiDbContext, IWebHostEnvironment webHostEnvironment)
        {
            this.apiDbContext = apiDbContext;
            this.webHostEnvironment = webHostEnvironment;
            this.blobStorageService = blobStorageService;
        }
        public async Task<Result<Module>> CreateModule(CreateModuleDto createModuleDto)
        {
            try
            {
                NiveauScolaireModule niveauScolaireModule = new NiveauScolaireModule()
                {
                    NiveauScolaireId = createModuleDto.NiveauScolaireId,
                    Module = new Module()
                    {
                        Nom = createModuleDto.Nom,
                    }

                };
                await apiDbContext.niveauScolaireModules.AddAsync(niveauScolaireModule);
                await apiDbContext.SaveChangesAsync();
                return Result<Module>.Success(niveauScolaireModule.Module);

            }
            catch (Exception ex)
            {

                return Result<Module>.Failure($"{ex.Message}");

            }
        }
        public async Task<Result<ModuleDto>> GetModuleById(int id, AppUser user)
        {
            try
            {
                Module? module = await apiDbContext.modules
                    .Include(x => x.Chapitres)
                        .ThenInclude(y => y.Quiz)
                        .ThenInclude(y => y.Questions)
                        .ThenInclude(y => y.Options)
                    .Include(x => x.Chapitres)
                        .ThenInclude(w => w.Cours)
                        .ThenInclude(e => e.Paragraphes)
                    .Include(z => z.Chapitres)
                        .ThenInclude(w => w.Controle)
                    .Include(z => z.Chapitres)
                        .ThenInclude(w => w.Syntheses)
                    .Include(z => z.Chapitres)
                        .ThenInclude(w => w.Schemas)
                    .Include(z => z.Chapitres)
                        .ThenInclude(w => w.Videos)
                    .Where(x => x.Id == id)
                    .Select(x => new Module
                    {
                        Id = x.Id,
                        Nom = x.Nom,
                        Chapitres = x.Chapitres
                            .Where(c => c.Statue == ObjectStatus.Approuver)
                            .ToList()
                    })
                    .FirstOrDefaultAsync();

                if (module == null)
                {
                    return Result<ModuleDto>.Failure("module notfound");
                }

                List<int> checkedChapters = await apiDbContext.checkChapters
                    .Where(cc => cc.StudentId == user.Id)
                    .Select(cc => cc.ChapitreId)
                    .ToListAsync();

                var moduleDto = module.toModuleDto(checkedChapters);

                return Result<ModuleDto>.Success(moduleDto);
            }
            catch (System.Exception ex)
            {

                return Result<ModuleDto>.Failure(ex.Message);
            }
        }
        public async Task<Result<Module>> GetModuleInformationByID(int moduleId)
        {
            Module? module = await apiDbContext.modules.Include(x => x.Chapitres).FirstOrDefaultAsync(x => x.Id == moduleId);
            if (module == null) return Result<Module>.Failure("module not found");
            return Result<Module>.Success(module);
        }
        public async Task<Result<Module>> UpdateModule(UpdateModuleDto updateModuleDto)
        {
            try
            {
                Module? module = await apiDbContext.modules.FirstOrDefaultAsync(x => x.Id == updateModuleDto.ModuleId);
                if (module == null)
                {
                    return Result<Module>.Failure("Module notfound");

                }
                module.Nom = updateModuleDto.Nom;
                await apiDbContext.SaveChangesAsync();
                return Result<Module>.Success(module);
            }
            catch (System.Exception ex)
            {

                return Result<Module>.Failure($"{ex.Message}");
            }
        }
        public async Task<bool> DeleteModule(int moduleId)
        {
            try
            {
                var module = await apiDbContext.modules
                    .Include(x => x.NiveauScolaireModules)
                    .Include(x => x.ExamFinal).ThenInclude(x => x.ResultExams)
                    .Include(m => m.Chapitres)
                        .ThenInclude(c => c.Quiz)
                            .ThenInclude(q => q.Questions)
                                .ThenInclude(q => q.Options)
                    .Include(m => m.Chapitres)
                        .ThenInclude(c => c.Quiz)
                        .ThenInclude(q => q.QuizResults)
                    .Include(m => m.TestNiveaus)
                    .Include(m => m.ModuleRequirements)
                    .Include(m => m.ModulesRequiredIn)
                    .Include(m => m.ExamFinal)
                    .Include(m => m.Chapitres)
                        .ThenInclude(x => x.Cours)
                        .ThenInclude(x => x.Paragraphes)
                             .Include(m => m.Chapitres)
                        .ThenInclude(x => x.Videos)
                        .Include(m => m.Chapitres)
                        .ThenInclude(x => x.Schemas)
                        .Include(m => m.Chapitres)
                        .ThenInclude(x => x.Syntheses)
                    .Include(m => m.Chapitres)
                        .ThenInclude(c => c.Controle).ThenInclude(x => x.ResultControles)
                    .Include(m => m.Chapitres)
                        .ThenInclude(c => c.CheckChapters)
                    .FirstOrDefaultAsync(m => m.Id == moduleId);

                if (module == null)
                {
                    return false;
                }

                // Delete Module Image if exists
                if (!string.IsNullOrEmpty(module.ModuleImg))
                {
                    await blobStorageService.DeleteImageVideoAsync(imageContainer, CloudinaryUrlHelper.ExtractFileName(module.ModuleImg));
                }

                // Delete Course Program if exists
                if (!string.IsNullOrEmpty(module.CourseProgram))
                {
                    await blobStorageService.DeleteFileAsync(programContainer, CloudinaryUrlHelper.ExtractFileName(module.CourseProgram));
                }

                // Delete Exam Final if exists
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

                // Iterate over Chapitres to delete related files
                foreach (var chapitre in module.Chapitres)
                {
                    // Delete Video if exists
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


                    // Delete Paragraphe Contenu if exists
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

                    // Delete Controle Ennonce and Solution if exists
                    if (chapitre.Controle != null)
                    {
                        if (!string.IsNullOrEmpty(chapitre.Controle.Ennonce))
                        {
                            await blobStorageService.DeleteFileAsync(controleContainer, CloudinaryUrlHelper.ExtractFileName(chapitre.Controle.Ennonce));
                        }

                        if (!string.IsNullOrEmpty(chapitre.Controle.Solution))
                        {
                            var oldSolutionFileName = Path.GetFileName(new Uri(chapitre.Controle.Solution).LocalPath);
                            await blobStorageService.DeleteFileAsync(controleContainer, CloudinaryUrlHelper.ExtractFileName(chapitre.Controle.Solution));
                        }
                    }
                }

                // Remove the Module from the database
                apiDbContext.modules.Remove(module);
                await apiDbContext.SaveChangesAsync();

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }


        public async Task<Result<Module>> UpdateModuleImage(UpdateModuleImageDto updateModuleImageDto)
        {
            try
            {
                Module? module = await apiDbContext.modules.FirstOrDefaultAsync(x => x.Id == updateModuleImageDto.ModuleId);
                if (module == null)
                {
                    return Result<Module>.Failure("module not found");
                }
                var imageContainer = "image-container";
                string imageUrl = await blobStorageService.UploadImageVideoAsync(updateModuleImageDto.ImageFile.OpenReadStream(), imageContainer, updateModuleImageDto.ImageFile.FileName);

                await blobStorageService.DeleteImageVideoAsync(imageContainer, CloudinaryUrlHelper.ExtractFileName(module.ModuleImg));


                module.ModuleImg = imageUrl;
                await apiDbContext.SaveChangesAsync();
                return Result<Module>.Success(module);
            }
            catch (System.Exception ex)
            {

                return Result<Module>.Failure(ex.Message);
            }
        }

        public async Task<Result<Module>> UpdateModuleProgram(UpdateModuleProgramDto updateModuleProgramDto)
        {
            try
            {
                Module? module = await apiDbContext.modules.FirstOrDefaultAsync(x => x.Id == updateModuleProgramDto.ModuleId);
                if (module == null)
                {
                    return Result<Module>.Failure("module not found");
                }
                var programContainer = "program-container";
                string programUrl = await blobStorageService.UploadFileAsync(updateModuleProgramDto.ProgramFile.OpenReadStream(), programContainer, updateModuleProgramDto.ProgramFile.FileName);
                if (!module.CourseProgram.IsNullOrEmpty())
                {

                    await blobStorageService.DeleteFileAsync(programContainer, CloudinaryUrlHelper.ExtractFileName(module.CourseProgram));

                }

                module.CourseProgram = programUrl;
                await apiDbContext.SaveChangesAsync();
                return Result<Module>.Success(module);
            }
            catch (System.Exception ex)
            {

                return Result<Module>.Failure(ex.Message);
            }
        }

        public async Task<Result<Module>> UpdateModuleDescription(UpdateModuleDescriptionDto updateModuleDescriptionDto)
        {
            try
            {
                Module? module = await apiDbContext.modules.FirstOrDefaultAsync(x => x.Id == updateModuleDescriptionDto.ModuleId);
                if (module == null)
                {
                    return Result<Module>.Failure("module not found");
                }

                module.Description = updateModuleDescriptionDto.Description;
                await apiDbContext.SaveChangesAsync();
                return Result<Module>.Success(module);
            }
            catch (System.Exception ex)
            {

                return Result<Module>.Failure(ex.Message);
            }
        }

        public async Task<Result<Module>> GetModuleInfo(int Id)
        {
            Module? module = await apiDbContext.modules.FirstOrDefaultAsync(x => x.Id == Id);
            if (module == null)
            {
                return Result<Module>.Failure("module notfound");
            }
            return Result<Module>.Success(module);
        }

        public async Task<Result<List<NiveauScolaire>>> GetModuleNiveauScolaires(int Id)
        {
            try
            {
                List<NiveauScolaire> niveauScolaires = await apiDbContext.niveauScolaireModules
                                                        .Include(x => x.NiveauScolaire)
                                                        .ThenInclude(x => x.Institution)
                                                        .Where(x => x.ModuleId == Id && x.NiveauScolaire != null)
                                                        .Select(x => x.NiveauScolaire!)
                                                        .ToListAsync();
                return Result<List<NiveauScolaire>>.Success(niveauScolaires);

            }
            catch (System.Exception ex)
            {

                return Result<List<NiveauScolaire>>.Failure(ex.Message);

            }
        }

        public async Task<Result<NiveauScolaire?>> CreateNiveauScolaireModule(CreateNiveauScolaireModuleDto createNiveauScolaireModuleDto)
        {
            try
            {
                if (await apiDbContext.niveauScolaireModules.AnyAsync(x => x.NiveauScolaireId == createNiveauScolaireModuleDto.NiveauScolaireId && x.ModuleId == createNiveauScolaireModuleDto.ModuleId))
                {
                    return Result<NiveauScolaire?>.Failure("le module est deja dans ce niveau");
                }
                else
                {
                    NiveauScolaireModule niveauScolaireModule = new NiveauScolaireModule()
                    {
                        NiveauScolaireId = createNiveauScolaireModuleDto.NiveauScolaireId,
                        ModuleId = createNiveauScolaireModuleDto.ModuleId

                    };
                    await apiDbContext.AddAsync(niveauScolaireModule);
                    await apiDbContext.SaveChangesAsync();
                    return Result<NiveauScolaire?>.Success(niveauScolaireModule.NiveauScolaire);
                }
            }
            catch (System.Exception ex)
            {

                return Result<NiveauScolaire?>.Failure(ex.Message);

            }
        }

        public async Task<bool> DeleteNiveauScolaireModule(int ModuleId, int NiveauScolaireId)
        {
            try
            {
                NiveauScolaireModule? niveauScolaireModule = await apiDbContext.niveauScolaireModules.FirstOrDefaultAsync(x => x.ModuleId == ModuleId && x.NiveauScolaireId == NiveauScolaireId);
                if (niveauScolaireModule == null)
                {
                    return false;
                }
                apiDbContext.niveauScolaireModules.Remove(niveauScolaireModule);
                await apiDbContext.SaveChangesAsync();
                return true;
            }
            catch (System.Exception)
            {

                return false;
            }

        }
    }
}
