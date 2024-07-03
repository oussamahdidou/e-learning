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
        // public DbSet<Doctor> doctors { get; set; }
        // public DbSet<LicenseDoctor> licenseDoctors { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // List<IdentityRole> Roles = new List<IdentityRole>()
            // {
            //     new IdentityRole()
            //     {
            //         Name="Admin",
            //         NormalizedName="ADMIN"
            //     },
            //     new IdentityRole()
            //     {
            //         Name="User",
            //         NormalizedName="User"
            //     }
            // };
            // List<Country> countries = new List<Country>()
            // {
            //     new Country(){
            //         Id=1,
            //         Name="Morocco"
            //     },
            //     new Country(){
            //         Id=2,
            //         Name="France",
            //     }
            // };
            // List<Ville> villes = new List<Ville>(){
            //     new Ville(){
            //         Id=1,
            //         Name="Casablanca",
            //         CountryId=1
            //     },
            //     new Ville(){
            //         Id=2,
            //         Name="Marrakech",
            //         CountryId=1
            //     },
            //     new Ville(){
            //          Id=3,
            //         Name="Errachidia",
            //         CountryId=1
            //     },
            // };

            // builder.Entity<Rating>(x => x.HasKey(p => new { p.AppUserId, p.EntrepriseId }));
            // builder.Entity<Rating>()
            // .HasOne(u => u.AppUser)
            // .WithMany(u => u.Ratings)
            // .HasForeignKey(p => p.AppUserId);
            // builder.Entity<Rating>()
            // .HasOne(u => u.Entreprise)
            // .WithMany(u => u.Ratings)
            // .HasForeignKey(p => p.EntrepriseId);
            // builder.Entity<Comment>(x =>
            // {
            //     x.HasKey(p => p.Id); // Set Id as the primary key
            //     x.Property(p => p.Id).ValueGeneratedOnAdd(); // Configure Id to be auto-generated
            //     x.HasOne(u => u.AppUser)
            //         .WithMany(u => u.Comments)
            //         .HasForeignKey(p => p.AppUserId);
            //     x.HasOne(u => u.Entreprise)
            //         .WithMany(u => u.Comments)
            //         .HasForeignKey(p => p.EntrepriseId);
            // });


            // builder.Entity<IdentityRole>().HasData(Roles);
            // builder.Entity<Country>().HasData(countries);
            // builder.Entity<Ville>().HasData(villes);
        }
    }
}