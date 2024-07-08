using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.interfaces;
using api.Model;
using api.Dtos.Institution;
using api.generique;
using Microsoft.EntityFrameworkCore;
using api.Dtos.NiveauScolaires;

namespace api.Repository
{
    public class InstitutionRepository : IInstitutionRepository
    {
        private readonly apiDbContext _context;

        public InstitutionRepository(apiDbContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<InstitutionDto>>> GetAllAsync()
        {
            try
            {
                var institutions = await _context.institutions
                    .Include(i => i.NiveauScolaires)
                    .ToListAsync();

                var institutionDtos = institutions.Select(i => new InstitutionDto
                {
                    Id = i.Id,
                    Nom = i.Nom,
                    NiveauScolaires = i.NiveauScolaires.Select(n => new NiveauScolaireDto
                    {
                        Id = n.Id,
                        Nom = n.Nom
                        // Add other properties as needed
                    }).ToList()
                });

                return Result<IEnumerable<InstitutionDto>>.Success(institutionDtos);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<InstitutionDto>>.Failure(ex.Message);
            }
        }

        public async Task<Result<InstitutionDto?>> GetByIdAsync(int id)
        {
            try
            {
                var institution = await _context.institutions
                    .Include(i => i.NiveauScolaires)
                    .FirstOrDefaultAsync(i => i.Id == id);

                if (institution == null)
                {
                    return Result<InstitutionDto?>.Success(null);
                }

                var institutionDto = new InstitutionDto
                {
                    Id = institution.Id,
                    Nom = institution.Nom,
                    NiveauScolaires = institution.NiveauScolaires.Select(n => new NiveauScolaireDto
                    {
                        Id = n.Id,
                        Nom = n.Nom
                        // Add other properties as needed
                    }).ToList()
                };

                return Result<InstitutionDto?>.Success(institutionDto);
            }
            catch (Exception ex)
            {
                return Result<InstitutionDto?>.Failure(ex.Message);
            }
        }

        public async Task<Result<Institution>> AddAsync(InstitutionDto institutionDto)
        {
            try
            {
                var institution = new Institution
                {
                    Nom = institutionDto.Nom
                    // Map other properties as needed
                };

                _context.institutions.Add(institution);
                await _context.SaveChangesAsync();

                return Result<Institution>.Success(institution);
            }
            catch (Exception ex)
            {
                return Result<Institution>.Failure(ex.Message);
            }
        }

        public async Task<Result> UpdateAsync(InstitutionDto institutionDto)
        {
            try
            {
                var institution = await _context.institutions.FindAsync(institutionDto.Id);

                if (institution == null)
                {
                    return Result.Failure("Institution not found");
                }

                institution.Nom = institutionDto.Nom;
                // Update other properties as needed

                _context.Entry(institution).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public async Task<Result> DeleteAsync(int id)
        {
            try
            {
                var institution = await _context.institutions.FindAsync(id);

                if (institution == null)
                {
                    return Result.Failure("Institution not found");
                }

                _context.institutions.Remove(institution);
                await _context.SaveChangesAsync();

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}