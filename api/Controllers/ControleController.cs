using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Controle;
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
    public class ControleController : ControllerBase
    {
        private readonly IControleRepository controleRepository;
        private readonly UserManager<AppUser> userManager;
        public ControleController(IControleRepository controleRepository, UserManager<AppUser> userManager)
        {
            this.controleRepository = controleRepository;
            this.userManager = userManager;
        }
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Teacher}")]

        [HttpPost]
        public async Task<IActionResult> CreateControle([FromForm] CreateControleDto createControleDto)
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
                createControleDto.Statue = ObjectStatus.Approuver;
                Result<Controle> result = await controleRepository.CreateControle(createControleDto);
                if (!result.IsSuccess)
                {
                    return BadRequest(result.Error);
                }
                return Ok(result.Value);
            }
            else if (!roles.Contains(UserRoles.Admin) && roles.Contains(UserRoles.Teacher) && appUser is Teacher teacher && teacher.Granted)
            {
                Result<Controle> result = await controleRepository.CreateControle(createControleDto);
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
        [HttpGet("GetControleById/{Id:int}")]
        public async Task<IActionResult> GetControleById([FromRoute] int Id)
        {
            Result<Controle> result = await controleRepository.GetControleById(Id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpGet("GetControleByModule/{Id:int}")]
        public async Task<IActionResult> GetControleByModule([FromRoute] int Id)
        {
            Result<List<Controle>> result = await controleRepository.GetControlesByModule(Id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPut("UpdateControleEnnonce")]
        public async Task<IActionResult> UpdateControleEnnonce([FromBody] UpdateControleEnnonceDto updateControleEnnonceDto)
        {
            Result<Controle> result = await controleRepository.UpdateControleEnnonce(updateControleEnnonceDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPut("UpdateControleSolution")]
        public async Task<IActionResult> UpdateControleSolution([FromBody] UpdateControleSolutionDto updateControleSolutionDto)
        {
            Result<Controle> result = await controleRepository.UpdateControleSolution(updateControleSolutionDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPut("UpdateControleName")]
        public async Task<IActionResult> UpdateControleName([FromBody] UpdateControleNameDto updateControleNameDto)
        {
            Result<Controle> result = await controleRepository.UpdateControleName(updateControleNameDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPut("Approuver/{id:int}")]
        public async Task<IActionResult> ApprouverControle([FromRoute] int id)
        {
            Result<Controle> result = await controleRepository.Approuver(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);

        }
        [HttpPut("Refuser{id:int}")]
        public async Task<IActionResult> RefuserControle([FromRoute] int id)
        {
            Result<Controle> result = await controleRepository.Refuser(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);

        }


    }
}