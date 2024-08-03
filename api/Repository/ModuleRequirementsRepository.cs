using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.RequiredModules;
using api.extensions;
using api.generique;
using api.interfaces;
using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ModuleRequirementsRepository : IModuleRequirementsRepository
    {
        private readonly apiDbContext apiDbContext;
        public ModuleRequirementsRepository(apiDbContext apiDbContext)
        {
            this.apiDbContext = apiDbContext;
        }
        public async Task<Result<ModuleRequirement>> CreateRequiredModule(CreateRequiredModuleDto createRequiredModuleDto)
        {
            try
            {
                if (createRequiredModuleDto.TargetModuleId == createRequiredModuleDto.RequiredModuleId || await apiDbContext.moduleRequirements.AnyAsync(x => x.TargetModuleId == createRequiredModuleDto.TargetModuleId && x.RequiredModuleId == createRequiredModuleDto.RequiredModuleId))
                {
                    return Result<ModuleRequirement>.Failure("this module already in required modules list");
                }
                ModuleRequirement moduleRequirement = new ModuleRequirement()
                {
                    RequiredModuleId = createRequiredModuleDto.RequiredModuleId,
                    TargetModuleId = createRequiredModuleDto.TargetModuleId,
                    Seuill = createRequiredModuleDto.Seuill,
                };
                await apiDbContext.moduleRequirements.AddAsync(moduleRequirement);
                await apiDbContext.SaveChangesAsync();
                return Result<ModuleRequirement>.Success(moduleRequirement);
            }
            catch (Exception ex)
            {

                return Result<ModuleRequirement>.Failure(ex.Message);

            }
        }

        public async Task<Result<ModuleRequirement>> DeleteRequiredModule(int TargetModuleId, int RequiredModuleId)
        {
            try
            {
                if (await apiDbContext.moduleRequirements.AnyAsync(x => x.TargetModuleId == TargetModuleId && x.RequiredModuleId == RequiredModuleId))
                {
                    ModuleRequirement? moduleRequirement = await apiDbContext.moduleRequirements.FirstOrDefaultAsync(x => x.RequiredModuleId == RequiredModuleId && x.TargetModuleId == TargetModuleId);
                    apiDbContext.moduleRequirements.Remove(moduleRequirement);
                    await apiDbContext.SaveChangesAsync();
                    return Result<ModuleRequirement>.Success(moduleRequirement);
                }
                return Result<ModuleRequirement>.Failure("this relation don`t exist");
            }
            catch (Exception ex)
            {

                return Result<ModuleRequirement>.Failure(ex.Message);

            }

        }

        public async Task<Result<List<RequiredModulesDto>>> GetRequiredInModules(int moduleId)
        {
            try
            {
                if (await apiDbContext.moduleRequirements.AnyAsync(x => x.RequiredModuleId == moduleId))
                {
                    List<RequiredModulesDto> requiredModulesDtos = await apiDbContext.moduleRequirements.Include(x => x.TargetModule).ThenInclude(x => x.NiveauScolaire).ThenInclude(x => x.Institution).Where(x => x.RequiredModuleId == moduleId).Select(x => x.RequiredInModulesFromModelToDto()).ToListAsync();
                    return Result<List<RequiredModulesDto>>.Success(requiredModulesDtos);
                }
                return Result<List<RequiredModulesDto>>.Failure("Module NotFound");
            }
            catch (Exception ex)
            {
                return Result<List<RequiredModulesDto>>.Failure(ex.Message);

            }
        }

        public async Task<Result<List<RequiredModulesDto>>> GetRequiredModules(int moduleId)
        {
            try
            {
                if (await apiDbContext.moduleRequirements.AnyAsync(x => x.TargetModuleId == moduleId))
                {
                    List<RequiredModulesDto> requiredModulesDtos = await apiDbContext.moduleRequirements.Include(x => x.RequiredModule).ThenInclude(x => x.NiveauScolaire).ThenInclude(x => x.Institution).Where(x => x.TargetModuleId == moduleId).Select(x => x.RequiredModulesFromModelToDto()).ToListAsync();
                    return Result<List<RequiredModulesDto>>.Success(requiredModulesDtos);
                }
                return Result<List<RequiredModulesDto>>.Success(new List<RequiredModulesDto>());
            }
            catch (Exception ex)
            {
                return Result<List<RequiredModulesDto>>.Failure(ex.Message);

            }
        }

        public async Task<Result<IsEligibleDto>> IsStudentEligibleForModule(StudentEligibleDto studentEligibleDto)
        {
            try
            {
                IsEligibleDto isEligibleDto = new IsEligibleDto();

                Result<List<RequiredModulesDto>> result = await GetRequiredModules(studentEligibleDto.TargetModuleId);

                if (result.IsSuccess)
                {
                    List<RequiredModulesDto> requiredModulesDtos = result.Value;
                    List<TestNiveau> testNiveaus = await apiDbContext.testNiveaus
                                            .Where(tn => tn.StudentId == studentEligibleDto.StudentId)
                                            .ToListAsync();

                    foreach (RequiredModulesDto requiredModulesDto in requiredModulesDtos)
                    {
                        TestNiveau? testNiveau = testNiveaus.FirstOrDefault(tn => tn.ModuleId == requiredModulesDto.Id);
                        if (testNiveau == null || testNiveau.Note < requiredModulesDto.Seuill)
                        {

                            isEligibleDto.modules.Add(requiredModulesDto);
                        }
                    }

                    if (isEligibleDto.modules.Count() > 0)
                    {
                        isEligibleDto.IsEligible = false;
                        return Result<IsEligibleDto>.Success(isEligibleDto);

                    }

                    isEligibleDto.IsEligible = true;
                    return Result<IsEligibleDto>.Success(isEligibleDto);

                }
                isEligibleDto.IsEligible = false;
                return Result<IsEligibleDto>.Success(isEligibleDto);

            }
            catch (Exception ex)
            {

                return Result<IsEligibleDto>.Failure(ex.Message);
            }

        }

        public async Task<Result<ModuleRequirement>> UpdateRequiredModule(CreateRequiredModuleDto createRequiredModuleDto)
        {
            try
            {
                if (await apiDbContext.moduleRequirements.AnyAsync(x => x.TargetModuleId == createRequiredModuleDto.TargetModuleId && x.RequiredModuleId == createRequiredModuleDto.RequiredModuleId))
                {
                    ModuleRequirement? moduleRequirement = await apiDbContext.moduleRequirements.FirstOrDefaultAsync(x => x.RequiredModuleId == createRequiredModuleDto.RequiredModuleId && x.TargetModuleId == createRequiredModuleDto.TargetModuleId);
                    moduleRequirement.Seuill = createRequiredModuleDto.Seuill;
                    await apiDbContext.SaveChangesAsync();
                    return Result<ModuleRequirement>.Success(moduleRequirement);
                }
                return Result<ModuleRequirement>.Failure("this relation don`t exist");
            }
            catch (Exception ex)
            {

                return Result<ModuleRequirement>.Failure(ex.Message);

            }
        }
    }
}