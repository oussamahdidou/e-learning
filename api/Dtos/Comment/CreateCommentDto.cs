using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    public class CreateCommentDto
    {
        public required string Text { get; set; }
        public int PosteId { get; set; }
        public required string UserId { get; set; }
    }
}