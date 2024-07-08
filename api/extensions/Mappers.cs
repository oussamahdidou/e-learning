using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.RequiredModules;
using api.Model;

namespace api.extensions
{
    public static class Mappers
    {
        public static RequiredModulesDto RequiredInModulesFromModelToDto(this ModuleRequirement moduleRequirement)
        {
            return new RequiredModulesDto()
            {
                Module = moduleRequirement.TargetModule,
                Seuill = moduleRequirement.Seuill,
            };
        }
        public static RequiredModulesDto RequiredModulesFromModelToDto(this ModuleRequirement moduleRequirement)
        {
            return new RequiredModulesDto()
            {
                Module = moduleRequirement.RequiredModule,
                Seuill = moduleRequirement.Seuill,
            };
        }
    }
}