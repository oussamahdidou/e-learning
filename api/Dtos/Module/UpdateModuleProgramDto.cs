using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Module
{
    public class UpdateModuleProgramDto
    {
        public int ModuleId { get; set; }
        public required IFormFile ProgramFile { get; set; }
    }
}