using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Model;
using api.Extensions;
using api.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Authorization;
using api.interfaces;
using api.interfaces;
using api.generique;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckChapterController : ControllerBase
    {
        private readonly ICheckChapterRepository _check;
        private readonly UserManager<AppUser> _manager;
        public CheckChapterController(ICheckChapterRepository check, UserManager<AppUser> manager)
        {
            _check = check;
            _manager = manager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetStudentCheckedChapter()
        {
            string username = User.GetUsername();
            AppUser? user = await _manager.FindByNameAsync(username);
            Result<List<CheckChapter>> result = await _check.GetStudentAllcheckChapters(user);
            if (!result.IsSuccess) return BadRequest(result.Error);
            return Ok(result.Value);
        }
        [HttpGet("{id:int}")]
        // [Authorize]
        public async Task<IActionResult> CreateCheckChapter([FromRoute] int id)
        {
            // string username = User.GetUsername();
            // AppUser? user = await _manager.FindByNameAsync(username);
            Result<CheckChapter> result = await _check.CreateCheckChapter("5f584df6-2795-4a9b-9364-d57c912ef0d8", id);
            if (!result.IsSuccess) return BadRequest(result.Error);
            return Ok(result.Value);
        }
        [HttpDelete("{chapterId}")]
        // [Authorize]
        public async Task<IActionResult> DeleteCheckChapter(int chapterId)
        {
            // string username = User.GetUsername();
            // AppUser? user = await _manager.FindByNameAsync(username);
            Result<bool> result = await _check.DeleteCheckChapter("5f584df6-2795-4a9b-9364-d57c912ef0d8", chapterId);
            if (!result.IsSuccess) return BadRequest(result.Error);
            return Ok(result.Value);
        }
    }
}