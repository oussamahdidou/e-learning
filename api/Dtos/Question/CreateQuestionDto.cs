using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Option;

namespace api.Dtos.Question
{
    public class CreateQuestionDto
    {
        public string Nom { get; set; } = "";
        public List<CreateOptionDto> Options { get; set; } = new List<CreateOptionDto>();
    }
}