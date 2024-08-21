using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Question;
using api.Dtos.Quiz;
using api.helpers;
using api.Model;

namespace api.Dtos.Chapitre
{
    public class CreateChapitreDto
    {
        public int Number { get; set; }
        public string Nom { get; set; } = "";
        public List<IFormFile> StudentCourseParagraphs { get; set; } = new List<IFormFile>();
        public List<IFormFile> ProfessorCourseParagraphs { get; set; } = new List<IFormFile>();
        public IFormFile? CoursVideoFile { get; set; }
        public string? CoursVideoLink { get; set; }
        public IFormFile? Synthese { get; set; }
        public IFormFile? Schema { get; set; }
        public bool Premium { get; set; } = true;
        public string Statue { get; set; } = ObjectStatus.Pending;
        public int ModuleId { get; set; }
        public int QuizId { get; set; }
    }
}
