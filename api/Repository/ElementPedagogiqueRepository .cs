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

        public ElementPedagogiqueRepository(apiDbContext context)
        {
            _context = context;
        }

        public async Task<Result<ElementPedagogique>> CreateElementPedagogiqueAsync(CreateElementPedagogiqueDto createElementDto)
        {
            try
            {
                var element = new ElementPedagogique
                {
                    Nom = createElementDto.Nom,
                    Lien = createElementDto.Lien,
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

        

        /*

public async Task<Result<ElementPedagogique>> UpdateElementPedagogique(UpdateElementPedagogiqueDto updateElementDto)
{
   try
   {
       var element = await _context.ElementPedagogiques.FindAsync(updateElementDto.Id);

       if (element == null)
       {
           return Result<ElementPedagogique>.Failure("Element pédagogique non trouvé");
       }

       element.Nom = updateElementDto.Titre;
       element.Url = updateElementDto.Url;

       await _context.SaveChangesAsync();

       return Result<ElementPedagogique>.Success(element);
   }
   catch (Exception ex)
   {
       return Result<ElementPedagogique>.Failure(ex.Message);
   }
}

public async Task<Result<bool>> DeleteElementPedagogique(int elementId)
{
   try
   {
       var element = await _context.ElementPedagogiques.FindAsync(elementId);

       if (element == null)
       {
           return Result<bool>.Failure("Element pédagogique non trouvé");
       }

       _context.ElementPedagogiques.Remove(element);
       await _context.SaveChangesAsync();

       return Result<bool>.Success(true);
   }
   catch (Exception ex)
   {
       return Result<bool>.Failure(ex.Message);
   }
}*/

        public async Task<Result<List<ElementPedagogique>>> GetElementsById(int Id)
        {
            try
            {
                var elements = await _context.elementPedagogiques
                    .Where(e => e.Id == Id)
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