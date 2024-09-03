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
                    Controle? controle = await _context.controles.FirstOrDefaultAsync(c => c.Id == ResultControle.ControleId);
                    try
                    {
                        string message = "Vous avez vérifié le chapitre avant le contrôle " + controle.Nom + "  et vous n'avez pas encore ajouté vos devoirs.";
                        await _mailer.SendEmailAsync("belkhiriyoussef33@gmail.com", "Reminder", message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error sending email: {ex.Message}");
                    }
                }
            }
        }
    }
}