using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Institution;
using api.generique;
using api.Model;

namespace api.interfaces
{
    public interface IinstitutionRepository
    {
<<<<<<< HEAD
        Task<Result<IEnumerable<InstitutionDto>>> GetAllAsync();
    
        Task<Result<InstitutionDto?>> GetByIdAsync(int id);
        Task<Result<Institution> >AddAsync(InstitutionDto institutionDto);
        Task <Result>UpdateAsync(InstitutionDto institutionDto);
        Task <Result>DeleteAsync(int id);
=======
        Task<Result<List<Institution>>> GetInstitutions();
        Task<Result<Institution>> GetInstitutionById(int id);
        Task<Result<Institution>> CreateInstitution(string InstitutionName);
        Task<Result<Institution>> UpdateInstitution(UpdateInstitutionDto updateInstitutionDto);
        Task<Result<Institution>> DeleteInstitution(int id);

>>>>>>> manall
    }
}