using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.interfaces;
using api.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ReminderRepository : IReminder
    {
        private readonly apiDbContext _context;
        private readonly IMailer _mailer;
        private readonly UserManager<AppUser> _userManager;

        public ReminderRepository(apiDbContext context , IMailer mailer , UserManager<AppUser> userManager)
        {
            _context = context;
            _mailer = mailer;
            _userManager = userManager;
        }
        public async Task SendReminder()
        {
            List<ResultControle> ResultControles = await _context.resultControles.ToListAsync();

            foreach(var ResultControle in ResultControles){
                if(ResultControle.Reponse == ""){
                    Student? student = await _context.students.FirstOrDefaultAsync(s => s.Id == ResultControle.StudentId);
                    string message = "you checked chapter befor controle and you didn't add you homework yet ";
                        await _mailer.SendEmailAsync(student.Email, "Reminder", message);
                }
            }
        }
    }
}