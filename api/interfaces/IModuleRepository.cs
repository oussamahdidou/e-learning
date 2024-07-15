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
        Task<Result<Module>> GetModuleById(int id);
        Task<Result<Module>> CreateModule(CreateModuleDto createModuleDto);
        Task<Result<Module>> UpdateModule(UpdateModuleDto updateModuleDto);

    }
}