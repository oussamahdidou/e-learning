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

        private Chapitre GenerateSasUrls(Chapitre chapitre)
        {
            chapitre.Synthese = _blobStorageService.GenerateSasToken(syntheseContainer, Path.GetFileName(new Uri(chapitre.Synthese).LocalPath), TimeSpan.FromMinutes(5));
            chapitre.Schema = _blobStorageService.GenerateSasToken(schemaContainer, Path.GetFileName(new Uri(chapitre.Schema).LocalPath), TimeSpan.FromMinutes(5));
            chapitre.VideoPath = _blobStorageService.GenerateSasToken(videoContainer, Path.GetFileName(new Uri(chapitre.VideoPath).LocalPath), TimeSpan.FromMinutes(5));

            return chapitre;
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
                return Result<Chapitre>.Success(GenerateSasUrls(chapitre));
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
                var videoUrl = "";
                var syntheseUrl = "";
                var schemaUrl = "";
                List<Paragraphe> studentcoursparagraphes = new List<Paragraphe>();
                List<Paragraphe> professeurscoursparagraphes = new List<Paragraphe>();

                if (createChapitreDto.CoursVideoFile != null)
                {

                    videoUrl = await _blobStorageService.UploadFileAsync(createChapitreDto.CoursVideoFile.OpenReadStream(), videoContainer, createChapitreDto.CoursVideoFile.FileName);
                    Console.WriteLine($"le fichier video {videoUrl}");
                }
                else
                {
                    videoUrl = createChapitreDto.CoursVideoLink;
                    Console.WriteLine($"le lien de la video {videoUrl}");

                }
                if (createChapitreDto.Synthese != null)
                {

                    syntheseUrl = await _blobStorageService.UploadFileAsync(createChapitreDto.Synthese.OpenReadStream(), syntheseContainer, createChapitreDto.Synthese.FileName);

                }
                if (createChapitreDto.Schema != null)
                {

                    schemaUrl = await _blobStorageService.UploadFileAsync(createChapitreDto.Schema.OpenReadStream(), schemaContainer, createChapitreDto.Schema.FileName);

                }
                foreach (var item in createChapitreDto.StudentCourseParagraphs)
                {
                    string url = await _blobStorageService.UploadFileAsync(item.OpenReadStream(), pdfContainer, item.FileName);
                    studentcoursparagraphes.Add(new Paragraphe() { Nom = $"Paragraphe {1}", Contenu = url });
                }
                foreach (var item in createChapitreDto.ProfessorCourseParagraphs)
                {
                    string url = await _blobStorageService.UploadFileAsync(item.OpenReadStream(), pdfContainer, item.FileName);
                    professeurscoursparagraphes.Add(new Paragraphe() { Nom = $"Paragraphe {1}", Contenu = url });
                }
                Chapitre chapitre = new Chapitre
                {
                    ChapitreNum = createChapitreDto.Number,
                    Nom = createChapitreDto.Nom,
                    ModuleId = createChapitreDto.ModuleId,
                    Premium = createChapitreDto.Premium,
                    VideoPath = videoUrl,
                    Schema = schemaUrl,
                    Synthese = syntheseUrl,
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

                // var containerName = "pdf-container";
                // var newPdfUrl = await _blobStorageService.UploadFileAsync(updateChapitrePdfDto.File.OpenReadStream(), containerName, updateChapitrePdfDto.File.FileName);

                // if (!string.IsNullOrEmpty(chapitre.CoursPdfPath))
                // {
                //     var oldPdfFileName = Path.GetFileName(new Uri(chapitre.CoursPdfPath).LocalPath);
                //     var deleteResult = await _blobStorageService.DeleteFileAsync(pdfContainer, oldPdfFileName);

                // }

                // chapitre.CoursPdfPath = newPdfUrl;
                // await apiDbContext.SaveChangesAsync();

                return Result<Chapitre>.Success(GenerateSasUrls(chapitre));
            }
            catch (Exception ex)
            {
                return Result<Chapitre>.Failure(ex.Message);
            }


            // var containerName = "pdf-container";

            // var newPdfUrl = await _blobStorageService.UploadFileAsync(updateChapitrePdfDto.File.OpenReadStream(), containerName, updateChapitrePdfDto.File.FileName);

            // var coursParagraphe = chapitre.StudentCoursParagraphes.FirstOrDefault(p => p.Paragraphe == updateChapitrePdfDto.ParagrapheUrl);

            // if (coursParagraphe == null)
            // {
            //     return Result<Chapitre>.Failure("Paragraphe not found");
            // }

            // if (!string.IsNullOrEmpty(coursParagraphe.Paragraphe))
            // {
            //     var oldPdfFileName = Path.GetFileName(new Uri(coursParagraphe.Paragraphe).LocalPath);
            //     await _blobStorageService.DeleteFileAsync(containerName, oldPdfFileName);
            // }

            // coursParagraphe.Paragraphe = newPdfUrl;

            // await apiDbContext.SaveChangesAsync();

            // return Result<Chapitre>.Success(GenerateSasUrls(chapitre));

        }

        public async Task<Result<Chapitre>> UpdateChapitreSchema(UpdateChapitreSchemaDto updateChapitreSchemaDto)
        {
            try
            {
                Chapitre? chapitre = await apiDbContext.chapitres.FirstOrDefaultAsync(x => x.Id == updateChapitreSchemaDto.Id);
                if (chapitre == null)
                {
                    return Result<Chapitre>.Failure("Chapitre not found");
                }


                var newSchemaUrl = await _blobStorageService.UploadFileAsync(updateChapitreSchemaDto.File.OpenReadStream(), schemaContainer, updateChapitreSchemaDto.File.FileName);

                if (!string.IsNullOrEmpty(chapitre.Schema))
                {
                    var oldSchemaFileName = Path.GetFileName(new Uri(chapitre.Schema).LocalPath);
                    var deleteResult = await _blobStorageService.DeleteFileAsync(schemaContainer, oldSchemaFileName);

                }

                chapitre.Schema = newSchemaUrl;
                await apiDbContext.SaveChangesAsync();

                return Result<Chapitre>.Success(GenerateSasUrls(chapitre));
            }
            catch (Exception ex)
            {
                return Result<Chapitre>.Failure(ex.Message);
            }
        }

        public async Task<Result<Chapitre>> UpdateChapitreSynthese(UpdateChapitreSyntheseDto updateChapitreSyntheseDto)
        {
            try
            {
                Chapitre? chapitre = await apiDbContext.chapitres.FirstOrDefaultAsync(x => x.Id == updateChapitreSyntheseDto.Id);
                if (chapitre == null)
                {
                    return Result<Chapitre>.Failure("Chapitre not found");
                }

                var newSyntheseUrl = await _blobStorageService.UploadFileAsync(updateChapitreSyntheseDto.File.OpenReadStream(), syntheseContainer, updateChapitreSyntheseDto.File.FileName);

                if (!string.IsNullOrEmpty(chapitre.Synthese))
                {
                    var oldSyntheseFileName = Path.GetFileName(new Uri(chapitre.Synthese).LocalPath);
                    var deleteResult = await _blobStorageService.DeleteFileAsync(syntheseContainer, oldSyntheseFileName);

                }

                chapitre.Synthese = newSyntheseUrl;
                await apiDbContext.SaveChangesAsync();

                return Result<Chapitre>.Success(GenerateSasUrls(chapitre));
            }
            catch (Exception ex)
            {
                return Result<Chapitre>.Failure(ex.Message);
            }
        }

        public async Task<Result<Chapitre>> UpdateChapitreVideo(UpdateChapitreVideoDto updateChapitreVideoDto)
        {
            try
            {
                Chapitre? chapitre = await apiDbContext.chapitres.FirstOrDefaultAsync(x => x.Id == updateChapitreVideoDto.Id);
                if (chapitre == null)
                {
                    return Result<Chapitre>.Failure("Chapitre not found");
                }

                var containerName = "video-container";
                var newVideoUrl = await _blobStorageService.UploadFileAsync(updateChapitreVideoDto.File.OpenReadStream(), containerName, updateChapitreVideoDto.File.FileName);

                if (!string.IsNullOrEmpty(chapitre.VideoPath))
                {

                    var oldVideoFileName = Path.GetFileName(new Uri(chapitre.VideoPath).LocalPath);
                    var deleteResult = await _blobStorageService.DeleteFileAsync(videoContainer, oldVideoFileName);

                }

                chapitre.VideoPath = newVideoUrl;
                await apiDbContext.SaveChangesAsync();

                return Result<Chapitre>.Success(GenerateSasUrls(chapitre));
            }
            catch (Exception ex)
            {
                return Result<Chapitre>.Failure(ex.Message);
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
                .Include(x => x.Cours).ThenInclude(x => x.Paragraphes)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (chapitre == null)
            {
                return false;
            }

            // Delete Video if exists
            if (!string.IsNullOrEmpty(chapitre.VideoPath))
            {
                var oldVideoFileName = Path.GetFileName(new Uri(chapitre.VideoPath).LocalPath);
                await _blobStorageService.DeleteFileAsync(videoContainer, oldVideoFileName);
            }

            // Delete Schema if exists
            if (!string.IsNullOrEmpty(chapitre.Schema))
            {
                var oldSchemaFileName = Path.GetFileName(new Uri(chapitre.Schema).LocalPath);
                await _blobStorageService.DeleteFileAsync(schemaContainer, oldSchemaFileName);
            }

            // Delete Synthese if exists
            if (!string.IsNullOrEmpty(chapitre.Synthese))
            {
                var oldSyntheseFileName = Path.GetFileName(new Uri(chapitre.Synthese).LocalPath);
                await _blobStorageService.DeleteFileAsync(syntheseContainer, oldSyntheseFileName);
            }

            // Delete Paragraphe Contenu if exists
            foreach (var cours in chapitre.Cours)
            {
                foreach (var paragraphe in cours.Paragraphes)
                {
                    if (!string.IsNullOrEmpty(paragraphe.Contenu))
                    {
                        var oldParagrapheFileName = Path.GetFileName(new Uri(paragraphe.Contenu).LocalPath);
                        await _blobStorageService.DeleteFileAsync(pdfContainer, oldParagrapheFileName);
                    }
                }
            }

            // If Chapitre has a Controle, delete Ennonce and Solution
            if (chapitre.Controle != null)
            {
                if (!string.IsNullOrEmpty(chapitre.Controle.Ennonce))
                {
                    var oldEnnonceFileName = Path.GetFileName(new Uri(chapitre.Controle.Ennonce).LocalPath);
                    await _blobStorageService.DeleteFileAsync(controleContainer, oldEnnonceFileName);
                }

                if (!string.IsNullOrEmpty(chapitre.Controle.Solution))
                {
                    var oldSolutionFileName = Path.GetFileName(new Uri(chapitre.Controle.Solution).LocalPath);
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
                Paragraphe paragraphe = new Paragraphe()
                {
                    Nom = "Paragraphe",
                    Contenu = url,
                    CoursId = createParagrapheDto.CoursId
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
                    var oldparagrapheFileName = Path.GetFileName(new Uri(paragraphe.Contenu).LocalPath);
                    var deleteResult = await _blobStorageService.DeleteFileAsync(pdfContainer, oldparagrapheFileName);

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

        public async Task<Result<Chapitre>> UpdateChapitreVideoLink(UpdateChapitreVideoLinkDto updateChapitreVideoLinkDto)
        {
            try
            {
                Chapitre? chapitre = await apiDbContext.chapitres.FirstOrDefaultAsync(x => x.Id == updateChapitreVideoLinkDto.chapitreId);
                if (chapitre != null)
                {
                    if (!string.IsNullOrEmpty(chapitre.VideoPath))
                    {
                        var oldchapitreFileName = Path.GetFileName(new Uri(chapitre.VideoPath).LocalPath);
                        var deleteResult = await _blobStorageService.DeleteFileAsync(pdfContainer, oldchapitreFileName);

                    }

                    chapitre.VideoPath = updateChapitreVideoLinkDto.Link;
                    await apiDbContext.SaveChangesAsync();

                    return Result<Chapitre>.Success(chapitre);
                }
                return Result<Chapitre>.Failure("chapitre not found");
            }
            catch (System.Exception ex)
            {
                return Result<Chapitre>.Failure(ex.Message);

            }
        }
    }
}