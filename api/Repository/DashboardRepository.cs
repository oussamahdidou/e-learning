using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.dashboard;
using api.extensions;
using api.generique;
using api.helpers;
using api.interfaces;
using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly apiDbContext apiDbContext;
        public DashboardRepository(apiDbContext apiDbContext)
        {
            this.apiDbContext = apiDbContext;
        }
        public async Task<Result<List<GetChapitresToUpdateControlesDto>>> GetChapitresToUpdateControles(int id)
        {
            try
            {
                Controle? controle = await apiDbContext.controles.Include(x => x.Chapitres).FirstOrDefaultAsync(x => x.Id == id);
                if (controle == null)
                {
                    return Result<List<GetChapitresToUpdateControlesDto>>.Failure("controlenotfound");
                }
                List<Chapitre> controleschapitres = controle.Chapitres;
                int ModuleId = controleschapitres.First().ModuleId;
                List<GetChapitresToUpdateControlesDto> checkedchapters = new List<GetChapitresToUpdateControlesDto>();
                List<Chapitre> modulechapitres = await apiDbContext.chapitres.Where(x => x.ModuleId == ModuleId && (x.ControleId == null || x.ControleId == id)).ToListAsync();
                foreach (Chapitre chapitre in modulechapitres)
                {
                    if (controleschapitres.Any(x => x.Id == chapitre.Id))
                    {
                        checkedchapters.Add(chapitre.FromChapitreToGetChapitresToUpdateControlesDto(true));
                    }
                    else
                    {
                        checkedchapters.Add(chapitre.FromChapitreToGetChapitresToUpdateControlesDto(false));

                    }
                }
                return Result<List<GetChapitresToUpdateControlesDto>>.Success(checkedchapters);
            }
            catch (System.Exception ex)
            {

                return Result<List<GetChapitresToUpdateControlesDto>>.Failure(ex.Message);
            }
        }

        public async Task<Result<List<GetChaptersDashboardByModuleDto>>> GetChaptersDashboardbyModule(int id)
        {
            try
            {

                List<GetChaptersDashboardByModuleDto> chapitres = await apiDbContext.chapitres.Include(x => x.Module).Where(x => x.ModuleId == id).Select(x => x.GetChaptersDashboardByModuleFromDtoToModel()).ToListAsync();
                return Result<List<GetChaptersDashboardByModuleDto>>.Success(chapitres);

            }
            catch (System.Exception ex)
            {

                return Result<List<GetChaptersDashboardByModuleDto>>.Failure(ex.Message);
            }
        }

        public async Task<Result<List<GetChapitresForControleDto>>> GetChaptersForControle(int id)
        {
            try
            {

                List<GetChapitresForControleDto> chapitres = await apiDbContext.chapitres.Where(x => x.ModuleId == id && x.ControleId == null).Select(x => x.getChapitresForControleDtoFromModelToDto()).ToListAsync();
                return Result<List<GetChapitresForControleDto>>.Success(chapitres);

            }
            catch (Exception ex)
            {

                return Result<List<GetChapitresForControleDto>>.Failure(ex.Message);
            }
        }

        public async Task<Result<Chapitre>> GetDashboardChapiter(int id)
        {
            // Step 1: Load the main entity (Chapitre)
            Chapitre? chapitre = await apiDbContext.chapitres.Include(x => x.Quiz).ThenInclude(q => q.Questions) // Load Questions with Quiz
                    .ThenInclude(q => q.Options)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (chapitre == null)
            {
                return Result<Chapitre>.Failure("chapitre not found");
            }

            // Step 2: Load related entities in separate queries
            chapitre.Videos = await apiDbContext.videos
                .Where(v => v.ChapitreId == id)
                .ToListAsync();

            chapitre.Syntheses = await apiDbContext.syntheses
                .Where(s => s.ChapitreId == id)
                .ToListAsync();

            chapitre.Schemas = await apiDbContext.schemas
                .Where(s => s.ChapitreId == id)
                .ToListAsync();

            // Load Cours and related Paragraphes separately
            chapitre.Cours = await apiDbContext.cours
                .Where(c => c.ChapitreId == id)
                .Include(c => c.Paragraphes) // Load Paragraphes with Cours
                .ToListAsync();

            // Load Quiz and related Questions and Options separately


            return Result<Chapitre>.Success(chapitre);
        }

        public async Task<Result<List<BarChartsDto>>> GetLeastCheckedModules()
        {
            var top5ModulesWithCheckCounts = await apiDbContext.modules
                                    .Select(m => new
                                    {
                                        ModuleName = m.Nom,
                                        CheckCount = m.Chapitres.SelectMany(c => c.CheckChapters).Count()
                                    })
                                            .OrderBy(x => x.CheckCount)
                                            .Take(5)
                                            .ToListAsync();

            List<BarChartsDto> chartsdata = top5ModulesWithCheckCounts
                .Select(x => new BarChartsDto
                {
                    Name = x.ModuleName,
                    Count = x.CheckCount
                })
                .ToList();

            return Result<List<BarChartsDto>>.Success(chartsdata);

        }

        public async Task<Result<List<BarChartsDto>>> GetMostCheckedModules()
        {
            var top5ModulesWithCheckCounts = await apiDbContext.modules
                        .Select(m => new
                        {
                            ModuleName = m.Nom,
                            CheckCount = m.Chapitres.SelectMany(c => c.CheckChapters).Count()
                        })
                                .OrderByDescending(x => x.CheckCount)
                                .Take(5)
                                .ToListAsync();

            List<BarChartsDto> chartsdata = top5ModulesWithCheckCounts
                .Select(x => new BarChartsDto
                {
                    Name = x.ModuleName,
                    Count = x.CheckCount
                })
                .ToList();

            return Result<List<BarChartsDto>>.Success(chartsdata);


        }

        public async Task<Result<List<PendingObjectsDto>>> GetObjectspourApprouver()
        {
            try
            {
                List<PendingObjectsDto> chapitres = await apiDbContext.chapitres.Where(x => x.Statue == ObjectStatus.Pending).Select(x => x.FromChapitreToPendingObjectsDto()).ToListAsync();
                List<PendingObjectsDto> controles = await apiDbContext.controles.Where(x => x.Status == ObjectStatus.Pending).Select(x => x.FromControleToPendingObjectsDto()).ToListAsync();
                List<PendingObjectsDto> exams = await apiDbContext.modules.Include(x => x.ExamFinal).Where(x => x.ExamFinal.Status == ObjectStatus.Pending).Select(x => x.FromExamToPendingObjectsDto()).ToListAsync();
                return Result<List<PendingObjectsDto>>.Success(chapitres.Concat(controles).Concat(exams).ToList());
            }
            catch (System.Exception ex)
            {

                return Result<List<PendingObjectsDto>>.Failure(ex.Message);
            }
        }

        public async Task<Result<StatsDto>> GetStats()
        {
            try
            {
                List<Student> students = await apiDbContext.students.ToListAsync();
                List<Teacher> teachers = await apiDbContext.teachers.ToListAsync();

                List<Chapitre> chapitres = await apiDbContext.chapitres.Where(x => x.Statue == ObjectStatus.Pending).ToListAsync();
                List<Controle> controles = await apiDbContext.controles.Where(x => x.Status == ObjectStatus.Pending).ToListAsync();
                List<ExamFinal> Exams = await apiDbContext.examFinals.Where(x => x.Status == ObjectStatus.Pending).ToListAsync();
                return Result<StatsDto>.Success(new StatsDto()
                {
                    CoursesNmbr = (int)teachers.Average(x => x.ChapterProgress),
                    StudentNmbr = students.Count,
                    TeacherNmbr = teachers.Count,
                    UnapprouvedNmbr = chapitres.Count + controles.Count + Exams.Count,
                });
            }
            catch (System.Exception ex)
            {

                return Result<StatsDto>.Failure(ex.Message);
            }

        }

        public async Task<Result<List<PendingObjectsDto>>> GetTeacherObjects(string TeacherId)
        {
            try
            {
                List<PendingObjectsDto> chapitres = await apiDbContext.chapitres.Where(x => x.TeacherId == TeacherId).Select(x => x.FromChapitreToPendingObjectsDto()).ToListAsync();
                List<PendingObjectsDto> controles = await apiDbContext.controles.Where(x => x.TeacherId == TeacherId).Select(x => x.FromControleToPendingObjectsDto()).ToListAsync();
                List<PendingObjectsDto> exams = await apiDbContext.modules.Include(x => x.ExamFinal).Where(x => x.ExamFinal.TeacherId == TeacherId).Select(x => x.FromExamToPendingObjectsDto()).ToListAsync();
                return Result<List<PendingObjectsDto>>.Success(chapitres.Concat(controles).Concat(exams).ToList());
            }
            catch (System.Exception ex)
            {

                return Result<List<PendingObjectsDto>>.Failure(ex.Message);
            }
        }

        public async Task<Result<List<BarChartsDto>>> GetTopTestNiveauModules()
        {
            var top5ModulesWithAverageNotes = await apiDbContext.modules
                           .GroupJoin(apiDbContext.testNiveaus,
                                      m => m.Id,
                                      tn => tn.ModuleId,
                                      (m, tn) => new { Module = m, TestNiveaux = tn })
                           .Select(g => new
                           {
                               ModuleName = g.Module.Nom,
                               AverageNote = g.TestNiveaux.Any() ? g.TestNiveaux.Average(tn => tn.Note) : 0
                           })
                           .OrderByDescending(x => x.AverageNote)
                           .Take(5)
                           .ToListAsync();

            List<BarChartsDto> chartsdata = top5ModulesWithAverageNotes
                .Select(x => new BarChartsDto
                {
                    Name = x.ModuleName,
                    Count = (int)Math.Round(x.AverageNote)
                })
                .ToList();

            return Result<List<BarChartsDto>>.Success(chartsdata);
        }

        public async Task<Result<List<BarChartsDto>>> GetWorstTestNiveauModules()
        {
            var top5ModulesWithAverageNotes = await apiDbContext.modules
                           .GroupJoin(apiDbContext.testNiveaus,
                                      m => m.Id,
                                      tn => tn.ModuleId,
                                      (m, tn) => new { Module = m, TestNiveaux = tn })
                           .Select(g => new
                           {
                               ModuleName = g.Module.Nom,
                               AverageNote = g.TestNiveaux.Any() ? g.TestNiveaux.Average(tn => tn.Note) : 0
                           })
                           .OrderBy(x => x.AverageNote)
                           .Take(5)
                           .ToListAsync();

            List<BarChartsDto> chartsdata = top5ModulesWithAverageNotes
                .Select(x => new BarChartsDto
                {
                    Name = x.ModuleName,
                    Count = (int)Math.Round(x.AverageNote)
                })
                .ToList();

            return Result<List<BarChartsDto>>.Success(chartsdata);

        }

        public async Task<Result<Teacher>> GrantTeacherAccess(string id)
        {
            try
            {
                Teacher? teacher = await apiDbContext.teachers.FirstOrDefaultAsync(x => x.Id == id);
                if (teacher == null)
                {
                    return Result<Teacher>.Failure("teacher not found");
                }
                teacher.Granted = true;
                await apiDbContext.SaveChangesAsync();
                return Result<Teacher>.Success(teacher);
            }
            catch (System.Exception ex)
            {

                return Result<Teacher>.Failure(ex.Message);

            }

        }

        public async Task<Result<Teacher>> RemoveGrantTeacherAccess(string id)
        {
            try
            {
                Teacher? teacher = await apiDbContext.teachers.FirstOrDefaultAsync(x => x.Id == id);
                if (teacher == null)
                {
                    return Result<Teacher>.Failure("teacher not found");
                }
                teacher.Granted = false;
                await apiDbContext.SaveChangesAsync();
                return Result<Teacher>.Success(teacher);
            }
            catch (System.Exception ex)
            {

                return Result<Teacher>.Failure(ex.Message);

            }
        }

        public async Task<bool> TeacherProgress(string TeacherId, NewProgress newProgress)
        {
            Teacher? teacher = await apiDbContext.teachers.FirstOrDefaultAsync(x => x.Id == TeacherId);
            if (teacher == null)
            {
                return false;
            }
            teacher.LastChapterProgressUpdate = DateTime.Now;
            teacher.ChapterProgress = newProgress.newProgress;
            await apiDbContext.SaveChangesAsync();
            return true;


        }

        public async Task<Result<List<BarChartsDto>>> TopContributerTeachers()
        {
            try
            {
                List<Teacher> teachers = await apiDbContext.teachers.Include(x => x.Chapitres).Include(x => x.Controles).Include(x => x.ExamFinals).ToListAsync();
                List<BarChartsDto> top5Teachers = teachers
                               .OrderByDescending(t => t.Chapitres.Count + t.ExamFinals.Count + t.Controles.Count)
                               .Select(x => x.FromTeacherToBarChartsDto()).Take(5)
                               .ToList();
                return Result<List<BarChartsDto>>.Success(top5Teachers);
            }
            catch (System.Exception ex)
            {

                return Result<List<BarChartsDto>>.Failure(ex.Message);
            }
        }

        public async Task<bool> UpdateControleChapitres(List<GetChapitresToUpdateControlesDto> getChapitresToUpdateControlesDtos, int controleId)
        {
            try
            {
                foreach (GetChapitresToUpdateControlesDto item in getChapitresToUpdateControlesDtos)
                {
                    Chapitre? chapitre = await apiDbContext.chapitres.FirstOrDefaultAsync(x => x.Id == item.Id);
                    if (chapitre == null)
                    {
                        return false;
                    }
                    if (item.Checked)
                    {
                        chapitre.ControleId = controleId;
                    }
                    else
                    {
                        chapitre.ControleId = null;
                    }
                    await apiDbContext.SaveChangesAsync();
                }
                return true;
            }
            catch (System.Exception)
            {

                return false;
            }
        }

        public async Task<Result<List<BarChartsDto>>> WorstContributersTeachers()
        {
            try
            {
                List<Teacher> teachers = await apiDbContext.teachers.Include(x => x.Chapitres).Include(x => x.Controles).Include(x => x.ExamFinals).ToListAsync();
                List<BarChartsDto> top5Teachers = teachers
                               .OrderBy(t => t.Chapitres.Count + t.ExamFinals.Count + t.Controles.Count)
                               .Select(x => x.FromTeacherToBarChartsDto()).Take(5)
                               .ToList();
                return Result<List<BarChartsDto>>.Success(top5Teachers);
            }
            catch (System.Exception ex)
            {

                return Result<List<BarChartsDto>>.Failure(ex.Message);
            }
        }
    }
}