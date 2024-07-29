using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Module;

namespace api.Dtos.UserCenter
{
    public class ModuleWithCheckCountDto
    {
        public ModuleDto Module { get; set; }
        public int CheckCount { get; set; }
    }
}