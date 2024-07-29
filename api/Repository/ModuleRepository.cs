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
using api.generique;
using api.helpers;
using api.interfaces;
using api.Mappers;
using api.Model;
using api.Mappers;
using api.interfaces;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.generique;
using Microsoft.AspNetCore.Http.HttpResults;

namespace api.Repository
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly apiDbContext apiDbContext;
        public ModuleRepository(apiDbContext apiDbContext)
        {
            this.apiDbContext = apiDbContext;
        }
        public async Task<Result<Module>> CreateModule(CreateModuleDto createModuleDto)
        {
            try
            {
                Module module = new Module()
                {
                    Nom = createModuleDto.Nom,
                    NiveauScolaireId = createModuleDto.NiveauScolaireId,
                };
                await apiDbContext.modules.AddAsync(module);
                await apiDbContext.SaveChangesAsync();
                return Result<Module>.Success(module);

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
                        .ThenInclude(y => y.Controle)
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
        public async Task<Result<Module>> GetModuleInformationByID(int moduleId){
            Module? module = await apiDbContext.modules.FindAsync(moduleId);
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
        public async Task<Result<Module>>DeleteModule(int id){
            try{
            Module? module = await apiDbContext.modules.FirstOrDefaultAsync(x=>x.Id==id);
            if(module==null){
                return Result<Module>.Failure("module n'existe pas");

            }
             apiDbContext.modules.Remove(module);
            await apiDbContext.SaveChangesAsync();
            return Result<Module>.Success(module);

        }
        catch (System.Exception ex){

            return Result<Module>.Failure($"{ex.Message}");
        }
    }
    }}
