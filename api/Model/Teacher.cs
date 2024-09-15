using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Model
{
    public class Teacher : AppUser
    {
        public string Nom { get; set; } = "Doe";
        public string Prenom { get; set; } = "Jhon";
        public string Etablissement { get; set; } = "";
        public string JustificatifDeLaProfession { get; set; } = "";
        public DateTime DateDeNaissance { get; set; }
        public string Specialite { get; set; } = "";
        public string Status { get; set; } = "";
        public DateTime LastChapterProgressUpdate { get; set; }
        public int ChapterProgress { get; set; }
        public List<ExamFinal> ExamFinals { get; set; } = new List<ExamFinal>();
        public List<Controle> Controles { get; set; } = new List<Controle>();
        public List<Chapitre> Chapitres { get; set; } = new List<Chapitre>();
        public List<Paragraphe> Paragraphes { get; set; } = new List<Paragraphe>();

        public List<Schema> Schemas { get; set; } = new List<Schema>();

        public List<Synthese> Syntheses { get; set; } = new List<Synthese>();
        public List<Video> Videos { get; set; } = new List<Video>();

    }
}