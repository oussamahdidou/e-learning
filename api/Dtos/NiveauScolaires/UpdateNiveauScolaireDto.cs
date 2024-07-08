using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using api.Dto;

namespace api.Dtos.NiveauScolaires
{
    public class UpdateNiveauScolaireDto
    {
        [Required]
        public int? Id { get; set; }

        [Required]
        public string Nom { get; set; } = "";

        [Required]
        public int InstitutionId { get; set; }

        public List<ModuleDto> Modules { get; set; } = new List<ModuleDto>();
    }
}
