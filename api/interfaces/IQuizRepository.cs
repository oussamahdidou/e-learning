using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Quiz;
using api.generique;
using api.Model;

namespace api.interfaces
{
    public interface IQuizRepository
    {
        Task<Result<QuizDto>> CreateQuiz(CreateQuizDto quizDto);
        Task<Result<QuizDto>> UpdateQuiz(int quizId, UpdateQuizDto updateQuizDto);
        Task<Result<QuizDto>> GetQuizById(int id);
        Task<Result<QuizDto>> DeleteQuiz(int id);
        Task<Result<Quiz>> Approuver(int id);
        Task<Result<Quiz>> Refuser(int id);
    }
}