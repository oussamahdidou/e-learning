<<<<<<< HEAD
using Microsoft.AspNetCore.Mvc;
=======
using System;
>>>>>>> manall
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
<<<<<<< HEAD
using api.Dto;
using api.generique;
using api.interfaces;
using Microsoft.AspNetCore.Authorization;
=======
using api.Dtos.Module;
using api.generique;
using api.interfaces;
using api.Model;
using Microsoft.AspNetCore.Mvc;
>>>>>>> manall

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModuleController : ControllerBase
    {
<<<<<<< HEAD
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
=======
        private readonly IModuleRepository moduleRepository;
        public ModuleController(IModuleRepository moduleRepository)
        {
            this.moduleRepository = moduleRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetModuleById(int id)
        {
            Result<Module> result = await moduleRepository.GetModuleById(id);
            if (result.IsSuccess)
>>>>>>> manall
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
<<<<<<< HEAD

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
=======
        [HttpPost]
        public async Task<IActionResult> CreateModule([FromBody] CreateModuleDto createModuleDto)
>>>>>>> manall
        {
            Result<Module> result = await moduleRepository.CreateModule(createModuleDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
<<<<<<< HEAD

            var result = await _moduleRepository.AddAsync(moduleDto);
            if (!result.IsSuccess)
=======
            return BadRequest(result.Error);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateModule([FromBody] UpdateModuleDto updateModuleDto)
        {
            Result<Module> result = await moduleRepository.UpdateModule(updateModuleDto);
            if (result.IsSuccess)
>>>>>>> manall
            {
                return Ok(result.Value);
            }
<<<<<<< HEAD
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
=======
            return BadRequest(result.Error);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteModule(int id){

          Result<Module> result = await moduleRepository.DeleteModule(id);
          if (result.IsSuccess){

            return Ok(result.Value);
          } 
          else {

            return BadRequest(result.Error);
          }
        }

>>>>>>> manall
    }
}