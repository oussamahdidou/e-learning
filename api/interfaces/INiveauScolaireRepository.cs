using api.Dtos.NiveauScolaire;
using api.generique;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Dtos.NiveauScolaire;
using api.generique;
using api.Model;

namespace api.interfaces
{
    public interface INiveauScolaireRepository
    {
        Task<Result<NiveauScolaire>> GetNiveauScolaireById(int id);
        Task<Result<NiveauScolaire>> CreateNiveauScolaire(CreateNiveauScolaireDto createNiveauScolaireDto);
        Task<Result<NiveauScolaire>> UpdateNiveauScolaire(UpdateNiveauScolaireDto updateNiveauScolaireDto);
        //Task <Result<NiveauScolaire>> DeleteNiveauScolaire(int id);
    }
}
