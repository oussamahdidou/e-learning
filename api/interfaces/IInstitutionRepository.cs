using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Institution;
using api.generique;
using api.Model;

namespace api.interfaces
{
    public interface IInstitutionRepository
    {
        Task<Result<IEnumerable<InstitutionDto>>> GetAllAsync();
    
        Task<Result<InstitutionDto?>> GetByIdAsync(int id);
        Task<Result<Institution> >AddAsync(InstitutionDto institutionDto);
        Task <Result>UpdateAsync(InstitutionDto institutionDto);
        Task <Result>DeleteAsync(int id);
    }
}