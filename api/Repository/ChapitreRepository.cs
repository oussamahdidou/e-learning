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
        private string pdfContainer = "pdf-container";
        private string videoContainer = "video-container";
        private string schemaContainer = "schema-container";
        private string syntheseContainer = "synthese-container";
        private string controleContainer = "controle-container";
        private readonly IBlobStorageService _blobStorageService;
        public ChapitreRepository(apiDbContext apiDbContext, IWebHostEnvironment webHostEnvironment, IBlobStorageService blobStorageService)
        {
            this.apiDbContext = apiDbContext;
            this.webHostEnvironment = webHostEnvironment;
            _blobStorageService = blobStorageService;
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

                List<Paragraphe> studentcoursparagraphes = new List<Paragraphe>();
                List<Paragraphe> professeurscoursparagraphes = new List<Paragraphe>();
                List<Video> videos = new List<Video>();
                List<Schema> schemas = new List<Schema>();
                List<Synthese> syntheses = new List<Synthese>();
                int index = 1;
                foreach (var item in createChapitreDto.Schemas)
                {
                    string url = await _blobStorageService.UploadFileAsync(item.OpenReadStream(), schemaContainer, item.FileName);
                    schemas.Add(new Schema() { Nom = $"Schema {index}", Link = url, ObjetNumber = index });
                    index++;
                }
                index = 1;
                foreach (var item in createChapitreDto.Syntheses)
                {
                    string url = await _blobStorageService.UploadFileAsync(item.OpenReadStream(), syntheseContainer, item.FileName);
                    syntheses.Add(new Synthese() { Nom = $"Synthese {index}", Link = url, ObjetNumber = index });
                    index++;
                }
                index = 1;
                foreach (var item in createChapitreDto.Videos)
                {
                    string url = await _blobStorageService.UploadFileAsync(item.OpenReadStream(), videoContainer, item.FileName);
                    videos.Add(new Video() { Nom = $"Video {index}", Link = url, ObjetNumber = index });
                    index++;
                }

                foreach (var item in createChapitreDto.VideosLink)
                {
                    videos.Add(new Video() { Nom = $"Video {index}", Link = item, ObjetNumber = index });
                    index++;
                }
                index = 1;
                foreach (var item in createChapitreDto.ProfessorCourseParagraphs)
                {
                    string url = await _blobStorageService.UploadFileAsync(item.OpenReadStream(), pdfContainer, item.FileName);
                    professeurscoursparagraphes.Add(new Paragraphe() { Nom = $"Paragraphe {index}", Contenu = url, ObjetNumber = index });
                    index++;
                }
                index = 1;
                foreach (var item in createChapitreDto.StudentCourseParagraphs)
                {
                    string url = await _blobStorageService.UploadFileAsync(item.OpenReadStream(), pdfContainer, item.FileName);
                    studentcoursparagraphes.Add(new Paragraphe() { Nom = $"Paragraphe {index}", Contenu = url, ObjetNumber = index });
                    index++;
                }
                Chapitre chapitre = new Chapitre
                {
                    ChapitreNum = createChapitreDto.Number,
                    Nom = createChapitreDto.Nom,
                    ModuleId = createChapitreDto.ModuleId,
                    Premium = createChapitreDto.Premium,
                    Videos = videos,
                    Schemas = schemas,
                    Syntheses = syntheses,
                    Statue = createChapitreDto.Statue,
                    QuizId = createChapitreDto.QuizId,
                    TeacherId = createChapitreDto.TeacherId,
                    Cours = new List<Cours>()
                    {
                        new Cours()
                            {
                                Titre = "Fichier Cours",
                                Paragraphes= professeurscoursparagraphes,
                                Type="Teacher"
                            },
                        new Cours()
                            {
                                Titre = "Fichier Cours",
                                Paragraphes= studentcoursparagraphes,
                                Type="Student"
                            }
                    }
                };



                List<Chapitre> chapitres = await apiDbContext.chapitres
                    .Where(x => x.ModuleId == createChapitreDto.ModuleId && x.ChapitreNum >= createChapitreDto.Number)
                    .ToListAsync();

                foreach (var item in chapitres)
                {
                    item.ChapitreNum++;
                }

                await apiDbContext.chapitres.AddAsync(chapitre);
                await apiDbContext.SaveChangesAsync();

                return Result<Chapitre>.Success(chapitre);
            }
            catch (Exception ex)
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
                    return Result<Chapitre>.Failure("Chapitre not found");
                }

                return Result<Chapitre>.Success(chapitre);
            }
            catch (Exception ex)
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
                chapitre.Statue = ObjectStatus.Denied;
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
                    return Result<Chapitre>.Failure("Chapitre not found");
                }


                return Result<Chapitre>.Success(chapitre);
            }
            catch (Exception ex)
            {
                return Result<Chapitre>.Failure(ex.Message);
            }




        }

        public async Task<Result<Schema>> UpdateChapitreSchema(UpdateChapitreSchemaDto updateChapitreSchemaDto)
        {
            try
            {
                Schema? chapitre = await apiDbContext.schemas.FirstOrDefaultAsync(x => x.Id == updateChapitreSchemaDto.Id);
                if (chapitre == null)
                {
                    return Result<Schema>.Failure("Chapitre not found");
                }


                var newSchemaUrl = await _blobStorageService.UploadFileAsync(updateChapitreSchemaDto.File.OpenReadStream(), schemaContainer, updateChapitreSchemaDto.File.FileName);

                if (!string.IsNullOrEmpty(chapitre.Link))
                {
                    var oldSchemaFileName = CloudinaryUrlHelper.ExtractFileName(chapitre.Link);
                    var deleteResult = await _blobStorageService.DeleteFileAsync(schemaContainer, oldSchemaFileName);

                }

                chapitre.Link = newSchemaUrl;
                await apiDbContext.SaveChangesAsync();

                return Result<Schema>.Success(chapitre);
            }
            catch (Exception ex)
            {
                return Result<Schema>.Failure(ex.Message);
            }
        }

        public async Task<Result<Synthese>> UpdateChapitreSynthese(UpdateChapitreSyntheseDto updateChapitreSyntheseDto)
        {
            try
            {
                Synthese? chapitre = await apiDbContext.syntheses.FirstOrDefaultAsync(x => x.Id == updateChapitreSyntheseDto.Id);
                if (chapitre == null)
                {
                    return Result<Synthese>.Failure("Chapitre not found");
                }

                var newSyntheseUrl = await _blobStorageService.UploadFileAsync(updateChapitreSyntheseDto.File.OpenReadStream(), syntheseContainer, updateChapitreSyntheseDto.File.FileName);

                if (!string.IsNullOrEmpty(chapitre.Link))
                {
                    var oldSyntheseFileName = CloudinaryUrlHelper.ExtractFileName(chapitre.Link);
                    var deleteResult = await _blobStorageService.DeleteFileAsync(syntheseContainer, oldSyntheseFileName);

                }

                chapitre.Link = newSyntheseUrl;
                await apiDbContext.SaveChangesAsync();

                return Result<Synthese>.Success(chapitre);
            }
            catch (Exception ex)
            {
                return Result<Synthese>.Failure(ex.Message);
            }
        }

        public async Task<Result<Video>> UpdateChapitreVideo(UpdateChapitreVideoDto updateChapitreVideoDto)
        {
            try
            {
                Video? chapitre = await apiDbContext.videos.FirstOrDefaultAsync(x => x.Id == updateChapitreVideoDto.Id);
                if (chapitre == null)
                {
                    return Result<Video>.Failure("Chapitre not found");
                }

                var containerName = "video-container";
                var newVideoUrl = await _blobStorageService.UploadFileAsync(updateChapitreVideoDto.File.OpenReadStream(), containerName, updateChapitreVideoDto.File.FileName);

                if (!string.IsNullOrEmpty(chapitre.Link))
                {

                    var oldVideoFileName = CloudinaryUrlHelper.ExtractFileName(chapitre.Link);
                    var deleteResult = await _blobStorageService.DeleteFileAsync(videoContainer, oldVideoFileName);

                }

                chapitre.Link = newVideoUrl;
                await apiDbContext.SaveChangesAsync();

                return Result<Video>.Success(chapitre);
            }
            catch (Exception ex)
            {
                return Result<Video>.Failure(ex.Message);
            }
        }

        public async Task<Result<Chapitre>> UpdateChapterName(UpdateChapitreNameDto updateChapitreNameDto)
        {
            Chapitre? chapitre = await apiDbContext.chapitres.FirstOrDefaultAsync(x => x.Id == updateChapitreNameDto.Id);
            if (chapitre == null)
            {
                return Result<Chapitre>.Failure("chapitre not found");
            }
            chapitre.Nom = updateChapitreNameDto.Nom;
            await apiDbContext.SaveChangesAsync();
            return Result<Chapitre>.Success(chapitre);
        }

        public async Task<bool> DeleteChapitre(int id)
        {
            // Retrieve the Chapitre including related data
            Chapitre? chapitre = await apiDbContext.chapitres
                .Include(x => x.CheckChapters)
                .Include(x => x.Videos)
                .Include(x => x.Syntheses)
                .Include(x => x.Schemas)
                .Include(x => x.Cours).ThenInclude(x => x.Paragraphes)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (chapitre == null)
            {
                return false;
            }

            foreach (var paragraphe in chapitre.Videos)
            {
                if (!string.IsNullOrEmpty(paragraphe.Link))
                {
                    await _blobStorageService.DeleteFileAsync(pdfContainer, CloudinaryUrlHelper.ExtractFileName(paragraphe.Link));
                }
            }
            foreach (var paragraphe in chapitre.Syntheses)
            {
                if (!string.IsNullOrEmpty(paragraphe.Link))
                {
                    await _blobStorageService.DeleteFileAsync(pdfContainer, CloudinaryUrlHelper.ExtractFileName(paragraphe.Link));
                }
            }
            foreach (var paragraphe in chapitre.Schemas)
            {
                if (!string.IsNullOrEmpty(paragraphe.Link))
                {
                    await _blobStorageService.DeleteFileAsync(pdfContainer, CloudinaryUrlHelper.ExtractFileName(paragraphe.Link));
                }
            }


            // Delete Paragraphe Contenu if exists
            foreach (var cours in chapitre.Cours)
            {
                foreach (var paragraphe in cours.Paragraphes)
                {
                    if (!string.IsNullOrEmpty(paragraphe.Contenu))
                    {
                        var oldParagrapheFileName = CloudinaryUrlHelper.ExtractFileName(paragraphe.Contenu);
                        await _blobStorageService.DeleteFileAsync(pdfContainer, oldParagrapheFileName);
                    }
                }
            }

            // If Chapitre has a Controle, delete Ennonce and Solution
            if (chapitre.Controle != null)
            {
                if (!string.IsNullOrEmpty(chapitre.Controle.Ennonce))
                {
                    var oldEnnonceFileName = CloudinaryUrlHelper.ExtractFileName(chapitre.Controle.Ennonce);
                    await _blobStorageService.DeleteFileAsync(controleContainer, oldEnnonceFileName);
                }

                if (!string.IsNullOrEmpty(chapitre.Controle.Solution))
                {
                    var oldSolutionFileName = CloudinaryUrlHelper.ExtractFileName(chapitre.Controle.Solution);
                    await _blobStorageService.DeleteFileAsync(controleContainer, oldSolutionFileName);
                }
            }

            // Remove the Chapitre from the database
            apiDbContext.chapitres.Remove(chapitre);
            await apiDbContext.SaveChangesAsync();

            // Check if Controle is orphaned (no Chapitres linked), and delete if so
            Controle? controle = await apiDbContext.controles.Include(x => x.Chapitres).FirstOrDefaultAsync(x => x.Chapitres.Count() == 0);
            if (controle != null)
            {
                apiDbContext.controles.Remove(controle);
                await apiDbContext.SaveChangesAsync();
            }

            return true;
        }


        public async Task<Result<Paragraphe>> CreateParagraphe(CreateParagrapheDto createParagrapheDto)
        {
            try
            {
                string url = await _blobStorageService.UploadFileAsync(createParagrapheDto.ParagrapheContenu.OpenReadStream(), pdfContainer, createParagrapheDto.ParagrapheContenu.FileName);
                var maxValue = await apiDbContext.paragraphes.Where(x => x.CoursId == createParagrapheDto.CoursId).MaxAsync(e => (int?)e.ObjetNumber) ?? 0;

                Paragraphe paragraphe = new Paragraphe()
                {
                    Nom = $"Paragraphe {maxValue + 1}",
                    Contenu = url,
                    CoursId = createParagrapheDto.CoursId,
                    Status = createParagrapheDto.Statue,
                    TeacherId = createParagrapheDto.TeacherId,
                    ObjetNumber = maxValue + 1
                };
                await apiDbContext.paragraphes.AddAsync(paragraphe);
                await apiDbContext.SaveChangesAsync();
                return Result<Paragraphe>.Success(paragraphe);
            }
            catch (System.Exception ex)
            {

                return Result<Paragraphe>.Failure(ex.Message);

            }
        }

        public async Task<Result<Paragraphe>> GetParagrapheByid(int id)
        {
            Paragraphe? paragraphe = await apiDbContext.paragraphes.FirstOrDefaultAsync(x => x.Id == id);
            if (paragraphe != null)
            {
                return Result<Paragraphe>.Success(paragraphe);
            }
            return Result<Paragraphe>.Failure("paragraphe not found");

        }

        public async Task<Result<Paragraphe>> UpdateParagraphe(UpdateParagrapheDto updateParagrapheDto)
        {
            try
            {
                Paragraphe? paragraphe = await apiDbContext.paragraphes.FirstOrDefaultAsync(x => x.Id == updateParagrapheDto.Id);
                if (paragraphe == null)
                {
                    return Result<Paragraphe>.Failure("Chapitre not found");
                }

                var newParagrapheUrl = await _blobStorageService.UploadFileAsync(updateParagrapheDto.File.OpenReadStream(), pdfContainer, updateParagrapheDto.File.FileName);

                if (!string.IsNullOrEmpty(paragraphe.Contenu))
                {
                    var oldParagrapheFileName = CloudinaryUrlHelper.ExtractFileName(paragraphe.Contenu);
                    var deleteResult = await _blobStorageService.DeleteFileAsync(pdfContainer, oldParagrapheFileName);

                }

                paragraphe.Contenu = newParagrapheUrl;
                await apiDbContext.SaveChangesAsync();

                return Result<Paragraphe>.Success(paragraphe);
            }
            catch (Exception ex)
            {
                return Result<Paragraphe>.Failure(ex.Message);
            }
        }

        public async Task<Result<Video>> UpdateChapitreVideoLink(UpdateChapitreVideoLinkDto updateChapitreVideoLinkDto)
        {
            try
            {
                Video? chapitre = await apiDbContext.videos.FirstOrDefaultAsync(x => x.Id == updateChapitreVideoLinkDto.chapitreId);
                if (chapitre != null)
                {
                    if (!string.IsNullOrEmpty(chapitre.Link))
                    {
                        var oldVideoFileName = CloudinaryUrlHelper.ExtractFileName(chapitre.Link);
                        var deleteResult = await _blobStorageService.DeleteFileAsync(videoContainer, oldVideoFileName);

                    }

                    chapitre.Link = updateChapitreVideoLinkDto.Link;
                    await apiDbContext.SaveChangesAsync();

                    return Result<Video>.Success(chapitre);
                }
                return Result<Video>.Failure("chapitre not found");
            }
            catch (System.Exception ex)
            {
                return Result<Video>.Failure(ex.Message);

            }
        }

        public async Task<Result<Video>> GetVideoById(int Id)
        {
            try
            {
                Video? video = await apiDbContext.videos.FirstOrDefaultAsync(x => x.Id == Id);
                if (video == null)
                {
                    return Result<Video>.Failure("video not found");
                }
                return Result<Video>.Success(video);
            }
            catch (System.Exception ex)
            {

                return Result<Video>.Failure(ex.Message);
            }


        }

        public async Task<Result<Synthese>> GetSyntheseById(int Id)
        {
            try
            {
                Synthese? synthese = await apiDbContext.syntheses.FirstOrDefaultAsync(x => x.Id == Id);
                if (synthese == null)
                {
                    return Result<Synthese>.Failure("video not found");
                }
                return Result<Synthese>.Success(synthese);
            }
            catch (System.Exception ex)
            {

                return Result<Synthese>.Failure(ex.Message);
            }
        }

        public async Task<Result<Schema>> GetSchemaById(int Id)
        {
            try
            {
                Schema? schema = await apiDbContext.schemas.FirstOrDefaultAsync(x => x.Id == Id);
                if (schema == null)
                {
                    return Result<Schema>.Failure("video not found");
                }
                return Result<Schema>.Success(schema);
            }
            catch (System.Exception ex)
            {

                return Result<Schema>.Failure(ex.Message);
            }
        }

        public async Task<Result<Video>> AddVideo(UpdateChapitreVideoDto updateChapitreVideoDto)
        {
            try
            {
                string url = await _blobStorageService.UploadFileAsync(updateChapitreVideoDto.File.OpenReadStream(), videoContainer, updateChapitreVideoDto.File.FileName);
                var maxValue = await apiDbContext.videos.Where(x => x.ChapitreId == updateChapitreVideoDto.Id).MaxAsync(e => (int?)e.ObjetNumber) ?? 0;

                // Set the next value
                Video video = new Video()
                {
                    Nom = $"Video {maxValue + 1}",
                    Link = url,
                    ChapitreId = updateChapitreVideoDto.Id,
                    Status = updateChapitreVideoDto.Statue,
                    TeacherId = updateChapitreVideoDto.TeacherId,
                    ObjetNumber = maxValue + 1

                };
                await apiDbContext.videos.AddAsync(video);
                await apiDbContext.SaveChangesAsync();
                return Result<Video>.Success(video);
            }
            catch (System.Exception ex)
            {

                return Result<Video>.Failure(ex.Message);

            }
        }

        public async Task<Result<Video>> AddVideoLink(UpdateChapitreVideoLinkDto updateChapitreVideoLinkDto)
        {
            try
            {
                var maxValue = await apiDbContext.videos.Where(x => x.ChapitreId == updateChapitreVideoLinkDto.chapitreId).MaxAsync(e => (int?)e.ObjetNumber) ?? 0;

                Video video = new Video()
                {
                    Nom = $"Video {maxValue + 1}",
                    Link = updateChapitreVideoLinkDto.Link,
                    ChapitreId = updateChapitreVideoLinkDto.chapitreId,
                    Status = updateChapitreVideoLinkDto.Statue,
                    TeacherId = updateChapitreVideoLinkDto.TeacherId,
                    ObjetNumber = maxValue + 1
                };
                await apiDbContext.videos.AddAsync(video);
                await apiDbContext.SaveChangesAsync();
                return Result<Video>.Success(video);
            }
            catch (System.Exception ex)
            {

                return Result<Video>.Failure(ex.Message);

            }
        }

        public async Task<Result<Schema>> AddChapitreSchema(UpdateChapitreSchemaDto updateChapitreSchemaDto)
        {
            try
            {
                var maxValue = await apiDbContext.schemas.Where(x => x.ChapitreId == updateChapitreSchemaDto.Id).MaxAsync(e => (int?)e.ObjetNumber) ?? 0;

                string url = await _blobStorageService.UploadFileAsync(updateChapitreSchemaDto.File.OpenReadStream(), schemaContainer, updateChapitreSchemaDto.File.FileName);
                Schema schema = new Schema()
                {
                    Nom = $"Schema {maxValue + 1}",
                    Link = url,
                    ChapitreId = updateChapitreSchemaDto.Id,
                    Status = updateChapitreSchemaDto.Statue,
                    TeacherId = updateChapitreSchemaDto.TeacherId,
                    ObjetNumber = maxValue + 1
                };
                await apiDbContext.schemas.AddAsync(schema);
                await apiDbContext.SaveChangesAsync();
                return Result<Schema>.Success(schema);
            }
            catch (System.Exception ex)
            {

                return Result<Schema>.Failure(ex.Message);

            }
        }

        public async Task<Result<Synthese>> AddChapitreSynthese(UpdateChapitreSyntheseDto updateChapitreSyntheseDto)
        {
            try
            {
                var maxValue = await apiDbContext.syntheses.Where(x => x.ChapitreId == updateChapitreSyntheseDto.Id).MaxAsync(e => (int?)e.ObjetNumber) ?? 0;

                string url = await _blobStorageService.UploadFileAsync(updateChapitreSyntheseDto.File.OpenReadStream(), syntheseContainer, updateChapitreSyntheseDto.File.FileName);
                Synthese synthese = new Synthese()
                {
                    Nom = $"Synthese {maxValue + 1}",
                    Link = url,
                    ChapitreId = updateChapitreSyntheseDto.Id,
                    Status = updateChapitreSyntheseDto.Statue,
                    TeacherId = updateChapitreSyntheseDto.TeacherId,
                    ObjetNumber = maxValue + 1
                };
                await apiDbContext.syntheses.AddAsync(synthese);
                await apiDbContext.SaveChangesAsync();
                return Result<Synthese>.Success(synthese);
            }
            catch (System.Exception ex)
            {

                return Result<Synthese>.Failure(ex.Message);

            }
        }

        public async Task<bool> DeleteVideo(int id)
        {
            try
            {
                Video? video = await apiDbContext.videos.FirstOrDefaultAsync(x => x.Id == id);
                if (video == null)
                {
                    return false;
                }
                else
                {
                    if (!string.IsNullOrEmpty(video.Link))
                    {
                        await _blobStorageService.DeleteFileAsync(videoContainer, CloudinaryUrlHelper.ExtractFileName(video.Link));
                    }
                    apiDbContext.videos.Remove(video);
                    await apiDbContext.SaveChangesAsync();
                    return true;

                }
            }
            catch (System.Exception)
            {

                return true;
            }
        }

        public async Task<bool> DeleteSchema(int id)
        {
            try
            {
                Schema? schema = await apiDbContext.schemas.FirstOrDefaultAsync(x => x.Id == id);
                if (schema == null)
                {
                    return false;
                }
                else
                {
                    if (!string.IsNullOrEmpty(schema.Link))
                    {
                        await _blobStorageService.DeleteFileAsync(schemaContainer, CloudinaryUrlHelper.ExtractFileName(schema.Link));
                    }
                    apiDbContext.schemas.Remove(schema);
                    await apiDbContext.SaveChangesAsync();
                    return true;

                }
            }
            catch (System.Exception)
            {

                return true;
            }
        }

        public async Task<bool> DeleteSynthese(int id)
        {
            try
            {
                Synthese? synthese = await apiDbContext.syntheses.FirstOrDefaultAsync(x => x.Id == id);
                if (synthese == null)
                {
                    return false;
                }
                else
                {
                    if (!string.IsNullOrEmpty(synthese.Link))
                    {
                        await _blobStorageService.DeleteFileAsync(syntheseContainer, CloudinaryUrlHelper.ExtractFileName(synthese.Link));
                    }
                    apiDbContext.syntheses.Remove(synthese);
                    await apiDbContext.SaveChangesAsync();
                    return true;

                }
            }
            catch (System.Exception)
            {

                return true;
            }
        }

        public async Task<Result<Paragraphe>> UpdateParagrapheName(UpdateChapitreNameDto updateChapitreNameDto)
        {
            Paragraphe? chapitre = await apiDbContext.paragraphes.FirstOrDefaultAsync(x => x.Id == updateChapitreNameDto.Id);
            if (chapitre == null)
            {
                return Result<Paragraphe>.Failure("chapitre not found");
            }
            chapitre.Nom = updateChapitreNameDto.Nom;
            await apiDbContext.SaveChangesAsync();
            return Result<Paragraphe>.Success(chapitre);
        }

        public async Task<Result<Video>> UpdateVideoName(UpdateChapitreNameDto updateChapitreNameDto)
        {
            Video? chapitre = await apiDbContext.videos.FirstOrDefaultAsync(x => x.Id == updateChapitreNameDto.Id);
            if (chapitre == null)
            {
                return Result<Video>.Failure("chapitre not found");
            }
            chapitre.Nom = updateChapitreNameDto.Nom;
            await apiDbContext.SaveChangesAsync();
            return Result<Video>.Success(chapitre);
        }

        public async Task<Result<Schema>> UpdateSchemaName(UpdateChapitreNameDto updateChapitreNameDto)
        {
            Schema? chapitre = await apiDbContext.schemas.FirstOrDefaultAsync(x => x.Id == updateChapitreNameDto.Id);
            if (chapitre == null)
            {
                return Result<Schema>.Failure("chapitre not found");
            }
            chapitre.Nom = updateChapitreNameDto.Nom;
            await apiDbContext.SaveChangesAsync();
            return Result<Schema>.Success(chapitre);
        }

        public async Task<Result<Synthese>> UpdateSyntheseName(UpdateChapitreNameDto updateChapitreNameDto)
        {
            Synthese? chapitre = await apiDbContext.syntheses.FirstOrDefaultAsync(x => x.Id == updateChapitreNameDto.Id);
            if (chapitre == null)
            {
                return Result<Synthese>.Failure("chapitre not found");
            }
            chapitre.Nom = updateChapitreNameDto.Nom;
            await apiDbContext.SaveChangesAsync();
            return Result<Synthese>.Success(chapitre);
        }

        public async Task<Result<Chapitre>> UpdateChapitreNumero(UpdateChapitreNumeroDto updateChapitreNumeroDto)
        {
            // Fetch the targeted chapitre
            var chapitreToUpdate = await apiDbContext.chapitres.FirstOrDefaultAsync(c => c.Id == updateChapitreNumeroDto.ChapitreId);
            if (chapitreToUpdate == null)
            {
                return Result<Chapitre>.Failure("chapitre notfound");
            }
            chapitreToUpdate.ChapitreNum = updateChapitreNumeroDto.ChapitreNumero;
            await apiDbContext.SaveChangesAsync();
            return Result<Chapitre>.Success(chapitreToUpdate);
        }
        public async Task<Result<Paragraphe>> ApprouverParagraphe(int id)
        {
            Paragraphe? paragraphe = await apiDbContext.paragraphes.FirstOrDefaultAsync(x => x.Id == id);
            if (paragraphe != null)
            {
                paragraphe.Status = ObjectStatus.Approuver;
                await apiDbContext.SaveChangesAsync();
                return Result<Paragraphe>.Success(paragraphe);
            }
            return Result<Paragraphe>.Failure("paragraphe notfound");
        }
        public async Task<Result<Paragraphe>> RefuserParagraphe(int id)
        {
            Paragraphe? paragraphe = await apiDbContext.paragraphes.FirstOrDefaultAsync(x => x.Id == id);
            if (paragraphe != null)
            {
                paragraphe.Status = ObjectStatus.Denied;
                await apiDbContext.SaveChangesAsync();
                return Result<Paragraphe>.Success(paragraphe);
            }
            return Result<Paragraphe>.Failure("paragraphe notfound");

        }

        public async Task<Result<Video>> ApprouverVideo(int id)
        {
            Video? video = await apiDbContext.videos.FirstOrDefaultAsync(x => x.Id == id);
            if (video != null)
            {
                video.Status = ObjectStatus.Approuver;
                await apiDbContext.SaveChangesAsync();
                return Result<Video>.Success(video);
            }
            return Result<Video>.Failure("video notfound");
        }

        public async Task<Result<Video>> RefuserVideo(int id)
        {
            Video? video = await apiDbContext.videos.FirstOrDefaultAsync(x => x.Id == id);
            if (video != null)
            {
                video.Status = ObjectStatus.Denied;
                await apiDbContext.SaveChangesAsync();
                return Result<Video>.Success(video);
            }
            return Result<Video>.Failure("video notfound");
        }

        public async Task<Result<Synthese>> ApprouverSynthese(int id)
        {
            Synthese? synthese = await apiDbContext.syntheses.FirstOrDefaultAsync(x => x.Id == id);
            if (synthese != null)
            {
                synthese.Status = ObjectStatus.Approuver;
                await apiDbContext.SaveChangesAsync();
                return Result<Synthese>.Success(synthese);
            }
            return Result<Synthese>.Failure("synthese notfound");
        }

        public async Task<Result<Synthese>> RefuserSynthese(int id)
        {
            Synthese? synthese = await apiDbContext.syntheses.FirstOrDefaultAsync(x => x.Id == id);
            if (synthese != null)
            {
                synthese.Status = ObjectStatus.Denied;
                await apiDbContext.SaveChangesAsync();
                return Result<Synthese>.Success(synthese);
            }
            return Result<Synthese>.Failure("synthese notfound");
        }

        public async Task<Result<Schema>> ApprouverSchema(int id)
        {
            Schema? schema = await apiDbContext.schemas.FirstOrDefaultAsync(x => x.Id == id);
            if (schema != null)
            {
                schema.Status = ObjectStatus.Approuver;
                await apiDbContext.SaveChangesAsync();
                return Result<Schema>.Success(schema);
            }
            return Result<Schema>.Failure("Schema notfound");
        }

        public async Task<Result<Schema>> RefuserSchema(int id)
        {
            Schema? schema = await apiDbContext.schemas.FirstOrDefaultAsync(x => x.Id == id);
            if (schema != null)
            {
                schema.Status = ObjectStatus.Denied;
                await apiDbContext.SaveChangesAsync();
                return Result<Schema>.Success(schema);
            }
            return Result<Schema>.Failure("Schema notfound");
        }

        public async Task<Result<Paragraphe>> UpdateParagrapheNumero(UpdateChapitreNumeroDto updateChapitreNumeroDto)
        {
            // Fetch the targeted chapitre
            var chapitreToUpdate = await apiDbContext.paragraphes.FirstOrDefaultAsync(c => c.Id == updateChapitreNumeroDto.ChapitreId);
            if (chapitreToUpdate == null)
            {
                return Result<Paragraphe>.Failure("chapitre notfound");
            }
            chapitreToUpdate.ObjetNumber = updateChapitreNumeroDto.ChapitreNumero;
            await apiDbContext.SaveChangesAsync();
            return Result<Paragraphe>.Success(chapitreToUpdate);
        }

        public async Task<Result<Video>> UpdateVideoNumero(UpdateChapitreNumeroDto updateChapitreNumeroDto)
        {
            var chapitreToUpdate = await apiDbContext.videos.FirstOrDefaultAsync(c => c.Id == updateChapitreNumeroDto.ChapitreId);
            if (chapitreToUpdate == null)
            {
                return Result<Video>.Failure("chapitre notfound");
            }
            chapitreToUpdate.ObjetNumber = updateChapitreNumeroDto.ChapitreNumero;
            await apiDbContext.SaveChangesAsync();
            return Result<Video>.Success(chapitreToUpdate);
        }

        public async Task<Result<Schema>> UpdateSchemaNumero(UpdateChapitreNumeroDto updateChapitreNumeroDto)
        {
            var chapitreToUpdate = await apiDbContext.schemas.FirstOrDefaultAsync(c => c.Id == updateChapitreNumeroDto.ChapitreId);
            if (chapitreToUpdate == null)
            {
                return Result<Schema>.Failure("chapitre notfound");
            }
            chapitreToUpdate.ObjetNumber = updateChapitreNumeroDto.ChapitreNumero;
            await apiDbContext.SaveChangesAsync();
            return Result<Schema>.Success(chapitreToUpdate);
        }

        public async Task<Result<Synthese>> UpdateSyntheseNumero(UpdateChapitreNumeroDto updateChapitreNumeroDto)
        {
            var chapitreToUpdate = await apiDbContext.syntheses.FirstOrDefaultAsync(c => c.Id == updateChapitreNumeroDto.ChapitreId);
            if (chapitreToUpdate == null)
            {
                return Result<Synthese>.Failure("chapitre notfound");
            }
            chapitreToUpdate.ObjetNumber = updateChapitreNumeroDto.ChapitreNumero;
            await apiDbContext.SaveChangesAsync();
            return Result<Synthese>.Success(chapitreToUpdate);
        }
    }
}