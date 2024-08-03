using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Model
{
    public class ResultExam
    {
        public string? StudentId { get; set; }
        public int ExamFinalId { get; set; }
        public Student? Student { get; set; }
        public ExamFinal? ExamFinal { get; set; }
        public string Reponse { get; set; } = "";
    }
}