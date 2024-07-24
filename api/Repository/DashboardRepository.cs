using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.dashboard;
using api.extensions;
using api.generique;
using api.interfaces;
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
    }
}