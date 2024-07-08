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
    public class ChapitreRepository : IChapitreRepository
    {
        private readonly apiDbContext _context;

        public ChapitreRepository(apiDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<Chapitre>>> GetAllAsync()
        {
            var chapitres = await _context.chapitres
                .Include(c => c.Module)
                .Include(c => c.Controle)
                .Include(c => c.Quiz)
                .Include(c => c.CheckChapters)
                .ToListAsync();
            return Result<List<Chapitre>>.Success(chapitres);
        }

        public async Task<Result<Chapitre>> GetByIdAsync(int id)
        {
            var chapitre = await _context.chapitres
                .Include(c => c.Module)
                .Include(c => c.Controle)
                .Include(c => c.Quiz)
                .Include(c => c.CheckChapters)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (chapitre == null)
            {
                return Result<Chapitre>.Failure("Chapitre not found.");
            }

            return Result<Chapitre>.Success(chapitre);
        }

        public async Task<Result<Chapitre>> AddAsync(Chapitre chapitre)
        {
            _context.chapitres.Add(chapitre);
            await _context.SaveChangesAsync();
            return Result<Chapitre>.Success(chapitre);
        }

        public async Task<Result> UpdateAsync(Chapitre chapitre)
        {
            var existingChapitre = await _context.chapitres.FindAsync(chapitre.Id);
            if (existingChapitre == null)
            {
                return Result.Failure($"Chapitre with ID {chapitre.Id} not found.");
            }

            _context.Entry(existingChapitre).CurrentValues.SetValues(chapitre);
            await _context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var chapitre = await _context.chapitres.FindAsync(id);
            if (chapitre == null)
            {
                return Result.Failure($"Chapitre with ID {id} not found.");
            }

            _context.chapitres.Remove(chapitre);
            await _context.SaveChangesAsync();
            return Result.Success();
        }
    }
}
