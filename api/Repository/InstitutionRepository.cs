using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.interfaces;
using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class InstitutionRepository : IInstitutionRepository
    {
        private readonly apiDbContext _context;

        public InstitutionRepository(apiDbContext context)
        {
            _context = context;
        }

        public async Task<Institution> AddAsync(Institution institution)
        {
            _context.institutions.Add(institution);
            await _context.SaveChangesAsync();
            return institution;
        }

        public async Task DeleteAsync(int id)
        {
            var institution = await _context.institutions.FindAsync(id);
            if (institution != null)
            {
                _context.institutions.Remove(institution);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Institution>> GetAllAsync()
        {
            return await _context.institutions.ToListAsync();
        }

        public async Task<Institution?> GetByIdAsync(int id)
        {
            return await _context.institutions.FindAsync(id);
        }

        public async Task UpdateAsync(Institution institution)
        {
            _context.Entry(institution).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
