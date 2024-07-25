using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.interfaces;
using api.Model;
using api.Data;
using api.generique;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CheckChapterRepository : ICheckChapterRepository
    {
        private readonly apiDbContext _context;
        public CheckChapterRepository(apiDbContext context)
        {
            _context = context;
        }
        public async Task<Result<List<CheckChapter>>> GetStudentAllcheckChapters(AppUser student)
        {
            try
            {
                List<CheckChapter> checkChapters = await _context.checkChapters.Where(x => x.StudentId == student.Id)
                .Select(checkChapitre => new CheckChapter
                {
                    StudentId = student.Id,
                    ChapitreId = checkChapitre.ChapitreId,
                    Student = checkChapitre.Student,
                    Chapitre = checkChapitre.Chapitre
                }).ToListAsync();

                return Result<List<CheckChapter>>.Success(checkChapters);
            }
            catch (Exception ex)
            {
                return Result<List<CheckChapter>>.Failure("Non checked Chaptre Found");
            }
        }

        public async Task<Result<CheckChapter>> CreateCheckChapter(string studentId, int chapterId)
        {
            if (await _context.checkChapters.AnyAsync(x => x.ChapitreId == chapterId && x.StudentId == studentId)) return Result<CheckChapter>.Success(new CheckChapter());
            Chapitre? chapter = await _context.chapitres.FindAsync(chapterId);
            if (chapter == null) return Result<CheckChapter>.Failure("chapter not found");
            Student? student = await _context.students.FindAsync(studentId);
            if (student == null) return Result<CheckChapter>.Failure("Student not found");
            CheckChapter checkChapter = new CheckChapter
            {
                StudentId = student.Id,
                ChapitreId = chapterId,
            };
            try
            {
                await _context.checkChapters.AddAsync(checkChapter);
                await _context.SaveChangesAsync();
                return Result<CheckChapter>.Success(checkChapter);
            }
            catch (Exception ex)
            {
                return Result<CheckChapter>.Failure("Something went wrong");
            }
        }

        public async Task<Result<bool>> DeleteCheckChapter(string id, int chapterId)
        {
            CheckChapter? checkChapter = await _context.checkChapters
                .FirstOrDefaultAsync(cc => cc.StudentId == id && cc.ChapitreId == chapterId);

            if (checkChapter == null) return Result<bool>.Failure("CheckChapter not found");

            try
            {
                _context.checkChapters.Remove(checkChapter);
                await _context.SaveChangesAsync();
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure("Something went wrong");
            }
        }
    }
}