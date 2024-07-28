using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Quiz;
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
    public class QuizController : ControllerBase
    {
        private readonly IQuizRepository _quizRepo;
        private readonly UserManager<AppUser> userManager;

        public QuizController(IQuizRepository quizRepository, UserManager<AppUser> userManager)
        {
            _quizRepo = quizRepository;
            this.userManager = userManager;
        }

        [HttpPost("Create")]
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Teacher}")]
        public async Task<IActionResult> CreateQuiz([FromBody] CreateQuizDto createQuizDto)
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
                createQuizDto.Statue = ObjectStatus.Approuver;
                Result<QuizDto> result = await _quizRepo.CreateQuiz(createQuizDto);
                if (!result.IsSuccess)
                {
                    return BadRequest(result.Error);
                }
                return Ok(result.Value);
            }
            else if (!roles.Contains(UserRoles.Admin) && roles.Contains(UserRoles.Teacher) && appUser is Teacher teacher && teacher.Granted)
            {
                Result<QuizDto> result = await _quizRepo.CreateQuiz(createQuizDto);
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

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateQuiz([FromRoute] int id, [FromBody] UpdateQuizDto updateQuizDto)
        {
            Result<QuizDto> result = await _quizRepo.UpdateQuiz(id, updateQuizDto);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet("GetById/{id}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> GetQuizById(int id)
        {
            Result<QuizDto> result = await _quizRepo.GetQuizById(id);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            Result<QuizDto> result = await _quizRepo.DeleteQuiz(id);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPut("Approuver/{id:int}")]
        public async Task<IActionResult> ApprouverQuiz([FromRoute] int id)
        {
            Result<Quiz> result = await _quizRepo.Approuver(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);

        }
        [HttpPut("Refuser{id:int}")]
        public async Task<IActionResult> RefuserQuiz([FromRoute] int id)
        {
            Result<Quiz> result = await _quizRepo.Refuser(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);

        }

    }
}