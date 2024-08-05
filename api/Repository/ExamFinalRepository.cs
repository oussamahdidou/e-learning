using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.ExamFinal;
using api.extensions;
using api.generique;
using api.helpers;
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

        public async Task<Result<ExamFinal>> getExamFinalById(int examId)
        {
            ExamFinal? examFinal = await apiDbContext.examFinals.FindAsync(examId);

            if(examFinal == null)
            {
                return Result<ExamFinal>.Failure("Exam Not Found");
            }

            return Result<ExamFinal>.Success(examFinal);
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
        public async Task<Result<ExamFinal>> GetExamFinaleByModule(int Id)
        {
            Module? module = await apiDbContext.modules.Include(x => x.ExamFinal).FirstOrDefaultAsync(x => x.Id == Id);
            if (module == null)
            {
                return Result<ExamFinal>.Failure("module don`t exist");
            }
            if (module.ExamFinal == null)
            {
                return Result<ExamFinal>.Failure("exam don`t exist");

            }
            return Result<ExamFinal>.Success(module.ExamFinal);
        }
        public async Task<Result<ExamFinal>> UpdateExamFinalEnnonce(UpdateExamFinalDto updateExamFinalDto)
        {
            try
            {
                ExamFinal? examFinal = await apiDbContext.examFinals.FirstOrDefaultAsync(x => x.Id == updateExamFinalDto.Id);
                if (examFinal == null)
                {
                    return Result<ExamFinal>.Failure("exam don`t exist");

                }
                Result<string> resultUpload = await updateExamFinalDto.File.UploadControle(webHostEnvironment);
                if (resultUpload.IsSuccess)
                {
                    Result<string> resultDelete = examFinal.Ennonce.DeleteFile();
                    if (resultDelete.IsSuccess)
                    {
                        examFinal.Ennonce = resultUpload.Value;
                        await apiDbContext.SaveChangesAsync();
                        return Result<ExamFinal>.Success(examFinal);
                    }
                    return Result<ExamFinal>.Failure("error in file delete");
                }
                return Result<ExamFinal>.Failure("error in file upload");
            }
            catch (System.Exception ex)
            {

                return Result<ExamFinal>.Failure(ex.Message);
            }
        }
        public async Task<Result<ExamFinal>> UpdateExamFinalSolution(UpdateExamFinalDto updateExamFinalDto)
        {
            try
            {
                ExamFinal? examFinal = await apiDbContext.examFinals.FirstOrDefaultAsync(x => x.Id == updateExamFinalDto.Id);
                if (examFinal == null)
                {
                    return Result<ExamFinal>.Failure("exam don`t exist");

                }
                Result<string> resultUpload = await updateExamFinalDto.File.UploadControleSolution(webHostEnvironment);
                if (resultUpload.IsSuccess)
                {
                    Result<string> resultDelete = examFinal.Solution.DeleteFile();
                    if (resultDelete.IsSuccess)
                    {
                        examFinal.Solution = resultUpload.Value;
                        await apiDbContext.SaveChangesAsync();
                        return Result<ExamFinal>.Success(examFinal);
                    }
                    return Result<ExamFinal>.Failure("error in file delete");
                }
                return Result<ExamFinal>.Failure("error in file upload");
            }
            catch (System.Exception ex)
            {

                return Result<ExamFinal>.Failure(ex.Message);
            }
        }
        public async Task<Result<ExamFinal>> Approuver(int id)
        {
            try
            {
                ExamFinal? examFinal = await apiDbContext.examFinals.FirstOrDefaultAsync(x => x.Id == id);
                if (examFinal == null)
                {
                    return Result<ExamFinal>.Failure("exam not found");
                }
                examFinal.Status = ObjectStatus.Approuver;
                await apiDbContext.SaveChangesAsync();
                return Result<ExamFinal>.Success(examFinal);
            }
            catch (System.Exception ex)
            {

                return Result<ExamFinal>.Failure(ex.Message);

            }
        }
        public async Task<Result<ExamFinal>> Refuser(int id)
        {
            try
            {
                ExamFinal? examFinal = await apiDbContext.examFinals.FirstOrDefaultAsync(x => x.Id == id);
                if (examFinal == null)
                {
                    return Result<ExamFinal>.Failure("exam not found");
                }
                examFinal.Status = ObjectStatus.Denied;
                await apiDbContext.SaveChangesAsync();
                return Result<ExamFinal>.Success(examFinal);
            }
            catch (System.Exception ex)
            {

                return Result<ExamFinal>.Failure(ex.Message);

            }
        }

        public async Task<bool> DeleteExam(int id)
        {
            try
            {
                Module? module = await apiDbContext.modules.FirstOrDefaultAsync(x => x.ExamFinalId == id);
                ExamFinal? examFinal = await apiDbContext.examFinals.FirstOrDefaultAsync(x => x.Id == id);
                if (module == null || examFinal == null)
                {
                    return false;
                }
                module.ExamFinalId = null;
                apiDbContext.examFinals.Remove(examFinal);
                await apiDbContext.SaveChangesAsync();
                return true;
            }
            catch (System.Exception)
            {

                return false;
            }
        }
    }
}