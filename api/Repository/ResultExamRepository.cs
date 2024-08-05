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
    public class ResultExamRepository : IResultExamRepository
    {
        private readonly apiDbContext _context;
        public ResultExamRepository(apiDbContext context)
        {
            _context = context; 
        }
         public async Task<Result<ResultExam>> AddResult(AppUser user, int examId, string filePath)
        {
            ExamFinal? controle = await _context.examFinals.FindAsync(examId);
            if (controle == null) return Result<ResultExam>.Failure("no final exam was found");
            Student? student = await _context.students.FindAsync(user.Id);
            if (student == null) return Result<ResultExam>.Failure("no Student was found");
            ResultExam ResultExam = new ResultExam
            {
                Student = student,
                ExamFinal = controle,
                StudentId = student.Id,
                ExamFinalId = controle.Id,
                Reponse = filePath
            };
            try
            {
                _context.resultExams.Add(ResultExam);
                await _context.SaveChangesAsync();
                return Result<ResultExam>.Success(ResultExam);
            }
            catch (Exception ex)
            {
                return Result<ResultExam>.Failure(ex.Message);
            }
        }

        public async Task<Result<ResultExam>> GetResultExamById(AppUser user, int examId)
        {
            ResultExam? result = await _context.resultExams
                .FirstOrDefaultAsync(rc => rc.ExamFinalId == examId && rc.StudentId == user.Id);

            if (result == null)
                return Result<ResultExam>.Failure("Result not found");

            return Result<ResultExam>.Success(result);
        }

        public async Task<Result<ResultExam>> RemoveResult(AppUser user, int examId)
        {
            ResultExam? result = await _context.resultExams
                .FirstOrDefaultAsync(rc => rc.ExamFinalId == examId && rc.StudentId == user.Id);

            if (result == null)
                return Result<ResultExam>.Failure("Result not found");

            _context.resultExams.Remove(result);
            await _context.SaveChangesAsync();
            return Result<ResultExam>.Success(result);
        }
    }
}