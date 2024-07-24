<<<<<<< HEAD
=======
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.NiveauScolaire;
using api.generique;
using api.interfaces;
using api.Model;
using api.Repository;
>>>>>>> manall
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Dtos.NiveauScolaires;
using api.generique;
using api.interfaces;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NiveauScolaireController : ControllerBase
    {
<<<<<<< HEAD
        private readonly INiveauScolaireRepository _niveauScolaireRepository;

        public NiveauScolaireController(INiveauScolaireRepository niveauScolaireRepository)
        {
            _niveauScolaireRepository = niveauScolaireRepository;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _niveauScolaireRepository.GetAllAsync();
            if (!result.IsSuccess)
=======
        private readonly INiveauScolaireRepository niveauScolaireRepository;
        public NiveauScolaireController(INiveauScolaireRepository niveauScolaireRepository)
        {
            this.niveauScolaireRepository = niveauScolaireRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetNiveauScolaireById(int id)
        {
            Result<NiveauScolaire> result = await niveauScolaireRepository.GetNiveauScolaireById(id);
            if (result.IsSuccess)
>>>>>>> manall
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
<<<<<<< HEAD

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _niveauScolaireRepository.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            if (result.Value == null)
            {
                return NotFound("NiveauScolaire not found");
            }
            return Ok(result.Value);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Add([FromBody] NiveauScolaireDto niveauScolaireDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _niveauScolaireRepository.AddAsync(niveauScolaireDto);
            if (!result.IsSuccess)
=======
        [HttpPost]
        public async Task<IActionResult> CreateNiveauScolaire([FromBody] CreateNiveauScolaireDto createNiveauScolaireDto)
        {
            Result<NiveauScolaire> result = await niveauScolaireRepository.CreateNiveauScolaire(createNiveauScolaireDto);
            if (result.IsSuccess)
>>>>>>> manall
            {
                return Ok(result.Value);
            }
<<<<<<< HEAD
            return Ok(result.Value);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] NiveauScolaireDto niveauScolaireDto)
=======
            return BadRequest(result.Error);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateNiveauScolaire([FromBody] UpdateNiveauScolaireDto updateNiveauScolaireDto)
>>>>>>> manall
        {
            Result<NiveauScolaire> result = await niveauScolaireRepository.UpdateNiveauScolaire(updateNiveauScolaireDto);
            if (result.IsSuccess)
            {
<<<<<<< HEAD
                return BadRequest("ID incorrect !");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _niveauScolaireRepository.UpdateAsync(niveauScolaireDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
           

            return Ok("NiveauScolaire updated successfully");
=======
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
>>>>>>> manall
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteNiveauScolaire(int id){
           
             Result<NiveauScolaire> result = await niveauScolaireRepository.DeleteNiveauScolaire(id);
            if(result.IsSuccess){

                return Ok(result.Value);

<<<<<<< HEAD
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _niveauScolaireRepository.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok("NiveauScolaire deleted successfully");
=======
            }
            else{

            return BadRequest(result.Error);
            }
>>>>>>> manall
        }
    }
}