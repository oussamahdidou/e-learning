using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Model;

namespace api.interfaces
{
    public interface IResultControleRepository
    {
        Task<ResultControle> AddResult();
        Task<ResultControle> RemoveResult();
        Task<List<ResultControle>> GetStudentAllResult();
        Task<ResultControle> UpdateResult();

    }
}