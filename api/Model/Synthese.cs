using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.helpers;

namespace api.Model
{
    public class Synthese
    {
        [Key]
        public int Id { get; set; }
        public string? Nom { get; set; }
        public int ObjetNumber { get; set; }
        public string? Link { get; set; }
        public Chapitre? Chapitre { get; set; }
        public int ChapitreId { get; set; }
        public string? TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
        public string Status { get; set; } = ObjectStatus.Pending;

    }
}