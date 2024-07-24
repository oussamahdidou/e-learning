using System;
using System.Collections.Generic;
<<<<<<< HEAD:api/Dtos/NiveauScolaires/CreateNiveauScolaireDto.cs
using System.ComponentModel.DataAnnotations;
using api.Dto;

namespace api.Dtos.NiveauScolaires
{
    public class CreateNiveauScolaireDto
    {
        [Required]
        public string Nom { get; set; } = "";

        [Required]
        public int InstitutionId { get; set; }

        public List<ModuleDto> Modules { get; set; } = new List<ModuleDto>();
=======
<<<<<<<< HEAD:api/Dtos/NiveauScolaires/NiveauScolaireDto.cs
using api.Dto;
========
using System.Linq;
using System.Threading.Tasks;
>>>>>>>> manall:api/Dtos/NiveauScolaire/CreateNiveauScolaireDto.cs

namespace api.Dtos.NiveauScolaire
{
    public class CreateNiveauScolaireDto
    {
<<<<<<<< HEAD:api/Dtos/NiveauScolaires/NiveauScolaireDto.cs
        public int Id { get; set; }
========
>>>>>>>> manall:api/Dtos/NiveauScolaire/CreateNiveauScolaireDto.cs
        public string Nom { get; set; } = "";
        public int InstitutionId { get; set; }
>>>>>>> manall:api/Dtos/NiveauScolaire/CreateNiveauScolaireDto.cs
    }
}
