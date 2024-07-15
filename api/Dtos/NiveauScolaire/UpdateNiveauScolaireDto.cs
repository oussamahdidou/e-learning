using System;
using System.Collections.Generic;
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