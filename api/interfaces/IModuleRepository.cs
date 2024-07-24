<<<<<<< HEAD
using api.Dto;
=======
using System;
>>>>>>> manall
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
<<<<<<< HEAD
using api.generique;
=======
using api.Dtos.Module;
using api.generique;
using api.Model;
>>>>>>> manall

namespace api.interfaces
{
    public interface IModuleRepository
    {
<<<<<<< HEAD
        Task<Result<IEnumerable<ModuleDto>>> GetAllAsync();
        Task<Result<ModuleDto>> GetByIdAsync(int id);
        Task<Result<ModuleDto>> AddAsync(ModuleDto moduleDto);
        Task<Result> UpdateAsync(ModuleDto moduleDto);
        Task<Result> DeleteAsync(int id);
=======
        Task<Result<Module>> GetModuleById(int id);
        Task<Result<Module>> CreateModule(CreateModuleDto createModuleDto);
        Task<Result<Module>> UpdateModule(UpdateModuleDto updateModuleDto);
        Task <Result<Module>> DeleteModule(int id);

>>>>>>> manall
    }
}
