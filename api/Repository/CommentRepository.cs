using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.extensions;
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
        public async Task<Result<CommentDto>> CreateComment(CreateCommentDto createCommentDto)
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
                return Result<CommentDto>.Success(comment.FromCommentToCommentDto());
            }
            catch (System.Exception ex)
            {
                return Result<CommentDto>.Failure(ex.Message);

            }

        }

        public async Task<Result<CommentDto>> DeleteComment(int Id)
        {
            try
            {
                Comment? comment = await apiDbContext.comments.FirstOrDefaultAsync(x => x.Id == Id);
                if (comment != null)
                {
                    apiDbContext.comments.Remove(comment);
                    await apiDbContext.SaveChangesAsync();
                    return Result<CommentDto>.Success(comment.FromCommentToCommentDto());
                }
                return Result<CommentDto>.Failure("comment not found");
            }
            catch (System.Exception ex)
            {

                return Result<CommentDto>.Failure(ex.Message);
            }

        }

        public async Task<Result<List<CommentDto>>> GetCommentsByPost(int id, CommentQuery commentQuery)
        {
            try
            {
                List<CommentDto> comments = await apiDbContext.comments.Include(x => x.AppUser).Where(x => x.PosteId == id).OrderByDescending(x => x.CreatedAt).Skip((commentQuery.Page - 1) * 10).Take(10).Select(x => x.FromCommentToCommentDto()).ToListAsync();
                return Result<List<CommentDto>>.Success(comments);
            }
            catch (System.Exception ex)
            {

                return Result<List<CommentDto>>.Failure(ex.Message);
            }
        }

        public async Task<Result<CommentDto>> UpdateComment(UpdateCommentDto updateCommentDto)
        {
            try
            {
                Comment? comment = await apiDbContext.comments.FirstOrDefaultAsync(x => x.Id == updateCommentDto.CommentId);
                if (comment != null)
                {
                    comment.Titre = updateCommentDto.Text;
                    await apiDbContext.SaveChangesAsync();
                    return Result<CommentDto>.Success(comment.FromCommentToCommentDto());
                }
                return Result<CommentDto>.Failure("comment not found");
            }
            catch (System.Exception ex)
            {

                return Result<CommentDto>.Failure(ex.Message);
            }
        }
    }
}