using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.helpers;

namespace api.Dtos.Controle
{
    public class CreateControleDto
    {
        public required string Nom { get; set; }
        public required IFormFile Ennonce { get; set; }
        public required IFormFile Solution { get; set; }
        public string Statue { get; set; } = ObjectStatus.Pending;
        public List<int> Chapters { get; set; } = new List<int>();
        public string? TeacherId { get; set; }
    }
}