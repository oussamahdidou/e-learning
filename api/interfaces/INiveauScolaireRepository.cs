using api.Dtos.NiveauScolaires;
using api.generique;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.interfaces
{
    public interface INiveauScolaireRepository
    {
        Task<Result<List<NiveauScolaireDto>>> GetAllAsync();
        Task<Result<NiveauScolaireDto>> GetByIdAsync(int id);
        Task<Result<NiveauScolaireDto>> AddAsync(NiveauScolaireDto niveauScolaireDto);
        Task<Result> UpdateAsync(NiveauScolaireDto niveauScolaireDto);
        Task<Result> DeleteAsync(int id);
    }
}
