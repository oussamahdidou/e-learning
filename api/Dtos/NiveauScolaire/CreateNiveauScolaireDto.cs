using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.NiveauScolaire
{
    public class CreateNiveauScolaireDto
    {
        public string Nom { get; set; } = "";
        public int InstitutionId { get; set; }
    }
}