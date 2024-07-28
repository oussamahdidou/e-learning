using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Chapitre;
using api.generique;
using api.interfaces;
using api.Model;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChapitreController : ControllerBase
    {
        private readonly IChapitreRepository chapitreRepository;
        public ChapitreController(IChapitreRepository chapitreRepository)
        {
            this.chapitreRepository = chapitreRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetChapitreById(int id)
        {
            Result<Chapitre> result = await chapitreRepository.GetChapitreById(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPost]
        public async Task<IActionResult> CreateChapitre([FromForm] CreateChapitreDto createChapitreDto)
        {
            Result<Chapitre> result = await chapitreRepository.CreateChapitre(createChapitreDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPut("UpdateChapitrePdf")]
        public async Task<IActionResult> UpdateChapitrePdf([FromForm] UpdateChapitrePdfDto updateChapitrePdfDto)
        {

            Result<Chapitre> result = await chapitreRepository.UpdateChapitrePdf(updateChapitrePdfDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);

        }
        [HttpPut("UpdateChapitreVideo")]
        public async Task<IActionResult> UpdateChapitreVideo([FromForm] UpdateChapitreVideoDto updateChapitreVideoDto)
        {

            Result<Chapitre> result = await chapitreRepository.UpdateChapitreVideo(updateChapitreVideoDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPut("UpdateChapitreSchema")]
        public async Task<IActionResult> UpdateChapitreSchema([FromForm] UpdateChapitreSchemaDto updateChapitreSchemaDto)
        {

            Result<Chapitre> result = await chapitreRepository.UpdateChapitreSchema(updateChapitreSchemaDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPut("UpdateChapitreSynthese")]
        public async Task<IActionResult> UpdateChapitreSynthese([FromForm] UpdateChapitreSyntheseDto updateChapitreSyntheseDto)
        {
            Result<Chapitre> result = await chapitreRepository.UpdateChapitreSynthese(updateChapitreSyntheseDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }

    }
}