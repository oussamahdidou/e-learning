using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.QuizResult
{
    public class QuizResultDto
    {
        public string? StudentId { get; set; }
        public int QuizId { get; set; }
        public double Note { get; set; }
    }
}