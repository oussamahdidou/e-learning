using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Institution;
using api.Mappers;
using api.Model;
using api.interfaces;
using api.generique;

namespace api.Service
{
    public class InstitutionService
    {
        private readonly IInstitutionRepository _institutionRepository;

        public InstitutionService(IInstitutionRepository institutionRepository)
        {
            _institutionRepository = institutionRepository;
        }

        public async Task<IEnumerable<InstitutionDto>> GetAllDtosAsync()
        {
            var institutions = await _institutionRepository.GetAllAsync();
            return institutions.Select(InstitutionMapper.MapToDto);
        }

        public async Task<Result<InstitutionDto>> GetDtoByIdAsync(int id)
        {
            var institution = await _institutionRepository.GetByIdAsync(id);
            if (institution == null)
            {
                return Result<InstitutionDto>.Failure("Institution not found");
            }
            return Result<InstitutionDto>.Success(InstitutionMapper.MapToDto(institution));
        }

        public async Task<Result<InstitutionDto>> AddDtoAsync(InstitutionDto institutionDto)
        {
            var institution = InstitutionMapper.MapToEntity(institutionDto);
            var addedInstitution = await _institutionRepository.AddAsync(institution);
            return Result<InstitutionDto>.Success(InstitutionMapper.MapToDto(addedInstitution));
        }

        public async Task<Result> UpdateDtoAsync(InstitutionDto institutionDto)
        {
            var existingInstitution = await _institutionRepository.GetByIdAsync(institutionDto.Id);
            if (existingInstitution == null)
            {
                return Result.Failure($"Institution with ID {institutionDto.Id} not found.");
            }

            existingInstitution.Nom = institutionDto.Nom;
            existingInstitution.NiveauScolaires = institutionDto.NiveauScolaires
                .Select(ns => new NiveauScolaire
                {
                    Id = ns.Id,
                    Nom = ns.Nom
                })
                .ToList();

            await _institutionRepository.UpdateAsync(existingInstitution);
            return Result.Success();
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var existingInstitution = await _institutionRepository.GetByIdAsync(id);
            if (existingInstitution == null)
            {
                return Result.Failure($"Institution with ID {id} not found.");
            }

            await _institutionRepository.DeleteAsync(id);
            return Result.Success();
        }
    }
}
