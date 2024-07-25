using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.dashboard;
using api.generique;
using api.Model;

namespace api.interfaces
{
    public interface IDashboardRepository
    {
        Task<Result<List<GetChapitresForControleDto>>> GetChaptersForControle(int id);
        Task<Result<List<GetChaptersDashboardByModuleDto>>> GetChaptersDashboardbyModule(int id);
        Task<Result<Chapitre>> GetDashboardChapiter(int id);
    }
}