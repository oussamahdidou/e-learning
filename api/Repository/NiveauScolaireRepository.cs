using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.NiveauScolaires;
using api.Model;
using api.Mappers;
using api.interfaces;
using api.generique;
using Microsoft.EntityFrameworkCore;
using api.Data;

namespace api.Repository
{
    public class NiveauScolaireRepository : INiveauScolaireRepository
    {
        private readonly apiDbContext _context;

        public NiveauScolaireRepository(apiDbContext context)
        {
            _context = context;
        }

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
    }
}
