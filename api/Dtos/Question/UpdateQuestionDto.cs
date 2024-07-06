using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Option;

namespace api.Dtos.Question
{
    public class UpdateQuestionDto
    {
        public int? Id { get; set; }
        public string Nom { get; set; } = "";
        public List<UpdateOptionDto> Options { get; set; }
    }
}