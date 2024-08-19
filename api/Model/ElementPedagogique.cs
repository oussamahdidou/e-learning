using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Model
{
    public class ElementPedagogique
    {
        [Key]
        public int Id { get; set; }
        public string Nom { get; set; } = "";
        public string Lien { get; set; } = "https://blog.coursify.me/wp-content/uploads/2018/08/plan-your-online-course.jpg";
        public int NiveauScolaireId { get; set; }
        public NiveauScolaire? NiveauScolaire { get; set; }
    }
}