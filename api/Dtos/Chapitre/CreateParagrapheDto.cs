using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.helpers;

namespace api.Dtos.Chapitre
{
    public class CreateParagrapheDto
    {
        public int CoursId { get; set; }
        public required IFormFile ParagrapheContenu { get; set; }
        public string Statue { get; set; } = ObjectStatus.Pending;
        public string? TeacherId { get; set; }
    }
}