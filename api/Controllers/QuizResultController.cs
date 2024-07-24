using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.QuizResult;
using api.Extensions;
using api.interfaces;
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
            // var username = User.GetUsername();

            // var student = await _userManager.FindByNameAsync(username);

            
                var result = await _quizResultRepo.CreateQuizResult("5f584df6-2795-4a9b-9364-d57c912ef0d8", createQuizResultDto);
                if(!result.IsSuccess)
                {
                    return BadRequest(result.Error);
                }
                return Ok(result.Value);
             
        }
    
        [HttpDelete("DeleteQuizResult/{quizId}/{studentId}")]
        // [Authorize(Roles="Admin,Teacher")]
        public async Task<IActionResult> DeleteQuizResult(string studentId, int quizId)
        {
            var result = await _quizResultRepo.DeleteQuizResult(studentId, quizId);

            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

    }
}