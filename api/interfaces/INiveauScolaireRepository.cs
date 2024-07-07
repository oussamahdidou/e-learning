using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Model;

namespace api.interfaces
{
    public interface INiveauScolaireRepository
    {
      Task<IEnumerable<NiveauScolaire>> GetAllAsync();
        Task<NiveauScolaire?> GetByIdAsync(int id);
        Task<NiveauScolaire> AddAsync(NiveauScolaire niveauScolaire);
        Task UpdateAsync(NiveauScolaire niveauScolaire);
        Task DeleteAsync(int id);  
    }
}