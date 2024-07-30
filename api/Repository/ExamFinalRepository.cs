using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.ExamFinal;
using api.extensions;
using api.generique;
using api.interfaces;
using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ExamFinalRepository : IExamFinalRepository
    {
        private readonly apiDbContext apiDbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ExamFinalRepository(apiDbContext apiDbContext, IWebHostEnvironment webHostEnvironment)
        {
            this.apiDbContext = apiDbContext;
            this.webHostEnvironment = webHostEnvironment;
        }
        public async Task<Result<ExamFinal>> CreateExamFinal(CreateExamFinalDto createExamFinalDto)
        {
            Result<string> ennonceresult = await createExamFinalDto.Ennonce.UploadControle(webHostEnvironment);
            Result<string> solutionresult = await createExamFinalDto.Solution.UploadControleSolution(webHostEnvironment);
            if (ennonceresult.IsSuccess && solutionresult.IsSuccess)
            {
                ExamFinal examFinal = new ExamFinal()
                {

                    Nom = createExamFinalDto.Nom,
                    Ennonce = ennonceresult.Value,
                    Solution = solutionresult.Value,
                    Status = createExamFinalDto.Status,

                };
                await apiDbContext.AddAsync(examFinal);
                await apiDbContext.SaveChangesAsync();
                Module? module = await apiDbContext.modules.FirstOrDefaultAsync(x => x.Id == createExamFinalDto.ModuleId);
                if (module == null)
                {
                    return Result<ExamFinal>.Failure("modulenotfound");
                }
                module.ExamFinalId = examFinal.Id;
                await apiDbContext.SaveChangesAsync();
                return Result<ExamFinal>.Success(examFinal);

            }
            return Result<ExamFinal>.Failure("error in files upload");

        }

        public Task<Result<ExamFinal>> UpdateExamFinalEnnonce(UpdateExamFinalDto updateExamFinalDto)
        {
            throw new NotImplementedException();
        }

        public Task<Result<ExamFinal>> UpdateExamFinalSolution(UpdateExamFinalDto updateExamFinalDto)
        {
            throw new NotImplementedException();
        }
    }
}