using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Option
{
    public class UpdateOptionDto
    {
        public int? Id { get; set; } 
        public string Nom { get; set; } = "";
        public bool Truth { get; set; }      
    }
}