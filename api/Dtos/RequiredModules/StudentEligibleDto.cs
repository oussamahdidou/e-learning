using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.RequiredModules
{
    public class StudentEligibleDto
    {
        public string? StudentId { get; set; }
        public int TargetModuleId { get; set; }
    }
}