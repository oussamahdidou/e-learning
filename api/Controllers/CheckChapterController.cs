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
            var username = User.GetUsername();
            var user = await _manager.FindByNameAsync(username);
            var checkedChapter = await _check.GetStudentAllcheckChapters(user);
            if (!checkedChapter.IsSuccess) return BadRequest(checkedChapter.Value);
            return Ok(checkedChapter.Value);
        }
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> CreateCheckChapter([FromRoute] int id)
        {
            var username = User.GetUsername();
            var user = await _manager.FindByNameAsync(username);
            var result = await _check.CreateCheckChapter(user, id);
            if (!result.IsSuccess) return BadRequest(result.Error);
            return Ok(result.Value);
        }
        [HttpDelete("{chapterId}")]
        [Authorize]
        public async Task<IActionResult> DeleteCheckChapter(int chapterId)
        {
            var username = User.GetUsername();
            var user = await _manager.FindByNameAsync(username);
            var result = await _check.DeleteCheckChapter(user, chapterId);
            if (!result.IsSuccess) return BadRequest(result.Error);
            return Ok(result.Value);
        }
    }
}