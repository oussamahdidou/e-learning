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

                List<GetChapitresForControleDto> chapitres = await apiDbContext.chapitres.Where(x => x.ModuleId == id).Select(x => x.getChapitresForControleDtoFromModelToDto()).ToListAsync();
                return Result<List<GetChapitresForControleDto>>.Success(chapitres);

            }
            catch (Exception ex)
            {

                return Result<List<GetChapitresForControleDto>>.Failure(ex.Message);
            }
        }

        public async Task<Result<Chapitre>> GetDashboardChapiter(int id)
        {
            Chapitre? chapitre = await apiDbContext.chapitres.Include(x => x.Quiz).ThenInclude(x => x.Questions).ThenInclude(x => x.Options).FirstOrDefaultAsync(x => x.Id == id);
            if (chapitre == null)
            {
                return Result<Chapitre>.Failure("chapitre not found");
            }
            return Result<Chapitre>.Success(chapitre);

        }

        public async Task<Result<ObjectsDto>> GetObjectspourApprouver()
        {
            try
            {
                List<Chapitre> chapitres = await apiDbContext.chapitres.Where(x => x.Statue == ObjectStatus.Pending).ToListAsync();
                List<Controle> controles = await apiDbContext.controles.Where(x => x.Status == ObjectStatus.Pending).ToListAsync();

                return Result<ObjectsDto>.Success(new ObjectsDto()
                {
                    chapitres = chapitres,
                    controles = controles
                });
            }
            catch (System.Exception ex)
            {

                return Result<ObjectsDto>.Failure(ex.Message);
            }
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
    }
}