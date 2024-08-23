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
using api.Dtos.UserCenter;
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

            Result<ModuleDto> result = await moduleRepository.GetModuleById(id, user);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpGet("moduleInfo/{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetModuleInformationById([FromRoute] int id)
        {
            string username = User.GetUsername();
            AppUser? user = await _manager.FindByNameAsync(username);
            if (user == null)
            {
                return BadRequest();
            }
            Result<Module> module = await moduleRepository.GetModuleInformationByID(id);

            ModuleWithCheckCountDto moduleInfo = new ModuleWithCheckCountDto
            {
                ModuleID = module.Value.Id,
                Nom = module.Value.Nom,
                NumberOfChapter = module.Value.Chapitres.Count(),
                ModuleImg = module.Value.ModuleImg,
                ModuleProgram = module.Value.CourseProgram,
                ModuleDescription = module.Value.Description,
            };
            if (module.IsSuccess)
            {
                return Ok(moduleInfo);
            }
            return BadRequest(module.Error);
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
        [HttpPut("UpdateModuleImage")]
        public async Task<IActionResult> UpdateModuleImage([FromForm] UpdateModuleImageDto updateModuleImageDto)
        {
            Result<Module> result = await moduleRepository.UpdateModuleImage(updateModuleImageDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPut("UpdateModuleProgram")]
        public async Task<IActionResult> UpdateModuleProgram([FromForm] UpdateModuleProgramDto updateModuleProgramDto)
        {
            Result<Module> result = await moduleRepository.UpdateModuleProgram(updateModuleProgramDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPut("UpdateModuleDescription")]
        public async Task<IActionResult> UpdateModuleDescription([FromBody] UpdateModuleDescriptionDto updateModuleDescriptionDto)
        {
            Result<Module> result = await moduleRepository.UpdateModuleDescription(updateModuleDescriptionDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpGet("GetModuleinfo/{id:int}")]
        public async Task<IActionResult> GetModuleinfo([FromRoute] int id)
        {
            Result<Module> result = await moduleRepository.GetModuleInfo(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpGet("GetModulesNiveauScolaire/{id:int}")]
        public async Task<IActionResult> GetModulesNiveauScolaire([FromRoute] int id)
        {
            Result<List<NiveauScolaire>> result = await moduleRepository.GetModuleNiveauScolaires(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPost("CreateModuleNiveauScolaire")]
        public async Task<IActionResult> CreateModuleNiveauScolaire([FromBody] CreateNiveauScolaireModuleDto createNiveauScolaireModuleDto)
        {
            Result<NiveauScolaire> result = await moduleRepository.CreateNiveauScolaireModule(createNiveauScolaireModuleDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpDelete("DeleteNiveauScolaireModule/{ModuleId:int}/{NiveauScolaireId:int}")]
        public async Task<IActionResult> DeleteNiveauScolaireModule([FromRoute] int ModuleId, [FromRoute] int NiveauScolaireId)
        {
            return Ok(await moduleRepository.DeleteNiveauScolaireModule(ModuleId, NiveauScolaireId));
        }
    }
}