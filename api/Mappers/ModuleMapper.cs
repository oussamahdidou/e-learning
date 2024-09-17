

using api.Dtos.Chapitre;
using api.Dtos.Control;
using api.Dtos.File;
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
                Chapitres = module.Chapitres?.Select(c => new ChapitreDto
                {
                    Id = c.Id,
                    ChapitreNum = c.ChapitreNum,
                    Nom = c.Nom,
                    Statue = checkedChapters.Contains(c.Id),
                    StudentCoursParagraphes = c.Cours?
                        .Where(cour => cour.Type == "Student")
                        .SelectMany(cour => cour.Paragraphes)
                        .Select(p => new FileDto
                        {
                            Id = p.Id,
                            Nom = p.Nom
                        })
                        .ToList() ?? new List<FileDto>(),  // Ensure the list is not null

                    Schemas = c.Schemas?
                        .Select(p => new FileDto
                        {
                            Id = p.Id,
                            Nom = p.Nom
                        }).ToList() ?? new List<FileDto>(),  // Ensure the list is not null

                    Videos = c.Videos?
                        .Select(p => new FileDto
                        {
                            Id = p.Id,
                            Nom = p.Nom
                        }).ToList() ?? new List<FileDto>(),  // Ensure the list is not null

                    Syntheses = c.Syntheses?
                        .Select(p => new FileDto
                        {
                            Id = p.Id,
                            Nom = p.Nom
                        }).ToList() ?? new List<FileDto>(),  // Ensure the list is not null

                    Premium = c.Premium,
                    Quiz = c.Quiz?.ToQuizDto()  // Ensure Quiz is not null
                }).ToList() ?? new List<ChapitreDto>(),  // Ensure the list is not null

                Controles = module.Chapitres?
                    .Select(c => c.Controle)
                    .Where(ctrl => ctrl != null)
                    .DistinctBy(ctrl => ctrl.Id)
                    .Select(ctrl => new ControleDto
                    {
                        Id = ctrl.Id,
                        Nom = ctrl?.Nom,
                        Ennonce = ctrl?.Ennonce,
                        Solution = ctrl?.Solution,
                        ChapitreNum = ctrl?.Chapitres.Select(ch => ch.ChapitreNum).ToList() ?? new List<int>()
                    }).ToList() ?? new List<ControleDto>()  // Ensure the list is not null
            };
        }

    }
}
