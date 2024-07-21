using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.NiveauScolaire;
using api.generique;
using api.interfaces;
using api.Model;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NiveauScolaireController : ControllerBase
    {
        private readonly INiveauScolaireRepository niveauScolaireRepository;
        public NiveauScolaireController(INiveauScolaireRepository niveauScolaireRepository)
        {
            this.niveauScolaireRepository = niveauScolaireRepository;
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetNiveauScolaireById([FromRoute] int id)
        {
            Result<NiveauScolaire> result = await niveauScolaireRepository.GetNiveauScolaireById(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNiveauScolaire([FromBody] CreateNiveauScolaireDto createNiveauScolaireDto)
        {
            Result<NiveauScolaire> result = await niveauScolaireRepository.CreateNiveauScolaire(createNiveauScolaireDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateNiveauScolaire([FromBody] UpdateNiveauScolaireDto updateNiveauScolaireDto)
        {
            Result<NiveauScolaire> result = await niveauScolaireRepository.UpdateNiveauScolaire(updateNiveauScolaireDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
    }
}