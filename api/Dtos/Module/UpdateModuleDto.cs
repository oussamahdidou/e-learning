using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Module
{
    public class UpdateModuleDto
    {
        public string Nom { get; set; } = "";
        public int ModuleId { get; set; }
    }
}
