using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Question;
using api.Model;

namespace api.Dtos.Quiz
{
    public class QuizDto
    {
        public int Id { get; set; }
        public string Nom { get; set; } = "";

        public List<QuestionDto> Questions { get; set; } = new List<QuestionDto>();     
    }
}