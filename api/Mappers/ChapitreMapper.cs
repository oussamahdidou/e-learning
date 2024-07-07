using api.Dtos.Chapitre;
using api.Model;

namespace api.Mappers
{
    public static class ChapitreMapper
    {
        public static ChapitreDto MapToDto(Chapitre chapitre)
        {
            return new ChapitreDto
            {
                Id = chapitre.Id,
                ChapitreNum = chapitre.ChapitreNum,
                Nom = chapitre.Nom,
                Statue = chapitre.Statue,
                CoursPdfPath = chapitre.CoursPdfPath,
                VideoPath = chapitre.VideoPath,
                Synthese = chapitre.Synthese,
                Schema = chapitre.Schema,
                Premium = chapitre.Premium,
                QuizId = chapitre.QuizId,
                ModuleId = chapitre.ModuleId,
                ControleId = chapitre.ControleId,
                //CheckChapters = chapitre.CheckChapters.Select(cc => new CheckChapterDto
                //{
                   // Id = cc.Id,
                    //ChapitreId = cc.ChapitreId,
                    //CheckStatus = cc.CheckStatus
                //}).ToList()
            };
        }

        public static Chapitre MapToEntity(ChapitreDto chapitreDto)
        {
            return new Chapitre
            {
                Id = chapitreDto.Id,
                ChapitreNum = chapitreDto.ChapitreNum,
                Nom = chapitreDto.Nom,
                Statue = chapitreDto.Statue,
                CoursPdfPath = chapitreDto.CoursPdfPath,
                VideoPath = chapitreDto.VideoPath,
                Synthese = chapitreDto.Synthese,
                Schema = chapitreDto.Schema,
                Premium = chapitreDto.Premium,
                QuizId = chapitreDto.QuizId,
                ModuleId = chapitreDto.ModuleId,
                ControleId = chapitreDto.ControleId,
                //CheckChapters = chapitreDto.CheckChapters.Select(cc => new CheckChapter
                //{
                    //Id = cc.Id,
                    //ChapitreId = cc.ChapitreId,
                    //CheckStatus = cc.CheckStatus
                //}).ToList()
            };
        }
    }
}
