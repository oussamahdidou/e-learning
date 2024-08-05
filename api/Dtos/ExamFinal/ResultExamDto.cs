using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.ResultExamDto
{
    public class ResultExamDto
    {
        public Model.ExamFinal? Controle { get; set; }
        public string? Reponse { get; set; }
    }
}