using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Question;
using api.Model;

namespace api.Mappers
{
    public static class QuestionMapper
    {
        public static QuestionDto ToQuestionDto(this Question question )
        {
            return new QuestionDto
            {
                Id = question.Id,
                Nom = question.Nom,
                Options = question.Options.Select(o => o.ToOptionDto()).ToList()
            };
        }
    }
}