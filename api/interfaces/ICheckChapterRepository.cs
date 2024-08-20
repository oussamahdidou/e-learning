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
        Task<Result<CheckChapter>> CreateCheckChapter(AppUser student , int chapterId , string avis);
        Task<Result<bool>> DeleteCheckChapter(string id, int chapterId);
    }
}