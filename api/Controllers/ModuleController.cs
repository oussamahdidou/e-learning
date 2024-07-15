using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Module;
using api.generique;
using api.interfaces;
using api.Model;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModuleController : ControllerBase
    {
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
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPost]
        public async Task<IActionResult> CreateModule([FromBody] CreateModuleDto createModuleDto)
        {
            Result<Module> result = await moduleRepository.CreateModule(createModuleDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateModule([FromBody] UpdateModuleDto updateModuleDto)
        {
            Result<Module> result = await moduleRepository.UpdateModule(updateModuleDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }

    }
}