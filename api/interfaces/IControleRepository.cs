using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Controle;
using api.generique;
using api.Model;

namespace api.interfaces
{
    public interface IControleRepository
    {
        Task<Result<Controle>> CreateControle(CreateControleDto createControleDto);
        Task<Result<Controle>> UpdateControleEnnonce(UpdateControleEnnonceDto updateControleEnnonceDto);
        Task<Result<Controle>> UpdateControleSolution(UpdateControleSolutionDto updateControleSolutionDto);
        Task<Result<Controle>> UpdateControleName(UpdateControleNameDto updateControleNameDto);
        Task<Result<List<Controle>>> GetControlesByModule(int Id);
        Task<Result<Controle>> GetControleById(int Id);
        Task<Result<Controle>> Approuver(int id);
        Task<Result<Controle>> Refuser(int id);
    }
}