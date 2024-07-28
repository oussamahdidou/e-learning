using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Question;
using api.helpers;

namespace api.Dtos.Quiz
{
    public class CreateQuizDto
    {
        public string Nom { get; set; } = "";
        public string Statue { get; set; } = ObjectStatus.Pending;
        public List<CreateQuestionDto> Questions { get; set; } = new List<CreateQuestionDto>();
    }
}