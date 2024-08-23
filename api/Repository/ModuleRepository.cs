using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Chapitre;
using api.Dtos.Control;
using api.Dtos.Module;
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
                        .ThenInclude(c => c.Controle).ThenInclude(x => x.ResultControles)
                    .Include(m => m.Chapitres)
                        .ThenInclude(c => c.CheckChapters)
                    .FirstOrDefaultAsync(m => m.Id == moduleId);

                if (module != null)
                {
                    apiDbContext.modules.Remove(module);
                    await apiDbContext.SaveChangesAsync();
                    return true;
                }
                return false;
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
                string imageUrl = await blobStorageService.UploadFileAsync(updateModuleImageDto.ImageFile.OpenReadStream(), imageContainer, updateModuleImageDto.ImageFile.FileName);

                await blobStorageService.DeleteFileAsync(imageContainer, new Uri(module.ModuleImg).Segments.Last());


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

                    await blobStorageService.DeleteFileAsync(programContainer, new Uri(module.CourseProgram).Segments.Last());

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
    }
}
