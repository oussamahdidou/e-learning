using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class apiDbContext : IdentityDbContext<AppUser>
    {
        public apiDbContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {

        }
        public DbSet<Admin> admins { get; set; }
        public DbSet<Teacher> teachers { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<Institution> institutions { get; set; }
        public DbSet<NiveauScolaire> niveauScolaires { get; set; }
        public DbSet<NiveauScolaireModule> niveauScolaireModules { get; set; }
        public DbSet<Module> modules { get; set; }
        public DbSet<Chapitre> chapitres { get; set; }
        public DbSet<Quiz> quizzes { get; set; }
        public DbSet<Question> questions { get; set; }
        public DbSet<Option> options { get; set; }
        public DbSet<Controle> controles { get; set; }
        public DbSet<QuizResult> quizResults { get; set; }
        public DbSet<TestNiveau> testNiveaus { get; set; }
        public DbSet<CheckChapter> checkChapters { get; set; }
        public DbSet<ResultControle> resultControles { get; set; }
        public DbSet<ModuleRequirement> moduleRequirements { get; set; }
        public DbSet<ResultExam> resultExams { get; set; }
        public DbSet<ExamFinal> examFinals { get; set; }
        public DbSet<Cours> cours { get; set; }
        public DbSet<Paragraphe> paragraphes { get; set; }
        public DbSet<ElementPedagogique> elementPedagogiques { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<Poste> postes { get; set; }
        public DbSet<Video> videos { get; set; }
        public DbSet<Synthese> syntheses { get; set; }
        public DbSet<Schema> schemas { get; set; }
        public DbSet<InstitutionStudent> institutionStudents { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            List<IdentityRole> Roles = new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Name="Admin",
                    NormalizedName="ADMIN"
                },
                new IdentityRole()
                {
                    Name="Teacher",
                    NormalizedName="TEACHER"
                },
                new IdentityRole()
                {
                    Name="Student",
                    NormalizedName="STUDENT"
                }

            };


            builder.Entity<ModuleRequirement>(x => x.HasKey(p => new { p.RequiredModuleId, p.TargetModuleId }));

            builder.Entity<ModuleRequirement>()
                .HasOne(u => u.RequiredModule)
                .WithMany(u => u.ModulesRequiredIn)
                .HasForeignKey(p => p.RequiredModuleId)
                .OnDelete(DeleteBehavior.Restrict); // No cascading delete

            builder.Entity<ModuleRequirement>()
                .HasOne(u => u.TargetModule)
                .WithMany(u => u.ModuleRequirements)
                .HasForeignKey(p => p.TargetModuleId)
                .OnDelete(DeleteBehavior.Restrict); // No cascading delete

            //**********************************************************************************************
            builder.Entity<QuizResult>(x => x.HasKey(p => new { p.StudentId, p.QuizId }));
            builder.Entity<QuizResult>()
            .HasOne(u => u.Student)
            .WithMany(u => u.QuizResults)
            .HasForeignKey(p => p.StudentId);
            builder.Entity<QuizResult>()
            .HasOne(u => u.Quiz)
            .WithMany(u => u.QuizResults)
            .HasForeignKey(p => p.QuizId);
            //**********************************************************************************************
            builder.Entity<ResultControle>(x => x.HasKey(p => new { p.StudentId, p.ControleId }));
            builder.Entity<ResultControle>()
            .HasOne(u => u.Student)
            .WithMany(u => u.ResultControles)
            .HasForeignKey(p => p.StudentId);
            builder.Entity<ResultControle>()
            .HasOne(u => u.Controle)
            .WithMany(u => u.ResultControles)
            .HasForeignKey(p => p.ControleId);
            //**********************************************************************************************
            builder.Entity<CheckChapter>(x => x.HasKey(p => new { p.StudentId, p.ChapitreId }));
            builder.Entity<CheckChapter>()
            .HasOne(u => u.Student)
            .WithMany(u => u.CheckChapters)
            .HasForeignKey(p => p.StudentId);
            builder.Entity<CheckChapter>()
            .HasOne(u => u.Chapitre)
            .WithMany(u => u.CheckChapters)
            .HasForeignKey(p => p.ChapitreId);
            //************************************************************************************************
            builder.Entity<TestNiveau>(x => x.HasKey(p => new { p.StudentId, p.ModuleId }));
            builder.Entity<TestNiveau>()
            .HasOne(u => u.Student)
            .WithMany(u => u.TestNiveaus)
            .HasForeignKey(p => p.StudentId);
            builder.Entity<TestNiveau>()
            .HasOne(u => u.Module)
            .WithMany(u => u.TestNiveaus)
            .HasForeignKey(p => p.ModuleId);
            builder.Entity<IdentityRole>().HasData(Roles);
            //*************************************************************************************************
            builder.Entity<ResultExam>(x => x.HasKey(p => new { p.StudentId, p.ExamFinalId }));
            builder.Entity<ResultExam>()
            .HasOne(u => u.Student)
            .WithMany(u => u.ResultExams)
            .HasForeignKey(p => p.StudentId);
            builder.Entity<ResultExam>()
            .HasOne(u => u.ExamFinal)
            .WithMany(u => u.ResultExams)
            .HasForeignKey(p => p.ExamFinalId);
            //*************************************************************************************************
            builder.Entity<NiveauScolaireModule>(x => x.HasKey(p => new { p.ModuleId, p.NiveauScolaireId }));
            builder.Entity<NiveauScolaireModule>()
            .HasOne(u => u.Module)
            .WithMany(u => u.NiveauScolaireModules)
            .HasForeignKey(p => p.ModuleId);
            builder.Entity<NiveauScolaireModule>()
            .HasOne(u => u.NiveauScolaire)
            .WithMany(u => u.NiveauScolaireModules)
            .HasForeignKey(p => p.NiveauScolaireId);
            //**************************************************************************************************
            builder.Entity<Comment>(x =>
                      {
                          x.HasKey(p => p.Id); // Set Id as the primary key
                          x.Property(p => p.Id).ValueGeneratedOnAdd(); // Configure Id to be auto-generated
                          x.HasOne(u => u.AppUser)
                              .WithMany(u => u.Comments)
                              .HasForeignKey(p => p.AppUserId);
                          x.HasOne(u => u.Poste)
                              .WithMany(u => u.Comments)
                              .HasForeignKey(p => p.PosteId);
                      });
            //*************************************************************************************************
            builder.Entity<InstitutionStudent>(x => x.HasKey(p => new { p.InstitutionId, p.StudentId }));
            builder.Entity<InstitutionStudent>()
            .HasOne(u => u.Institution)
            .WithMany(u => u.InstitutionStudents)
            .HasForeignKey(p => p.InstitutionId);
            builder.Entity<InstitutionStudent>()
            .HasOne(u => u.Student)
            .WithMany(u => u.InstitutionStudents)
            .HasForeignKey(p => p.StudentId);
        }
    }
}