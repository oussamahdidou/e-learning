using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.ElementPedagogique
{
    public class CreateElementPedagogiqueDto
    {
        [Required]
        public required string Nom { get; set; }

        [Required]
        public required IFormFile Lien { get; set; }
        [Required]
        public int NiveauScolaireId { get; set; }
    }
}