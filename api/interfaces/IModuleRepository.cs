using api.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.interfaces
{
    public interface IModuleRepository
    {
        Task<IEnumerable<Module>> GetAllAsync();
        Task<Module> GetByIdAsync(int id);
        Task<Module> AddAsync(Module module);
        Task UpdateAsync(Module module);
        Task DeleteAsync(int id);
        
    }
}