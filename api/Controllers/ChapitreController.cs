using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Chapitre;
using api.Extensions;
using api.generique;
using api.helpers;
using api.interfaces;
using api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChapitreController : ControllerBase
    {
        private readonly IChapitreRepository chapitreRepository;
        private readonly UserManager<AppUser> userManager;
        public ChapitreController(IChapitreRepository chapitreRepository, UserManager<AppUser> userManager)
        {
            this.chapitreRepository = chapitreRepository;
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetChapitreById(int id)
        {
            Result<Chapitre> result = await chapitreRepository.GetChapitreById(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Teacher}")]
        [HttpPost]
        public async Task<IActionResult> CreateChapitre([FromForm] CreateChapitreDto createChapitreDto)
        {
            string username = User.GetUsername();
            AppUser? appUser = await userManager.FindByNameAsync(username);
            if (appUser == null)
            {

                return Unauthorized("Attend que l`admin te donne l`acces pour ajouter");

            }
            IList<string> roles = await userManager.GetRolesAsync(appUser);
            if (roles.Contains(UserRoles.Admin))
            {
                createChapitreDto.Statue = ObjectStatus.Approuver;
                Result<Chapitre> result = await chapitreRepository.CreateChapitre(createChapitreDto);
                if (!result.IsSuccess)
                {
                    return BadRequest(result.Error);
                }
                return Ok(result.Value);
            }
            else if (!roles.Contains(UserRoles.Admin) && roles.Contains(UserRoles.Teacher) && appUser is Teacher teacher && teacher.Granted)
            {
                createChapitreDto.TeacherId = appUser.Id;
                Result<Chapitre> result = await chapitreRepository.CreateChapitre(createChapitreDto);
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
        [HttpPut("UpdateChapitrePdf")]
        public async Task<IActionResult> UpdateChapitrePdf([FromForm] UpdateChapitrePdfDto updateChapitrePdfDto)
        {

            Result<Chapitre> result = await chapitreRepository.UpdateChapitrePdf(updateChapitrePdfDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);

        }
        [HttpPut("UpdateChapitreVideo")]
        public async Task<IActionResult> UpdateChapitreVideo([FromForm] UpdateChapitreVideoDto updateChapitreVideoDto)
        {

            Result<Video> result = await chapitreRepository.UpdateChapitreVideo(updateChapitreVideoDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPut("UpdateChapitreVideoLink")]
        public async Task<IActionResult> UpdateChapitreVideoLink([FromBody] UpdateChapitreVideoLinkDto updateChapitreVideoLinkDto)
        {

            Result<Video> result = await chapitreRepository.UpdateChapitreVideoLink(updateChapitreVideoLinkDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPut("UpdateChapitreSchema")]
        public async Task<IActionResult> UpdateChapitreSchema([FromForm] UpdateChapitreSchemaDto updateChapitreSchemaDto)
        {

            Result<Schema> result = await chapitreRepository.UpdateChapitreSchema(updateChapitreSchemaDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPut("UpdateChapitreSynthese")]
        public async Task<IActionResult> UpdateChapitreSynthese([FromForm] UpdateChapitreSyntheseDto updateChapitreSyntheseDto)
        {
            Result<Synthese> result = await chapitreRepository.UpdateChapitreSynthese(updateChapitreSyntheseDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPost("AddSynthese")]
        public async Task<IActionResult> AddSynthese([FromForm] UpdateChapitreSyntheseDto updateChapitreSyntheseDto)
        {
            Result<Synthese> result = await chapitreRepository.AddChapitreSynthese(updateChapitreSyntheseDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPost("AddSchema")]
        public async Task<IActionResult> AddSchema([FromForm] UpdateChapitreSchemaDto updateChapitreSchemaDto)
        {
            Result<Schema> result = await chapitreRepository.AddChapitreSchema(updateChapitreSchemaDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPost("AddVideo")]
        public async Task<IActionResult> AddVideo([FromForm] UpdateChapitreVideoDto updateChapitreVideoDto)
        {
            Result<Video> result = await chapitreRepository.AddVideo(updateChapitreVideoDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPost("AddVideoLink")]
        public async Task<IActionResult> AddVideoLink([FromBody] UpdateChapitreVideoLinkDto updateChapitreVideoLinkDto)
        {
            Result<Video> result = await chapitreRepository.AddVideoLink(updateChapitreVideoLinkDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPut("Approuver/{id:int}")]
        public async Task<IActionResult> ApprouverChapitre([FromRoute] int id)
        {
            Result<Chapitre> result = await chapitreRepository.Approuver(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);

        }
        [HttpPut("Refuser/{id:int}")]
        public async Task<IActionResult> RefuserChapitre([FromRoute] int id)
        {
            Result<Chapitre> result = await chapitreRepository.Refuser(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);

        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteChapitre([FromRoute] int id)
        {
            return Ok(await chapitreRepository.DeleteChapitre(id));
        }
        [HttpPut("UpdateChapterName")]
        public async Task<IActionResult> UpdateChapterName([FromBody] UpdateChapitreNameDto updateChapitreNameDto)
        {
            Result<Chapitre> result = await chapitreRepository.UpdateChapterName(updateChapitreNameDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPost("CreateParagraphe")]
        public async Task<IActionResult> CreateParagraphe([FromForm] CreateParagrapheDto createParagrapheDto)
        {
            Result<Paragraphe> result = await chapitreRepository.CreateParagraphe(createParagrapheDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpGet("Paragraphe/{id:int}")]
        public async Task<IActionResult> Paragraphe([FromRoute] int id)
        {
            Result<Paragraphe> result = await chapitreRepository.GetParagrapheByid(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPut("UpdateParagraphe")]
        public async Task<IActionResult> UpdateParagraphe([FromForm] UpdateParagrapheDto UpdateParagrapheDto)
        {

            Result<Paragraphe> result = await chapitreRepository.UpdateParagraphe(UpdateParagrapheDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);

        }
        [HttpGet("Video/{Id:int}")]
        public async Task<IActionResult> GetVideoById([FromRoute] int Id)
        {

            Result<Video> result = await chapitreRepository.GetVideoById(Id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);

        }
        [HttpGet("Synthese/{Id:int}")]
        public async Task<IActionResult> GetSyntheseById([FromRoute] int Id)
        {

            Result<Synthese> result = await chapitreRepository.GetSyntheseById(Id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);

        }
        [HttpGet("Schema/{Id:int}")]
        public async Task<IActionResult> GetSchemaById([FromRoute] int Id)
        {

            Result<Schema> result = await chapitreRepository.GetSchemaById(Id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);

        }
        [HttpDelete("Schema/{id:int}")]
        public async Task<IActionResult> DeleteSchema([FromRoute] int id)
        {
            return Ok(await chapitreRepository.DeleteSchema(id));

        }
        [HttpDelete("Video/{id:int}")]
        public async Task<IActionResult> DeleteVideo([FromRoute] int id)
        {
            return Ok(await chapitreRepository.DeleteVideo(id));

        }
        [HttpDelete("Synthese/{id:int}")]
        public async Task<IActionResult> DeleteSynthese([FromRoute] int id)
        {
            return Ok(await chapitreRepository.DeleteSynthese(id));

        }

        [HttpPut("UpdateVideoName")]
        public async Task<IActionResult> UpdateVideoName([FromBody] UpdateChapitreNameDto updateChapitreNameDto)
        {
            Result<Video> result = await chapitreRepository.UpdateVideoName(updateChapitreNameDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPut("UpdateSchemaName")]
        public async Task<IActionResult> UpdateSchemaName([FromBody] UpdateChapitreNameDto updateChapitreNameDto)
        {
            Result<Schema> result = await chapitreRepository.UpdateSchemaName(updateChapitreNameDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPut("UpdateSyntheseName")]
        public async Task<IActionResult> UpdateSyntheseName([FromBody] UpdateChapitreNameDto updateChapitreNameDto)
        {
            Result<Synthese> result = await chapitreRepository.UpdateSyntheseName(updateChapitreNameDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPut("UpdateParagrapheName")]
        public async Task<IActionResult> UpdateParagrapheName([FromBody] UpdateChapitreNameDto updateChapitreNameDto)
        {
            Result<Paragraphe> result = await chapitreRepository.UpdateParagrapheName(updateChapitreNameDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
    }

}