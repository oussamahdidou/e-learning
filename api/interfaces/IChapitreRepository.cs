using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
<<<<<<< HEAD
=======
using api.Dtos.Chapitre;
>>>>>>> manall
using api.generique;
using api.Model;

namespace api.interfaces
{
    public interface IChapitreRepository
    {
<<<<<<< HEAD
        Task<Result<List<Chapitre>>> GetAllAsync();
        Task<Result<Chapitre>> GetByIdAsync(int id);
        Task<Result<Chapitre>> AddAsync(Chapitre chapitre);
        Task<Result> UpdateAsync(Chapitre chapitre);
        Task<Result> DeleteAsync(int id);
=======

        Task<Result<Chapitre>> GetChapitreById(int id);
        Task<Result<Chapitre>> CreateChapitre(CreateChapitreDto createChapitreDto);
>>>>>>> manall
    }
}