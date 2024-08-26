using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.generique;
using api.helpers;
using api.Model;

namespace api.interfaces
{
    public interface IPosteRepository
    {
        Task<Result<Poste>> GetPostById(int id);

        Task<Result<List<Poste>>> GetAllPosts(QueryObject queryObject);

        Task<Result<List<Poste>>> GetUserPosts(AppUser user);
    }
}