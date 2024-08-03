using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.RequiredModules;
using api.generique;
using api.Model;

namespace api.interfaces
{
    public interface IModuleRequirementsRepository
    {
        Task<Result<List<RequiredModulesDto>>> GetRequiredModules(int moduleId);
        Task<Result<List<RequiredModulesDto>>> GetRequiredInModules(int moduleId);
        Task<Result<ModuleRequirement>> CreateRequiredModule(CreateRequiredModuleDto createRequiredModuleDto);
        Task<Result<ModuleRequirement>> UpdateRequiredModule(CreateRequiredModuleDto createRequiredModuleDto);
        Task<Result<ModuleRequirement>> DeleteRequiredModule(int TargetModuleId, int RequiredModuleId);
        Task<Result<IsEligibleDto>> IsStudentEligibleForModule(StudentEligibleDto studentEligibleDto);
    }
}