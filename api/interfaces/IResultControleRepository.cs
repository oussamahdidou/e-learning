using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.generique;
using api.Model;

namespace api.interfaces
{
    public interface IResultControleRepository
    {
        Task<Result<ResultControle>> AddResult(AppUser user , int controleId , string filePath);
        Task<Result<ResultControle>> RemoveResult(AppUser user , int controleId);
        Task<Result<List<ResultControle>>> GetStudentAllResult(AppUser user);
        Task<Result<ResultControle>> GetResultControleById(AppUser user, int controleId);

    }
}