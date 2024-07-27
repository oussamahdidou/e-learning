using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.QuizResult;
using api.generique;
using api.Model;

namespace api.interfaces
{
    public interface IQuizResultRepository
    {
        Task<Result<QuizResult>> CreateQuizResult(AppUser student, CreateQuizResultDto createQuizResultDto);
        Task<Result<QuizResult>> UpdateQuizResult(AppUser student, CreateQuizResultDto result);
        Task<Result<QuizResult>> GetQuizResultId(AppUser student, int quizId);
        Task<Result<bool>> DeleteQuizResult(AppUser student, int quizId);
    }
}