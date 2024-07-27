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
        private readonly apiDbContext _context;

        public QuizResultRepository(apiDbContext context)
        {
            _context = context;
        }

        public async Task<Result<QuizResult>> CreateQuizResult(AppUser student, CreateQuizResultDto createQuizResultDto)
        {
            try
            {
                QuizResult? existingQuizResult = await _context.quizResults
                    .FirstOrDefaultAsync(qr => qr.StudentId == student.Id && qr.QuizId == createQuizResultDto.QuizId);

                if (existingQuizResult != null)
                {
                    // Update existing quiz result
                    existingQuizResult.Note = createQuizResultDto.note;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    // Create new quiz result
                    QuizResult quizResult = new QuizResult
                    {
                        StudentId = student.Id,
                        QuizId = createQuizResultDto.QuizId,
                        Note = createQuizResultDto.note
                    };

                    _context.Add(quizResult);
                }

                await _context.SaveChangesAsync();

                return Result<QuizResult>.Success(new QuizResult
                {
                    StudentId = student.Id,
                    QuizId = createQuizResultDto.QuizId,
                    Note = createQuizResultDto.note
                });
            }
            catch (Exception ex)
            {
                return Result<QuizResult>.Failure($"An error occurred while adding quiz results: {ex.Message}");
            }
        }


        public async Task<Result<bool>> DeleteQuizResult(AppUser student, int quizId)
        {
            try
            {
                QuizResult? quizResult = await _context.quizResults
                    .FirstOrDefaultAsync(qr => qr.StudentId == student.Id && qr.QuizId == quizId);

                if (quizResult == null)
                {
                    return Result<bool>.Failure($"Quiz result not found for student '{student.Id}' and quiz '{quizId}'.");
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

        public async Task<Result<QuizResult>> UpdateQuizResult(AppUser student, CreateQuizResultDto result)
        {
            QuizResult? quizResult = await _context.quizResults.FirstOrDefaultAsync((x) => x.StudentId == student.Id && x.QuizId == result.QuizId);
            if(quizResult == null)return Result<QuizResult>.Failure("quiz Resultat n'est existe pas");
            quizResult.Note = result.note;
            await _context.SaveChangesAsync();
            return Result<QuizResult>.Success(quizResult);
        }

        public async Task<Result<QuizResult>> GetQuizResultId(AppUser student, int quizId)
        {
            QuizResult? quizResult = await _context.quizResults.FirstOrDefaultAsync((x) => x.StudentId == student.Id && x.QuizId == quizId);
            if(quizResult == null)return Result<QuizResult>.Failure("quiz Resultat n'est existe pas");
            return Result<QuizResult>.Success(quizResult);
        }
    }
}