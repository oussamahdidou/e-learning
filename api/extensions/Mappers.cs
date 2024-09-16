using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Dtos.dashboard;
using api.Dtos.RequiredModules;
using api.Model;

namespace api.extensions
{
    public static class Mappers
    {
        public static RequiredModulesDto RequiredInModulesFromModelToDto(this ModuleRequirement moduleRequirement)
        {
            return new RequiredModulesDto()
            {
                Name = moduleRequirement.TargetModule.Nom,
                Institution = moduleRequirement.TargetModule.NiveauScolaireModules.FirstOrDefault().NiveauScolaire.Institution.Nom,
                Id = moduleRequirement.TargetModule.Id,
                NiveauScolaire = moduleRequirement.TargetModule.NiveauScolaireModules.FirstOrDefault().NiveauScolaire.Nom,
                Seuill = moduleRequirement.Seuill,
            };
        }
        public static RequiredModulesDto RequiredModulesFromModelToDto(this ModuleRequirement moduleRequirement)
        {
            return new RequiredModulesDto()
            {
                Name = moduleRequirement.RequiredModule.Nom,
                Institution = moduleRequirement.RequiredModule.NiveauScolaireModules.FirstOrDefault().NiveauScolaire.Institution.Nom,
                Id = moduleRequirement.RequiredModule.Id,
                NiveauScolaire = moduleRequirement.RequiredModule.NiveauScolaireModules.FirstOrDefault().NiveauScolaire.Nom,
                Seuill = moduleRequirement.Seuill,
            };
        }
        public static GetChaptersDashboardByModuleDto GetChaptersDashboardByModuleFromDtoToModel(this Chapitre chapitre)
        {
            return new GetChaptersDashboardByModuleDto()
            {
                Id = chapitre.Id,
                module = chapitre.Module.Nom,
                Name = chapitre.Nom,
                Number = chapitre.ChapitreNum

            };
        }
        public static GetChapitresForControleDto getChapitresForControleDtoFromModelToDto(this Chapitre chapitre)
        {
            return new GetChapitresForControleDto
            {
                Id = chapitre.Id,
                Name = chapitre.Nom
            };
        }
        public static PendingObjectsDto FromChapitreToPendingObjectsDto(this Chapitre chapitre)
        {
            return new PendingObjectsDto()
            {
                Id = chapitre.Id,
                Nom = chapitre.Nom,
                Type = "Chapitre"
            };
        }
        public static PendingObjectsDto FromControleToPendingObjectsDto(this Controle controle)
        {
            return new PendingObjectsDto()
            {
                Id = controle.Id,
                Nom = controle.Nom,
                Type = "Controle"
            };
        }
        public static PendingObjectsDto FromExamToPendingObjectsDto(this Module module)
        {
            return new PendingObjectsDto()
            {
                Id = module.Id,
                Nom = module.ExamFinal.Nom,
                Type = "Exam"
            };
        }
        public static GetChapitresToUpdateControlesDto FromChapitreToGetChapitresToUpdateControlesDto(this Chapitre chapitre, bool Checked)
        {
            return new GetChapitresToUpdateControlesDto()
            {
                Id = chapitre.Id,
                Name = chapitre.Nom,
                Checked = Checked

            };
        }
        public static PendingObjectsDto FromParagrapheToPendingObjectDto(this Paragraphe paragraphe)
        {
            return new PendingObjectsDto()
            {
                Id = paragraphe.Id,
                Nom = paragraphe.Nom,
                Type = "Paragraphe"

            };
        }
        public static PendingObjectsDto FromVideoToPendingObjectDto(this Video Video)
        {
            return new PendingObjectsDto()
            {
                Id = Video.Id,
                Nom = Video.Nom,
                Type = "Video"

            };
        }
        public static PendingObjectsDto FromSyntheseToPendingObjectDto(this Synthese Synthese)
        {
            return new PendingObjectsDto()
            {
                Id = Synthese.Id,
                Nom = Synthese.Nom,
                Type = "Synthese"

            };
        }
        public static PendingObjectsDto FromSchemaToPendingObjectDto(this Schema Schema)
        {
            return new PendingObjectsDto()
            {
                Id = Schema.Id,
                Nom = Schema.Nom,
                Type = "Schema"

            };
        }
        public static BarChartsDto FromTeacherToBarChartsDto(this Teacher teacher)
        {
            return new BarChartsDto()
            {
                Name = $"{teacher.Nom} {teacher.Prenom}",
                Count = teacher.Chapitres.Count() + teacher.Controles.Count() + teacher.ExamFinals.Count(),
            };
        }
        public static CommentDto FromCommentToCommentDto(this Comment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment), "Comment cannot be null");
            }

            var author = comment.AppUser;

            return new CommentDto()
            {
                Id = comment.Id,
                Author = author?.UserName ?? "Unknown", // Default value if AppUser is null
                Titre = comment.Titre,
                CreatedAt = comment.CreatedAt,
                IsAdminComment = author is Admin,
                AuthorId = author?.Id ?? "Unknown" // Default value if AppUser is null
            };
        }

    }
}