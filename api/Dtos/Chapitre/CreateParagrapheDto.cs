using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Chapitre
{
    public class CreateParagrapheDto
    {
        public int CoursId { get; set; }
        public required IFormFile ParagrapheContenu { get; set; }

    }
}