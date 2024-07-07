using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Option;

namespace api.Dtos.Question
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string Nom { get; set; } = "";

        public List<OptionDto> Options { get; set; } = new List<OptionDto>();
    }
}