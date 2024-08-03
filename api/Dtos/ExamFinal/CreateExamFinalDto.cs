using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.helpers;

namespace api.Dtos.ExamFinal
{
    public class CreateExamFinalDto
    {
        public required string Nom { get; set; }
        public required IFormFile Ennonce { get; set; }
        public required IFormFile Solution { get; set; }
        public string Status { get; set; } = ObjectStatus.Pending;
        public int ModuleId { get; set; }
    }
}