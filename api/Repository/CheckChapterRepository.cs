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
                var checkChapters = await _context.checkChapters.Where(x => x.StudentId == student.Id)
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

        public async Task<Result<CheckChapter>> CreateCheckChapter(AppUser user , int chapterId)
        {
            var chapter = await _context.chapitres.FindAsync(chapterId);
            if(chapter == null) return Result<CheckChapter>.Failure("chapter not found");
            var student = await _context.students.FindAsync(user.Id);
            if(student == null) return Result<CheckChapter>.Failure("Student not found");
            var checkChapter = new CheckChapter
            {
                StudentId = student.Id,
                ChapitreId = chapterId,
                Chapitre = chapter,
                Student = student,
            };
            try{
                await _context.checkChapters.AddAsync(checkChapter);
                await _context.SaveChangesAsync();
                return Result<CheckChapter>.Success(checkChapter);
            }catch(Exception ex){
                return Result<CheckChapter>.Failure("Something went wrong");
            }
        }

        public async Task<Result<bool>> DeleteCheckChapter(AppUser user, int chapterId)
        {
            var checkChapter = await _context.checkChapters
                .FirstOrDefaultAsync(cc => cc.StudentId == user.Id && cc.ChapitreId == chapterId);
            
            if (checkChapter == null) return Result<bool>.Failure("CheckChapter not found");

            var student = await _context.students.FindAsync(user.Id);
            if(student == null) return Result<bool>.Failure("Student not found");

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