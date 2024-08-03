using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.RequiredModules
{
    public class IsEligibleDto
    {
        public bool IsEligible { get; set; } = false;
        public List<RequiredModulesDto> modules { get; set; } = new List<RequiredModulesDto>();
    }
}