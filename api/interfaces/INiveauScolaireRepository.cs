using api.Dtos.NiveauScolaires;
using api.generique;
using System.Collections.Generic;
using System.Threading.Tasks;
<<<<<<< HEAD
=======
using api.Dtos.NiveauScolaire;
using api.generique;
using api.Model;
>>>>>>> manall

namespace api.interfaces
{
    public interface INiveauScolaireRepository
    {
<<<<<<< HEAD
        Task<Result<List<NiveauScolaireDto>>> GetAllAsync();
        Task<Result<NiveauScolaireDto>> GetByIdAsync(int id);
        Task<Result<NiveauScolaireDto>> AddAsync(NiveauScolaireDto niveauScolaireDto);
        Task<Result> UpdateAsync(NiveauScolaireDto niveauScolaireDto);
        Task<Result> DeleteAsync(int id);
=======
        Task<Result<NiveauScolaire>> GetNiveauScolaireById(int id);
        Task<Result<NiveauScolaire>> CreateNiveauScolaire(CreateNiveauScolaireDto createNiveauScolaireDto);
        Task<Result<NiveauScolaire>> UpdateNiveauScolaire(UpdateNiveauScolaireDto updateNiveauScolaireDto);
        Task <Result<NiveauScolaire>> DeleteNiveauScolaire(int id);
>>>>>>> manall
    }
}
