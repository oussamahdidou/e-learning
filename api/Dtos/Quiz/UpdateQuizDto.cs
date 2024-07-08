using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Question;

namespace api.Dtos.Quiz
{
    public class UpdateQuizDto
    {
        public string Nom { get; set; } = "";

        public List<UpdateQuestionDto> Questions { get; set; } = new List<UpdateQuestionDto>();

    }
}