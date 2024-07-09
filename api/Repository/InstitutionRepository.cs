using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Institution;
using api.generique;
using api.interfaces;
using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class InstitutionRepository : IinstitutionRepository
    {
        private readonly apiDbContext apiDbContext;
        public InstitutionRepository(apiDbContext apiDbContext)
        {
            this.apiDbContext = apiDbContext;
        }
        public async Task<Result<Institution>> CreateInstitution(string InstitutionName)
        {
            try
            {
                Institution institution = new Institution()
                {
                    Nom = InstitutionName,
                };
                await apiDbContext.institutions.AddAsync(institution);
                await apiDbContext.SaveChangesAsync();
                return Result<Institution>.Success(institution);

            }
            catch (Exception ex)
            {

                return Result<Institution>.Failure($"{ex.Message}");

            }
        }

        public async Task<Result<Institution>> GetInstitutionById(int id)
        {
            try
            {
                Institution? institution = await apiDbContext.institutions.Include(x => x.NiveauScolaires).FirstOrDefaultAsync(x => x.Id == id);
                if (institution == null)
                {
                    return Result<Institution>.Failure("institution notfound");

                }
                return Result<Institution>.Success(institution);
            }
            catch (Exception ex)
            {

                return Result<Institution>.Failure($"{ex.Message}");
            }
        }

        public async Task<Result<List<Institution>>> GetInstitutions()
        {
            try
            {
                List<Institution> institutions = await apiDbContext.institutions.ToListAsync();
                return Result<List<Institution>>.Success(institutions);
            }
            catch (Exception ex)
            {

                return Result<List<Institution>>.Failure($"{ex.Message}");
            }
        }

        public async Task<Result<Institution>> UpdateInstitution(UpdateInstitutionDto updateInstitutionDto)
        {
            try
            {
                Institution? institution = await apiDbContext.institutions.FirstOrDefaultAsync(x => x.Id == updateInstitutionDto.Id);
                if (institution == null)
                {
                    return Result<Institution>.Failure("institution notfound");

                }
                institution.Nom = updateInstitutionDto.Nom;
                await apiDbContext.SaveChangesAsync();
                return Result<Institution>.Success(institution);
            }
            catch (System.Exception ex)
            {

                return Result<Institution>.Failure($"{ex.Message}");
            }
        }
    }
}