using api.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.generique;

namespace api.interfaces
{
    public interface IModuleRepository
    {
        Task<Result<IEnumerable<ModuleDto>>> GetAllAsync();
        Task<Result<ModuleDto>> GetByIdAsync(int id);
        Task<Result<ModuleDto>> AddAsync(ModuleDto moduleDto);
        Task<Result> UpdateAsync(ModuleDto moduleDto);
        Task<Result> DeleteAsync(int id);
    }
}
