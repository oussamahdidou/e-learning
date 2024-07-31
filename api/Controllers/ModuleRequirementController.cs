using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.RequiredModules;
using api.Extensions;
using api.generique;
using api.interfaces;
using api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModuleRequirementController : ControllerBase
    {
        private readonly IModuleRequirementsRepository moduleRequirementsRepository;
        private readonly UserManager<AppUser> userManager;
        public ModuleRequirementController(IModuleRequirementsRepository moduleRequirementsRepository, UserManager<AppUser> userManager)
        {
            this.moduleRequirementsRepository = moduleRequirementsRepository;
            this.userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> CreateRequiredModule([FromBody] CreateRequiredModuleDto createRequiredModuleDto)
        {
            Result<ModuleRequirement> result = await moduleRequirementsRepository.CreateRequiredModule(createRequiredModuleDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateRequiredModule([FromBody] CreateRequiredModuleDto createRequiredModuleDto)
        {
            Result<ModuleRequirement> result = await moduleRequirementsRepository.UpdateRequiredModule(createRequiredModuleDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpDelete("{TargetModuleId:int}/{RequiredModuleId:int}")]
        public async Task<IActionResult> DeleteRequiredModule([FromRoute] int TargetModuleId, [FromRoute] int RequiredModuleId)
        {
            Result<ModuleRequirement> result = await moduleRequirementsRepository.DeleteRequiredModule(TargetModuleId, RequiredModuleId);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpGet("RequiredModules/{id:int}")]
        public async Task<IActionResult> GetRequiredModules([FromRoute] int id)
        {
            Result<List<RequiredModulesDto>> result = await moduleRequirementsRepository.GetRequiredModules(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpGet("RequiredInModules/{id:int}")]
        public async Task<IActionResult> GetRequiredInModules([FromRoute] int id)
        {
            Result<List<RequiredModulesDto>> result = await moduleRequirementsRepository.GetRequiredInModules(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [Authorize(Roles = UserRoles.Student)]
        [HttpGet("IsStudentEligibleForModule/{moduleId:int}")]
        public async Task<IActionResult> IsStudentEligibleForModule([FromRoute] int moduleId)
        {
            string userId = (await userManager.FindByNameAsync(User.GetUsername())).Id;
            StudentEligibleDto studentEligibleDto = new StudentEligibleDto()
            {
                StudentId = userId,
                TargetModuleId = moduleId,
            };
            return Ok(await moduleRequirementsRepository.IsStudentEligibleForModule(studentEligibleDto));
        }
    }
}