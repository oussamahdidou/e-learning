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
                Name = moduleRequirement.TargetModule.Nom,
                Institution = moduleRequirement.TargetModule.NiveauScolaire.Institution.Nom,
                Id = moduleRequirement.TargetModule.Id,
                NiveauScolaire = moduleRequirement.TargetModule.NiveauScolaire.Nom,
                Seuill = moduleRequirement.Seuill,
            };
        }
        public static RequiredModulesDto RequiredModulesFromModelToDto(this ModuleRequirement moduleRequirement)
        {
            return new RequiredModulesDto()
            {
                Name = moduleRequirement.RequiredModule.Nom,
                Institution = moduleRequirement.RequiredModule.NiveauScolaire.Institution.Nom,
                Id = moduleRequirement.RequiredModule.Id,
                NiveauScolaire = moduleRequirement.RequiredModule.NiveauScolaire.Nom,
                Seuill = moduleRequirement.Seuill,
            };
        }
    }
}