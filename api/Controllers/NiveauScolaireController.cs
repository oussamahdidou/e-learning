using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Dtos.NiveauScolaires;
using api.generique;
using api.interfaces;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NiveauScolaireController : ControllerBase
    {
        private readonly INiveauScolaireRepository _niveauScolaireRepository;

        public NiveauScolaireController(INiveauScolaireRepository niveauScolaireRepository)
        {
            _niveauScolaireRepository = niveauScolaireRepository;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _niveauScolaireRepository.GetAllAsync();
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _niveauScolaireRepository.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            if (result.Value == null)
            {
                return NotFound("NiveauScolaire not found");
            }
            return Ok(result.Value);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Add([FromBody] NiveauScolaireDto niveauScolaireDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _niveauScolaireRepository.AddAsync(niveauScolaireDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] NiveauScolaireDto niveauScolaireDto)
        {
            if (id != niveauScolaireDto.Id)
            {
                return BadRequest("ID incorrect !");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _niveauScolaireRepository.UpdateAsync(niveauScolaireDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
           

            return Ok("NiveauScolaire updated successfully");
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _niveauScolaireRepository.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok("NiveauScolaire deleted successfully");
        }
    }
}
