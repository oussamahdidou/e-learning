using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.QuizResult;
using api.Extensions;
using api.generique;
using api.interfaces;
using api.Mappers;
using api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizResultController : ControllerBase
    {
        private readonly IQuizResultRepository _quizResultRepo;
        private readonly UserManager<AppUser> _userManager;
        public QuizResultController(IQuizResultRepository quizResultRepository, UserManager<AppUser> userManager)
        {
            _quizResultRepo = quizResultRepository;
            _userManager = userManager;
        }

         [HttpPost("Create")]
        //  [Authorize(Roles ="Student")]
        public async Task<IActionResult> CreateQuizResult([FromBody] CreateQuizResultDto createQuizResultDto)
        {
            string username = User.GetUsername();
            AppUser? student = await _userManager.FindByNameAsync(username);
            if (student == null) return BadRequest();
            
                var result = await _quizResultRepo.CreateQuizResult(student, createQuizResultDto);
                if(!result.IsSuccess)
                {
                    return BadRequest(result.Error);
                }
                return Ok(result.Value.ToQuizResultDto());
             
        }

        [HttpPut]
        [Authorize]

        public async Task<IActionResult> UpdateQuizResult([FromBody] CreateQuizResultDto resultat){
            string username = User.GetUsername();
            AppUser? student = await _userManager.FindByNameAsync(username);
            if (student == null) return BadRequest();
            Result<QuizResult> result = await _quizResultRepo.UpdateQuizResult(student, resultat);

            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value.ToQuizResultDto());
        }

        [HttpGet("{quizId}")]
        [Authorize]

        public async Task<IActionResult> GetQuizResultById(int quizId){
            string username = User.GetUsername();
            AppUser? student = await _userManager.FindByNameAsync(username);
            if (student == null) return BadRequest();
            Result<QuizResult> result = await _quizResultRepo.GetQuizResultId(student, quizId);

            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value.ToQuizResultDto());
        }
    
        [HttpDelete("DeleteQuizResult/{quizId}")]
        // [Authorize(Roles="Admin,Teacher")]
        public async Task<IActionResult> DeleteQuizResult( int quizId)
        {
            string username = User.GetUsername();
            AppUser? student = await _userManager.FindByNameAsync(username);
            if (student == null) return BadRequest();
            var result = await _quizResultRepo.DeleteQuizResult(student, quizId);

            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

    }
}