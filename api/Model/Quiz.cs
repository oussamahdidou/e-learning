using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Model
{
    public class Quiz
    {
        [Key]
        public int Id { get; set; }
        public string Nom { get; set; } = "";
        public string Statue { get; set; } = "";

        public List<Question> Questions { get; set; } = new List<Question>();
        public List<QuizResult> QuizResults { get; set; } = new List<QuizResult>();

    }
}