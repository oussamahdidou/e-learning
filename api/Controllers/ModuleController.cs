using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Module;
using api.Extensions;
using api.generique;
using api.interfaces;
using api.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModuleController : ControllerBase
    {
        private readonly IModuleRepository moduleRepository;
         private readonly UserManager<AppUser> _manager;
        public ModuleController(IModuleRepository moduleRepository, UserManager<AppUser> manager)
        {
            this.moduleRepository = moduleRepository;
            _manager = manager;
        }
        [HttpGet]
        public async Task<IActionResult> GetModuleById(int id)
        {
            string username = User.GetUsername();
            AppUser? user = await _manager.FindByNameAsync(username);
            if(user == null){
                return BadRequest();
            }
            Result<ModuleDto> result = await moduleRepository.GetModuleById(id, user.Id);
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