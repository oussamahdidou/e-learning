using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<Result<ModuleDto>> GetModuleById(int id, string studentId)
{
    try
    {
        // Fetch the module along with related entities
        var module = await apiDbContext.modules
            .Include(x => x.Chapitres)
                .ThenInclude(y => y.Quiz)
                .ThenInclude(y => y.Questions)
                .ThenInclude(y => y.Options)
            .Include(x => x.Chapitres)
                .ThenInclude(y => y.Controle)  // Ensure this is correct type
            .FirstOrDefaultAsync(x => x.Id == id);

        if (module == null)
        {
            return Result<ModuleDto>.Failure("Module not found");
        }

        // Fetch checked chapters for the given student
        List<int> checkedChapters = await apiDbContext.checkChapters
            .Where(cc => cc.StudentId == studentId && 
            module.Chapitres.Any(ch => ch.Id == cc.ChapitreId))
            .Select(cc => cc.ChapitreId)
            .ToListAsync();

        // Filter chapters in memory
        var filteredChapitres = module.Chapitres
            .Where(c => c.Statue == ObjectStatus.Approuver)
            .ToList();

        // Create the DTO
        var moduleDto = new ModuleDto
        {
            Id = module.Id,
            Nom = module.Nom,
            Chapitres = filteredChapitres.Select(c => new ChapitreDto
            {
                Id = c.Id,
                ChapitreNum = c.ChapitreNum,
                Nom = c.Nom,
                Statue = checkedChapters.Contains(c.Id),
                CoursPdfPath = c.CoursPdfPath,
                VideoPath = c.VideoPath,
                Synthese = c.Synthese,
                Schema = c.Schema,
                Premium = c.Premium,
                Quiz = c.Quiz?.ToQuizDto() // Ensure Quiz is not null
            }).ToList(),
            Controles = filteredChapitres
                .Select(c => c.Controle)  // If Controle is not a collection
                .Where(ctrl => ctrl != null)
                .Select(ctrl => new ControleDto
                {
                    Id = ctrl.Id,
                    Nom = ctrl.Nom,
                    Ennonce = ctrl.Ennonce,
                    Solution = ctrl.Solution,
                    ChapitreNum = ctrl.Chapitres.Select(ch => ch.ChapitreNum).ToList()
                }).DistinctBy(ctrl => ctrl.Id) // Ensure DistinctBy works correctly
                .ToList()
        };

        return Result<ModuleDto>.Success(moduleDto);
    }
    catch (System.Exception ex)
    {
        return Result<ModuleDto>.Failure(ex.Message);
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
    }
}