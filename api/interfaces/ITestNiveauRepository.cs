using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Quiz;
using api.Dtos.TestNiveau;
using api.generique;
using api.Model;

namespace api.interfaces
{
    public interface ITestNiveauRepository
    {
        Task<Result<QuizDto>> GetTestNiveauQuestions(int moduleId);
        Task<Result<TestNiveau>> RegisterTestNiveauResult(TestNiveauResultDto testNiveauResultDto);

        Task<Result<double>> GetTestNiveauScore(string studentId, int moduleId);
    }
}