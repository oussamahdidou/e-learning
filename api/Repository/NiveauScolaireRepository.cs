using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                await apiDbContext.niveauScolaires.AddAsync(niveauScolaire);
                await apiDbContext.SaveChangesAsync();
                return Result<NiveauScolaire>.Success(niveauScolaire);

            }
            catch (Exception ex)
            {

                return Result<NiveauScolaire>.Failure($"{ex.Message}");

            }
        }
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
    }
}