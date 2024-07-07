using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.QuizResult
{
    public class CreateQuizResultDto
    {
        public int QuizId { get; set; }
        
        public double note { get; set; }
    }
}