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
        public async Task<bool> DeleteNiveauScolaire(int niveauScolaireId)
        {
            try
            {
                var niveauScolaire = await apiDbContext.niveauScolaires
                .Include(ns => ns.Modules).ThenInclude(x => x.ExamFinal).ThenInclude(x => x.ResultExams)
                    .Include(ns => ns.Modules)
                        .ThenInclude(m => m.Chapitres)
                            .ThenInclude(c => c.Quiz)
                                .ThenInclude(q => q.Questions)
                                    .ThenInclude(q => q.Options)
                    .Include(ns => ns.Modules)
                        .ThenInclude(m => m.Chapitres)
                            .ThenInclude(c => c.Quiz)
                            .ThenInclude(q => q.QuizResults)
                    .Include(ns => ns.Modules)
                        .ThenInclude(m => m.TestNiveaus)
                    .Include(ns => ns.Modules)
                        .ThenInclude(m => m.ModuleRequirements)
                    .Include(ns => ns.Modules)
                        .ThenInclude(m => m.ModulesRequiredIn)
                    .Include(ns => ns.Modules)
                        .ThenInclude(m => m.ExamFinal)
                    .Include(ns => ns.Modules)
                        .ThenInclude(m => m.Chapitres)
                            .ThenInclude(c => c.Controle).ThenInclude(x => x.ResultControles)
                    .Include(ns => ns.Modules)
                        .ThenInclude(m => m.Chapitres)
                            .ThenInclude(c => c.CheckChapters)
                    .FirstOrDefaultAsync(ns => ns.Id == niveauScolaireId);

                if (niveauScolaire != null)
                {
                    apiDbContext.niveauScolaires.Remove(niveauScolaire);
                    await apiDbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (System.Exception)
            {
                return false;
            }
        }


    }
}