using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;

namespace api.Dtos.NiveauScolaire
{
    public class NiveauScolaireDto
    {
      public int Id { get; set; }
      public string Nom { get; set; } = "";
      public int InstitutionId { get; set; }   

       public List<ModuleDto> ModuleDto { get; set; } = new List<ModuleDto>();

       public NiveauScolaireDto()
        {
            ModuleDto = new List<ModuleDto>();
        }
    }
}