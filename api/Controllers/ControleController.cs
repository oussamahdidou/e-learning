using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [HttpGet("GetControleById/{Id:int}")]
        public async Task<IActionResult> GetControleById([FromRoute] int Id)
        {
            Result<Controle> result = await controleRepository.GetControleById(Id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpGet("GetControleByModule/{Id:int}")]
        public async Task<IActionResult> GetControleByModule([FromRoute] int Id)
        {
            Result<List<Controle>> result = await controleRepository.GetControlesByModule(Id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPut("UpdateControleEnnonce")]
        public async Task<IActionResult> UpdateControleEnnonce([FromBody] UpdateControleEnnonceDto updateControleEnnonceDto)
        {
            Result<Controle> result = await controleRepository.UpdateControleEnnonce(updateControleEnnonceDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPut("UpdateControleSolution")]
        public async Task<IActionResult> UpdateControleSolution([FromBody] UpdateControleSolutionDto updateControleSolutionDto)
        {
            Result<Controle> result = await controleRepository.UpdateControleSolution(updateControleSolutionDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPut(" UpdateControleName")]
        public async Task<IActionResult> UpdateControleName([FromBody] UpdateControleNameDto updateControleNameDto)
        {
            Result<Controle> result = await controleRepository.UpdateControleName(updateControleNameDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }



    }
}