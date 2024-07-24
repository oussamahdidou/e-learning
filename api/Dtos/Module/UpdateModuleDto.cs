using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.ComponentModel.DataAnnotations;

namespace api.Dto
{
    public class UpdateModuleDto
    {
        [Required]
        public int? Id { get; set; }
        
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
    public class UpdateModuleDto
    {
        public string Nom { get; set; } = "";
        public int ModuleId { get; set; }
    }
}
>>>>>>> manall
