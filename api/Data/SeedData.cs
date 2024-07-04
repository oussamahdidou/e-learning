using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace api.Data
{
    public static class SeedData
    {
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // Roles
                string[] roleNames = { UserRoles.Admin, UserRoles.Student, UserRoles.Teacher };
                foreach (var roleName in roleNames)
                {
                    if (!await roleManager.RoleExistsAsync(roleName))
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                // Admin
                string adminEmail = "admin115@gmail.com";
                if (await userManager.FindByEmailAsync(adminEmail) == null)
                {
                    var newAdmin = new Admin()
                    {
                        UserName = adminEmail.Split('@')[0],
                        Email = adminEmail,
                        EmailConfirmed = true,
                    };
                    await userManager.CreateAsync(newAdmin, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdmin, UserRoles.Admin);
                }

                // Teachers
                var teacherEmails = new List<string>
                {
                    "teacher115@gmail.com",
                    "teacher215@gmail.com",
                    "teacher315@gmail.com"
                };

                for (int i = 0; i < teacherEmails.Count; i++)
                {
                    if (await userManager.FindByEmailAsync(teacherEmails[i]) == null)
                    {
                        var newTeacher = new Teacher()
                        {
                            UserName = teacherEmails[i].Split('@')[0],
                            Email = teacherEmails[i],
                            EmailConfirmed = true,
                            Granted = i == 0 // Only the first teacher will have Granted access set to true
                        };
                        await userManager.CreateAsync(newTeacher, "Coding@1234?");
                        await userManager.AddToRoleAsync(newTeacher, UserRoles.Teacher);
                    }
                }

                // Students
                var studentEmails = new List<string>
                {
                    "student115@gmail.com",
                    "student215@gmail.com",
                    "student315@gmail.com"
                };

                foreach (var studentEmail in studentEmails)
                {
                    if (await userManager.FindByEmailAsync(studentEmail) == null)
                    {
                        var newStudent = new Student()
                        {
                            UserName = studentEmail.Split('@')[0],
                            Email = studentEmail,
                            EmailConfirmed = true,
                        };
                        await userManager.CreateAsync(newStudent, "Coding@1234?");
                        await userManager.AddToRoleAsync(newStudent, UserRoles.Student);
                    }
                }
            }
        }
    }
}
