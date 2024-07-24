using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.dashboard;
using api.generique;

namespace api.interfaces
{
    public interface IDashboardRepository
    {
        Task<Result<List<GetChapitresForControleDto>>> GetChaptersForControle(int id);
        Task<Result<List<GetChaptersDashboardByModuleDto>>> GetChaptersDashboardbyModule(int id);
    }
}