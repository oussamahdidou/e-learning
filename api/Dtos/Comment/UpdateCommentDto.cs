using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    public class UpdateCommentDto
    {
        public required string Text { get; set; }
        public int CommentId { get; set; }
    }
}