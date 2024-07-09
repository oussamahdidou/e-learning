using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Model
{
    public class ModuleRequirement
    {
        public int TargetModuleId { get; set; }
        public int RequiredModuleId { get; set; }
        public Module? TargetModule { get; set; }
        public Module? RequiredModule { get; set; }
        public double Seuill { get; set; }
    }
}