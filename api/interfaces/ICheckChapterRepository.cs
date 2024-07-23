using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.generique;
using api.Model;

namespace api.interfaces
{
    public interface ICheckChapterRepository
    {
        Task<Result<List<CheckChapter>>> GetStudentAllcheckChapters(AppUser student);
        Task<Result<CheckChapter>> CreateCheckChapter(string studentId , int chapterId);
        Task<Result<bool>> DeleteCheckChapter(AppUser user, int chapterId);
    }
}