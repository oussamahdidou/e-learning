using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Dto;
using api.generique;
using api.interfaces;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModuleController : ControllerBase
    {
        private readonly IModuleRepository _moduleRepository;

        public ModuleController(IModuleRepository moduleRepository)
        {
            _moduleRepository = moduleRepository;
        }

        [HttpGet("GetAll")]
      
        public async Task<IActionResult> GetAllModules()
        {
            var result = await _moduleRepository.GetAllAsync();
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet("GetById/{id}")]
       
        public async Task<IActionResult> GetModule(int id)
        {
            var result = await _moduleRepository.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            if (result.Value == null)
            {
                return NotFound("Module not found");
            }
            return Ok(result.Value);
        }

        [HttpPost("Create")]
        
        public async Task<IActionResult> CreateModule([FromBody] ModuleDto moduleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _moduleRepository.AddAsync(moduleDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return CreatedAtAction(nameof(GetModule), new { id = result.Value?.Id }, result.Value);
        }

        [HttpPut("Update/{id}")]
       
        public async Task<IActionResult> UpdateModule([FromRoute] int id, [FromBody] ModuleDto moduleDto)
        {
            if (id != moduleDto.Id)
            {
                return BadRequest("L'ID du module ne correspond pas à l'ID de l'URL.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingModuleResult = await _moduleRepository.GetByIdAsync(id);
            if (!existingModuleResult.IsSuccess)
            {
                return NotFound(existingModuleResult.Error);
            }

            var updateResult = await _moduleRepository.UpdateAsync(moduleDto);
            if (!updateResult.IsSuccess)
            {
                return BadRequest(updateResult.Error);
            }
            return Ok("Module updated successfully");
        }

        [HttpDelete("Delete/{id}")]
               public async Task<IActionResult> DeleteModule(int id)
        {
            var existingModuleResult = await _moduleRepository.GetByIdAsync(id);
            if (!existingModuleResult.IsSuccess)
            {
                return NotFound(existingModuleResult.Error);
            }

            var deleteResult = await _moduleRepository.DeleteAsync(id);
            if (!deleteResult.IsSuccess)
            {
                return BadRequest(deleteResult.Error);
            }
            return Ok("Module deleted successfully");
        }
    }
}