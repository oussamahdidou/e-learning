using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Institution;
using api.generique;
using api.interfaces;
using api.Model;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstitutionController : ControllerBase
    {
        private readonly IinstitutionRepository institutionRepository;
        public InstitutionController(IinstitutionRepository institutionRepository)
        {
            this.institutionRepository = institutionRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllInstitutions()
        {
            Result<List<Institution>> result = await institutionRepository.GetInstitutions();
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetInstitutionById([FromRoute] int id)
        {
            Result<Institution> result = await institutionRepository.GetInstitutionById(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPost]
        public async Task<IActionResult> CreateInstitution([FromBody] CreateInstitutionDto createInstitutionDto)
        {
            Result<Institution> result = await institutionRepository.CreateInstitution(createInstitutionDto.name);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateInstitution([FromBody] UpdateInstitutionDto updateInstitutionDto)
        {
            Result<Institution> result = await institutionRepository.UpdateInstitution(updateInstitutionDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
    }
}