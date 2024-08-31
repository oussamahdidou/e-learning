using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string? Titre { get; set; }
        public string? Author { get; set; }
        public string? AuthorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsAdminComment { get; set; }
    }
}