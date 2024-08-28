using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.ElementPedagogique;
using api.generique;
using api.Model;

namespace api.interfaces
{
    public interface IElementPedagogiqueRepository
    {
        Task<Result<List<ElementPedagogique>>> GetElementsById(int Id);
        // Task<Result<ElementPedagogique>> CreateObjetPedagogiqueAsync(CreateElementPedagogiqueDto createElementPedagogiqueDto);
        Task<Result<ElementPedagogique>> CreateElementPedagogiqueAsync(CreateElementPedagogiqueDto createElementDto); // Updated to match the repository implementation
        Task<Result<bool>> DeleteElementPedagogique(int id);
    }
}