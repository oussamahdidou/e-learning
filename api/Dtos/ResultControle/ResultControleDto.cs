using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Model;

namespace api.Dtos.ResultControle
{
    public class ResultControleDto
    {
        public Model.Controle? Controle { get; set; }
        public string? Reponse { get; set; }
    }
}