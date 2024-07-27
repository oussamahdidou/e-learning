using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Controle
{
    public class UpdateControleEnnonceDto
    {
        public int Id { get; set; }
        public required IFormFile Ennonce { get; set; }
    }
}