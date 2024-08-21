using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Module;

namespace api.Dtos.UserCenter
{
    public class ModuleWithCheckCountDto
    {
        public int ModuleID { get; set; }
        public string? Nom { get; set; }
        public int NumberOfChapter { get; set; }
        public string? ModuleImg { get; set; }
        public string? ModuleDescription { get; set; }
        public string? ModuleProgram { get; set; }
        public int? CheckCount { get; set; }
    }
}