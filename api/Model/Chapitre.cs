using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Model
{
    public class Chapitre
    {
        [Key]
        public int Id { get; set; }
        public int ChapitreNum { get; set; }
        public string Nom { get; set; } = "";
        public string Statue { get; set; } = "";
        public List<Cours> Cours { get; set; } = new List<Cours>();
        public List<Video> Videos { get; set; } = new List<Video>();
        public List<Synthese> Syntheses { get; set; } = new List<Synthese>();
        public List<Schema> Schemas { get; set; } = new List<Schema>();

        public bool Premium { get; set; } = true;
        public int? QuizId { get; set; }
        public int ModuleId { get; set; }
        public int? ControleId { get; set; }
        public string? TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
        public Controle? Controle { get; set; }
        public Module? Module { get; set; }
        public Quiz? Quiz { get; set; }
        public List<CheckChapter> CheckChapters { get; set; } = new List<CheckChapter>();
    }
}