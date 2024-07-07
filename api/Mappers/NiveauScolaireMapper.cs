using api.Dto;
using api.Dtos.NiveauScolaires;
using api.Model;

namespace api.Mappers
{
    public static class NiveauScolaireMapper
    {
        public static NiveauScolaireDto MapToDto(NiveauScolaire niveauScolaire)
        {
            return new NiveauScolaireDto
            {
                Id = niveauScolaire.Id,
                Nom = niveauScolaire.Nom,
                InstitutionId = niveauScolaire.InstitutionId,
                Modules = niveauScolaire.Modules
                    .Select(m => new ModuleDto
                    {
                        Id = m.Id,
                        Nom = m.Nom,
                        // Map other properties of Module if needed
                    })
                    .ToList()
            };
        }

        public static NiveauScolaire MapToEntity(NiveauScolaireDto niveauScolaireDto)
        {
            return new NiveauScolaire
            {
                Id = niveauScolaireDto.Id,
                Nom = niveauScolaireDto.Nom,
                InstitutionId = niveauScolaireDto.InstitutionId,
                Modules = niveauScolaireDto.Modules
                    .Select(m => new Module
                    {
                        Id = m.Id,
                        Nom = m.Nom,
                        // Map other properties of Module if needed
                    })
                    .ToList()
            };
        }
    }
}
