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
                createExamFinalDto.TeacherId = appUser.Id;
                Result<ExamFinal> result = await examFinalRepository.CreateExamFinal(createExamFinalDto);
                if (!result.IsSuccess)
                {
                    return BadRequest(result.Error);
                }
                return Ok(result.Value);
            }
            else
            {
                return Unauthorized("Attend que l`admin te donne l`acces pour ajouter");

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
        [HttpGet("GetExamFinaleById/{id:int}")]
        public async Task<IActionResult> GetExamFinaleById([FromRoute] int id)
        {
            Result<ExamFinal> result = await examFinalRepository.getExamFinalById(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPut("UpdateExamEnnonce")]
        public async Task<IActionResult> UpdateExamFinaleEnnonce([FromForm] UpdateExamFinalDto updateExamFinalDto)
        {
            Result<ExamFinal> result = await examFinalRepository.UpdateExamFinalEnnonce(updateExamFinalDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPut("UpdateExamSolution")]
        public async Task<IActionResult> UpdateExamFinaleSolution([FromForm] UpdateExamFinalDto updateExamFinalDto)
        {
            Result<ExamFinal> result = await examFinalRepository.UpdateExamFinalSolution(updateExamFinalDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPut("Approuver/{id:int}")]
        public async Task<IActionResult> Approuver([FromRoute] int id)
        {
            Result<ExamFinal> result = await examFinalRepository.Approuver(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPut("Refuser/{id:int}")]
        public async Task<IActionResult> Refuser([FromRoute] int id)
        {
            Result<ExamFinal> result = await examFinalRepository.Refuser(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteExam([FromRoute] int id)
        {
            return Ok(await examFinalRepository.DeleteExam(id));
        }
    }
}