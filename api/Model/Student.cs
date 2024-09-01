using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Model
{
    public class Student : AppUser
    {
        public string Nom { get; set; } = "Doe";
        public string Prenom { get; set; } = "Jhon";
        public string Etablissement { get; set; } = "";
        public string Branche { get; set; } = "";
        public string Niveaus { get; set; } = "";
        public DateTime DateDeNaissance { get; set; }
        public string? tuteurMail {get; set;}
        public string? phoneNumber {get; set;}
        public List<TestNiveau> TestNiveaus { get; set; } = new List<TestNiveau>();
        public List<CheckChapter> CheckChapters = new List<CheckChapter>();
        public List<ResultControle> ResultControles = new List<ResultControle>();
        public List<QuizResult> QuizResults { get; set; } = new List<QuizResult>();
        public List<ResultExam> ResultExams { get; set; } = new List<ResultExam>();


    }
}