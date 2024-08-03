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
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetModuleById([FromRoute] int id)
        {
            string username = User.GetUsername();
            AppUser? user = await _manager.FindByNameAsync(username);
            if (user == null)
            {
                return BadRequest();
            }
            // 5f584df6-2795-4a9b-9364-d57c912ef0d8
            // 0bcd548d-9341-4a51-9c3a-540a84ba67e9
            Result<ModuleDto> result = await moduleRepository.GetModuleById(id, user);
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
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteModule(int id)
        {

            return Ok(await moduleRepository.DeleteModule(id));

        }

    }
}