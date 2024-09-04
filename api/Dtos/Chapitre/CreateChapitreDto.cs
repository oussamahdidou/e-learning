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
        public List<IFormFile> Videos { get; set; } = new List<IFormFile>();
        public List<string> VideosLink { get; set; } = new List<string>();
        public List<IFormFile> Syntheses { get; set; } = new List<IFormFile>();
        public List<IFormFile> Schemas { get; set; } = new List<IFormFile>();
        public bool Premium { get; set; } = true;
        public string Statue { get; set; } = ObjectStatus.Pending;
        public int ModuleId { get; set; }
        public int QuizId { get; set; }
        public string? TeacherId { get; set; }

    }
}
