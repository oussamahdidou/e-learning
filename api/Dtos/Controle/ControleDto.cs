using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Control
{
    public class ControleDto
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Ennonce { get; set; }
        public string Solution { get; set; }
        public List<int> ChapitreNum { get; set; }
    }
}