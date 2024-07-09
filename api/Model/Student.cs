using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Model
{
    public class Student : AppUser
    {
        public List<TestNiveau> TestNiveaus { get; set; } = new List<TestNiveau>();
        public List<CheckChapter> CheckChapters = new List<CheckChapter>();
        public List<ResultControle> ResultControles = new List<ResultControle>();
        public List<QuizResult> QuizResults { get; set; } = new List<QuizResult>();

    }
}