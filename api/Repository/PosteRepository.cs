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
            var poste =  _context.postes.Include(p => p.Comments).AsQueryable();

            if(!string.IsNullOrWhiteSpace(queryObject.titre))
            {
               poste = poste.Where(x => EF.Functions.Like(x.Titre, $"%{queryObject.titre}%"));
            }

            if(!string.IsNullOrWhiteSpace(queryObject.contenu))
            {
               poste = poste.Where(x => EF.Functions.Like(x.Content, $"%{queryObject.contenu}%"));
            }

            if (queryObject.sortByMostComments)
            {
                poste = poste.OrderByDescending(p => p.Comments.Count);
            }

            // pagination
            int skip = (queryObject.pageNumber - 1) * queryObject.pageSize;
            poste = poste.Skip(skip).Take(queryObject.pageSize);

            return Result<List<Poste>>.Success(await poste.ToListAsync());
        }


        public async Task<Result<List<Poste>>> GetUserPosts(AppUser user)
        {
            List<Poste> postes = await _context.postes.Where(x => x.AppUserId == user.Id).ToListAsync();

            return Result<List<Poste>>.Success(postes);
        }
    }
}