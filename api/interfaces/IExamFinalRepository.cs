using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.ExamFinal;
using api.generique;
using api.Model;

namespace api.interfaces
{
    public interface IExamFinalRepository
    {
        Task<Result<ExamFinal>> CreateExamFinal(CreateExamFinalDto createExamFinalDto);
        Task<Result<ExamFinal>> UpdateExamFinalEnnonce(UpdateExamFinalDto updateExamFinalDto);
        Task<Result<ExamFinal>> UpdateExamFinalSolution(UpdateExamFinalDto updateExamFinalDto);
        Task<Result<ExamFinal>> GetExamFinaleByModule(int Id);
        Task<Result<ExamFinal>> getExamFinalById(int examId);
        Task<Result<ExamFinal>> Refuser(int id);
        Task<Result<ExamFinal>> Approuver(int id);
        Task<bool> DeleteExam(int id);
    }
}