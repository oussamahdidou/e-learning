using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Module
{
    public class UpdateModuleImageDto
    {
        public int ModuleId { get; set; }
        public required IFormFile ImageFile { get; set; }
    }
}