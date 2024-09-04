using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
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
        public List<int?>? StudentCoursParagraphes { get; set; }

        public List<Video> Videos { get; set; } = new List<Video>();
        public List<Synthese> Syntheses { get; set; } = new List<Synthese>();
        public List<Schema> Schemas { get; set; } = new List<Schema>();
        public bool Premium { get; set; }
        public QuizDto Quiz { get; set; } = new QuizDto();
    }
}
