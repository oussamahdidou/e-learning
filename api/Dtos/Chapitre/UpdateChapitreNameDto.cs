using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Chapitre
{
    public class UpdateChapitreNameDto
    {
        public int Id { get; set; }
        public required string Nom { get; set; }
    }
}