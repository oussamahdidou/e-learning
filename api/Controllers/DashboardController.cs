using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.dashboard;
using api.generique;
using api.interfaces;
using api.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardRepository dashboardRepository;
        private readonly UserManager<AppUser> userManager;
        public DashboardController(IDashboardRepository dashboardRepository, UserManager<AppUser> userManager)
        {
            this.dashboardRepository = dashboardRepository;
            this.userManager = userManager;
        }
        [HttpGet("GetChaptersByModule/{id:int}")]
        public async Task<IActionResult> GetChaptersByModule([FromRoute] int id)
        {
            Result<List<GetChaptersDashboardByModuleDto>> result = await dashboardRepository.GetChaptersDashboardbyModule(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpGet("GetChaptersForControleByModule/{id:int}")]
        public async Task<IActionResult> GetChaptersForControleByModule([FromRoute] int id)
        {
            Result<List<GetChapitresForControleDto>> result = await dashboardRepository.GetChaptersForControle(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpGet("DashboardChapter/{id:int}")]

        public async Task<IActionResult> GetDashboardChapter([FromRoute] int id)
        {
            Result<Chapitre> result = await dashboardRepository.GetDashboardChapiter(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpGet("Teachers")]
        public async Task<IActionResult> GetAllTeachers()
        {
            IList<AppUser> teachers = await userManager.GetUsersInRoleAsync(UserRoles.Teacher);
            return Ok(teachers);
        }
        [HttpPut("Grant/{id}")]
        public async Task<IActionResult> GrantTeacherAccess([FromRoute] string id)
        {
            Result<Teacher> result = await dashboardRepository.GrantTeacherAccess(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPut("RemoveGrant/{id}")]
        public async Task<IActionResult> RemoveGrantTeacherAccess([FromRoute] string id)
        {
            Result<Teacher> result = await dashboardRepository.RemoveGrantTeacherAccess(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpGet("GetObjectspourApprouver")]
        public async Task<IActionResult> GetObjectspourApprouver()
        {
            Result<List<PendingObjectsDto>> result = await dashboardRepository.GetObjectspourApprouver();
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpGet("GetChapitresToUpdateControles/{id:int}")]
        public async Task<IActionResult> GetChapitresToUpdateControles([FromRoute] int id)
        {
            Result<List<GetChapitresToUpdateControlesDto>> result = await dashboardRepository.GetChapitresToUpdateControles(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPut("Updatecontrolechapters/{id:int}")]
        public async Task<IActionResult> Updatecontrolechapters([FromRoute] int id, [FromBody] List<GetChapitresToUpdateControlesDto> getChapitresToUpdateControlesDtos)
        {
            bool result = await dashboardRepository.UpdateControleChapitres(getChapitresToUpdateControlesDtos, id);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}