using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.ExamFinal;
using api.Dtos.ResultExamDto;
using api.Model;

namespace api.Mappers
{
    public static class ExamFinalMapper
    {
            public static ResultExamDto ToExamFinalDto (this ResultExam exam){
            return new ResultExamDto
            {
                Controle = exam.ExamFinal,
                Reponse = exam.Reponse
            };
        }
    }
}