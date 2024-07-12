using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Model
{
    public class QuizResult
    {
        public string? StudentId { get; set; }
        public int QuizId { get; set; }
        public Student? Student { get; set; }
        public Quiz? Quiz { get; set; }
        public double Note { get; set; }
    }
}