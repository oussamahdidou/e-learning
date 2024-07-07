using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;

namespace api.Dtos.NiveauScolaires
{
    public class NiveauScolaireDto
    {
         public int Id { get; set; }
        public string Nom { get; set; } = "";
        public int InstitutionId { get; set; }
        public List<ModuleDto> Modules { get; set; } = new List<ModuleDto>();
    }
}