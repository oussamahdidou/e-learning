using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Module;
using api.generique;
using api.Model;

namespace api.interfaces
{
    public interface IModuleRepository
    {
        Task<Result<ModuleDto>> GetModuleById(int id, AppUser user);
        Task<Result<Module>> CreateModule(CreateModuleDto createModuleDto);
        Task<Result<Module>> UpdateModule(UpdateModuleDto updateModuleDto);
        Task<bool> DeleteModule(int moduleId);
        Task<Result<Module>> GetModuleInformationByID(int moduleId);
        Task<Result<Module>> UpdateModuleImage(UpdateModuleImageDto updateModuleImageDto);
        Task<Result<Module>> UpdateModuleProgram(UpdateModuleProgramDto updateModuleProgramDto);
        Task<Result<Module>> UpdateModuleDescription(UpdateModuleDescriptionDto updateModuleDescriptionDto);
        Task<Result<Module>> GetModuleInfo(int Id);
        Task<Result<List<NiveauScolaire>>> GetModuleNiveauScolaires(int Id);
        Task<Result<NiveauScolaire>> CreateNiveauScolaireModule(CreateNiveauScolaireModuleDto createNiveauScolaireModuleDto);



    }
}
