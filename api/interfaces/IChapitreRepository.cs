using System.Collections.Generic;
using System.Threading.Tasks;
using api.Model;

namespace api.interfaces
{
    public interface IChapitreRepository
    {
        Task<IEnumerable<Chapitre>> GetAllAsync();
        Task<Chapitre?> GetByIdAsync(int id);
        Task<Chapitre> AddAsync(Chapitre chapitre);
        Task UpdateAsync(Chapitre chapitre);
        Task DeleteAsync(int id);
    }
}
