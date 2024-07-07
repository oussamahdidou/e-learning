using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
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

        public async Task<IEnumerable<Chapitre>> GetAllAsync()
        {
            return await _context.chapitres
                .Include(c => c.Module)
                .Include(c => c.Controle)
                .Include(c => c.Quiz)
                .Include(c => c.CheckChapters)
                .ToListAsync();
        }

        public async Task<Chapitre?> GetByIdAsync(int id)
        {
            return await _context.chapitres
                .Include(c => c.Module)
                .Include(c => c.Controle)
                .Include(c => c.Quiz)
                .Include(c => c.CheckChapters)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Chapitre> AddAsync(Chapitre chapitre)
        {
            _context.chapitres.Add(chapitre);
            await _context.SaveChangesAsync();
            return chapitre;
        }

        public async Task UpdateAsync(Chapitre chapitre)
        {
            _context.Entry(chapitre).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var chapitre = await _context.chapitres.FindAsync(id);
            if (chapitre != null)
            {
                _context.chapitres.Remove(chapitre);
                await _context.SaveChangesAsync();
            }
        }
    }
}
