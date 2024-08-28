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
        Task<Result<List<Comment>>> GetCommentsByPost(int id, CommentQuery commentQuery);
        Task<Result<Comment>> CreateComment(CreateCommentDto createCommentDto);
        Task<Result<Comment>> UpdateComment(UpdateCommentDto updateCommentDto);
        Task<Result<Comment>> DeleteComment(int Id);

    }
}