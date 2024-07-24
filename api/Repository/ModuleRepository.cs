using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Module;
using api.generique;
using api.interfaces;
using api.Model;
using Microsoft.EntityFrameworkCore;

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
    }}