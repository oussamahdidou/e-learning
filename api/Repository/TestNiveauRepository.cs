using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Quiz;
using api.Dtos.TestNiveau;
using api.generique;
using api.interfaces;
using api.Mappers;
using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class TestNiveauRepository : ITestNiveauRepository
    {
        private readonly apiDbContext apiDbContext;
        public TestNiveauRepository(apiDbContext apiDbContext)
        {
            this.apiDbContext = apiDbContext;
        }
        public async Task<Result<QuizDto>> GetTestNiveauQuestions(int moduleId)
        {
            try
            {
                if (await apiDbContext.modules.AnyAsync(x => x.Id == moduleId))
                {
                    List<Question> questions = await apiDbContext.modules.Where(m => m.Id == moduleId)
                                           .Include(m => m.Chapitres)
                                           .ThenInclude(c => c.Quiz)
                                           .ThenInclude(q => q.Questions)
                                           .ThenInclude(o => o.Options)
                                           .SelectMany(m => m.Chapitres)
                                           .SelectMany(c => c.Quiz.Questions)
                                           .OrderBy(q => Guid.NewGuid())
                                           .Take(20)
                                           .ToListAsync();

                    Quiz quiz = new Quiz
                    {
                        Nom = "Test De Niveau",
                        Questions = questions
                    };
                    QuizDto quizDto = quiz.ToQuizDto();
                    return Result<QuizDto>.Success(quizDto);
                }
                return Result<QuizDto>.Failure("module notfound");
            }
            catch (Exception ex)
            {

                return Result<QuizDto>.Failure(ex.Message);

            }

        }

        public async Task<Result<double>> GetTestNiveauScore(string studentId, int moduleId)
        {
            double note = await apiDbContext.testNiveaus
            .Where(x => x.ModuleId == moduleId && x.StudentId == studentId)
            .Select(x => x.Note)
            .FirstOrDefaultAsync();
            if (note == null)
            {
                return Result<double>.Failure("Not found");
            }
            return Result<double>.Success(note);
        }

        public async Task<Result<TestNiveau>> RegisterTestNiveauResult(TestNiveauResultDto testNiveauResultDto)
        {
            try
            {
                if (await apiDbContext.modules.AnyAsync(x => x.Id == testNiveauResultDto.ModuleId))
                {
                    if (await apiDbContext.testNiveaus.AnyAsync(x => x.ModuleId == testNiveauResultDto.ModuleId && x.StudentId == testNiveauResultDto.StudentId))
                    {
                        TestNiveau? testNiveau = await apiDbContext.testNiveaus.FirstOrDefaultAsync(x => x.ModuleId == testNiveauResultDto.ModuleId && x.StudentId == testNiveauResultDto.StudentId);
                        testNiveau.Note = testNiveauResultDto.Note;
                        await apiDbContext.SaveChangesAsync();
                        return Result<TestNiveau>.Success(testNiveau);
                    }
                    else
                    {
                        TestNiveau testNiveau = new TestNiveau()
                        {
                            ModuleId = testNiveauResultDto.ModuleId,
                            StudentId = testNiveauResultDto.StudentId,
                            Note = testNiveauResultDto.Note,
                        };
                        await apiDbContext.testNiveaus.AddAsync(testNiveau);
                        await apiDbContext.SaveChangesAsync();
                        return Result<TestNiveau>.Success(testNiveau);
                    }
                }
                return Result<TestNiveau>.Failure("module notfound");
            }
            catch (Exception ex)
            {

                return Result<TestNiveau>.Failure(ex.Message);
            }
        }
    }
}