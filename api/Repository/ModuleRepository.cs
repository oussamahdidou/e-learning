using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data;
using api.interfaces;
using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly apiDbContext _context;

        public ModuleRepository(apiDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Module>> GetAllAsync()
        {
            return await _context.modules.ToListAsync();
        }

        public async Task<Module> GetByIdAsync(int id)
        {
            var module = await _context.modules.FindAsync(id);
            if (module == null)
            {
                throw new KeyNotFoundException($"Module with ID {id} not found.");
            }
            return module;
        }

        public async Task<Module> AddAsync(Module module)
        {
            await _context.modules.AddAsync(module);
            await _context.SaveChangesAsync();
            return module;
        }

        public async Task UpdateAsync(Module module)
        {
            _context.Entry(module).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var module = await _context.modules.FindAsync(id);
            if (module != null)
            {
                _context.modules.Remove(module);
                await _context.SaveChangesAsync();
            }
        }
    }
}