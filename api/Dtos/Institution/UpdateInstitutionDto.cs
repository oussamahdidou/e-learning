using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using api.Dtos.NiveauScolaires;

namespace api.Dtos.Institution
{
    public class UpdateInstitutionDto
    {
        [Required]
        public int? Id { get; set; }

        [Required]
        public string Nom { get; set; } = "";

        public List<NiveauScolaireDto> NiveauScolaires { get; set; } = new List<NiveauScolaireDto>();
    }
}
