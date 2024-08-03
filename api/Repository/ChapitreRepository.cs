using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Chapitre;
using api.extensions;
using api.generique;
using api.helpers;
using api.interfaces;
using api.Mappers;
using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ChapitreRepository : IChapitreRepository
    {
        private readonly apiDbContext apiDbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ChapitreRepository(apiDbContext apiDbContext, IWebHostEnvironment webHostEnvironment)
        {
            this.apiDbContext = apiDbContext;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<Result<Chapitre>> Approuver(int id)
        {
            try
            {
                Chapitre? chapitre = await apiDbContext.chapitres.FirstOrDefaultAsync(x => x.Id == id);
                if (chapitre == null)
                {
                    return Result<Chapitre>.Failure("Chapitre not found");
                }
                chapitre.Statue = ObjectStatus.Approuver;
                await apiDbContext.SaveChangesAsync();
                return Result<Chapitre>.Success(chapitre);
            }
            catch (System.Exception ex)
            {

                return Result<Chapitre>.Failure(ex.Message);

            }
        }

        public async Task<Result<Chapitre>> CreateChapitre(CreateChapitreDto createChapitreDto)
        {
            try
            {
                Result<string> syntheseresult = await createChapitreDto.Synthese.UploadSynthese(webHostEnvironment);
                Result<string> schemaresult = await createChapitreDto.Schema.UploadSchema(webHostEnvironment);
                Result<string> resultcoursPdf = await createChapitreDto.CoursPdf.UploadCoursPdf(webHostEnvironment);
                Result<string> resultvideo = await createChapitreDto.Video.UploadVideo(webHostEnvironment);

                if (syntheseresult.IsSuccess &&
                    schemaresult.IsSuccess &&
                    resultcoursPdf.IsSuccess &&
                    resultvideo.IsSuccess)
                {

                    Chapitre chapitre = new Chapitre()
                    {
                        ChapitreNum = createChapitreDto.ChapitreNum,
                        Nom = createChapitreDto.Nom,
                        ModuleId = createChapitreDto.ModuleId,
                        Premium = createChapitreDto.Premium,
                        CoursPdfPath = resultcoursPdf.Value,
                        VideoPath = resultvideo.Value,
                        Schema = schemaresult.Value,
                        Synthese = syntheseresult.Value,
                        Statue = createChapitreDto.Statue,
                        QuizId = createChapitreDto.QuizId,

                    };
                    await apiDbContext.chapitres.AddAsync(chapitre);
                    await apiDbContext.SaveChangesAsync();
                    return Result<Chapitre>.Success(chapitre);


                }
                return Result<Chapitre>.Failure(syntheseresult.Error + schemaresult.Error + resultcoursPdf.Error + resultvideo.Error);
            }
            catch (System.Exception ex)
            {

                return Result<Chapitre>.Failure(ex.Message);
            }

            /*_context.chapitres.Remove(chapitre);
             await _context.SaveChangesAsync();
             return Result.Success();*/
        }

        public async Task<Result<Chapitre>> GetChapitreById(int id)
        {
            try
            {
                Chapitre? chapitre = await apiDbContext.chapitres.FirstOrDefaultAsync(x => x.Id == id);
                if (chapitre == null)
                {
                    return Result<Chapitre>.Failure("chapitre notfound");

                }
                return Result<Chapitre>.Success(chapitre);
            }
            catch (System.Exception ex)
            {

                return Result<Chapitre>.Failure(ex.Message);
            }
        }

        public async Task<Result<Chapitre>> Refuser(int id)
        {
            try
            {
                Chapitre? chapitre = await apiDbContext.chapitres.FirstOrDefaultAsync(x => x.Id == id);
                if (chapitre == null)
                {
                    return Result<Chapitre>.Failure("Chapitre not found");
                }
                chapitre.Statue = ObjectStatus.Pending;
                await apiDbContext.SaveChangesAsync();
                return Result<Chapitre>.Success(chapitre);
            }
            catch (System.Exception ex)
            {

                return Result<Chapitre>.Failure(ex.Message);

            }
        }

        public async Task<Result<Chapitre>> UpdateChapitrePdf(UpdateChapitrePdfDto updateChapitrePdfDto)
        {
            try
            {
                Chapitre? chapitre = await apiDbContext.chapitres.FirstOrDefaultAsync(x => x.Id == updateChapitrePdfDto.Id);
                if (chapitre == null)
                {
                    return Result<Chapitre>.Failure("chapitre not found");
                }
                Result<string> resultUpload = await updateChapitrePdfDto.File.UploadCoursPdf(webHostEnvironment);
                Result<string> resultDelete = chapitre.CoursPdfPath.DeleteFile();
                if (resultUpload.IsSuccess)
                {
                    if (resultDelete.IsSuccess)
                    {
                        chapitre.CoursPdfPath = resultUpload.Value;
                        await apiDbContext.SaveChangesAsync();
                        return Result<Chapitre>.Success(chapitre);
                    }
                    return Result<Chapitre>.Failure($"{resultDelete.Error}");
                }
                return Result<Chapitre>.Failure($"{resultUpload.Error}");

            }
            catch (System.Exception ex)
            {

                return Result<Chapitre>.Failure($"{ex.Message}");
            }
        }

        public async Task<Result<Chapitre>> UpdateChapitreSchema(UpdateChapitreSchemaDto updateChapitreSchemaDto)
        {
            try
            {
                Chapitre? chapitre = await apiDbContext.chapitres.FirstOrDefaultAsync(x => x.Id == updateChapitreSchemaDto.Id);
                if (chapitre == null)
                {
                    return Result<Chapitre>.Failure("chapitre not found");
                }
                Result<string> resultUpload = await updateChapitreSchemaDto.File.UploadSchema(webHostEnvironment);
                Result<string> resultDelete = chapitre.Schema.DeleteFile();
                if (resultUpload.IsSuccess)
                {
                    if (resultDelete.IsSuccess)
                    {
                        chapitre.Schema = resultUpload.Value;
                        await apiDbContext.SaveChangesAsync();
                        return Result<Chapitre>.Success(chapitre);
                    }
                    return Result<Chapitre>.Failure($"{resultDelete.Error}");
                }
                return Result<Chapitre>.Failure($"{resultUpload.Error}");
            }
            catch (System.Exception ex)
            {

                return Result<Chapitre>.Failure($"{ex.Message}");
            }

        }

        public async Task<Result<Chapitre>> UpdateChapitreSynthese(UpdateChapitreSyntheseDto updateChapitreSyntheseDto)
        {
            try
            {
                Chapitre? chapitre = await apiDbContext.chapitres.FirstOrDefaultAsync(x => x.Id == updateChapitreSyntheseDto.Id);
                if (chapitre == null)
                {
                    return Result<Chapitre>.Failure("chapitre not found");
                }
                Result<string> resultUpload = await updateChapitreSyntheseDto.File.UploadSynthese(webHostEnvironment);
                Result<string> resultDelete = chapitre.Synthese.DeleteFile();
                if (resultUpload.IsSuccess)
                {
                    if (resultDelete.IsSuccess)
                    {
                        chapitre.Synthese = resultUpload.Value;
                        await apiDbContext.SaveChangesAsync();
                        return Result<Chapitre>.Success(chapitre);
                    }
                    return Result<Chapitre>.Failure($"{resultDelete.Error}");
                }
                return Result<Chapitre>.Failure($"{resultUpload.Error}");
            }
            catch (System.Exception ex)
            {

                return Result<Chapitre>.Failure($"{ex.Message}");
            }
        }

        public async Task<Result<Chapitre>> UpdateChapitreVideo(UpdateChapitreVideoDto updateChapitreVideoDto)
        {
            try
            {
                Chapitre? chapitre = await apiDbContext.chapitres.FirstOrDefaultAsync(x => x.Id == updateChapitreVideoDto.Id);
                if (chapitre == null)
                {
                    return Result<Chapitre>.Failure("chapitre not found");
                }
                Result<string> resultUpload = await updateChapitreVideoDto.File.UploadVideo(webHostEnvironment);
                Result<string> resultDelete = chapitre.VideoPath.DeleteFile();
                if (resultUpload.IsSuccess)
                {
                    if (resultDelete.IsSuccess)
                    {
                        chapitre.VideoPath = resultUpload.Value;
                        await apiDbContext.SaveChangesAsync();
                        return Result<Chapitre>.Success(chapitre);
                    }
                    return Result<Chapitre>.Failure($"{resultDelete.Error}");
                }
                return Result<Chapitre>.Failure($"{resultUpload.Error}");
            }
            catch (System.Exception ex)
            {

                return Result<Chapitre>.Failure($"{ex.Message}");
            }
        }
    }
}