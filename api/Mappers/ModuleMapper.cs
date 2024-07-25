using api.Dto;
using api.Model;
using System.Linq;

namespace api.Mappers
{
    public static class ModuleMapper
    {
        public static ModuleDto MapToDto(Module module)
        {
            return new ModuleDto
            {
                Id = module.Id,
                Nom = module.Nom,
                NiveauScolaireId = module.NiveauScolaireId,
                NiveauScolaireName = module.NiveauScolaire?.Nom,
                ChapitreIds = module.Chapitres.Select(c => c.Id).ToList()
                // Vous pouvez mapper d'autres propriétés si nécessaire
                // RequiredModuleIds = module.ModuleRequirements.Select(mr => mr.RequiredModuleId).ToList(),
                // RequiredForModuleIds = module.ModulesRequiredIn.Select(mr => mr.ModuleId).ToList(),
                // TestNiveauIds = module.TestNiveaus.Select(tn => tn.Id).ToList()
            };
        }

        public static Module MapToEntity(ModuleDto moduleDto)
        {
            return new Module
            {
                Id = moduleDto.Id,
                Nom = moduleDto.Nom,
                NiveauScolaireId = moduleDto.NiveauScolaireId,
                // Vous pouvez mapper d'autres propriétés si nécessaire
                Chapitres = moduleDto.ChapitreIds.Select(id => new Chapitre { Id = id }).ToList()
                // ModuleRequirements = moduleDto.RequiredModuleIds.Select(id => new ModuleRequirement { RequiredModuleId = id }).ToList(),
                // ModulesRequiredIn = moduleDto.RequiredForModuleIds.Select(id => new ModuleRequirement { ModuleId = id }).ToList(),
                // TestNiveaus = moduleDto.TestNiveauIds.Select(id => new TestNiveau { Id = id }).ToList()
            };
        }
    }
}
