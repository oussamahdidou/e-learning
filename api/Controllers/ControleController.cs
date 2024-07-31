using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Control;
using api.Dtos.Controle;
using api.generique;
using api.interfaces;
using api.Model;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ControleController : ControllerBase
    {
        private readonly IControleRepository controleRepository;
        public ControleController(IControleRepository controleRepository)
        {
            this.controleRepository = controleRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateControle([FromForm] CreateControleDto createControleDto)
        {
            Result<Controle> result = await controleRepository.CreateControle(createControleDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }

        [HttpGet("{controleId:int}")]
        public async Task<IActionResult> GetControleById([FromRoute]int controleId)
        {
            Result<ControleDto> result = await controleRepository.GetControleById(controleId);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
    }
}