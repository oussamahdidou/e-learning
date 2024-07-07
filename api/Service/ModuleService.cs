using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.interfaces;
using api.Model;
using api.Dto;
using api.generique;

namespace api.Service
{
    public class ModulService
    {
        private readonly IModuleRepository _moduleRepository;

        public ModulService(IModuleRepository modulRepository)
        {
            _moduleRepository = modulRepository;
        }

        public async Task<Result<IEnumerable<ModuleDto>>> GetAllDtosAsync()
        {
            var modules = await _moduleRepository.GetAllAsync();
            return Result<IEnumerable<ModuleDto>>.Success(modules.Select(MapToDto));
        }

        public async Task<Result<ModuleDto>> GetDtoByIdAsync(int id)
        {
            var module = await _moduleRepository.GetByIdAsync(id);
            return module != null ? Result<ModuleDto>.Success(MapToDto(module)) : Result<ModuleDto>.Failure("Module not found.");
        }

        public async Task<Result<ModuleDto>> AddDtoAsync(ModuleDto moduleDto)
        {
            var module = MapToEntity(moduleDto);
            var addedModule = await _moduleRepository.AddAsync(module);
            return Result<ModuleDto>.Success(MapToDto(addedModule));
        }

        public async Task<Result> UpdateDtoAsync(ModuleDto moduleDto)
        {
            var existingModule = await _moduleRepository.GetByIdAsync(moduleDto.Id);
            if (existingModule == null)
            {
                return Result.Failure($"Module with ID {moduleDto.Id} not found.");
            }
            
            UpdateEntityFromDto(existingModule, moduleDto);
            await _moduleRepository.UpdateAsync(existingModule);
            return Result.Success();
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var existingModule = await _moduleRepository.GetByIdAsync(id);
            if (existingModule == null)
            {
                return Result.Failure($"Module with ID {id} not found.");
            }

            await _moduleRepository.DeleteAsync(id);
            return Result.Success();
        }

        private ModuleDto MapToDto(Module module)
        {
            return new ModuleDto
            {
                Id = module.Id,
                Nom = module.Nom,
                NiveauScolaireId = module.NiveauScolaireId,
                NiveauScolaireName = module.NiveauScolaire?.Nom,
                ChapitreIds = module.Chapitres.Select(c => c.Id).ToList(),
            };
        }

        private Module MapToEntity(ModuleDto moduleDto)
        {
            return new Module
            {
                Id = moduleDto.Id,
                Nom = moduleDto.Nom,
                NiveauScolaireId = moduleDto.NiveauScolaireId,
            };
        }

        private void UpdateEntityFromDto(Module module, ModuleDto moduleDto)
        {
            module.Nom = moduleDto.Nom;
            module.NiveauScolaireId = moduleDto.NiveauScolaireId;
        }
    }
}
