using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Controle
{
    public class UpdateControleNameDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}