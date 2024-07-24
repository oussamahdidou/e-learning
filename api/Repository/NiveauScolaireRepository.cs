using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
<<<<<<< HEAD
using api.Dtos.NiveauScolaires;
using api.Model;
using api.Mappers;
using api.interfaces;
using api.generique;
using Microsoft.EntityFrameworkCore;
=======
>>>>>>> manall
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
<<<<<<< HEAD

        public async Task<Result<List<NiveauScolaireDto>>> GetAllAsync()
        {
            var niveauScolaires = await _context.niveauScolaires.Include(ns => ns.Modules).ToListAsync();
            var dtos = niveauScolaires.Select(NiveauScolaireMapper.MapToDto).ToList();
            return Result<List<NiveauScolaireDto>>.Success(dtos);
        }

        public async Task<Result<NiveauScolaireDto>> GetByIdAsync(int id)
        {
            var niveauScolaire = await _context.niveauScolaires
                .Include(ns => ns.Modules)
                .FirstOrDefaultAsync(ns => ns.Id == id);
            if (niveauScolaire == null)
            {
                return Result<NiveauScolaireDto>.Failure("NiveauScolaire not found.");
            }
            var dto = NiveauScolaireMapper.MapToDto(niveauScolaire);
            return Result<NiveauScolaireDto>.Success(dto);
        }

        public async Task<Result<NiveauScolaireDto>> AddAsync(NiveauScolaireDto niveauScolaireDto)
        {
            var niveauScolaire = NiveauScolaireMapper.MapToEntity(niveauScolaireDto);
            _context.niveauScolaires.Add(niveauScolaire);
            await _context.SaveChangesAsync();
            var dto = NiveauScolaireMapper.MapToDto(niveauScolaire);
            return Result<NiveauScolaireDto>.Success(dto);
        }

        public async Task<Result> UpdateAsync(NiveauScolaireDto niveauScolaireDto)
        {
            var existingNiveauScolaire = await _context.niveauScolaires.FindAsync(niveauScolaireDto.Id);
            if (existingNiveauScolaire == null)
            {
                return Result.Failure($"NiveauScolaire with ID {niveauScolaireDto.Id} not found.");
=======
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

>>>>>>> manall
            }

            existingNiveauScolaire.Nom = niveauScolaireDto.Nom;
            existingNiveauScolaire.InstitutionId = niveauScolaireDto.InstitutionId;
            existingNiveauScolaire.Modules = niveauScolaireDto.Modules
                .Select(m => new Module
                {
                    Id = m.Id,
                    Nom = m.Nom,
                })
                .ToList();

            _context.niveauScolaires.Update(existingNiveauScolaire);
            await _context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var existingNiveauScolaire = await _context.niveauScolaires.FindAsync(id);
            if (existingNiveauScolaire == null)
            {
                return Result.Failure($"NiveauScolaire with ID {id} not found.");
            }

            _context.niveauScolaires.Remove(existingNiveauScolaire);
            await _context.SaveChangesAsync();
            return Result.Success();
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