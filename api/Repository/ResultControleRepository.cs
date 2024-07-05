using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.interfaces;
using api.Model;

namespace api.Repository
{
    public class ResultControleRepository : IResultControleRepository
    {
        public Task<ResultControle> AddResult()
        {
            throw new NotImplementedException();
        }

        public Task<List<ResultControle>> GetStudentAllResult()
        {
            throw new NotImplementedException();
        }

        public Task<ResultControle> RemoveResult()
        {
            throw new NotImplementedException();
        }

        public Task<ResultControle> UpdateResult()
        {
            throw new NotImplementedException();
        }
    }
}