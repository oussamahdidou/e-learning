using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;
using api.Dtos.NiveauScolaire;

namespace api.Dtos.NiveauScolaire
{
    public class CreateNiveauScolaireDto
    {
        public string Nom { get; set; } = "";
        public int InstitutionId { get; set; }
        ///
        public List<ModuleDto> Modules { get; set; } = new List<ModuleDto>();
    }
}