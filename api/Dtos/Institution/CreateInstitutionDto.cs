using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using api.Dtos.NiveauScolaire;

namespace api.Dtos.Institution
{
    public class CreateInstitutionDto
    {
        [Required]
        public string Nom { get; set; } = "";

        //public List<NiveauScolaireDto> NiveauScolaires { get; set; } = new List<NiveauScolaireDto>();
    }
}
