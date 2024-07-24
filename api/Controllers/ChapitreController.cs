<<<<<<< HEAD
using Microsoft.AspNetCore.Mvc;
using api.Dtos.Chapitre;
using api.generique;
using api.interfaces;
using api.Mappers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
=======
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Chapitre;
using api.generique;
using api.interfaces;
using api.Model;
using Microsoft.AspNetCore.Mvc;
>>>>>>> manall

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChapitreController : ControllerBase
    {
<<<<<<< HEAD
        private readonly IChapitreRepository _chapitreRepository;

        public ChapitreController(IChapitreRepository chapitreRepository)
        {
            _chapitreRepository = chapitreRepository;
        }

        [HttpGet("GetAll")]
        
        public async Task<IActionResult> GetAllChapitres()
        {
            var result = await _chapitreRepository.GetAllAsync();
            if (!result.IsSuccess)
=======
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
>>>>>>> manall
            {
                return Ok(result.Value);
            }
<<<<<<< HEAD

            var chapitreDtos = result.Value?.Select(ChapitreMapper.MapToDto) ?? Enumerable.Empty<ChapitreDto>();
            return Ok(chapitreDtos);
        }

        [HttpGet("GetById/{id}")]
        
        public async Task<IActionResult> GetChapitreById(int id)
        {
            var result = await _chapitreRepository.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }

            var chapitreDto = result.Value != null ? ChapitreMapper.MapToDto(result.Value) : null;
            return chapitreDto != null ? Ok(chapitreDto) : NotFound("Chapitre not found");
        }

        [HttpPost("Create")]
       
        public async Task<IActionResult> AddChapitre([FromBody] ChapitreDto chapitreDto)
        {
            var chapitre = ChapitreMapper.MapToEntity(chapitreDto);
            var result = await _chapitreRepository.AddAsync(chapitre);
            if (!result.IsSuccess)
=======
            return BadRequest(result.Error);
        }
        [HttpPost]
        public async Task<IActionResult> CreateChapitre([FromBody] CreateChapitreDto createChapitreDto)
        {
            Result<Chapitre> result = await chapitreRepository.CreateChapitre(createChapitreDto);
            if (result.IsSuccess)
>>>>>>> manall
            {
                return Ok(result.Value);
            }
<<<<<<< HEAD

            var addedChapitreDto = result.Value != null ? ChapitreMapper.MapToDto(result.Value) : null;
            return addedChapitreDto != null 
                ? CreatedAtAction(nameof(GetChapitreById), new { id = addedChapitreDto.Id }, addedChapitreDto)
                : BadRequest("Failed to create chapitre");
        }

        [HttpPut("Update/{id}")]
        
        public async Task<IActionResult> UpdateChapitre([FromRoute] int id, [FromBody] ChapitreDto chapitreDto)
        {
            if (id != chapitreDto.Id)
            {
                return BadRequest("Mismatched chapter ID.");
            }

            var getResult = await _chapitreRepository.GetByIdAsync(id);
            if (!getResult.IsSuccess)
            {
                return NotFound(getResult.Error);
            }

            var existingChapitre = getResult.Value;
            if (existingChapitre == null)
            {
                return NotFound("Chapitre not found");
            }

            existingChapitre.ChapitreNum = chapitreDto.ChapitreNum;
            existingChapitre.Nom = chapitreDto.Nom;
            existingChapitre.Statue = chapitreDto.Statue;
            existingChapitre.CoursPdfPath = chapitreDto.CoursPdfPath;
            existingChapitre.VideoPath = chapitreDto.VideoPath;
            existingChapitre.Synthese = chapitreDto.Synthese;
            existingChapitre.Schema = chapitreDto.Schema;
            existingChapitre.Premium = chapitreDto.Premium;
            existingChapitre.QuizId = chapitreDto.QuizId;
            existingChapitre.ModuleId = chapitreDto.ModuleId;
            existingChapitre.ControleId = chapitreDto.ControleId;

            var updateResult = await _chapitreRepository.UpdateAsync(existingChapitre);
            if (!updateResult.IsSuccess)
            {
                return BadRequest(updateResult.Error);
            }

            return Ok("Chapitre updated successfully");
        }

        [HttpDelete("Delete/{id}")]
     
        public async Task<IActionResult> DeleteChapitre(int id)
        {
            var result = await _chapitreRepository.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }

            var deleteResult = await _chapitreRepository.DeleteAsync(id);
            if (!deleteResult.IsSuccess)
            {
                return BadRequest(deleteResult.Error);
            }

            return Ok("Chapitre deleted successfully");
        }
=======
            return BadRequest(result.Error);
        }

>>>>>>> manall
    }
}