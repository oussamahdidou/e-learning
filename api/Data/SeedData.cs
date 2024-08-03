using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.helpers;
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
        public static void Initialize(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                apiDbContext? context = serviceScope.ServiceProvider.GetService<apiDbContext>();
                if (context.controles.Any() || context.chapitres.Any() || context.institutions.Any())
                {
                    return;   // DB has been seeded
                }
                context.Database.EnsureCreated();

                // Seed your data here
                if (!context.institutions.Any())
                {
                    var institution1 = new Institution
                    {
                        Nom = "School A",
                        NiveauScolaires = new List<NiveauScolaire>
                    {
                        new NiveauScolaire
                        {
                            Nom = "Primary",
                            Modules = new List<Module>
                            {
                                new Module
                                {
                                    Nom = "Mathematics",
                                    Chapitres = new List<Chapitre>
                                    {
                                        new Chapitre
                                        {
                                            Schema="path/to/schema",
                                            Synthese="path/to/synthese",
                                            ChapitreNum=1,
                                            Statue=ObjectStatus.Pending,
                                            VideoPath="/path/to/video",
                                            CoursPdfPath="/path/to/pdf/",
                                            Nom = "Introduction to Addition",
                                            Premium = true,
                                            Quiz = new Quiz
                                            {
                                                Statue=ObjectStatus.Pending,
                                                Nom = "Addition Quiz",
                                                Questions = new List<Question>
                                                {
                                                    new Question
                                                    {
                                                        Nom = "What is 2 + 2?",
                                                        Options = new List<Option>
                                                        {
                                                            new Option { Nom = "3", Truth = false },
                                                            new Option { Nom = "4", Truth = true }
                                                        }
                                                    }
                                                }
                                            },
                                            Controle = new Controle
                                            {
                                                Nom = "Addition Test",
                                                Ennonce = "Solve the following addition problems.",
                                                Solution = "1. 2 + 2 = 4",
                                                Status = ObjectStatus.Pending,
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    };

                    var institution2 = new Institution
                    {
                        Nom = "School B",
                        NiveauScolaires = new List<NiveauScolaire>
                    {
                        new NiveauScolaire
                        {
                            Nom = "High School",
                            Modules = new List<Module>
                            {
                                new Module
                                {
                                    Nom = "Physics",
                                    Chapitres = new List<Chapitre>
                                    {
                                        new Chapitre
                                        {

                                            Nom = "Mechanics",
                                            Premium = true,
                                            Schema="path/to/schema",
                                            Synthese="path/to/synthese",
                                            ChapitreNum=1,
                                            Statue=ObjectStatus.Pending,
                                            VideoPath="/path/to/video",
                                            CoursPdfPath="/path/to/pdf/",
                                            Quiz = new Quiz
                                            {
                                                Statue = ObjectStatus.Pending,
                                                Nom = "Mechanics Quiz",
                                                Questions = new List<Question>
                                                {
                                                    new Question
                                                    {
                                                        Nom = "What is Newton's first law?",
                                                        Options = new List<Option>
                                                        {
                                                            new Option { Nom = "Law of Inertia", Truth = true },
                                                            new Option { Nom = "Law of Gravity", Truth = false }
                                                        }
                                                    }
                                                }
                                            },
                                            Controle = new Controle
                                            {
                                                Nom = "Mechanics Exam",
                                                Ennonce = "Answer the following questions on mechanics.",
                                                Solution = "1. Newton's first law states that an object...",
                                                Status = ObjectStatus.Pending
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    };

                    var institution3 = new Institution
                    {
                        Nom = "School C",
                        NiveauScolaires = new List<NiveauScolaire>
                    {
                        new NiveauScolaire
                        {
                            Nom = "Middle School",
                            Modules = new List<Module>
                            {
                                new Module
                                {
                                    Nom = "History",
                                    Chapitres = new List<Chapitre>
                                    {
                                        new Chapitre
                                        {
                                            Nom = "World War II",
                                            Premium = false,
                                            Schema="path/to/schema",
                                            Synthese="path/to/synthese",
                                            ChapitreNum=1,
                                            Statue=ObjectStatus.Pending,
                                            VideoPath="/path/to/video",
                                            CoursPdfPath="/path/to/pdf/",
                                            Quiz = new Quiz
                                            { Statue=  ObjectStatus.Pending,
                                                Nom = "WWII Quiz",
                                                Questions = new List<Question>
                                                {
                                                    new Question
                                                    {
                                                        Nom = "When did WWII start?",
                                                        Options = new List<Option>
                                                        {
                                                            new Option { Nom = "1939", Truth = true },
                                                            new Option { Nom = "1945", Truth = false }
                                                        }
                                                    }
                                                }
                                            },
                                            Controle = new Controle
                                            {
                                                Nom = "World War II Test",
                                                Ennonce = "Answer the following questions about World War II.",
                                                Solution = "1. World War II began in 1939...",
                                                Status = ObjectStatus.Pending,
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    };

                    context.institutions.AddRange(institution1, institution2, institution3);
                    context.SaveChanges();
                }
            }
        }
    }
}
