using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Question;
using api.Dtos.Quiz;
using api.helpers;

namespace api.Dtos.Chapitre
{
    public class CreateChapitreDto
    {
        public int ChapitreNum { get; set; }
        public string Nom { get; set; } = "";
        public required IFormFile CoursPdf { get; set; }
        public required IFormFile Video { get; set; }
        public required IFormFile Synthese { get; set; }
        public required IFormFile Schema { get; set; }
        public bool Premium { get; set; }
        public string Statue { get; set; } = ObjectStatus.Pending;
        public int ModuleId { get; set; }
        public int QuizId { get; set; }
    }
}
