using api.Data;
using api.Dtos.NiveauScolaire;
using api.generique;
using api.interfaces;
using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class NiveauScolaireRepository : INiveauScolaireRepository
    {
        private readonly apiDbContext apiDbContext;
        public NiveauScolaireRepository(apiDbContext apiDbContext)
        {
            this.apiDbContext = apiDbContext;
        }
        public async Task<Result<NiveauScolaire>> CreateNiveauScolaire(CreateNiveauScolaireDto createNiveauScolaireDto)
{
    try
    {
        NiveauScolaire niveauScolaire = new NiveauScolaire()
        {
            Nom = createNiveauScolaireDto.Nom,
            InstitutionId = createNiveauScolaireDto.InstitutionId,
        };

        if (createNiveauScolaireDto.Modules != null && createNiveauScolaireDto.Modules.Any())
        {
            niveauScolaire.Modules = createNiveauScolaireDto.Modules
                .Select(m => new Module
                {
                   
                    Id = m.Id,
                    Nom = m.Nom,
                })
                .ToList();
        }

        await apiDbContext.niveauScolaires.AddAsync(niveauScolaire);
        await apiDbContext.SaveChangesAsync();
        return Result<NiveauScolaire>.Success(niveauScolaire);
    }
    catch (Exception ex)
    {
        return Result<NiveauScolaire>.Failure($"{ex.Message}");
    }
}
       /* public async Task<Result<NiveauScolaire>> DeleteAsync(int id)
        {
            var existingNiveauScolaire = await _context.niveauScolaires.FindAsync(id);
            if (existingNiveauScolaire == null)
            {
                return Result.Failure($"NiveauScolaire with ID {id} not found.");
            }

            _context.niveauScolaires.Remove(existingNiveauScolaire);
            await _context.SaveChangesAsync();
            return Result.Success();
        }*/
        public async Task<Result<NiveauScolaire>> GetNiveauScolaireById(int id)
        {
            try
            {
                NiveauScolaire? niveauScolaire = await apiDbContext.niveauScolaires.Include(x => x.Modules).FirstOrDefaultAsync(x => x.Id == id);
                if (niveauScolaire == null)
                {
                    return Result<NiveauScolaire>.Failure("niveauScolaire notfound");

                }
                return Result<NiveauScolaire>.Success(niveauScolaire);
            }
            catch (System.Exception ex)
            {

                return Result<NiveauScolaire>.Failure(ex.Message);
            }
        }
        public async Task<Result<NiveauScolaire>> UpdateNiveauScolaire(UpdateNiveauScolaireDto updateNiveauScolaireDto)
        {
            try
            {
                NiveauScolaire? NiveauScolaire = await apiDbContext.niveauScolaires.FirstOrDefaultAsync(x => x.Id == updateNiveauScolaireDto.NiveauScolaireId);
                if (NiveauScolaire == null)
                {
                    return Result<NiveauScolaire>.Failure("NiveauScolaire notfound");

                }
                NiveauScolaire.Nom = updateNiveauScolaireDto.Nom;
                await apiDbContext.SaveChangesAsync();
                return Result<NiveauScolaire>.Success(NiveauScolaire);
            }
            catch (System.Exception ex)
            {

                return Result<NiveauScolaire>.Failure($"{ex.Message}");
            }
        }
        public async Task<Result<NiveauScolaire>> DeleteNiveauScolaire(int id){
            try{
                NiveauScolaire? niveauScolaire = await apiDbContext.niveauScolaires.FindAsync(id);
                if(niveauScolaire == null){
                    return Result<NiveauScolaire>.Failure("niveau scolaire not found")
;                }
                apiDbContext.niveauScolaires.Remove(niveauScolaire);
                 await apiDbContext.SaveChangesAsync();
                 return Result<NiveauScolaire>.Success(niveauScolaire);

            }
            catch(System.Exception ex){
                return Result<NiveauScolaire>.Failure($"{ex.Message}");
            }

        }
    }
}