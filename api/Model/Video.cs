using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Model
{
    public class Video
    {
        [Key]
        public int Id { get; set; }
        public string? Nom { get; set; }
        public string? Link { get; set; }
        public Chapitre? Chapitre { get; set; }
        public int ChapitreId { get; set; }
        public string? TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
    }
}