using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.helpers;

namespace api.Dtos.Controle
{
    public class CreateControleDto
    {
        public string Nom { get; set; }
        public IFormFile Ennonce { get; set; }
        public IFormFile Solution { get; set; }
        public string Statue { get; set; } = ObjectStatus.Pending;
        public List<int> Chapters { get; set; } = new List<int>();
    }
}