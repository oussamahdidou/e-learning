using api.interfaces;
using api.Model;
using api.Dtos.NiveauScolaires;
using api.Mappers;
using api.generique;

namespace api.Service
{
    public class NiveauScolaireService
    {
        private readonly INiveauScolaireRepository _niveauScolaireRepository;

        public NiveauScolaireService(INiveauScolaireRepository niveauScolaireRepository)
        {
            _niveauScolaireRepository = niveauScolaireRepository;
        }

        public async Task<Result<IEnumerable<NiveauScolaireDto>>> GetAllDtosAsync()
        {
            var niveauScolaires = await _niveauScolaireRepository.GetAllAsync();
            var dtos = niveauScolaires.Select(NiveauScolaireMapper.MapToDto);
            return Result<IEnumerable<NiveauScolaireDto>>.Success(dtos);
        }

        public async Task<Result<NiveauScolaireDto>> GetDtoByIdAsync(int id)
        {
            var niveauScolaire = await _niveauScolaireRepository.GetByIdAsync(id);
            if (niveauScolaire == null)
            {
                return Result<NiveauScolaireDto>.Failure($"NiveauScolaire with ID {id} not found.");
            }
            var dto = NiveauScolaireMapper.MapToDto(niveauScolaire);
            return Result<NiveauScolaireDto>.Success(dto);
        }

        public async Task<Result<NiveauScolaireDto>> AddDtoAsync(NiveauScolaireDto niveauScolaireDto)
        {
            var niveauScolaire = NiveauScolaireMapper.MapToEntity(niveauScolaireDto);
            var addedNiveauScolaire = await _niveauScolaireRepository.AddAsync(niveauScolaire);
            var dto = NiveauScolaireMapper.MapToDto(addedNiveauScolaire);
            return Result<NiveauScolaireDto>.Success(dto);
        }

        public async Task<Result> UpdateDtoAsync(NiveauScolaireDto niveauScolaireDto)
        {
            var existingNiveauScolaire = await _niveauScolaireRepository.GetByIdAsync(niveauScolaireDto.Id);
            if (existingNiveauScolaire == null)
            {
                return Result.Failure($"NiveauScolaire with ID {niveauScolaireDto.Id} not found.");
            }

            existingNiveauScolaire.Nom = niveauScolaireDto.Nom;
            existingNiveauScolaire.InstitutionId = niveauScolaireDto.InstitutionId;
            existingNiveauScolaire.Modules = niveauScolaireDto.Modules
                .Select(m => new Module
                {
                    Id = m.Id,
                    Nom = m.Nom,
                })
                .ToList();

            await _niveauScolaireRepository.UpdateAsync(existingNiveauScolaire);
            return Result.Success();
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var existingNiveauScolaire = await _niveauScolaireRepository.GetByIdAsync(id);
            if (existingNiveauScolaire == null)
            {
                return Result.Failure($"NiveauScolaire with ID {id} not found.");
            }

            await _niveauScolaireRepository.DeleteAsync(id);
            return Result.Success();
        }
    }
}
