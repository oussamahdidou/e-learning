using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.RequiredModules
{
    public class CreateRequiredModuleDto
    {
        public int TargetModuleId { get; set; }
        public int RequiredModuleId { get; set; }
        public double Seuill { get; set; }
    }
}