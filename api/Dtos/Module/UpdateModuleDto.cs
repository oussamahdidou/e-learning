using System;
using System.Collections.Generic;
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
