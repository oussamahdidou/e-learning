using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    public class PosteDto
    {
        public int Id { get; set; }
        public string? Titre { get; set; }
        public string? Content { get; set; }
        public string? Image { get; set; }
        public string? Fichier { get; set; }
        public string? Author { get; set; }
        public string? AuthorId { get; set; }
        public int CommentsNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsAdminPoste { get; set; }
    }
}