using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Model
{
    public class Paragraphe
    {
        [Key]
        public int Id { get; set; }
        public string? Contenu { get; set; }

        public int CoursId { get; set; }
        public Cours? Cours { get; set; }
    }
}