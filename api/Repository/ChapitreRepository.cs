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
                        Statue = ObjectStatus.Pending,
                        QuizId = createChapitreDto.QuizId,

                    };
                    await apiDbContext.chapitres.AddAsync(chapitre);
                    await apiDbContext.SaveChangesAsync();
                    return Result<Chapitre>.Success(chapitre);


                }
                return Result<Chapitre>.Failure("hasn`t been uploaded");
            }
            catch (System.Exception ex)
            {

                return Result<Chapitre>.Failure(ex.Message);
            }
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

    }
}