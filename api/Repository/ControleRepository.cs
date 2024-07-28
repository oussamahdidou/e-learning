using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
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
        public ControleRepository(apiDbContext apiDbContext, IWebHostEnvironment webHostEnvironment)
        {
            this.apiDbContext = apiDbContext;
            this.webHostEnvironment = webHostEnvironment;
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
                Result<string> ennonceresult = await createControleDto.Ennonce.UploadControle(webHostEnvironment);
                Result<string> solutionresult = await createControleDto.Solution.UploadControleSolution(webHostEnvironment);

                if (ennonceresult.IsSuccess && solutionresult.IsSuccess)
                {
                    Controle controle = new Controle()
                    {
                        Ennonce = ennonceresult.Value,
                        Solution = solutionresult.Value,
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
                return Result<Controle>.Failure("error in files upload");
            }
            catch (System.Exception ex)
            {

                return Result<Controle>.Failure(ex.Message);
            }


        }

        public async Task<Result<Controle>> GetControleById(int Id)
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
                Result<string> resultUpload = await updateControleEnnonceDto.Ennonce.UploadControle(webHostEnvironment);
                if (resultUpload.IsSuccess)
                {
                    Result<string> resultDelete = controle.Solution.DeleteFile();
                    if (resultDelete.IsSuccess)
                    {
                        controle.Ennonce = resultUpload.Value;
                        await apiDbContext.SaveChangesAsync();
                        return Result<Controle>.Success(controle);
                    }
                    return Result<Controle>.Failure("error in file delete");
                }
                return Result<Controle>.Failure("error in file upload");
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
                Result<string> resultUpload = await updateControleSolutionDto.Solution.UploadControleSolution(webHostEnvironment);
                if (resultUpload.IsSuccess)
                {
                    Result<string> resultDelete = controle.Solution.DeleteFile();
                    if (resultDelete.IsSuccess)
                    {
                        controle.Solution = resultUpload.Value;
                        await apiDbContext.SaveChangesAsync();
                        return Result<Controle>.Success(controle);
                    }
                    return Result<Controle>.Failure("error in file delete");
                }
                return Result<Controle>.Failure("error in file upload");
            }
            catch (System.Exception ex)
            {

                return Result<Controle>.Failure(ex.Message);
            }
        }
    }
}