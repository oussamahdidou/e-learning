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
        public string Nom { get; set; } = string.Empty;

        [Required]
        [Url]
        public string Lien { get; set; } = "https://blog.coursify.me/wp-content/uploads/2018/08/plan-your-online-course.jpg";

        [Required]
        public int NiveauScolaireId { get; set; }  
    }
}