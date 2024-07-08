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
        public async Task<Result<ResultControle>> AddResult(AppUser user , int controleId , string filePath)
        {
            var controle = await _context.controles.FindAsync(controleId);
            if(controle == null) return Result<ResultControle>.Failure("no chapter was found");
            var student = await _context.students.FindAsync(user.Id);
            if(student == null) return Result<ResultControle>.Failure("no Student was found");
            var resultControle = new ResultControle
            {
                StudentId = student.Id,
                ControleId= controleId,
                Student = student,
                Controle = controle,
                Reponse =  filePath
            };
            try{
                _context.resultControles.Add(resultControle);
                await _context.SaveChangesAsync();
                return Result<ResultControle>.Success(resultControle);
            }catch(Exception ex){
                return Result<ResultControle>.Failure("Something went wrong");
            }
        }

        public async Task<Result<ResultControle>> GetResultControleById(AppUser user, int controleId)
        {
            var result = await _context.resultControles
                .FirstOrDefaultAsync(rc => rc.ControleId == controleId && rc.StudentId == user.Id);

            if (result == null)
                return Result<ResultControle>.Failure("Result not found");

            return Result<ResultControle>.Success(result);
        }

        public async Task<Result<List<ResultControle>>> GetStudentAllResult(AppUser user)
        {
            var results = await _context.resultControles
                .Where(rc => rc.StudentId == user.Id)
                .ToListAsync();

            if (results == null || !results.Any())
                return Result<List<ResultControle>>.Failure("No results found");

            return Result<List<ResultControle>>.Success(results);
        }

        public async Task<Result<ResultControle>> RemoveResult(AppUser user, int controleId)
        {
            var result = await _context.resultControles
                .FirstOrDefaultAsync(rc => rc.ControleId == controleId && rc.StudentId == user.Id);

            if (result == null)
                return Result<ResultControle>.Failure("Result not found");

            _context.resultControles.Remove(result);
            await _context.SaveChangesAsync();
            return Result<ResultControle>.Success(result);
        }
    }
}