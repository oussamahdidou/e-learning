using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.helpers;

namespace api.Model
{
        public class Paragraphe
        {
                [Key]
                public int Id { get; set; }
                public string? Nom { get; set; }
                public string? Contenu { get; set; }
                public string? TeacherId { get; set; }
                public Teacher? Teacher { get; set; }
                public int CoursId { get; set; }
                public Cours? Cours { get; set; }
                public string Status { get; set; } = ObjectStatus.Pending;
        }
}