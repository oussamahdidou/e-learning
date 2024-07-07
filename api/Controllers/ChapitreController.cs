using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using api.Service;
using api.Dtos.Chapitre;
using api.generique;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChapitreController : ControllerBase
    {
        private readonly ChapitreService _chapitreService;

        public ChapitreController(ChapitreService chapitreService)
        {
            _chapitreService = chapitreService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChapitreDto>>> GetAllChapitres()
        {
            var result = await _chapitreService.GetAllDtosAsync();
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }

            if (result.Value == null)
            {
                return NotFound("No chapters found.");
            }

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChapitreDto>> GetChapitreById(int id)
        {
            var result = await _chapitreService.GetDtoByIdAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }

            if (result.Value == null)
            {
                return NotFound("Chapter not found.");
            }

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<ActionResult<ChapitreDto>> AddChapitre(ChapitreDto chapitreDto)
        {
            var result = await _chapitreService.AddDtoAsync(chapitreDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }

            if (result.Value == null)
            {
                return BadRequest("Failed to add chapter.");
            }

            return CreatedAtAction(nameof(GetChapitreById), new { id = result.Value.Id }, result.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChapitre(int id, ChapitreDto chapitreDto)
        {
            if (id != chapitreDto.Id)
            {
                return BadRequest();
            }

            var result = await _chapitreService.UpdateDtoAsync(chapitreDto);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChapitre(int id)
        {
            var result = await _chapitreService.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return NoContent();
        }
    }
}
