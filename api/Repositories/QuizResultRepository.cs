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

namespace api.Repositories
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
                var quiz = await _context.quizzes
                    .FindAsync(createQuizResultDto.QuizId);

                if(quiz == null)
                {
                    return Result<QuizResultDto>.Failure($"Quiz not found. ");
                }

                var quizResult = new QuizResult
                {
                    StudentId = studentId,
                    QuizId = createQuizResultDto.QuizId,
                    Note = createQuizResultDto.note
                };

                _context.Add(quizResult);
                await _context.SaveChangesAsync();

                return Result<QuizResultDto>.Success(new QuizResultDto{
                    StudentId = quizResult.StudentId,
                    QuizId = quizResult.QuizId,
                    Note = quizResult.Note
                });
            }
            catch(Exception ex)
            {
                return Result<QuizResultDto>.Failure($"An error occured while adding quizresults: {ex.Message}");
            }
        }
    }
}