using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Model
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Titre { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public int PosteId { get; set; }
        public Poste? Poste { get; set; }
    }
}