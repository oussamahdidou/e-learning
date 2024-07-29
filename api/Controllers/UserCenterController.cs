using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.dashboard;
using api.Extensions;
using api.interfaces;
using api.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using api.Mappers;
using api.Dtos.UserCenter;
using api.generique;
using api.Dtos.Module;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserCenterController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ICheckChapterRepository _checkchapter;
        private readonly IModuleRepository _module;

        public UserCenterController(UserManager<AppUser> userManager , ICheckChapterRepository checkchapter , IModuleRepository module)
        {
            _module = module;
            _userManager = userManager;
            _checkchapter = checkchapter;
        }
        [HttpGet]
        public async Task<IActionResult>  GetAllStudentCheckChapter(){
            string username = User.GetUsername();
            AppUser? student = await _userManager.FindByNameAsync(username);
            if (student == null) return BadRequest();
            Result<List<CheckChapter>> studentAllCheckedChapter = await _checkchapter.GetStudentAllcheckChapters(student);
            if(!studentAllCheckedChapter.IsSuccess)
            {
                return BadRequest(studentAllCheckedChapter.Error);
            }
            
            CheckChapterDto moduleIds = studentAllCheckedChapter.Value.ToCheckChapterDto();
            List<ModuleWithCheckCountDto> modulesWithCheckCount = new List<ModuleWithCheckCountDto>();

            foreach (int moduleId in moduleIds.ModuleIds)
            {
                Result<Module> moduleResult = await _module.GetModuleInformationByID(moduleId);
                if (moduleResult != null && moduleResult.IsSuccess)
                {
                    var module = moduleResult.Value;
                    var checkCount = studentAllCheckedChapter.Value.Count(x => x.Chapitre.ModuleId == moduleId);

                    modulesWithCheckCount.Add(new ModuleWithCheckCountDto
                    {
                        Module = new ModuleDto {
                            Id = module.Id,
                            Nom = module.Nom,
                            NumberOfChapter = module.Chapitres.Count()
                        },
                        CheckCount = checkCount
                    });
                }
            }
            return Ok(modulesWithCheckCount);
        } 
    }
}