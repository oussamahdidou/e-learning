using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Poste
{
    public class UpdatePostDto
    {
        [Required]
        public int Id {get; set;} 
        [Required]
        public string? Titre {get; set;} 
        [Required]
        public string? Content {get; set;} 
        public IFormFile? Image {get; set;} 
        public IFormFile? Fichier {get; set;} 
        
        [Required]
        public string? AppUserId {get; set;}
    }
}