using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.extensions;
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
    public class ResultExamController : ControllerBase
    {
        private readonly UserManager<AppUser> _manager;
        private readonly IResultExamRepository _resultRepo;
        private readonly IWebHostEnvironment _environment;
        public ResultExamController(UserManager<AppUser> manager, IResultExamRepository resultRepo, IWebHostEnvironment environment)
        {
            _manager = manager;
            _resultRepo = resultRepo;
            _environment = environment;
        }

        [HttpPost("{id:int}")]
        public async Task<IActionResult> UploadSolution(IFormFile file, [FromRoute] int id)  
        {
            string username = User.GetUsername();
            AppUser? user = await _manager.FindByNameAsync(username);

            if (user == null)
                return BadRequest("User not found.");

            Result<string> result = await file.UploadControleReponse(_environment);

            if (!result.IsSuccess)
                return BadRequest(result.Error);
            Result<ResultExam> addResult = await _resultRepo.AddResult(user, id, result.Value); 
            if (!addResult.IsSuccess)
                return BadRequest(result.Error);
            return Ok(result.Value);
        }

        [HttpGet("{controleId}")]
        public async Task<IActionResult> GetResultExamById(int examId)
        {
            string username = User.GetUsername();
            AppUser? user = await _manager.FindByNameAsync(username);

            if (user == null)
                return BadRequest("User not found.");

            Result<ResultExam> result = await _resultRepo.GetResultExamById(user, examId);

            if (!result.IsSuccess)
                return BadRequest(result.Error);


            return Ok(result.Value.ToExamFinalDto());
        }
        [HttpDelete("{controleId}")]
        [Authorize]
        public async Task<IActionResult> RemoveResult(int examId)
        {
            string username = User.GetUsername();
            AppUser? user = await _manager.FindByNameAsync(username);

            if (user == null)
                return BadRequest("User not found.");

            Result<ResultExam> result = await _resultRepo.RemoveResult(user, examId);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            string filePath = Path.Combine(_environment.WebRootPath, result.Value.Reponse.TrimStart('/'));
            if (System.IO.File.Exists(filePath))
            {
                try
                {
                    System.IO.File.Delete(filePath);
                }
                catch (Exception ex)
                {
                    return BadRequest($"File deletion failed: {ex.Message}");
                }
            }

            return Ok(result.Value.ToExamFinalDto());
        }
    }
}