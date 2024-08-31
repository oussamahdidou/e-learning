using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class PosteController : ControllerBase
    {
        private readonly IPosteRepository _posteRepo;

        private readonly UserManager<AppUser> _manager;
        public PosteController(IPosteRepository posteRepo, UserManager<AppUser> manager)
        {
            _posteRepo = posteRepo;
            _manager = manager;
        }

        [HttpGet("getposte/{id:int}")]
        // [Authorize]
        public async Task<IActionResult> getPosteById([FromRoute] int id)
        {
            Result<Poste> result = await _posteRepo.GetPostById(id);

            if(result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }


        [HttpGet("gelallposts")]
        // [Authorize]
        public async Task<IActionResult> getAllPosts([FromQuery] QueryObject queryObject)
        {
            Result<List<Poste>> result = await _posteRepo.GetAllPosts(queryObject);

            if(result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }


        [HttpGet("getuserposts")]
        [Authorize]
        public async Task<IActionResult> getUserPosts()
        {
            string username = User.GetUsername();
            AppUser? user = await _manager.FindByNameAsync(username);

            if(user == null)
            {
                return BadRequest("User not found");
            }

            Result<List<Poste>> result = await _posteRepo.GetUserPosts(user);

            if(result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }
    }
}