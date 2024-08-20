using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Model
{
    public class Cours
    {
        [Key]
        public int Id { get; set; }
        public string? Titre { get; set; }
        public string? Type { get; set; } // "Prof" or "Etudiant"

        public int ChapitreId { get; set; }
        public Chapitre? Chapitre { get; set; }

        // Navigation Property
        public List<Paragraphe> Paragraphes { get; set; } = new List<Paragraphe>();
    }
}