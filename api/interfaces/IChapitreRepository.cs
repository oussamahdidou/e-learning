using System.Collections.Generic;
using System.Threading.Tasks;
using api.generique;
using api.Model;

namespace api.interfaces
{
    public interface IChapitreRepository
    {
        Task<Result<List<Chapitre>>> GetAllAsync();
        Task<Result<Chapitre>> GetByIdAsync(int id);
        Task<Result<Chapitre>> AddAsync(Chapitre chapitre);
        Task<Result> UpdateAsync(Chapitre chapitre);
        Task<Result> DeleteAsync(int id);
    }
}
