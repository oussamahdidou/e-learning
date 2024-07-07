using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.NiveauScolaires;
using api.Dtos.Institution;
using api.Model;

namespace api.Mappers
{
    public class InstitutionMapper
    {
        public static InstitutionDto MapToDto(Institution institution)
    {
        return new InstitutionDto
        {
            Id = institution.Id,
            Nom = institution.Nom,
            NiveauScolaires = institution.NiveauScolaires
                .Select(ns => new NiveauScolaireDto
                {
                    Id = ns.Id,
                    Nom = ns.Nom
                })
                .ToList()
        };
    }
        public static Institution MapToEntity(InstitutionDto institutionDto)
    {
        return new Institution
        {
            Id = institutionDto.Id,
            Nom = institutionDto.Nom,
            NiveauScolaires = institutionDto.NiveauScolaires
                .Select(ns => new NiveauScolaire
                {
                    Id = ns.Id,
                    Nom = ns.Nom
                })
                .ToList()
        };
    }
    }
}