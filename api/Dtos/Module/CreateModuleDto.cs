using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.ComponentModel.DataAnnotations;

namespace api.Dto
{
    public class CreateModuleDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Nom { get; set; } = "";
        
        [Required]
        public int NiveauScolaireId { get; set; }
        
        public List<int> ChapitreIds { get; set; } = new List<int>();
    }
}
=======
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Module
{
    public class CreateModuleDto
    {
        public string Nom { get; set; } = "";
        public int NiveauScolaireId { get; set; }
    }
}
>>>>>>> manall
