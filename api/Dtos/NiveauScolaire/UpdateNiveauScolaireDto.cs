using System;
using System.Collections.Generic;
<<<<<<< HEAD:api/Dtos/NiveauScolaires/UpdateNiveauScolaireDto.cs
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
=======
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.NiveauScolaire
{
    public class UpdateNiveauScolaireDto
    {
        public int NiveauScolaireId { get; set; }
        public string Nom { get; set; } = "";


    }
}
>>>>>>> manall:api/Dtos/NiveauScolaire/UpdateNiveauScolaireDto.cs
