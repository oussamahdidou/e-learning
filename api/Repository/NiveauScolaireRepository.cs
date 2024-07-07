using System.Collections.Generic;
using System.Threading.Tasks;
using api.Model;
using api.interfaces;
using Microsoft.EntityFrameworkCore;
using api.Data;

namespace api.Repository
{
    public class NiveauScolaireRepository : INiveauScolaireRepository
    {
        private readonly apiDbContext _context;

        public NiveauScolaireRepository(apiDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NiveauScolaire>> GetAllAsync()
        {
            return await _context.niveauScolaires.Include(ns => ns.Modules).ToListAsync();
        }

        public async Task<NiveauScolaire?> GetByIdAsync(int id)
        {
            return await _context.niveauScolaires
                .Include(ns => ns.Modules)
                .FirstOrDefaultAsync(ns => ns.Id == id);
        }

        public async Task<NiveauScolaire> AddAsync(NiveauScolaire niveauScolaire)
        {
            _context.niveauScolaires.Add(niveauScolaire);
            await _context.SaveChangesAsync();
            return niveauScolaire;
        }

        public async Task UpdateAsync(NiveauScolaire niveauScolaire)
        {
            _context.niveauScolaires.Update(niveauScolaire);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var niveauScolaire = await _context.niveauScolaires.FindAsync(id);
            if (niveauScolaire != null)
            {
                _context.niveauScolaires.Remove(niveauScolaire);
                await _context.SaveChangesAsync();
            }
        }
    }
}
