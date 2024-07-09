using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Model
{
    public class ResultControle
    {
        public string? StudentId { get; set; }
        public int ControleId { get; set; }
        public Student? Student { get; set; }
        public Controle? Controle { get; set; }
        public string Reponse { get; set; } = "";
    }
}