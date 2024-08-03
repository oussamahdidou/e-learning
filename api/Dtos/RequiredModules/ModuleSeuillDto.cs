using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.RequiredModules
{
    public class ModuleSeuillDto
    {
        public string? ModuleName { get; set; }
        public double Seuill { get; set; }
    }
}