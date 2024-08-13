using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Module
{
    public class UpdateModuleDescriptionDto
    {
        public int ModuleId { get; set; }
        public required string Description { get; set; }
    }
}