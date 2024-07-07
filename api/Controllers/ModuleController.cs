using System.Collections.Generic;
using System.Threading.Tasks;
using api.Dto;
using api.Service;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModuleController : ControllerBase
    {
        private readonly ModulService _modulService;

        public ModuleController(ModulService modulService)
        {
            _modulService = modulService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModuleDto>>> GetAllModules()
        {
            var result = await _modulService.GetAllDtosAsync();
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ModuleDto>> GetModule(int id)
        {
            var result = await _modulService.GetDtoByIdAsync(id);
            if (!result.IsSuccess )
            {
                return NotFound(result.Error );
            }
            if(result.Value == null){
                return NotFound("Module not found");
            }
            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<ActionResult<ModuleDto>> CreateModule(ModuleDto moduleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _modulService.AddDtoAsync(moduleDto);
            if (!result.IsSuccess || result.Value == null)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtAction(nameof(GetModule), new { id = result.Value.Id }, result.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateModule(int id, ModuleDto moduleDto)
        {
            if (id != moduleDto.Id)
            {
                return BadRequest("L'ID du module ne correspond pas à l'ID de l'URL.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _modulService.UpdateDtoAsync(moduleDto);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModule(int id)
        {
            var result = await _modulService.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return NoContent();
        }
    }
}
