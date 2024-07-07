using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Quiz;
using api.Model;

namespace api.Mappers
{
    public static class QuizMapper
    {
        public static QuizDto ToQuizDto(this Quiz quiz)
        {
            return new QuizDto
            {
                Id = quiz.Id,
                Nom = quiz.Nom,
                Statue = quiz.Statue,
                Questions = quiz.Questions.Select(q => q.ToQuestionDto()).ToList()
            };
        }
    }
}