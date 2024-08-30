using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.generique;
using api.helpers;
using api.Model;

namespace api.interfaces
{
    public interface ICommentRepository
    {
        Task<Result<List<CommentDto>>> GetCommentsByPost(int id, CommentQuery commentQuery);
        Task<Result<CommentDto>> CreateComment(CreateCommentDto createCommentDto);
        Task<Result<CommentDto>> UpdateComment(UpdateCommentDto updateCommentDto);
        Task<Result<CommentDto>> DeleteComment(int Id);

    }
}