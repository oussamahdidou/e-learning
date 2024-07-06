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
        Task<Result<Quiz>> CreateQuiz(CreateQuizDto quizDto);
        Task<Result<Quiz>> UpdateQuiz(int quizId, UpdateQuizDto updateQuizDto);
        Task<Result<Quiz>> GetQuizById(int id);
    }
}