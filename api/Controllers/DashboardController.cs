using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.dashboard;
using api.generique;
using api.interfaces;
using api.Model;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardRepository dashboardRepository;
        public DashboardController(IDashboardRepository dashboardRepository)
        {
            this.dashboardRepository = dashboardRepository;
        }
        [HttpGet("GetChaptersByModule/{id:int}")]
        public async Task<IActionResult> GetChaptersByModule([FromRoute] int id)
        {
            Result<List<GetChaptersDashboardByModuleDto>> result = await dashboardRepository.GetChaptersDashboardbyModule(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return Ok(result.Error);
        }
        [HttpGet("GetChaptersForControleByModule/{id:int}")]
        public async Task<IActionResult> GetChaptersForControleByModule([FromRoute] int id)
        {
            Result<List<GetChapitresForControleDto>> result = await dashboardRepository.GetChaptersForControle(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return Ok(result.Error);
        }
        [HttpGet("DashboardChapter/{id:int}")]

        public async Task<IActionResult> GetDashboardChapter([FromRoute] int id)
        {
            Result<Chapitre> result = await dashboardRepository.GetDashboardChapiter(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return Ok(result.Error);
        }
    }
}