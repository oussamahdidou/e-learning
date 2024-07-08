using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.QuizResult;
using api.generique;
using api.interfaces;
using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class QuizResultRepository : IQuizResultRepository
    {
        private readonly apiDbContext _context ;

        public QuizResultRepository(apiDbContext context)
        {
            _context = context;
        }

        public async Task<Result<QuizResultDto>> CreateQuizResult(string studentId, CreateQuizResultDto createQuizResultDto)
        {
            try
            {
                var existingQuizResult = await _context.quizResults
                    .FirstOrDefaultAsync(qr => qr.StudentId == studentId && qr.QuizId == createQuizResultDto.QuizId);

                if (existingQuizResult != null)
                {
                    // Update existing quiz result
                    existingQuizResult.Note = createQuizResultDto.note;
                    _context.Update(existingQuizResult);
                }
                else
                {
                    // Create new quiz result
                    var quizResult = new QuizResult
                    {
                        StudentId = studentId,
                        QuizId = createQuizResultDto.QuizId,
                        Note = createQuizResultDto.note
                    };

                    _context.Add(quizResult);
                }

                await _context.SaveChangesAsync();

                return Result<QuizResultDto>.Success(new QuizResultDto
                {
                    StudentId = studentId,
                    QuizId = createQuizResultDto.QuizId,
                    Note = createQuizResultDto.note
                });
            }
            catch (Exception ex)
            {
                return Result<QuizResultDto>.Failure($"An error occurred while adding quiz results: {ex.Message}");
            }
        }
        
        public async Task<Result<bool>> DeleteQuizResult(string studentId, int quizId)
        {
            try
            {
                var quizResult = await _context.quizResults
                    .FirstOrDefaultAsync(qr => qr.StudentId == studentId && qr.QuizId == quizId);

                if (quizResult == null)
                {
                    return Result<bool>.Failure($"Quiz result not found for student '{studentId}' and quiz '{quizId}'.");
                }

                _context.quizResults.Remove(quizResult);
                await _context.SaveChangesAsync();

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"An error occurred while deleting quiz result: {ex.Message}");
            }
        }

    }
}