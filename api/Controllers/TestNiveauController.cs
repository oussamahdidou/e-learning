using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Quiz;
using api.Dtos.TestNiveau;
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
    public class TestNiveauController : ControllerBase
    {
        private readonly ITestNiveauRepository testNiveauRepository;
        private readonly UserManager<AppUser> userManager;
        public TestNiveauController(ITestNiveauRepository testNiveauRepository, UserManager<AppUser> userManager)
        {
            this.testNiveauRepository = testNiveauRepository;
            this.userManager = userManager;
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTestNiveauQuestions([FromRoute] int id)
        {
            Result<QuizDto> result = await testNiveauRepository.GetTestNiveauQuestions(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [Authorize(Roles = UserRoles.Student)]
        [HttpPost("TestResult/{moduleId:int}/{note:double}")]
        public async Task<IActionResult> RegisterTestNiveauResult([FromRoute] int moduleId, [FromRoute] double note)
        {
            string username = User.GetUsername();
            if(username == null){
                return BadRequest("User not found");
            }
            AppUser? appUser = await userManager.FindByNameAsync(username);
             if(appUser == null){
                return BadRequest("User not found");
            }
            TestNiveauResultDto testNiveauResultDto = new TestNiveauResultDto()
            {
                ModuleId = moduleId,
                Note = note,
                StudentId = appUser.Id
            };
            Result<TestNiveau> result = await testNiveauRepository.RegisterTestNiveauResult(testNiveauResultDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }

        [HttpGet("GetScore/{moduleId:int}")]
        public async Task<IActionResult> GetTestNiveauScore([FromRoute] int moduleId)
        {
            string username = User.GetUsername();
            if(username == null){
                return BadRequest("User not found");
            }
            AppUser? appUser = await userManager.FindByNameAsync(username);
            if(appUser == null){
                return BadRequest("User not found");
            }
            Result<double> result = await testNiveauRepository.GetTestNiveauScore(appUser.Id,moduleId);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
    }
}