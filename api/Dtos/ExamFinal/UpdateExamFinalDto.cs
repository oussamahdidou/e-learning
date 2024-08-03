using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.ExamFinal
{
    public class UpdateExamFinalDto
    {
        public int Id { get; set; }
        public required IFormFile File { get; set; }
    }
}