using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.generique;
using api.helpers;
using api.interfaces;
using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly apiDbContext apiDbContext;
        public CommentRepository(apiDbContext apiDbContext)
        {
            this.apiDbContext = apiDbContext;
        }
        public async Task<Result<Comment>> CreateComment(CreateCommentDto createCommentDto)
        {
            try
            {
                Comment comment = new Comment()
                {
                    Titre = createCommentDto.Text,
                    CreatedAt = DateTime.Now,
                    PosteId = createCommentDto.PosteId,
                    AppUserId = createCommentDto.UserId,
                };
                await apiDbContext.comments.AddAsync(comment);
                await apiDbContext.SaveChangesAsync();
                return Result<Comment>.Success(comment);
            }
            catch (System.Exception ex)
            {
                return Result<Comment>.Failure(ex.Message);

            }

        }

        public async Task<Result<Comment>> DeleteComment(int Id)
        {
            try
            {
                Comment? comment = await apiDbContext.comments.FirstOrDefaultAsync(x => x.Id == Id);
                if (comment != null)
                {
                    apiDbContext.comments.Remove(comment);
                    await apiDbContext.SaveChangesAsync();
                    return Result<Comment>.Success(comment);
                }
                return Result<Comment>.Failure("comment not found");
            }
            catch (System.Exception ex)
            {

                return Result<Comment>.Failure(ex.Message);
            }

        }

        public async Task<Result<List<Comment>>> GetCommentsByPost(int id, CommentQuery commentQuery)
        {
            try
            {
                List<Comment> comments = await apiDbContext.comments.Where(x => x.PosteId == id).Skip((commentQuery.Page - 1) * 5).Take(5).ToListAsync();
                return Result<List<Comment>>.Success(comments);
            }
            catch (System.Exception ex)
            {

                return Result<List<Comment>>.Failure(ex.Message);
            }
        }

        public async Task<Result<Comment>> UpdateComment(UpdateCommentDto updateCommentDto)
        {
            try
            {
                Comment? comment = await apiDbContext.comments.FirstOrDefaultAsync(x => x.Id == updateCommentDto.CommentId);
                if (comment != null)
                {
                    comment.Titre = updateCommentDto.Text;
                    await apiDbContext.SaveChangesAsync();
                    return Result<Comment>.Success(comment);
                }
                return Result<Comment>.Failure("comment not found");
            }
            catch (System.Exception ex)
            {

                return Result<Comment>.Failure(ex.Message);
            }
        }
    }
}