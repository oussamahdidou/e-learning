using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using api.Dtos.Institution;
using api.interfaces;
using Microsoft.AspNetCore.Authorization;
using api.generique;
using System.Collections.Generic;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstitutionController : ControllerBase
    {
        private readonly IInstitutionRepository _institutionRepository;

        public InstitutionController(IInstitutionRepository institutionRepository)
        {
            _institutionRepository = institutionRepository;
        }

        [HttpGet("GetAll")]
       
        public async Task<IActionResult> GetAllInstitutions()
        {
            var result = await _institutionRepository.GetAllAsync();
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet("GetById/{id}")]
      
        public async Task<IActionResult> GetInstitutionById(int id)
        {
            var result = await _institutionRepository.GetByIdAsync(id);
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

        [HttpPost("Create")]
       
        public async Task<IActionResult> CreateInstitution([FromBody] InstitutionDto institutionDto)
        {
            var result = await _institutionRepository.AddAsync(institutionDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpPut("Update/{id}")]
       
        public async Task<IActionResult> UpdateInstitution([FromRoute] int id, [FromBody] InstitutionDto institutionDto)
        {
            if (id != institutionDto.Id)
            {
                return BadRequest("ID mismatch.");
            }

            var result = await _institutionRepository.UpdateAsync(institutionDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok("Institution updated successfully");
        }

        [HttpDelete("Delete/{id}")]
                public async Task<IActionResult> DeleteInstitution(int id)
        {
            var result = await _institutionRepository.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok("Institution deleted successfully");
        }
    }
}