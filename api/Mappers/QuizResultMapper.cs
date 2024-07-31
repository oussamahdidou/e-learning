using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.QuizResult;
using api.Model;

namespace api.Mappers
{
    public static class QuizResultMapper
    {
        public static QuizResultDto ToQuizResultDto (this QuizResult quizResultMapper){
            return new QuizResultDto
            {
                QuizId = quizResultMapper.QuizId,
                Note = quizResultMapper.Note
            };
        }
    }
}