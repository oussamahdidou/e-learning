using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api.Dto
{
    public class ModuleDto
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Nom { get; set; } = "";
        
        public int NiveauScolaireId { get; set; }
        
        public string? NiveauScolaireName { get; set; }
        
        public List<int> ChapitreIds { get; set; } = new List<int>();
    }
}
