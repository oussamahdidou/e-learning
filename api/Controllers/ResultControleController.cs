using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.extensions;
using api.Extensions;
using api.interfaces;
using api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using api.Mappers;
using api.generique;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultControleController : ControllerBase
    {
        private readonly UserManager<AppUser> _manager;
        private readonly IResultControleRepository _resultRepo;
        private readonly IWebHostEnvironment _environment;
        private readonly IBlobStorageService _blobStorageService;
        public ResultControleController(UserManager<AppUser> manager, IResultControleRepository resultRepo, IWebHostEnvironment environment , IBlobStorageService blobStorageService)
        {
            _manager = manager;
            _resultRepo = resultRepo;
            _environment = environment;
            _blobStorageService = blobStorageService;
        }
        [HttpPost("{id:int}")]
        public async Task<IActionResult> UploadSolution(IFormFile file, [FromRoute] int id)  
        {
            string username = User.GetUsername();
            AppUser? user = await _manager.FindByNameAsync(username);

            if (user == null)
                return BadRequest("User not found.");
            // 5f584df6-2795-4a9b-9364-d57c912ef0d8
            // 0bcd548d-9341-4a51-9c3a-540a84ba67e9

            string ControleResultContainer = "controle-result-container";
            var ControleResultUrl = await _blobStorageService.UploadFileAsync(file.OpenReadStream(), ControleResultContainer, file.FileName);

            Result<ResultControle> addResult = await _resultRepo.AddResult(user, id, ControleResultUrl); 
            if (!addResult.IsSuccess)
                return BadRequest(addResult.Error);
            
            string ControleResultSasUrl = _blobStorageService.GenerateSasToken(ControleResultContainer, Path.GetFileName(ControleResultUrl), TimeSpan.FromMinutes(2));
            addResult.Value.Reponse = ControleResultSasUrl;
            return Ok(addResult.Value.Reponse);
        } 
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetStudentAllResults()
        {
            string username = User.GetUsername();
            AppUser? user = await _manager.FindByNameAsync(username);

            if (user == null)
                return BadRequest("User not found.");

            Result<List<ResultControle>> results = await _resultRepo.GetStudentAllResult(user);

            if (!results.IsSuccess)
                return BadRequest(results.Error);

            return Ok(results.Value);
        }
        [HttpGet("{controleId}")]
        // [Authorize]
        public async Task<IActionResult> GetResultControleById(int controleId)
        {
            string username = User.GetUsername();
            AppUser? user = await _manager.FindByNameAsync(username);

            if (user == null)
                return BadRequest("User not found.");

            Result<ResultControle> result = await _resultRepo.GetResultControleById(user, controleId);

            if (!result.IsSuccess)
                return BadRequest(result.Error);


            return Ok(result.Value.ToResultControleDto());
        }
        [HttpDelete("{controleId}")]
        [Authorize]
        public async Task<IActionResult> RemoveResult(int controleId)
        {
            string username = User.GetUsername();
            AppUser? user = await _manager.FindByNameAsync(username);

            if (user == null)
                return BadRequest("User not found.");

            Result<ResultControle> result = await _resultRepo.RemoveResult(user, controleId);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            if (!string.IsNullOrEmpty(result.Value.Reponse))
                {
                    string ControleResultContainer = "controle-result-container";
                    var oldControleResultFileName = Path.GetFileName(new Uri(result.Value.Reponse).LocalPath);

                    var deleteResult = await _blobStorageService.DeleteFileAsync(ControleResultContainer, oldControleResultFileName);
                    if (!deleteResult)
                    {
                        return BadRequest();
                    }
                }

            return Ok(result.Value);
        }

    }
}
