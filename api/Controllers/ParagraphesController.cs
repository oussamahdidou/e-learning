using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.generique;
using api.interfaces;
using api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParagraphesController : ControllerBase
    {
        private readonly IParagrapheRepository paragrapheRepository;
        public ParagraphesController(IParagrapheRepository paragrapheRepository)
        {
            this.paragrapheRepository = paragrapheRepository;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourById([FromRoute] int id)
        {
            Result<Paragraphe> result = await paragrapheRepository.GetCourById(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteParagraphe([FromRoute] int id)
        {
            return Ok(await paragrapheRepository.DeletePAragraphe(id));

        }
    }
}