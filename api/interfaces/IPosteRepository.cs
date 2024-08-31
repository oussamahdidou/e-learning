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
    public interface IPosteRepository
    {
        Task<Result<PosteDto>> GetPostById(int id);

        Task<Result<List<PosteDto>>> GetAllPosts(QueryObject queryObject);

        Task<Result<List<PosteDto>>> GetUserPosts(AppUser user, CommentQuery commentQuery);

        Task AddAsync(Poste poste);

        Task UpdateAsync(Poste poste);

        Task DeleteAsync(int id);
    }
}