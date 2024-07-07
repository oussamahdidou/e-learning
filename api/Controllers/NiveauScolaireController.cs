using api.Dtos.NiveauScolaires;
using api.Service;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NiveauScolaireController : ControllerBase
    {
        private readonly NiveauScolaireService _niveauScolaireService;

        public NiveauScolaireController(NiveauScolaireService niveauScolaireService)
        {
            _niveauScolaireService = niveauScolaireService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NiveauScolaireDto>>> GetAll()
        {
            var result = await _niveauScolaireService.GetAllDtosAsync();
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NiveauScolaireDto>> GetById(int id)
        {
            var result = await _niveauScolaireService.GetDtoByIdAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<ActionResult<NiveauScolaireDto>> Add(NiveauScolaireDto niveauScolaireDto)
        {
            var result = await _niveauScolaireService.AddDtoAsync(niveauScolaireDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, NiveauScolaireDto niveauScolaireDto)
        {
            if (id != niveauScolaireDto.Id)
            {
                return BadRequest("ID mismatch.");
            }

            var result = await _niveauScolaireService.UpdateDtoAsync(niveauScolaireDto);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _niveauScolaireService.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return NoContent();
        }
    }
}
