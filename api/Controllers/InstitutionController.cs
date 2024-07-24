<<<<<<< HEAD
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using api.Dtos.Institution;
using api.interfaces;
using Microsoft.AspNetCore.Authorization;
using api.generique;
using System.Collections.Generic;
=======
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Institution;
using api.generique;
using api.interfaces;
using api.Model;
using Microsoft.AspNetCore.Mvc;
>>>>>>> manall

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstitutionController : ControllerBase
    {
<<<<<<< HEAD
        private readonly IInstitutionRepository _institutionRepository;

        public InstitutionController(IInstitutionRepository institutionRepository)
        {
            _institutionRepository = institutionRepository;
        }

        [HttpGet("GetAll")]
       
        public async Task<IActionResult> GetAllInstitutions()
        {
            var result = await _institutionRepository.GetAllAsync();
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet("GetById/{id}")]
      
        public async Task<IActionResult> GetInstitutionById(int id)
        {
            var result = await _institutionRepository.GetByIdAsync(id);
            if (!result.IsSuccess)
=======
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
>>>>>>> manall
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
<<<<<<< HEAD

        [HttpPost("Create")]
       
        public async Task<IActionResult> CreateInstitution([FromBody] InstitutionDto institutionDto)
        {
            var result = await _institutionRepository.AddAsync(institutionDto);
            if (!result.IsSuccess)
=======
        [HttpPost]
        public async Task<IActionResult> CreateInstitution([FromBody] string InstitutionName)
        {
            Result<Institution> result = await institutionRepository.CreateInstitution(InstitutionName);
            if (result.IsSuccess)
>>>>>>> manall
            {
                return Ok(result.Value);
            }
<<<<<<< HEAD
            return Ok(result.Value);
        }

        [HttpPut("Update/{id}")]
       
        public async Task<IActionResult> UpdateInstitution([FromRoute] int id, [FromBody] InstitutionDto institutionDto)
=======
            return BadRequest(result.Error);
        }
        [HttpPut]
        public async Task<IActionResult> CreateInstitution([FromBody] UpdateInstitutionDto updateInstitutionDto)
>>>>>>> manall
        {
            Result<Institution> result = await institutionRepository.UpdateInstitution(updateInstitutionDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
<<<<<<< HEAD

            var result = await _institutionRepository.UpdateAsync(institutionDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok("Institution updated successfully");
        }

        [HttpDelete("Delete/{id}")]
                public async Task<IActionResult> DeleteInstitution(int id)
        {
            var result = await _institutionRepository.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok("Institution deleted successfully");
        }
=======
            return BadRequest(result.Error);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstitution(int id)
        {
        Result<Institution> result = await institutionRepository.DeleteInstitution(id);

       if (result.IsSuccess)
       {
        return Ok(result.Value);
       }
       else
      {
        return BadRequest(result.Error);
       }
       }
         

>>>>>>> manall
    }
}