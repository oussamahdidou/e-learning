using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Extensions;
using api.generique;
using api.helpers;
using api.interfaces;
using api.Model;
using api.Dtos.Post;
using api.Dtos.Poste;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PosteController : ControllerBase
    {
        private readonly IPosteRepository _posteRepo;

        private readonly UserManager<AppUser> _manager;

        private readonly IBlobStorageService _blobStorageService;

        public PosteController(IPosteRepository posteRepo, UserManager<AppUser> manager, IBlobStorageService blobStorageService)
        {
            _posteRepo = posteRepo;
            _manager = manager;
            this._blobStorageService = blobStorageService;
        }

        [HttpGet("getposte/{id:int}")]
        // [Authorize]
        public async Task<IActionResult> getPosteById([FromRoute] int id)
        {
            Result<PosteDto> result = await _posteRepo.GetPostById(id);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }


        [HttpGet("gelallposts")]
        // [Authorize]
        public async Task<IActionResult> getAllPosts([FromQuery] QueryObject queryObject)
        {
            Result<List<PosteDto>> result = await _posteRepo.GetAllPosts(queryObject);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }


        [HttpGet("getuserposts")]
        [Authorize]
        public async Task<IActionResult> getUserPosts([FromQuery] CommentQuery commentQuery)
        {
            string username = User.GetUsername();
            AppUser? user = await _manager.FindByNameAsync(username);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            Result<List<PosteDto>> result = await _posteRepo.GetUserPosts(user, commentQuery);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }
        [HttpPost]
        public async Task<ActionResult<Poste>> Create([FromForm] NewPostDto newPostDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Incorrect form fields format");
            }
            var image = "";
            var fichier = "";
            AppUser? user = await _manager.FindByIdAsync(newPostDto.AppUserId);
            if (user is null)
                return BadRequest("You should be logged in to create a poste");
            if (newPostDto.Image != null)
            {
                image = await _blobStorageService.UploadImageVideoAsync(newPostDto.Image.OpenReadStream(), "schema-container", newPostDto.Image.FileName);
                Console.WriteLine($"l'image du poste {image}");
            }
            if (newPostDto.Fichier != null)
            {
                fichier = await _blobStorageService.UploadFileAsync(newPostDto.Fichier.OpenReadStream(), "schema-container", newPostDto.Fichier.FileName);
                Console.WriteLine($"fichier du poste {fichier}");
            }
            if (newPostDto.Fichier != null && newPostDto.Image != null)
            {
                var poste = new Poste
                {
                    Titre = newPostDto.Titre,
                    Content = newPostDto.Content,
                    Image = image,
                    Fichier = fichier,
                    AppUserId = newPostDto.AppUserId,
                };
                await _posteRepo.AddAsync(poste);
                return NoContent();
            }
            else if (newPostDto.Fichier == null && newPostDto.Image == null)
            {
                var poste = new Poste
                {
                    Titre = newPostDto.Titre,
                    Content = newPostDto.Content,
                    AppUserId = newPostDto.AppUserId,
                };
                await _posteRepo.AddAsync(poste);
                return NoContent();
            }
            else if (newPostDto.Fichier != null && newPostDto.Image == null)
            {
                var poste = new Poste
                {
                    Titre = newPostDto.Titre,
                    Content = newPostDto.Content,
                    Fichier = fichier,
                    AppUserId = newPostDto.AppUserId,
                };
                await _posteRepo.AddAsync(poste);
                return NoContent();
            }
            else if (newPostDto.Fichier == null && newPostDto.Image != null)
            {
                var poste = new Poste
                {
                    Titre = newPostDto.Titre,
                    Content = newPostDto.Content,
                    Image = image,
                    AppUserId = newPostDto.AppUserId,
                };
                await _posteRepo.AddAsync(poste);
                return NoContent();
            }
            else
            {
                return BadRequest("Erreur");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdatePostDto updatePostDto)
        {
            var image = "";
            var fichier = "";
            var result = await _posteRepo.GetPostById(updatePostDto.Id);
            if (!result.IsSuccess) // Check if the result is successful
            {
                return NotFound(); // Handle the case where the post does not exist or error occurs
            }
            var existingPosteDto = result.Value; // Extract the PosteDto from the result

            // Convert PosteDto to Poste entity
            var old_poste = new Poste
            {
                Id = existingPosteDto.Id,
                Titre = existingPosteDto.Titre,
                Content = existingPosteDto.Content,
                AppUserId = existingPosteDto.AuthorId,
                Image = existingPosteDto.Image,
                Fichier = existingPosteDto.Fichier
            };
            
            // AppUser? user = await _manager.FindByIdAsync(newPostDto.AppUserId);
            // if (user is null)
            //     return BadRequest("You should be logged in to create a poste");
            if (updatePostDto.Image != null)
            {
                image = await _blobStorageService.UploadImageVideoAsync(updatePostDto.Image.OpenReadStream(), "schema-container", updatePostDto.Image.FileName);
                Console.WriteLine($"l'image du poste {image}");
            }
            if (updatePostDto.Fichier != null)
            {
                fichier = await _blobStorageService.UploadFileAsync(updatePostDto.Fichier.OpenReadStream(), "schema-container", updatePostDto.Fichier.FileName);
                Console.WriteLine($"fichier du poste {fichier}");
            }
            if (updatePostDto.Fichier != null && updatePostDto.Image != null)
            {
                
                    
                old_poste.Titre = updatePostDto.Titre;
                old_poste.Content = updatePostDto.Content;
                old_poste.Image = image;
                old_poste.Fichier = fichier;
                    
                
                await _posteRepo.UpdateAsync(old_poste);
                return NoContent();
            }
            else if (updatePostDto.Fichier == null && updatePostDto.Image == null)
            {
                
                    
                old_poste.Titre = updatePostDto.Titre;
                old_poste.Content = updatePostDto.Content;
                    
                
                await _posteRepo.UpdateAsync(old_poste);
                return NoContent();
            }
            else if (updatePostDto.Fichier != null && updatePostDto.Image == null)
            {
                
                    
                old_poste.Titre = updatePostDto.Titre;
                old_poste.Content = updatePostDto.Content;
                old_poste.Fichier = fichier;
                    
                
                await _posteRepo.UpdateAsync(old_poste);
                return NoContent();
            }
            else if (updatePostDto.Fichier == null && updatePostDto.Image != null)
            {
                
                    
                old_poste.Titre = updatePostDto.Titre;
                old_poste.Content = updatePostDto.Content;
                old_poste.Image = image;
                
                await _posteRepo.UpdateAsync(old_poste);
                return NoContent();
            }
            else
            {
                return BadRequest("Erreur");
            }
            // if (id != poste.Id) return BadRequest();
            // await _posteRepo.UpdateAsync(poste);
            // return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _posteRepo.DeleteAsync(id);
            return NoContent();
        }
    }
}