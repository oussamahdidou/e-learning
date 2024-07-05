using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.interfaces;
using api.Model;

namespace api.Repository
{
    public class CheckChapterRepository : ICheckChapterRepository
    {
        public Task<List<CheckChapter>> GetStudentAllcheckChapters()
        {
            throw new NotImplementedException();
        }

        public Task<CheckChapter> UpdateCheckChapter()
        {
            throw new NotImplementedException();
        }
    }
}