using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Option
{
    public class CreateOptionDto
    {
        public string Nom { get; set; } = "";
        public bool Truth { get; set; } = false;
    }
}