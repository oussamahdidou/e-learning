using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.ElementPedagogique;
using api.generique;
using api.interfaces;
using api.Model;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace api.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class ElementPedagogiqueController : ControllerBase
    {
        private readonly IElementPedagogiqueRepository _elementPedagogiqueRepository;

        public ElementPedagogiqueController(IElementPedagogiqueRepository elementPedagogiqueRepository)
        {
            _elementPedagogiqueRepository = elementPedagogiqueRepository;
        }

        [HttpPost]

        public async Task<IActionResult> CreateElementPedagogique([FromForm] CreateElementPedagogiqueDto createElementDto)
        {
            Result<ElementPedagogique> result = await _elementPedagogiqueRepository.CreateElementPedagogiqueAsync(createElementDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }


        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetElementsById([FromRoute] int Id)
        {
            Result<List<ElementPedagogique>> result = await _elementPedagogiqueRepository.GetElementsById(Id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }





        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteElementPedagogique([FromRoute] int id)
        {
            Result<bool> result = await _elementPedagogiqueRepository.DeleteElementPedagogique(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
    }
}
