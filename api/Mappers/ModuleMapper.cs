

using api.Dtos.Chapitre;
using api.Dtos.Control;
using api.Dtos.Module;
using api.Model;

namespace api.Mappers
{
    public static class ModuleMapper
    {
        public static ModuleDto toModuleDto(this Module module, IEnumerable<int> checkedChapters)
        {
            return new ModuleDto
            {
                Id = module.Id,
                Nom = module.Nom,
                Chapitres = module.Chapitres.Select(c => new ChapitreDto
                {
                    Id = c.Id,
                    ChapitreNum = c.ChapitreNum,
                    Nom = c.Nom,
                    Statue = checkedChapters.Contains(c.Id),
                    StudentCoursParagraphes = c.Cours
                        .Where(cour => cour.Type == "Student")  // Filter Cours by Type "Etudiant"
                        .SelectMany(cour => cour.Paragraphes)    // Flatten the list of Paragraphes
                        .Select(p => p?.Id)                  // Select only the Contenu of each Paragraphe
                        .ToList(),
                    VideoPath = c.VideoPath,
                    Synthese = c.Synthese,
                    Schema = c.Schema,
                    Premium = c.Premium,
                    Quiz = c.Quiz.ToQuizDto()
                }).ToList(),
                Controles = module.Chapitres
                   .Select(c => c.Controle)
                   .Where(ctrl => ctrl != null)
                   .DistinctBy(ctrl => ctrl.Id)
                   .Select(ctrl => new ControleDto
                   {
                       Id = ctrl.Id,
                       Nom = ctrl.Nom,
                       Ennonce = ctrl.Ennonce,
                       Solution = ctrl.Solution,
                       ChapitreNum = ctrl.Chapitres.Select(ch => ch.ChapitreNum).ToList()
                   }).ToList()
            };
        }
    }
}
