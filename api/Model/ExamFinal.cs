using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Model
{
    public class ExamFinal
    {
        [Key]
        public int Id { get; set; }
        public string Nom { get; set; } = "";
        public string Ennonce { get; set; } = "";
        public string Solution { get; set; } = "";
        public string Status { get; set; } = "";
        public string? TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
        public List<ResultExam> ResultExams { get; set; } = new List<ResultExam>();


    }


}