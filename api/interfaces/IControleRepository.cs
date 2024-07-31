using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Control;
using api.Dtos.Controle;
using api.generique;
using api.Model;

namespace api.interfaces
{
    public interface IControleRepository
    {
        Task<Result<Controle>> CreateControle(CreateControleDto createControleDto);

        Task<Result<ControleDto>> GetControleById(int controleId);
    }
}