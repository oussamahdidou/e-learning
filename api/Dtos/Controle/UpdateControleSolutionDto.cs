using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Controle
{
    public class UpdateControleSolutionDto
    {

        public int Id { get; set; }
        public required IFormFile Solution { get; set; }
    }
}