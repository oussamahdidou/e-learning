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
        Task<Result<List<PendingObjectsDto>>> GetObjectspourApprouver();
        Task<Result<Teacher>> GrantTeacherAccess(string id);
        Task<Result<Teacher>> RemoveGrantTeacherAccess(string id);
        Task<Result<List<GetChapitresToUpdateControlesDto>>> GetChapitresToUpdateControles(int id);
        Task<bool> UpdateControleChapitres(List<GetChapitresToUpdateControlesDto> getChapitresToUpdateControlesDtos, int controleId);
        Task<Result<List<BarChartsDto>>> GetMostCheckedModules();
        Task<Result<StatsDto>> GetStats();
    }
}