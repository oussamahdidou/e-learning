using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Model;

namespace api.interfaces
{
    public interface IInstitutionRepository
    {
        Task<IEnumerable<Institution>> GetAllAsync();
        Task<Institution?> GetByIdAsync(int id);
        Task<Institution> AddAsync(Institution institution);
        Task UpdateAsync(Institution institution);
        Task DeleteAsync(int id);
    }
}