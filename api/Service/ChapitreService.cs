using api.Dtos.Chapitre;
using api.generique; // Assurez-vous d'inclure le namespace correct pour Result<T>
using api.interfaces;
using api.Mappers;

namespace api.Service
{
    public class ChapitreService
    {
        private readonly IChapitreRepository _chapitreRepository;

        public ChapitreService(IChapitreRepository chapitreRepository)
        {
            _chapitreRepository = chapitreRepository;
        }

        public async Task<Result<IEnumerable<ChapitreDto>>> GetAllDtosAsync()
        {
            var chapitres = await _chapitreRepository.GetAllAsync();
            return Result<IEnumerable<ChapitreDto>>.Success(chapitres.Select(ChapitreMapper.MapToDto));
        }

        public async Task<Result<ChapitreDto>> GetDtoByIdAsync(int id)
        {
            var chapitre = await _chapitreRepository.GetByIdAsync(id);
            if (chapitre == null)
            {
                return Result<ChapitreDto>.Failure("Chapitre not found.");
            }
            return Result<ChapitreDto>.Success(ChapitreMapper.MapToDto(chapitre));
        }

        public async Task<Result<ChapitreDto>> AddDtoAsync(ChapitreDto chapitreDto)
        {
            var chapitre = ChapitreMapper.MapToEntity(chapitreDto);
            var addedChapitre = await _chapitreRepository.AddAsync(chapitre);
            return Result<ChapitreDto>.Success(ChapitreMapper.MapToDto(addedChapitre));
        }

        public async Task<Result> UpdateDtoAsync(ChapitreDto chapitreDto)
        {
            var existingChapitre = await _chapitreRepository.GetByIdAsync(chapitreDto.Id);
            if (existingChapitre == null)
            {
                return Result.Failure($"Chapitre with ID {chapitreDto.Id} not found.");
            }

            existingChapitre.ChapitreNum = chapitreDto.ChapitreNum;
            existingChapitre.Nom = chapitreDto.Nom;
            existingChapitre.Statue = chapitreDto.Statue;
            existingChapitre.CoursPdfPath = chapitreDto.CoursPdfPath;
            existingChapitre.VideoPath = chapitreDto.VideoPath;
            existingChapitre.Synthese = chapitreDto.Synthese;
            existingChapitre.Schema = chapitreDto.Schema;
            existingChapitre.Premium = chapitreDto.Premium;
            existingChapitre.QuizId = chapitreDto.QuizId;
            existingChapitre.ModuleId = chapitreDto.ModuleId;
            existingChapitre.ControleId = chapitreDto.ControleId;
            // existingChapitre.CheckChapters = chapitreDto.CheckChapters
            // .Select(cc => new CheckChapter
            // {
            // Id = cc.Id,
            // ChapitreId = cc.ChapitreId,
            // CheckStatus = cc.CheckStatus
            // })
            // .ToList();

            await _chapitreRepository.UpdateAsync(existingChapitre);
            return Result.Success();
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var existingChapitre = await _chapitreRepository.GetByIdAsync(id);
            if (existingChapitre == null)
            {
                return generique.Result.Failure("Chapitre not found.");
            }

            await _chapitreRepository.DeleteAsync(id);
            return Result.Success();
        }
    }
}
