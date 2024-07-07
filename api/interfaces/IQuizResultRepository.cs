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
        Task<Result<QuizResultDto>> CreateQuizResult(string studentId, CreateQuizResultDto createQuizResultDto);
    }
}