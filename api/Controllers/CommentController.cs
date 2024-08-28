using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.generique;
using api.helpers;
using api.interfaces;
using api.Model;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository commentRepository;
        public CommentController(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentDto createCommentDto)
        {
            Result<Comment> result = await commentRepository.CreateComment(createCommentDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateComment([FromBody] UpdateCommentDto updateCommentDto)
        {
            Result<Comment> result = await commentRepository.UpdateComment(updateCommentDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPosteComments([FromRoute] int id, [FromQuery] CommentQuery commentQuery)
        {
            Result<List<Comment>> result = await commentRepository.GetCommentsByPost(id, commentQuery);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            Result<Comment> result = await commentRepository.DeleteComment(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
    }
}