using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.generique;
using api.helpers;
using api.interfaces;
using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PosteRepository : IPosteRepository
    {

        private readonly apiDbContext _context;
        public PosteRepository(apiDbContext context)
        {
            _context = context;
        }
        public async Task<Result<Poste>> GetPostById(int id)
        {
            Poste? poste = await _context.postes.FirstOrDefaultAsync(x => x.Id == id);

            if(poste == null)
            {
                return Result<Poste>.Failure("Poste not found");
            }

            return Result<Poste>.Success(poste);
        }

        public async Task<Result<List<Poste>>> GetAllPosts(QueryObject queryObject)
        {
            var poste =  _context.postes.AsQueryable();

            if(!string.IsNullOrWhiteSpace(queryObject.titre))
            {
               poste = poste.Where(x => x.Titre.Equals(queryObject.titre));
            }
            
            return Result<List<Poste>>.Success(await poste.ToListAsync());
        }


        public async Task<Result<List<Poste>>> GetUserPosts(AppUser user)
        {
            List<Poste> postes = await _context.postes.Where(x => x.AppUserId == user.Id).ToListAsync();

            return Result<List<Poste>>.Success(postes);
        }
    }
}