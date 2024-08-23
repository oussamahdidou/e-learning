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
        private readonly IBlobStorageService blobStorageService;
        public ExamFinalRepository(IBlobStorageService blobStorageService, apiDbContext apiDbContext, IWebHostEnvironment webHostEnvironment)
        {
            this.apiDbContext = apiDbContext;
            this.webHostEnvironment = webHostEnvironment;
            this.blobStorageService = blobStorageService;
        }

        public async Task<Result<ExamFinal>> getExamFinalById(int examId)
        {
            ExamFinal? examFinal = await apiDbContext.examFinals.FindAsync(examId);

            if (examFinal == null)
            {
                return Result<ExamFinal>.Failure("Exam Not Found");
            }

            return Result<ExamFinal>.Success(examFinal);
        }
        public async Task<Result<ExamFinal>> CreateExamFinal(CreateExamFinalDto createExamFinalDto)
        {
            var examContainer = "controle-container";
            string enonceUrl = await blobStorageService.UploadFileAsync(createExamFinalDto.Ennonce.OpenReadStream(), examContainer, createExamFinalDto.Ennonce.FileName);
            string solutionUrl = await blobStorageService.UploadFileAsync(createExamFinalDto.Solution.OpenReadStream(), examContainer, createExamFinalDto.Solution.FileName);

            ExamFinal examFinal = new ExamFinal()
            {

                Nom = createExamFinalDto.Nom,
                Ennonce = enonceUrl,
                Solution = solutionUrl,
                Status = createExamFinalDto.Status,
                TeacherId = createExamFinalDto.TeacherId

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
                var examContainer = "controle-container";
                string enonceUrl = await blobStorageService.UploadFileAsync(updateExamFinalDto.File.OpenReadStream(), examContainer, updateExamFinalDto.File.FileName);

                await blobStorageService.DeleteFileAsync(examContainer, new Uri(examFinal.Ennonce).Segments.Last());

                examFinal.Ennonce = enonceUrl;
                await apiDbContext.SaveChangesAsync();
                return Result<ExamFinal>.Success(examFinal);



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
                var examContainer = "controle-container";
                string solutionUrl = await blobStorageService.UploadFileAsync(updateExamFinalDto.File.OpenReadStream(), examContainer, updateExamFinalDto.File.FileName);

                await blobStorageService.DeleteFileAsync(examContainer, new Uri(examFinal.Solution).Segments.Last());

                examFinal.Solution = solutionUrl;
                await apiDbContext.SaveChangesAsync();
                return Result<ExamFinal>.Success(examFinal);



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