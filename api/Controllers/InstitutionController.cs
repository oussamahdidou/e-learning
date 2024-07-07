using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Dtos.Institution;
using api.Service;
using api.generique;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstitutionController : ControllerBase
    {
        private readonly InstitutionService _institutionService;

        public InstitutionController(InstitutionService institutionService)
        {
            _institutionService = institutionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstitutionDto>>> GetAll()
        {
            var result = await _institutionService.GetAllDtosAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InstitutionDto>> GetById(int id)
        {
            var result = await _institutionService.GetDtoByIdAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            if (result.Value == null)
            {
                return NotFound("Institution not found");
            }
            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<ActionResult<InstitutionDto>> Create(InstitutionDto institutionDto)
        {
            var result = await _institutionService.AddDtoAsync(institutionDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            if (result.Value == null)
            {
                return BadRequest("Error creating institution");
            }
            return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, InstitutionDto institutionDto)
        {
            if (id != institutionDto.Id)
            {
                return BadRequest("ID mismatch.");
            }

            var result = await _institutionService.UpdateDtoAsync(institutionDto);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _institutionService.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }

            return NoContent();
        }
    }
}
