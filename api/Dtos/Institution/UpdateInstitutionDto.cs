using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.ComponentModel.DataAnnotations;
using api.Dtos.NiveauScolaires;
=======
using System.Linq;
using System.Threading.Tasks;
>>>>>>> manall

namespace api.Dtos.Institution
{
    public class UpdateInstitutionDto
    {
<<<<<<< HEAD
        [Required]
        public int? Id { get; set; }

        [Required]
        public string Nom { get; set; } = "";

        public List<NiveauScolaireDto> NiveauScolaires { get; set; } = new List<NiveauScolaireDto>();
    }
}
=======
        public int Id { get; set; }
        public string Nom { get; set; } = "";

    }
}
>>>>>>> manall
