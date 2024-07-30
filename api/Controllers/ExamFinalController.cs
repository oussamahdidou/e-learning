using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.ExamFinal;
using api.Extensions;
using api.generique;
using api.helpers;
using api.interfaces;
using api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExamFinalController : ControllerBase
    {
        private readonly IExamFinalRepository examFinalRepository;
        private readonly UserManager<AppUser> userManager;
        public ExamFinalController(IExamFinalRepository examFinalRepository, UserManager<AppUser> userManager)
        {
            this.examFinalRepository = examFinalRepository;
            this.userManager = userManager;
        }
        [HttpPost]
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Teacher}")]
        public async Task<IActionResult> CreateExamFinal([FromForm] CreateExamFinalDto createExamFinalDto)
        {
            string username = User.GetUsername();
            AppUser? appUser = await userManager.FindByNameAsync(username);
            if (appUser == null)
            {

                return Unauthorized("u need to logging");

            }
            IList<string> roles = await userManager.GetRolesAsync(appUser);
            if (roles.Contains(UserRoles.Admin))
            {
                createExamFinalDto.Status = ObjectStatus.Approuver;
                Result<ExamFinal> result = await examFinalRepository.CreateExamFinal(createExamFinalDto);
                if (!result.IsSuccess)
                {
                    return BadRequest(result.Error);
                }
                return Ok(result.Value);
            }
            else if (!roles.Contains(UserRoles.Admin) && roles.Contains(UserRoles.Teacher) && appUser is Teacher teacher && teacher.Granted)
            {
                Result<ExamFinal> result = await examFinalRepository.CreateExamFinal(createExamFinalDto);
                if (!result.IsSuccess)
                {
                    return BadRequest(result.Error);
                }
                return Ok(result.Value);
            }
            else
            {
                return Unauthorized("u need to logging");

            }
        }
        [HttpGet("GetExamFinaleByModule/{id:int}")]
        public async Task<IActionResult> GetExamFinaleByModule([FromRoute] int id)
        {
            Result<ExamFinal> result = await examFinalRepository.GetExamFinaleByModule(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
    }
}