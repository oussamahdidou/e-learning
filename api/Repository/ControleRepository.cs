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
                        Status = ObjectStatus.Pending,

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
    }
}