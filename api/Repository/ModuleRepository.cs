using System.Collections.Generic;
using System.Linq;
<<<<<<< HEAD
using System.Threading.Tasks;
using api.Dto;
=======
using System.Linq.Expressions;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Module;
using api.generique;
using api.interfaces;
>>>>>>> manall
using api.Model;
using api.Mappers;
using api.interfaces;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.generique;

namespace api.Repository
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly apiDbContext apiDbContext;
        public ModuleRepository(apiDbContext apiDbContext)
        {
            this.apiDbContext = apiDbContext;
        }
<<<<<<< HEAD

        public async Task<Result<IEnumerable<ModuleDto>>> GetAllAsync()
        {
            var modules = await _context.modules.Include(m => m.Chapitres).ToListAsync();
            return Result<IEnumerable<ModuleDto>>.Success(modules.Select(ModuleMapper.MapToDto));
        }

        public async Task<Result<ModuleDto>> GetByIdAsync(int id)
        {
            var module = await _context.modules
                .Include(m => m.Chapitres)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (module == null)
            {
                return Result<ModuleDto>.Failure("Module not found");
            }
            
            return Result<ModuleDto>.Success(ModuleMapper.MapToDto(module));
        }

        public async Task<Result<ModuleDto>> AddAsync(ModuleDto moduleDto)
        {
            var module = ModuleMapper.MapToEntity(moduleDto);
            _context.modules.Add(module);
            await _context.SaveChangesAsync();
            return Result<ModuleDto>.Success(ModuleMapper.MapToDto(module));
        }

        public async Task<Result> UpdateAsync(ModuleDto moduleDto)
        {
            var existingModule = await _context.modules.FindAsync(moduleDto.Id);
            if (existingModule == null)
            {
                return Result.Failure("Module not found");
            }
            
            existingModule.Nom = moduleDto.Nom;
            existingModule.NiveauScolaireId = moduleDto.NiveauScolaireId;
            existingModule.Chapitres = moduleDto.ChapitreIds
                .Select(id => new Chapitre { Id = id })
                .ToList();

            _context.modules.Update(existingModule);
            await _context.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var module = await _context.modules.FindAsync(id);
            if (module == null)
            {
                return Result.Failure("Module not found");
            }

            _context.modules.Remove(module);
            await _context.SaveChangesAsync();

            return Result.Success();
=======
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
>>>>>>> manall
        }
        public async Task<Result<Module>> GetModuleById(int id)
        {
            try
            {
                Module? module = await apiDbContext.modules.Include(x => x.Chapitres).FirstOrDefaultAsync(x => x.Id == id);
                if (module == null)
                {
                    return Result<Module>.Failure("module notfound");

                }
                return Result<Module>.Success(module);
            }
            catch (System.Exception ex)
            {

                return Result<Module>.Failure(ex.Message);
            }
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
<<<<<<< HEAD
}
=======
    }}
>>>>>>> manall
