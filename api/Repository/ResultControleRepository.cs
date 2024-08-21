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
    public class ResultControleRepository : IResultControleRepository
    {
        private readonly apiDbContext _context;
        public ResultControleRepository(apiDbContext context)
        {
            _context = context; 
        }
        public async Task<Result<ResultControle>> AddResult(AppUser user, int controleId, string filePath)
        {
            ResultControle resultControle = new ResultControle
            {
                StudentId = user.Id,
                ControleId = controleId,
                Reponse = filePath
            };
                await _context.resultControles.AddAsync(resultControle);
                await _context.SaveChangesAsync();
                return Result<ResultControle>.Success(resultControle);
        }

        public async Task<Result<ResultControle>> GetResultControleById(AppUser user, int controleId)
        {
            ResultControle? result = await _context.resultControles
                .FirstOrDefaultAsync(rc => rc.ControleId == controleId && rc.StudentId == user.Id);

            if (result == null)
                return Result<ResultControle>.Failure("Result not found");

            return Result<ResultControle>.Success(result);
        }

        public async Task<Result<List<ResultControle>>> GetStudentAllResult(AppUser user)
        {
            List<ResultControle> results = await _context.resultControles
                .Where(rc => rc.StudentId == user.Id)
                .ToListAsync();

            if (results == null || !results.Any())
                return Result<List<ResultControle>>.Failure("No results found");

            return Result<List<ResultControle>>.Success(results);
        }

        public async Task<Result<ResultControle>> RemoveResult(AppUser user, int controleId)
        {
            ResultControle? result = await _context.resultControles
                .FirstOrDefaultAsync(rc => rc.ControleId == controleId && rc.StudentId == user.Id);

            if (result == null)
                return Result<ResultControle>.Failure("Result not found");

            _context.resultControles.Remove(result);
            await _context.SaveChangesAsync();
            return Result<ResultControle>.Success(result);
        }
    }
}