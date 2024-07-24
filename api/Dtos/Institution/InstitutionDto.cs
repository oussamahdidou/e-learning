using System;
using System.Collections.Generic;
using api.Dtos.NiveauScolaire;

namespace api.Dtos.Institution
{
    public class InstitutionDto
    {
        public int Id { get; set; }
        public string Nom { get; set; } = "";
        public List<NiveauScolaireDto> NiveauScolaires { get; set; } = new List<NiveauScolaireDto>();

        public InstitutionDto()
        {
            NiveauScolaires = new List<NiveauScolaireDto>();
        }
    }
}
