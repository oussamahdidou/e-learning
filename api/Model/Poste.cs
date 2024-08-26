using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Model
{
    public class Poste
    {
        [Key]
        public int Id { get; set; }
        public string Titre { get; set; } = "";
        public string Content { get; set; } = "";
        public string? Image { get; set; }
        public string? Fichier { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();

    }
}