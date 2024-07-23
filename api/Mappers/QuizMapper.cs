using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Option;
using api.Dtos.Question;
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
        public static Quiz FromCreateQuizDtoToQuiz(this CreateQuizDto createQuizDto)
        {
            return new Quiz
            {
                Nom = createQuizDto.Nom,
                Statue = createQuizDto.Statue,
                Questions = createQuizDto.Questions.Select(q => q.ToQuestion()).ToList()
            };
        }

        public static Question ToQuestion(this CreateQuestionDto createQuestionDto)
        {
            return new Question
            {
                Nom = createQuestionDto.Nom,
                Options = createQuestionDto.Options.Select(o => o.ToOption()).ToList()
            };
        }

        public static Option ToOption(this CreateOptionDto createOptionDto)
        {
            return new Option
            {
                Nom = createOptionDto.Nom,
                Truth = createOptionDto.Truth
            };
        }
    }
}