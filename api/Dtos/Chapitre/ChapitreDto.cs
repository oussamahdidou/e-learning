using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using api.Dtos.File;
using api.Dtos.Quiz;
using api.Model;

namespace api.Dtos.Chapitre
{
    public class ChapitreDto
    {
        public int Id { get; set; }
        public int ChapitreNum { get; set; }
        public required string Nom { get; set; }
        public bool Statue { get; set; } = false;
        public List<FileDto>? StudentCoursParagraphes { get; set; }

        public List<FileDto>? Videos { get; set; }
        public List<FileDto> Syntheses { get; set; } 
        public List<FileDto> Schemas { get; set; }
        public bool Premium { get; set; }
        public QuizDto Quiz { get; set; } = new QuizDto();
    }
}
