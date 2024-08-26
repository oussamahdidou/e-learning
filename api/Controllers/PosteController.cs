using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.generique;
using api.interfaces;
using api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PosteController : ControllerBase
    {
        private readonly IPosteRepository _posteRepo;

        public PosteController(IPosteRepository posteRepo)
        {
            _posteRepo = posteRepo;
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
    }
}