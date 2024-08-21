using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Control;
using api.Dtos.Controle;
using api.extensions;
using api.generique;
using api.helpers;
using api.interfaces;
using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ControleRepository : IControleRepository
    {
        private readonly apiDbContext apiDbContext;
        private readonly IWebHostEnvironment webHostEnvironment;

        private readonly IBlobStorageService _blobStorageService;
        public ControleRepository(apiDbContext apiDbContext, IWebHostEnvironment webHostEnvironment, IBlobStorageService blobStorageService)
        {
            this.apiDbContext = apiDbContext;
            this.webHostEnvironment = webHostEnvironment;
            _blobStorageService = blobStorageService;
        }

        public async Task<Result<Controle>> Approuver(int id)
        {
            try
            {
                Controle? controle = await apiDbContext.controles.FirstOrDefaultAsync(x => x.Id == id);
                if (controle == null)
                {
                    return Result<Controle>.Failure("Controle not found");
                }
                controle.Status = ObjectStatus.Approuver;
                await apiDbContext.SaveChangesAsync();
                return Result<Controle>.Success(controle);
            }
            catch (System.Exception ex)
            {

                return Result<Controle>.Failure(ex.Message);

            }
        }

        public async Task<Result<Controle>> CreateControle(CreateControleDto createControleDto)
        {
            try
            {
                var controleContainer = "controle-container";
                string enonceUrl = await _blobStorageService.UploadFileAsync(createControleDto.Ennonce.OpenReadStream(), controleContainer, createControleDto.Ennonce.FileName);
                string solutionleUrl = await _blobStorageService.UploadFileAsync(createControleDto.Solution.OpenReadStream(), controleContainer, createControleDto.Solution.FileName);



                Controle controle = new Controle()
                {
                    Ennonce = enonceUrl,
                    Solution = solutionleUrl,
                    Nom = createControleDto.Nom,
                    Status = createControleDto.Statue,

                };
                await apiDbContext.controles.AddAsync(controle);
                await apiDbContext.SaveChangesAsync();
                var chaptersToUpdate = await apiDbContext.chapitres
                                                     .Where(c => createControleDto.Chapters.Contains(c.Id))
                                                     .ToListAsync();

                foreach (var chapter in chaptersToUpdate)
                {
                    chapter.ControleId = controle.Id;
                }
                await apiDbContext.SaveChangesAsync();
                return Result<Controle>.Success(controle);

            }
            catch (System.Exception ex)
            {

                return Result<Controle>.Failure(ex.Message);
            }


        }

        public async Task<Result<ControleDto>> GetControleById(int controleId)
        {
            Controle? controle = await apiDbContext.controles.Include(c => c.Chapitres).FirstOrDefaultAsync(x => x.Id == controleId);

            if (controle == null)
            {
                return Result<ControleDto>.Failure("Controle Not Found");
            }

            ControleDto controleDto = new ControleDto
            {
                Id = controle.Id,
                Ennonce = controle.Ennonce,
                Solution = controle.Solution,
                ChapitreNum = controle.Chapitres.Select(x => x.ChapitreNum).ToList(),

            };
            return Result<ControleDto>.Success(controleDto);
        }

        public async Task<Result<Controle>> GetDashboardControleById(int Id)
        {
            try
            {
                Controle? controle = await apiDbContext.controles.FirstOrDefaultAsync(x => x.Id == Id);
                if (controle == null)
                {
                    return Result<Controle>.Failure("controle notfound");
                }
                return Result<Controle>.Success(controle);
            }
            catch (System.Exception ex)
            {

                return Result<Controle>.Failure(ex.Message);
            }

        }

        public async Task<Result<List<Controle>>> GetControlesByModule(int Id)
        {
            try
            {


                List<Controle?> controles = await apiDbContext.chapitres
                            .Where(ch => ch.ModuleId == Id)
                            .Select(ch => ch.Controle)
                            .Where(controle => controle != null)
                            .Distinct()
                            .ToListAsync();
                return Result<List<Controle>>.Success(controles!.Cast<Controle>().ToList());
            }
            catch (System.Exception ex)
            {

                return Result<List<Controle>>.Failure(ex.Message);
            }
        }

        public async Task<Result<Controle>> Refuser(int id)
        {
            try
            {
                Controle? controle = await apiDbContext.controles.FirstOrDefaultAsync(x => x.Id == id);
                if (controle == null)
                {
                    return Result<Controle>.Failure("Controle not found");
                }
                controle.Status = ObjectStatus.Denied;
                await apiDbContext.SaveChangesAsync();
                return Result<Controle>.Success(controle);
            }
            catch (System.Exception ex)
            {

                return Result<Controle>.Failure(ex.Message);

            }
        }

        public async Task<Result<Controle>> UpdateControleEnnonce(UpdateControleEnnonceDto updateControleEnnonceDto)
        {
            try
            {
                Controle? controle = await apiDbContext.controles.FirstOrDefaultAsync(x => x.Id == updateControleEnnonceDto.Id);

                if (controle == null)
                {
                    return Result<Controle>.Failure("controle not found");
                }
                var controleContainer = "controle-container";
                string enonceUrl = await _blobStorageService.UploadFileAsync(updateControleEnnonceDto.Ennonce.OpenReadStream(), controleContainer, updateControleEnnonceDto.Ennonce.FileName);


                await _blobStorageService.DeleteFileAsync(controleContainer, new Uri(controle.Ennonce).Segments.Last());
                controle.Ennonce = enonceUrl;
                await apiDbContext.SaveChangesAsync();
                return Result<Controle>.Success(controle);

            }
            catch (System.Exception ex)
            {

                return Result<Controle>.Failure(ex.Message);
            }
        }

        public async Task<Result<Controle>> UpdateControleName(UpdateControleNameDto updateControleNameDto)
        {
            try
            {
                Controle? controle = await apiDbContext.controles.FirstOrDefaultAsync(x => x.Id == updateControleNameDto.Id);
                if (controle == null)
                {
                    return Result<Controle>.Failure("controle not found");
                }
                controle.Nom = updateControleNameDto.Name;
                await apiDbContext.SaveChangesAsync();
                return Result<Controle>.Success(controle);
            }
            catch (System.Exception ex)
            {

                return Result<Controle>.Failure(ex.Message);
            }
        }

        public async Task<Result<Controle>> UpdateControleSolution(UpdateControleSolutionDto updateControleSolutionDto)
        {
            try
            {
                Controle? controle = await apiDbContext.controles.FirstOrDefaultAsync(x => x.Id == updateControleSolutionDto.Id);

                if (controle == null)
                {
                    return Result<Controle>.Failure("controle not found");
                }
                var controleContainer = "controle-container";
                string solutionUrl = await _blobStorageService.UploadFileAsync(updateControleSolutionDto.Solution.OpenReadStream(), controleContainer, updateControleSolutionDto.Solution.FileName);



                await _blobStorageService.DeleteFileAsync(controleContainer, new Uri(controle.Ennonce).Segments.Last());
                controle.Solution = solutionUrl;
                await apiDbContext.SaveChangesAsync();
                return Result<Controle>.Success(controle);

            }
            catch (System.Exception ex)
            {

                return Result<Controle>.Failure(ex.Message);
            }
        }

        public async Task<bool> DeleteControle(int id)
        {
            List<Chapitre> chapitres = await apiDbContext.chapitres.Where(x => x.ControleId == id).ToListAsync();
            Controle? controle = await apiDbContext.controles.FirstOrDefaultAsync(x => x.Id == id);
            if (controle != null)
            {
                foreach (var chapitre in chapitres)
                {
                    chapitre.ControleId = null;
                }
                apiDbContext.controles.Remove(controle);
                await apiDbContext.SaveChangesAsync(); return true;
            }
            return false;
        }
    }
}