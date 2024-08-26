using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.generique;
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
    }
}