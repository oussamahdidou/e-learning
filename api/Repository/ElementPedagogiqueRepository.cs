using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.ElementPedagogique;
using api.generique;
using api.interfaces;
using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ElementPedagogiqueRepository : IElementPedagogiqueRepository
    {
        private readonly apiDbContext _context;
        private readonly IBlobStorageService blobStorageService;

        public ElementPedagogiqueRepository(apiDbContext context, IBlobStorageService blobStorageService)
        {
            _context = context;
            this.blobStorageService = blobStorageService;
        }

        public async Task<Result<ElementPedagogique>> CreateElementPedagogiqueAsync(CreateElementPedagogiqueDto createElementDto)
        {
            try
            {
                var pdfcontainer = "pdf-container";
                var objet = await blobStorageService.UploadFileAsync(createElementDto.Lien.OpenReadStream(), pdfcontainer, createElementDto.Lien.FileName);

                var element = new ElementPedagogique
                {
                    Nom = createElementDto.Nom,
                    Lien = objet,
                    NiveauScolaireId = createElementDto.NiveauScolaireId
                };

                await _context.elementPedagogiques.AddAsync(element);
                await _context.SaveChangesAsync();

                return Result<ElementPedagogique>.Success(element);
            }
            catch (Exception ex)
            {
                return Result<ElementPedagogique>.Failure(ex.Message);
            }
        }

        public Task<Result<bool>> DeleteElementPedagogique(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<Result<List<ElementPedagogique>>> GetElementsById(int Id)
        {
            try
            {
                var elements = await _context.elementPedagogiques
                    .Where(e => e.NiveauScolaireId == Id)
                    .ToListAsync();

                if (elements == null || !elements.Any())
                {
                    return Result<List<ElementPedagogique>>.Failure("Aucun élément pédagogique trouvé pour ce niveau scolaire");
                }

                return Result<List<ElementPedagogique>>.Success(elements);
            }
            catch (Exception ex)
            {
                return Result<List<ElementPedagogique>>.Failure($"{ex.Message}");
            }
        }
    }
}