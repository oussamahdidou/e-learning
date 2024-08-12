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
using Microsoft.AspNetCore.Authorization;
using Org.BouncyCastle.Crypto.Modes;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserCenterController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ICheckChapterRepository _checkchapter;
        private readonly IModuleRepository _module;
        private readonly IUserCenterInterface _usercenter;

        public UserCenterController(UserManager<AppUser> userManager, ICheckChapterRepository checkchapter, IModuleRepository module, IUserCenterInterface userCenter)
        {
            _module = module;
            _userManager = userManager;
            _checkchapter = checkchapter;
            _usercenter = userCenter;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllStudentCheckChapter()
        {
            string username = User.GetUsername();
            AppUser? student = await _userManager.FindByNameAsync(username);
            if (student == null) return BadRequest();
            Result<List<CheckChapter>> studentAllCheckedChapter = await _checkchapter.GetStudentAllcheckChapters(student);
            if (!studentAllCheckedChapter.IsSuccess)
            {
                return BadRequest(studentAllCheckedChapter.Error);
            }
            if (studentAllCheckedChapter.Value.Count() == 0)
            {
                return Ok(new List<ModuleWithCheckCountDto>());

            }
            CheckChapterDto moduleIds = studentAllCheckedChapter.Value.ToCheckChapterDto();
            List<ModuleWithCheckCountDto> modulesWithCheckCount = new List<ModuleWithCheckCountDto>();

            foreach (int moduleId in moduleIds.ModuleIds)
            {
                Result<Module> moduleResult = await _module.GetModuleInformationByID(moduleId);
                if (!moduleResult.IsSuccess) return BadRequest(moduleResult.Error);
                Module module = moduleResult.Value;
                int checkCount = studentAllCheckedChapter.Value.Count(x => x.Chapitre?.ModuleId == moduleId);

                modulesWithCheckCount.Add(new ModuleWithCheckCountDto
                {
                    ModuleID = module.Id,
                    Nom = module.Nom,
                    NumberOfChapter = module.Chapitres.Count(),
                    ModuleImg = module.ModuleImg,
                    CheckCount = checkCount
                });
            }
            return Ok(modulesWithCheckCount);
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            string username = User.GetUsername();
            AppUser? user = await _userManager.FindByNameAsync(username);
            if (user == null) return BadRequest();
            Result<bool> result = await _usercenter.ChangePassword(user, changePasswordDto);
            if (!result.IsSuccess) return Ok(result.Error);
            return Ok(result.Value);
        }
    }
}