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
        public static async Task Initialize(IApplicationBuilder applicationBuilder)
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
                        Nom = "ENCG",

                        NiveauScolaires = new List<NiveauScolaire>
{
                        new NiveauScolaire
        {
            Nom = "Semestre 1",
            Modules = new List<Module>
            {
                new Module
                {
                    Nom = "Langues et Communication 1",
                    Chapitres = new List<Chapitre>
                    {
                        new Chapitre {
                        ChapitreNum = 1, Nom = "Les Fondamentaux de la Communication", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf",
                        Quiz=new Quiz()
    {
        Nom = "Les Fondamentaux de la Communication",
        Statue = ObjectStatus.Approuver,
        Questions = new List<Question>()
        {
            new Question()
            {
                Nom = "Quel est le but principal de la communication verbale ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Exprimer des émotions uniquement", Truth = false },
                    new Option { Nom = "Échanger des informations", Truth = true },
                    new Option { Nom = "Éviter les malentendus", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est un élément clé de la communication non verbale ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Le ton de la voix", Truth = false },
                    new Option { Nom = "Les expressions faciales", Truth = true },
                    new Option { Nom = "Les mots utilisés", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Qu'est-ce que l'écoute active ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Répondre immédiatement sans réflexion", Truth = false },
                    new Option { Nom = "Écouter sans faire attention aux détails", Truth = false },
                    new Option { Nom = "Écouter attentivement et reformuler ce qui a été dit", Truth = true }
                }
            },
            new Question()
            {
                Nom = "Quel est le rôle du feedback dans la communication ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Ignorer les erreurs", Truth = false },
                    new Option { Nom = "Clarifier les messages échangés", Truth = true },
                    new Option { Nom = "Accélérer la discussion", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle technique est utilisée pour éviter les malentendus ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Utiliser des termes vagues", Truth = false },
                    new Option { Nom = "Clarifier les termes et demander des confirmations", Truth = true },
                    new Option { Nom = "Parler plus vite", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Qu'est-ce qui est essentiel pour une communication efficace en groupe ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "L'écoute des membres du groupe", Truth = true },
                    new Option { Nom = "Imposer ses idées", Truth = false },
                    new Option { Nom = "Réduire les échanges de feedback", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Comment la communication affecte-t-elle les relations interpersonnelles ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Elle renforce la confiance", Truth = true },
                    new Option { Nom = "Elle crée des malentendus", Truth = false },
                    new Option { Nom = "Elle n'a pas d'impact", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est un obstacle courant à la communication efficace ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Les distractions externes", Truth = true },
                    new Option { Nom = "Les échanges clairs", Truth = false },
                    new Option { Nom = "La préparation avant la communication", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel aspect de la communication est souvent sous-estimé ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Les gestes et le langage corporel", Truth = true },
                    new Option { Nom = "Le vocabulaire utilisé", Truth = false },
                    new Option { Nom = "La structure des phrases", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Pourquoi est-il important de poser des questions lors d'une conversation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Pour interrompre l'interlocuteur", Truth = false },
                    new Option { Nom = "Pour clarifier les informations et montrer de l'intérêt", Truth = true },
                    new Option { Nom = "Pour contrôler la conversation", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est un signe de mauvaise communication ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La confusion des messages reçus", Truth = true },
                    new Option { Nom = "La compréhension mutuelle", Truth = false },
                    new Option { Nom = "Le respect des délais", Truth = false }
                }
            }
        }
    } },
                        new Chapitre {
                            ChapitreNum = 2, Nom = "Techniques de Rédaction Professionnelle", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf"
                        ,
                        Quiz=  new Quiz()
    {
        Nom = "Techniques de Rédaction Professionnelle",
        Statue = ObjectStatus.Approuver,
        Questions = new List<Question>()
        {
            new Question()
            {
                Nom = "Quel est l'objectif principal d'une rédaction professionnelle ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Exprimer des opinions personnelles", Truth = false },
                    new Option { Nom = "Transmettre des informations de manière claire et précise", Truth = true },
                    new Option { Nom = "Utiliser un langage complexe", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle technique est essentielle pour améliorer la clarté d'un texte ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Utiliser des phrases longues et compliquées", Truth = false },
                    new Option { Nom = "Éviter le jargon et utiliser des phrases courtes", Truth = true },
                    new Option { Nom = "Écrire sans structure", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le rôle des paragraphes dans un texte professionnel ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Les paragraphes permettent de séparer les idées et de structurer le texte", Truth = true },
                    new Option { Nom = "Les paragraphes servent uniquement à remplir de l'espace", Truth = false },
                    new Option { Nom = "Les paragraphes n'ont pas d'importance dans un texte professionnel", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Pourquoi est-il important de relire un texte avant de le publier ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Pour trouver des erreurs grammaticales et de typographie", Truth = true },
                    new Option { Nom = "Pour ajouter des détails non pertinents", Truth = false },
                    new Option { Nom = "Pour augmenter la longueur du texte", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est la meilleure manière d'organiser un document professionnel ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Commencer par les détails les plus importants", Truth = true },
                    new Option { Nom = "Commencer par une longue introduction", Truth = false },
                    new Option { Nom = "Éviter les sous-titres", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est un élément clé pour rendre un texte professionnel convaincant ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Utiliser des arguments basés sur des preuves", Truth = true },
                    new Option { Nom = "Ajouter des anecdotes personnelles", Truth = false },
                    new Option { Nom = "Utiliser un langage informel", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le rôle des titres et sous-titres dans un texte ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Ils aident à structurer le texte et à guider le lecteur", Truth = true },
                    new Option { Nom = "Ils sont uniquement décoratifs", Truth = false },
                    new Option { Nom = "Ils doivent être longs et détaillés", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Comment éviter les répétitions dans un texte professionnel ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Utiliser des synonymes et reformuler les phrases", Truth = true },
                    new Option { Nom = "Réutiliser les mêmes mots et phrases", Truth = false },
                    new Option { Nom = "Ignorer les répétitions", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Pourquoi est-il important d'utiliser un ton professionnel dans un texte ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Pour maintenir la crédibilité et la clarté", Truth = true },
                    new Option { Nom = "Pour paraître plus amical", Truth = false },
                    new Option { Nom = "Pour ajouter des émotions", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est un moyen efficace d'améliorer la lisibilité d'un texte ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Utiliser des listes à puces et des tableaux", Truth = true },
                    new Option { Nom = "Écrire de longs blocs de texte sans interruption", Truth = false },
                    new Option { Nom = "Utiliser des polices de caractères très stylisées", Truth = false }
                }
            }
        }
    }   },
                        new Chapitre {Quiz =  new Quiz()
    {
        Nom = "Communication Interpersonnelle",
        Statue = ObjectStatus.Approuver,
        Questions = new List<Question>()
        {
            new Question()
            {
                Nom = "Quel est le principal objectif de la communication interpersonnelle ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Échanger des informations et renforcer les relations", Truth = true },
                    new Option { Nom = "Imposer ses idées", Truth = false },
                    new Option { Nom = "Éviter les conflits", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est un élément essentiel pour une écoute active ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Faire des interruptions fréquentes", Truth = false },
                    new Option { Nom = "Écouter attentivement et poser des questions de clarification", Truth = true },
                    new Option { Nom = "Éviter le contact visuel", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Comment les émotions influencent-elles la communication interpersonnelle ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Les émotions n'ont aucun impact sur la communication", Truth = false },
                    new Option { Nom = "Les émotions peuvent affecter la compréhension et la réactivité", Truth = true },
                    new Option { Nom = "Les émotions rendent la communication plus facile", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est un signe de communication non verbale ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Le ton de la voix", Truth = false },
                    new Option { Nom = "Les gestes et le langage corporel", Truth = true },
                    new Option { Nom = "Les mots utilisés", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Pourquoi est-il important de faire preuve d'empathie dans la communication interpersonnelle ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Pour manipuler l'autre personne", Truth = false },
                    new Option { Nom = "Pour comprendre les sentiments et perspectives de l'autre personne", Truth = true },
                    new Option { Nom = "Pour éviter de partager ses propres sentiments", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Comment la rétroaction affecte-t-elle la communication interpersonnelle ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Elle aide à ajuster le message et à clarifier les malentendus", Truth = true },
                    new Option { Nom = "Elle est généralement non nécessaire", Truth = false },
                    new Option { Nom = "Elle rend la communication plus complexe", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est un obstacle courant à une communication efficace ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "L'écoute active", Truth = false },
                    new Option { Nom = "Les distractions et les interruptions", Truth = true },
                    new Option { Nom = "La clarté des messages", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle stratégie peut améliorer la communication dans une relation professionnelle ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Faire des suppositions sur les intentions de l'autre personne", Truth = false },
                    new Option { Nom = "Établir des attentes claires et des objectifs communs", Truth = true },
                    new Option { Nom = "Éviter les discussions sur les problèmes", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Pourquoi le feedback constructif est-il important dans la communication interpersonnelle ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Pour critiquer sans fournir de solutions", Truth = false },
                    new Option { Nom = "Pour aider à l'amélioration et à la croissance personnelle", Truth = true },
                    new Option { Nom = "Pour éviter d'aborder les problèmes", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Comment la culture influence-t-elle la communication interpersonnelle ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La culture n'a pas d'impact sur la communication", Truth = false },
                    new Option { Nom = "La culture influence les normes et les attentes en matière de communication", Truth = true },
                    new Option { Nom = "La culture rend la communication plus facile", Truth = false }
                }
            }
        }
    },  ChapitreNum = 3, Nom = "Communication Interpersonnelle", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" },
                        new Chapitre {Quiz =new Quiz()
    {
        Nom = "Pratique de la Communication Orale",
        Statue = ObjectStatus.Approuver,
        Questions = new List<Question>()
        {
            new Question()
            {
                Nom = "Quel est un élément clé pour une présentation orale réussie ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Utiliser des diapositives avec beaucoup de texte", Truth = false },
                    new Option { Nom = "Connaître son sujet et pratiquer sa présentation", Truth = true },
                    new Option { Nom = "Parler rapidement pour couvrir plus de contenu", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Comment peut-on maintenir l'attention de son auditoire durant une présentation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "En utilisant des anecdotes et des exemples pertinents", Truth = true },
                    new Option { Nom = "En lisant directement ses notes", Truth = false },
                    new Option { Nom = "En restant silencieux pendant la présentation", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le rôle du langage corporel dans la communication orale ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Il ne joue aucun rôle", Truth = false },
                    new Option { Nom = "Il renforce le message verbal et aide à exprimer des émotions", Truth = true },
                    new Option { Nom = "Il est uniquement décoratif", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Pourquoi est-il important de structurer un discours ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Pour garantir que le discours soit facile à suivre et logique", Truth = true },
                    new Option { Nom = "Pour inclure le plus grand nombre de faits possible", Truth = false },
                    new Option { Nom = "Pour rendre le discours plus long", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est une bonne pratique pour gérer le trac avant une présentation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Ignorer les sentiments de trac", Truth = false },
                    new Option { Nom = "Préparer et répéter la présentation plusieurs fois", Truth = true },
                    new Option { Nom = "Eviter de faire des répétitions", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Comment répondre efficacement aux questions du public après une présentation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Éviter de répondre directement et changer de sujet", Truth = false },
                    new Option { Nom = "Écouter attentivement la question et répondre clairement", Truth = true },
                    new Option { Nom = "Répondre rapidement sans réfléchir", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle technique peut aider à améliorer la diction et l'articulation lors d'une présentation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Parler lentement et pratiquer des exercices de diction", Truth = true },
                    new Option { Nom = "Utiliser un langage très complexe", Truth = false },
                    new Option { Nom = "Parler en murmurant", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Pourquoi est-il important de connaître son auditoire avant une présentation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Pour adapter le discours aux attentes et au niveau de compréhension de l'auditoire", Truth = true },
                    new Option { Nom = "Pour impressionner l'auditoire avec des statistiques", Truth = false },
                    new Option { Nom = "Pour préparer des diapositives supplémentaires", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est un moyen efficace d'utiliser les supports visuels pendant une présentation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Les utiliser pour compléter et renforcer les points principaux", Truth = true },
                    new Option { Nom = "Les utiliser comme substitution au discours", Truth = false },
                    new Option { Nom = "Les utiliser uniquement pour remplir du temps", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est l'importance de la pratique lors de la préparation d'une présentation orale ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La pratique permet de renforcer la confiance et d'améliorer la fluidité", Truth = true },
                    new Option { Nom = "La pratique est moins importante que le contenu", Truth = false },
                    new Option { Nom = "La pratique est uniquement nécessaire pour les présentations longues", Truth = false }
                }
            }
        }
    }
    , ChapitreNum = 4, Nom = "Pratique de la Communication Orale", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" }
                    }
                },
                new Module
                {
                    Nom = "Initiation au management",
                    Chapitres = new List<Chapitre>
                    {
                        new Chapitre {Quiz= new Quiz()
    {
        Nom = "Concepts de Base du Management",
        Statue = ObjectStatus.Approuver,
        Questions = new List<Question>()
        {
            new Question()
            {
                Nom = "Quel est le principal rôle d'un manager ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Superviser et coordonner les activités de l'équipe pour atteindre les objectifs", Truth = true },
                    new Option { Nom = "Créer des conflits pour stimuler la productivité", Truth = false },
                    new Option { Nom = "Éviter la prise de décision", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est la première étape du processus de gestion ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La planification", Truth = true },
                    new Option { Nom = "L'évaluation des performances", Truth = false },
                    new Option { Nom = "La mise en œuvre", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Qu'est-ce que la délégation dans le management ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Transférer des tâches et des responsabilités à des membres de l'équipe tout en conservant la supervision", Truth = true },
                    new Option { Nom = "Éviter de donner des instructions claires", Truth = false },
                    new Option { Nom = "Assumer toutes les tâches soi-même", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Pourquoi la communication est-elle cruciale dans le management ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Pour assurer la clarté des objectifs et des attentes", Truth = true },
                    new Option { Nom = "Pour éviter les réunions", Truth = false },
                    new Option { Nom = "Pour réduire les interactions entre les membres de l'équipe", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'objectif principal de l'évaluation des performances ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Identifier les forces et les faiblesses des employés pour améliorer la performance", Truth = true },
                    new Option { Nom = "Accuser les employés de leurs erreurs", Truth = false },
                    new Option { Nom = "Réduire les coûts de l'entreprise", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Qu'est-ce que le leadership dans le contexte du management ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Inspirer et motiver les membres de l'équipe pour atteindre les objectifs communs", Truth = true },
                    new Option { Nom = "Donner des ordres sans tenir compte des avis", Truth = false },
                    new Option { Nom = "Éviter de prendre des décisions", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est l'importance de la planification stratégique dans le management ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Elle permet de définir les objectifs à long terme et les plans pour les atteindre", Truth = true },
                    new Option { Nom = "Elle est secondaire par rapport à la gestion quotidienne", Truth = false },
                    new Option { Nom = "Elle consiste uniquement à établir des budgets", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Qu'est-ce que la gestion du changement ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Le processus d'adaptation aux nouvelles conditions et d'implémentation de modifications", Truth = true },
                    new Option { Nom = "Éviter les changements et maintenir le statu quo", Truth = false },
                    new Option { Nom = "Chasser les employés qui posent des problèmes", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est la différence entre le management et le leadership ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Le management se concentre sur l'organisation et la supervision, tandis que le leadership inspire et motive", Truth = true },
                    new Option { Nom = "Le management est axé sur les relations personnelles, tandis que le leadership est axé sur les résultats", Truth = false },
                    new Option { Nom = "Le management est une forme de leadership, mais le leadership ne nécessite pas de management", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est un des défis courants dans la gestion d'une équipe ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Gérer les conflits et les personnalités divergentes", Truth = true },
                    new Option { Nom = "Maintenir une communication limitée", Truth = false },
                    new Option { Nom = "Éviter les réunions", Truth = false }
                }
            }
        }
    } , ChapitreNum = 1, Nom = "Concepts de Base du Management", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" },
                        new Chapitre {Quiz=  new Quiz()
    {
        Nom = "Théories du Leadership",
        Statue = ObjectStatus.Approuver,
        Questions = new List<Question>()
        {
            new Question()
            {
                Nom = "Quelle théorie du leadership se concentre sur les traits personnels des leaders ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La théorie des traits", Truth = true },
                    new Option { Nom = "La théorie des contingences", Truth = false },
                    new Option { Nom = "La théorie des comportements", Truth = false }
                }
            },
            new Question()
            {
                Nom = "La théorie de la contingence du leadership propose que l'efficacité du leadership dépend de :",
                Options = new List<Option>()
                {
                    new Option { Nom = "La personnalité du leader uniquement", Truth = false },
                    new Option { Nom = "La capacité du leader à s'adapter à la situation et aux subordonnés", Truth = true },
                    new Option { Nom = "La capacité du leader à suivre des règles strictes", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le principe de base de la théorie des comportements de leadership ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Les comportements des leaders sont plus importants que leurs traits personnels", Truth = true },
                    new Option { Nom = "Les traits personnels des leaders sont plus importants que leurs comportements", Truth = false },
                    new Option { Nom = "Les comportements des leaders ne sont pas significatifs pour le leadership", Truth = false }
                }
            },
            new Question()
            {
                Nom = "La théorie de la transformation du leadership met l'accent sur :",
                Options = new List<Option>()
                {
                    new Option { Nom = "Le rôle du leader dans la motivation et l'inspiration des membres de l'équipe", Truth = true },
                    new Option { Nom = "Le respect des règles et des procédures", Truth = false },
                    new Option { Nom = "Les traits de personnalité innés des leaders", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle théorie du leadership se concentre sur les relations entre les leaders et les suiveurs ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La théorie des échanges leader-membres (LMX)", Truth = true },
                    new Option { Nom = "La théorie des traits", Truth = false },
                    new Option { Nom = "La théorie des comportements", Truth = false }
                }
            },
            new Question()
            {
                Nom = "La théorie du leadership situationnel de Hersey et Blanchard se base sur :",
                Options = new List<Option>()
                {
                    new Option { Nom = "Le niveau de maturité des subordonnés", Truth = true },
                    new Option { Nom = "Les traits innés des leaders", Truth = false },
                    new Option { Nom = "Les relations personnelles entre les membres de l'équipe", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le concept central de la théorie de la gestion par objectifs (MBO) ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Définir des objectifs clairs et mesurer les performances par rapport à ces objectifs", Truth = true },
                    new Option { Nom = "Se concentrer uniquement sur les aspects financiers du leadership", Truth = false },
                    new Option { Nom = "Éviter la fixation d'objectifs", Truth = false }
                }
            },
            new Question()
            {
                Nom = "La théorie du leadership charismatique suggère que :",
                Options = new List<Option>()
                {
                    new Option { Nom = "Les leaders charismatiques inspirent et motivent les autres par leur personnalité et leur vision", Truth = true },
                    new Option { Nom = "Les leaders charismatiques se concentrent uniquement sur la gestion des tâches", Truth = false },
                    new Option { Nom = "Le charisme est secondaire par rapport aux compétences techniques", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'objectif principal de la théorie du leadership participatif ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Impliquer les membres de l'équipe dans le processus décisionnel", Truth = true },
                    new Option { Nom = "Prendre toutes les décisions seul", Truth = false },
                    new Option { Nom = "Éviter les interactions avec les membres de l'équipe", Truth = false }
                }
            },
            new Question()
            {
                Nom = "La théorie de la contingence de Fiedler se concentre sur :",
                Options = new List<Option>()
                {
                    new Option { Nom = "L'adéquation entre le style de leadership du leader et la situation", Truth = true },
                    new Option { Nom = "Les traits personnels du leader", Truth = false },
                    new Option { Nom = "Les compétences techniques du leader", Truth = false }
                }
            }
        }
    }, ChapitreNum = 2, Nom = "Théories du Leadership", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" },
                        new Chapitre {Quiz=  new Quiz(){
        Nom = "Gestion des Équipes",
        Statue = ObjectStatus.Approuver,
        Questions = new List<Question>()
        {
            new Question()
            {
                Nom = "Quelle est la principale responsabilité d'un manager d'équipe ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Gérer les ressources humaines et matérielles pour atteindre les objectifs de l'équipe", Truth = true },
                    new Option { Nom = "Éviter les conflits internes", Truth = false },
                    new Option { Nom = "Assumer toutes les tâches techniques soi-même", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Comment un leader peut-il encourager la collaboration au sein de son équipe ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "En favorisant une communication ouverte et en établissant des objectifs communs", Truth = true },
                    new Option { Nom = "En limitant les interactions entre les membres de l'équipe", Truth = false },
                    new Option { Nom = "En imposant des tâches sans discuter", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est un indicateur clé de la performance d'une équipe ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "L'atteinte des objectifs fixés dans les délais impartis", Truth = true },
                    new Option { Nom = "Le nombre de réunions tenues", Truth = false },
                    new Option { Nom = "Le niveau de confort des membres de l'équipe", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est un avantage de la gestion participative ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Elle permet aux membres de l'équipe de contribuer aux décisions et augmente l'engagement", Truth = true },
                    new Option { Nom = "Elle simplifie le processus décisionnel en évitant les consultations", Truth = false },
                    new Option { Nom = "Elle concentre le pouvoir de décision uniquement entre les mains du manager", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est un des principaux défis dans la gestion d'équipes diversifiées ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Gérer les différences culturelles et de communication", Truth = true },
                    new Option { Nom = "Encourager une homogénéité de pensée", Truth = false },
                    new Option { Nom = "Limiter les échanges entre membres d'origine différente", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle méthode est efficace pour résoudre les conflits au sein d'une équipe ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Encourager un dialogue ouvert et trouver des solutions collaboratives", Truth = true },
                    new Option { Nom = "Ignorer le conflit et espérer qu'il se résolve de lui-même", Truth = false },
                    new Option { Nom = "Favoriser des sanctions pour ceux qui créent des conflits", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le rôle de la rétroaction dans la gestion des équipes ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Fournir des informations sur les performances et les domaines à améliorer", Truth = true },
                    new Option { Nom = "Éviter de discuter des performances des membres", Truth = false },
                    new Option { Nom = "Ne pas tenir compte des commentaires des membres de l'équipe", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Comment un manager peut-il motiver une équipe ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "En reconnaissant les efforts et les réalisations des membres", Truth = true },
                    new Option { Nom = "En imposant des récompenses sans consultation", Truth = false },
                    new Option { Nom = "En restreignant les opportunités de développement personnel", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Pourquoi est-il important de définir des rôles clairs au sein de l'équipe ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Pour éviter les chevauchements de responsabilités et clarifier les attentes", Truth = true },
                    new Option { Nom = "Pour éviter de donner trop de responsabilités aux membres de l'équipe", Truth = false },
                    new Option { Nom = "Pour que le manager puisse faire toutes les tâches", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est la meilleure approche pour gérer une équipe virtuelle ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Utiliser des outils de communication numérique efficaces et maintenir une communication régulière", Truth = true },
                    new Option { Nom = "Minimiser les réunions pour réduire les coûts", Truth = false },
                    new Option { Nom = "Encourager les membres à travailler de manière indépendante sans suivi", Truth = false }
                }
            }
        }
    }, ChapitreNum = 3, Nom = "Gestion des Équipes", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" },
                        new Chapitre {Quiz=  new Quiz(){
        Nom = "Processus Décisionnels en Management",
        Statue = ObjectStatus.Approuver,
        Questions = new List<Question>()
        {
            new Question()
            {
                Nom = "Quelle est la première étape du processus décisionnel en management ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Identifier et définir le problème", Truth = true },
                    new Option { Nom = "Évaluer les solutions possibles", Truth = false },
                    new Option { Nom = "Mettre en œuvre la décision", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'objectif de la phase d'analyse des alternatives dans le processus décisionnel ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Évaluer les différentes options et choisir la meilleure", Truth = true },
                    new Option { Nom = "Éviter de prendre une décision", Truth = false },
                    new Option { Nom = "Choisir une solution au hasard", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle technique aide à structurer le processus décisionnel en identifiant les avantages et les inconvénients de chaque option ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Analyse SWOT", Truth = true },
                    new Option { Nom = "Analyse des parties prenantes", Truth = false },
                    new Option { Nom = "Analyse de rentabilité", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Comment la prise de décision basée sur des données (data-driven) améliore-t-elle le processus décisionnel ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "En fournissant des informations objectives et vérifiables pour soutenir les décisions", Truth = true },
                    new Option { Nom = "En évitant l'utilisation de statistiques", Truth = false },
                    new Option { Nom = "En favorisant les décisions basées uniquement sur l'intuition", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est la différence entre une décision stratégique et une décision opérationnelle ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Les décisions stratégiques concernent les objectifs à long terme, tandis que les décisions opérationnelles concernent les opérations quotidiennes", Truth = true },
                    new Option { Nom = "Les décisions stratégiques sont prises par les employés, tandis que les décisions opérationnelles sont prises par les managers", Truth = false },
                    new Option { Nom = "Les décisions stratégiques sont moins importantes que les décisions opérationnelles", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Qu'est-ce que la prise de décision participative ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Impliquer les membres de l'équipe dans le processus décisionnel", Truth = true },
                    new Option { Nom = "Laisser les managers prendre toutes les décisions sans consultation", Truth = false },
                    new Option { Nom = "Éviter les discussions sur les décisions avec les parties prenantes", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle méthode est utilisée pour évaluer les risques associés à une décision ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Analyse des risques", Truth = true },
                    new Option { Nom = "Analyse des coûts", Truth = false },
                    new Option { Nom = "Analyse des bénéfices", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Pourquoi est-il important de suivre l'implémentation d'une décision ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Pour s'assurer que la décision est correctement exécutée et pour ajuster les actions si nécessaire", Truth = true },
                    new Option { Nom = "Pour éviter d'évaluer les résultats", Truth = false },
                    new Option { Nom = "Pour se concentrer uniquement sur les prochaines décisions", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'avantage de la méthode de décision en groupe ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Elle permet de bénéficier de diverses perspectives et expertises", Truth = true },
                    new Option { Nom = "Elle ralentit le processus décisionnel", Truth = false },
                    new Option { Nom = "Elle est moins efficace que la prise de décision individuelle", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Qu'est-ce que la prise de décision basée sur l'intuition ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Prendre des décisions basées sur des instincts ou des sentiments personnels", Truth = true },
                    new Option { Nom = "Prendre des décisions basées uniquement sur des données objectives", Truth = false },
                    new Option { Nom = "Prendre des décisions en suivant les règles et procédures établies", Truth = false }
                }
            }
        }
    }, ChapitreNum = 4, Nom = "Processus Décisionnels en Management", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" }
                    }
                },
                new Module
                {
                    Nom = "Instruments quantitatifs 1",
                    Chapitres = new List<Chapitre>
                    {
                        new Chapitre {Quiz = new Quiz()
    {
        Nom = "Statistiques Descriptives",
        Statue = ObjectStatus.Approuver,
        Questions = new List<Question>()
        {
            new Question()
            {
                Nom = "Quel est l'objectif principal des statistiques descriptives ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Résumé et description des caractéristiques principales d'un ensemble de données", Truth = true },
                    new Option { Nom = "Prédire les tendances futures des données", Truth = false },
                    new Option { Nom = "Tester des hypothèses sur les données", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le terme utilisé pour la mesure de la tendance centrale la plus courante ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La moyenne", Truth = true },
                    new Option { Nom = "La variance", Truth = false },
                    new Option { Nom = "L'écart type", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Comment appelle-t-on la mesure qui indique la valeur la plus fréquente dans un ensemble de données ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La mode", Truth = true },
                    new Option { Nom = "La médiane", Truth = false },
                    new Option { Nom = "La moyenne", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle mesure décrit la dispersion ou la variabilité des données autour de la moyenne ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "L'écart type", Truth = true },
                    new Option { Nom = "La moyenne", Truth = false },
                    new Option { Nom = "La médiane", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est la médiane d'un ensemble de données ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La valeur qui divise l'ensemble de données en deux parties égales", Truth = true },
                    new Option { Nom = "La valeur la plus élevée dans l'ensemble de données", Truth = false },
                    new Option { Nom = "La moyenne des valeurs", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'indicateur de dispersion qui représente la moyenne des carrés des écarts par rapport à la moyenne ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La variance", Truth = true },
                    new Option { Nom = "L'écart type", Truth = false },
                    new Option { Nom = "La médiane", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel graphique est souvent utilisé pour afficher la distribution d'une variable continue ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "L'histogramme", Truth = true },
                    new Option { Nom = "Le diagramme en barres", Truth = false },
                    new Option { Nom = "Le nuage de points", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le terme utilisé pour la différence entre la valeur maximale et la valeur minimale dans un ensemble de données ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "L'étendue", Truth = true },
                    new Option { Nom = "L'écart type", Truth = false },
                    new Option { Nom = "La variance", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Comment appelle-t-on une mesure qui résume une variable quantitative en utilisant des quartiles ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Le boxplot (ou boîte à moustaches)", Truth = true },
                    new Option { Nom = "L'histogramme", Truth = false },
                    new Option { Nom = "Le diagramme en barres", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le terme pour une mesure de la direction et de la force de la relation entre deux variables ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La corrélation", Truth = true },
                    new Option { Nom = "La variance", Truth = false },
                    new Option { Nom = "L'écart type", Truth = false }
                }
            }
        }
    }, ChapitreNum = 1, Nom = "Statistiques Descriptives", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" },
                        new Chapitre {Quiz= new Quiz()
    {
        Nom = "Analyse de Régression",
        Statue = ObjectStatus.Approuver,
        Questions = new List<Question>()
        {
            new Question()
            {
                Nom = "Quel est l'objectif principal de l'analyse de régression ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Modéliser la relation entre une variable dépendante et une ou plusieurs variables indépendantes", Truth = true },
                    new Option { Nom = "Déterminer la moyenne d'un ensemble de données", Truth = false },
                    new Option { Nom = "Évaluer la fréquence d'occurrence d'une variable", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Dans une régression linéaire simple, quel est le terme utilisé pour la variable que vous essayez de prédire ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La variable dépendante", Truth = true },
                    new Option { Nom = "La variable indépendante", Truth = false },
                    new Option { Nom = "La constante", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le but de la méthode des moindres carrés dans l'analyse de régression ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Minimiser la somme des carrés des résidus entre les valeurs observées et les valeurs prédites", Truth = true },
                    new Option { Nom = "Maximiser la différence entre les valeurs observées et les valeurs prédites", Truth = false },
                    new Option { Nom = "Éviter les erreurs de prévision", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le terme utilisé pour mesurer la force et la direction de la relation linéaire entre deux variables dans une régression ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Le coefficient de corrélation", Truth = true },
                    new Option { Nom = "L'écart type", Truth = false },
                    new Option { Nom = "La médiane", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Qu'indique le coefficient de détermination (R²) dans une régression ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La proportion de la variance de la variable dépendante expliquée par le modèle de régression", Truth = true },
                    new Option { Nom = "La somme totale des erreurs de prévision", Truth = false },
                    new Option { Nom = "La variance des résidus", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est la différence entre la régression linéaire simple et la régression linéaire multiple ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La régression linéaire simple utilise une seule variable indépendante, tandis que la régression linéaire multiple en utilise plusieurs", Truth = true },
                    new Option { Nom = "La régression linéaire multiple est plus facile à interpréter que la régression linéaire simple", Truth = false },
                    new Option { Nom = "La régression linéaire simple est utilisée pour les données catégorielles, tandis que la régression linéaire multiple est utilisée pour les données continues", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle hypothèse est faite concernant les résidus dans la régression linéaire ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Les résidus doivent être distribués normalement avec une variance constante", Truth = true },
                    new Option { Nom = "Les résidus doivent être non corrélés avec la variable dépendante", Truth = false },
                    new Option { Nom = "Les résidus doivent être égaux aux valeurs prédites", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'impact des valeurs aberrantes sur une analyse de régression ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Elles peuvent influencer de manière disproportionnée les coefficients de régression", Truth = true },
                    new Option { Nom = "Elles n'affectent pas le modèle de régression", Truth = false },
                    new Option { Nom = "Elles améliorent toujours la qualité du modèle", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le rôle du terme 'intercept' (ou constante) dans une équation de régression linéaire ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Représenter la valeur prédite lorsque toutes les variables indépendantes sont égales à zéro", Truth = true },
                    new Option { Nom = "Indiquer la force de la relation entre les variables", Truth = false },
                    new Option { Nom = "Mesurer la variance des résidus", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est l'importance de vérifier l'hypothèse d'homoscédasticité dans une régression ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Pour s'assurer que la variance des résidus est constante à travers les valeurs de la variable indépendante", Truth = true },
                    new Option { Nom = "Pour vérifier que les variables indépendantes sont corrélées entre elles", Truth = false },
                    new Option { Nom = "Pour évaluer la normalité des valeurs observées", Truth = false }
                }
            }
        }
    }, ChapitreNum = 2, Nom = "Analyse de Régression", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" },
                        new Chapitre {Quiz=  new Quiz()
    {
        Nom = "Probabilités et Distributions",
        Statue = ObjectStatus.Approuver,
        Questions = new List<Question>()
        {
            new Question()
            {
                Nom = "Quel est le principe fondamental de la probabilité ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Mesurer la chance ou la probabilité qu'un événement se produise", Truth = true },
                    new Option { Nom = "Calculer la moyenne d'un ensemble de données", Truth = false },
                    new Option { Nom = "Évaluer les écarts types des données", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est la probabilité d'obtenir un nombre pair lors du lancement d'un dé à six faces ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "1/2", Truth = true },
                    new Option { Nom = "1/3", Truth = false },
                    new Option { Nom = "1/6", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle loi de probabilité est utilisée pour modéliser le nombre de succès dans une série de tentatives indépendantes ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La loi binomiale", Truth = true },
                    new Option { Nom = "La loi normale", Truth = false },
                    new Option { Nom = "La loi de Poisson", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est la distribution de probabilité continue la plus couramment utilisée pour modéliser des phénomènes naturels ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La distribution normale", Truth = true },
                    new Option { Nom = "La distribution binomiale", Truth = false },
                    new Option { Nom = "La distribution uniforme", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel paramètre caractérise la dispersion des valeurs dans une distribution normale ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "L'écart type", Truth = true },
                    new Option { Nom = "La moyenne", Truth = false },
                    new Option { Nom = "La médiane", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Dans une distribution binomiale, quel est le terme utilisé pour la probabilité d'un succès dans une seule tentative ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La probabilité de succès (p)", Truth = true },
                    new Option { Nom = "La probabilité d'échec (1-p)", Truth = false },
                    new Option { Nom = "Le nombre de succès", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le terme pour la distribution des valeurs d'une variable aléatoire qui suit une loi de Poisson ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La distribution de Poisson", Truth = true },
                    new Option { Nom = "La distribution normale", Truth = false },
                    new Option { Nom = "La distribution binomiale", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est la somme des probabilités de tous les événements possibles dans un espace de probabilité ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "1", Truth = true },
                    new Option { Nom = "0", Truth = false },
                    new Option { Nom = "Variable selon les événements", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel type de distribution est utilisé pour modéliser les temps d'attente entre les événements dans un processus de Poisson ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La distribution exponentielle", Truth = true },
                    new Option { Nom = "La distribution normale", Truth = false },
                    new Option { Nom = "La distribution uniforme", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle fonction de densité est associée à une distribution normale standard ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La courbe en cloche", Truth = true },
                    new Option { Nom = "La courbe en U", Truth = false },
                    new Option { Nom = "La courbe en plateau", Truth = false }
                }
            }
        }
    }, ChapitreNum = 3, Nom = "Probabilités et Distributions", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" },
                        new Chapitre {Quiz= new Quiz()
    {
        Nom = "Méthodes de Collecte de Données",
        Statue = ObjectStatus.Approuver,
        Questions = new List<Question>()
        {
            new Question()
            {
                Nom = "Quelle méthode de collecte de données implique l'utilisation de questionnaires structurés ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "L'enquête", Truth = true },
                    new Option { Nom = "L'observation participante", Truth = false },
                    new Option { Nom = "L'entretien non structuré", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle méthode de collecte de données consiste à observer les comportements dans leur environnement naturel ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "L'observation directe", Truth = true },
                    new Option { Nom = "L'enquête par questionnaire", Truth = false },
                    new Option { Nom = "L'analyse documentaire", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le principal avantage des entretiens structurés par rapport aux entretiens non structurés ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Une meilleure comparabilité des réponses", Truth = true },
                    new Option { Nom = "Une plus grande flexibilité pour explorer les réponses", Truth = false },
                    new Option { Nom = "Un coût plus faible", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel type de collecte de données est le plus approprié pour recueillir des informations qualitatives détaillées ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "L'entretien semi-structuré", Truth = true },
                    new Option { Nom = "Le sondage à choix multiples", Truth = false },
                    new Option { Nom = "L'enquête par téléphone", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le principal inconvénient des questionnaires auto-administrés ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Un taux de réponse potentiellement faible", Truth = true },
                    new Option { Nom = "Un coût élevé pour les administrer", Truth = false },
                    new Option { Nom = "Un manque de standardisation des questions", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'objectif principal des études de cas dans la collecte de données ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Fournir une analyse approfondie d'un phénomène spécifique", Truth = true },
                    new Option { Nom = "Évaluer un grand nombre d'individus de manière rapide", Truth = false },
                    new Option { Nom = "Obtenir des données quantitatives généralisables", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle méthode de collecte de données est souvent utilisée pour recueillir des informations historiques ou des documents existants ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "L'analyse documentaire", Truth = true },
                    new Option { Nom = "L'observation participante", Truth = false },
                    new Option { Nom = "Le sondage par questionnaire", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le principal avantage de la collecte de données par observation participante ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Obtenir des données contextuelles riches et détaillées", Truth = true },
                    new Option { Nom = "Assurer une grande objectivité dans les réponses", Truth = false },
                    new Option { Nom = "Réduire les biais de sélection", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle méthode est la plus adaptée pour obtenir des données de grande échelle rapidement et à moindre coût ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "L'enquête en ligne", Truth = true },
                    new Option { Nom = "L'entretien approfondi", Truth = false },
                    new Option { Nom = "L'observation en laboratoire", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le principal objectif des tests pilotes dans la collecte de données ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Évaluer la clarté et la pertinence des instruments de collecte de données", Truth = true },
                    new Option { Nom = "Recueillir les données finales pour l'analyse", Truth = false },
                    new Option { Nom = "Augmenter le taux de réponse des participants", Truth = false }
                }
            }
        }
    }, ChapitreNum = 4, Nom = "Méthodes de Collecte de Données", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" }
                    }
                },
                new Module
                {
                    Nom = "Environnement des organisations 1",
                    Chapitres = new List<Chapitre>
                    {
                        new Chapitre {Quiz = new Quiz()
    {
        Nom = "Structure Organisationnelle",
        Statue = ObjectStatus.Approuver,
        Questions = new List<Question>()
        {
            new Question()
            {
                Nom = "Quelle est la principale caractéristique d'une structure organisationnelle fonctionnelle ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Les employés sont regroupés en départements fonctionnels tels que la finance, le marketing, etc.", Truth = true },
                    new Option { Nom = "Les équipes sont organisées autour de projets spécifiques avec une autonomie élevée", Truth = false },
                    new Option { Nom = "Les employés sont regroupés en équipes de produit avec une hiérarchie plate", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel type de structure organisationnelle est caractérisé par la décentralisation de la prise de décision ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La structure organisationnelle matricielle", Truth = true },
                    new Option { Nom = "La structure organisationnelle fonctionnelle", Truth = false },
                    new Option { Nom = "La structure organisationnelle hiérarchique", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Dans une structure organisationnelle matricielle, comment sont généralement organisés les employés ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Les employés reportent à deux supérieurs : un fonctionnel et un de projet", Truth = true },
                    new Option { Nom = "Les employés reportent uniquement à un supérieur fonctionnel", Truth = false },
                    new Option { Nom = "Les employés sont regroupés selon les produits avec une autonomie totale", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est la principale différence entre une structure organisationnelle divisée par produit et une structure divisée par région ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Une structure divisée par produit organise les employés autour de lignes de produits, tandis qu'une structure divisée par région organise autour de zones géographiques", Truth = true },
                    new Option { Nom = "Une structure divisée par produit est plus hiérarchique que celle divisée par région", Truth = false },
                    new Option { Nom = "Une structure divisée par région est utilisée pour les startups, tandis qu'une structure divisée par produit est utilisée pour les grandes entreprises", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'avantage principal d'une structure organisationnelle plate ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Une communication plus rapide et une prise de décision plus agile", Truth = true },
                    new Option { Nom = "Une spécialisation accrue des tâches", Truth = false },
                    new Option { Nom = "Une hiérarchie plus définie et une meilleure supervision", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel type de structure est généralement choisi pour les entreprises multinationales afin de gérer les opérations à travers différentes régions ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La structure organisationnelle par région", Truth = true },
                    new Option { Nom = "La structure organisationnelle fonctionnelle", Truth = false },
                    new Option { Nom = "La structure organisationnelle matricielle", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle structure organisationnelle est la plus adaptée pour les entreprises innovantes et les startups qui nécessitent flexibilité et réactivité ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La structure organisationnelle adhocratique", Truth = true },
                    new Option { Nom = "La structure organisationnelle fonctionnelle", Truth = false },
                    new Option { Nom = "La structure organisationnelle hiérarchique", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le principal objectif d'une structure organisationnelle hiérarchique ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Définir clairement les niveaux de gestion et les lignes de responsabilité", Truth = true },
                    new Option { Nom = "Encourager une grande autonomie des équipes", Truth = false },
                    new Option { Nom = "Favoriser une communication rapide entre les membres de l'équipe", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Dans une structure organisationnelle par projet, quel est l'élément central de l'organisation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Les projets spécifiques et les équipes dédiées à ces projets", Truth = true },
                    new Option { Nom = "Les départements fonctionnels comme le marketing et la finance", Truth = false },
                    new Option { Nom = "Les régions géographiques où les opérations ont lieu", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le principal avantage d'une structure organisationnelle fonctionnelle pour les grandes entreprises ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Une spécialisation accrue et une expertise développée dans chaque fonction", Truth = true },
                    new Option { Nom = "Une meilleure coordination entre différents départements", Truth = false },
                    new Option { Nom = "Une plus grande autonomie des équipes de projet", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le rôle d'une structure organisationnelle dans le cadre de la gestion de la performance et des ressources humaines ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Déterminer comment les responsabilités et les rôles sont attribués et coordonnés au sein de l'organisation", Truth = true },
                    new Option { Nom = "Évaluer les performances individuelles des employés", Truth = false },
                    new Option { Nom = "Gérer les budgets et les prévisions financières", Truth = false }
                }
            }
        }
    }, ChapitreNum = 1, Nom = "Structure Organisationnelle", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" },
                        new Chapitre {Quiz = new Quiz(){
        Nom = "Culture d'Entreprise",
        Statue = ObjectStatus.Approuver,
        Questions = new List<Question>()
        {
            new Question()
            {
                Nom = "Quelle est la principale composante de la culture d'entreprise ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Les valeurs et les croyances partagées au sein de l'organisation", Truth = true },
                    new Option { Nom = "La taille de l'entreprise et le nombre d'employés", Truth = false },
                    new Option { Nom = "Le type de produits ou services offerts", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel rôle joue la culture d'entreprise dans le recrutement des employés ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Elle influence la capacité de l'entreprise à attirer et retenir les talents compatibles avec ses valeurs", Truth = true },
                    new Option { Nom = "Elle détermine le niveau de rémunération offert aux employés", Truth = false },
                    new Option { Nom = "Elle définit les compétences techniques requises pour les postes", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'impact de la culture d'entreprise sur la satisfaction des employés ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Une culture d'entreprise positive peut améliorer la satisfaction et l'engagement des employés", Truth = true },
                    new Option { Nom = "Une culture d'entreprise négative a peu d'impact sur la satisfaction des employés", Truth = false },
                    new Option { Nom = "La culture d'entreprise n'a aucun effet sur la satisfaction des employés", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle méthode est couramment utilisée pour évaluer la culture d'entreprise ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Les enquêtes de satisfaction des employés", Truth = true },
                    new Option { Nom = "Les analyses financières trimestrielles", Truth = false },
                    new Option { Nom = "Les audits de conformité juridique", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est la différence entre la culture d'entreprise et le climat organisationnel ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La culture d'entreprise est plus stable et profonde, tandis que le climat organisationnel peut changer plus rapidement", Truth = true },
                    new Option { Nom = "Il n'y a pas de différence significative entre les deux termes", Truth = false },
                    new Option { Nom = "La culture d'entreprise est un aspect externe, tandis que le climat organisationnel est interne", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'effet d'une culture d'entreprise cohérente sur la performance organisationnelle ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Elle peut améliorer la performance en alignant les comportements des employés avec les objectifs de l'entreprise", Truth = true },
                    new Option { Nom = "Elle a peu ou pas d'impact sur la performance organisationnelle", Truth = false },
                    new Option { Nom = "Elle peut diminuer la performance en limitant l'innovation", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est la principale méthode pour intégrer la culture d'entreprise lors de l'intégration de nouveaux employés ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Les programmes d'orientation et de formation sur les valeurs de l'entreprise", Truth = true },
                    new Option { Nom = "Les évaluations de performance rigoureuses dès le début", Truth = false },
                    new Option { Nom = "L'attribution de tâches complexes dès le départ", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel type de culture d'entreprise est souvent associé à une structure organisationnelle hiérarchique ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Une culture d'entreprise axée sur le contrôle et la conformité", Truth = true },
                    new Option { Nom = "Une culture d'entreprise axée sur l'innovation et la flexibilité", Truth = false },
                    new Option { Nom = "Une culture d'entreprise axée sur la collaboration et le travail en équipe", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'impact de la culture d'entreprise sur la communication interne ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Une culture d'entreprise ouverte favorise une communication transparente et efficace", Truth = true },
                    new Option { Nom = "La culture d'entreprise n'a pas d'impact sur la communication interne", Truth = false },
                    new Option { Nom = "Une culture d'entreprise rigide favorise une meilleure communication interne", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est la meilleure approche pour changer une culture d'entreprise existante ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Mettre en œuvre des changements progressifs et impliquer les employés dans le processus", Truth = true },
                    new Option { Nom = "Imposer des changements radicaux sans consulter les employés", Truth = false },
                    new Option { Nom = "Maintenir la culture actuelle et ignorer les demandes de changement", Truth = false }
                }
            }
        }
    }, ChapitreNum = 2, Nom = "Culture d'Entreprise", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" },
                        new Chapitre {Quiz = new Quiz(){
        Nom = "Analyse du Cycle de Vie des Organisations",
        Statue = ObjectStatus.Approuver,
        Questions = new List<Question>()
        {
            new Question()
            {
                Nom = "Quel est le premier stade du cycle de vie d'une organisation selon le modèle classique ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La phase de création", Truth = true },
                    new Option { Nom = "La phase de maturité", Truth = false },
                    new Option { Nom = "La phase de déclin", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'objectif principal de la phase de croissance dans le cycle de vie des organisations ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Accroître la part de marché et développer les opérations", Truth = true },
                    new Option { Nom = "Réduire les coûts et optimiser les processus", Truth = false },
                    new Option { Nom = "Fermer des unités non rentables", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelles caractéristiques sont généralement associées à la phase de maturité d'une organisation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Une stabilité accrue et une optimisation des processus", Truth = true },
                    new Option { Nom = "Une croissance rapide et une expansion sur de nouveaux marchés", Truth = false },
                    new Option { Nom = "Des difficultés financières et une perte de parts de marché", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est un des principaux défis rencontrés par une organisation en phase de déclin ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Réduire les coûts tout en tentant de revitaliser les opérations", Truth = true },
                    new Option { Nom = "Maintenir une croissance rapide et l'expansion internationale", Truth = false },
                    new Option { Nom = "Innover de manière continue pour attirer de nouveaux clients", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le principal objectif de la phase de transition dans le cycle de vie des organisations ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Réorganiser et réinventer l'entreprise pour faire face aux défis futurs", Truth = true },
                    new Option { Nom = "Maximiser les profits en maintenant les processus actuels", Truth = false },
                    new Option { Nom = "Éliminer les unités non rentables sans changer la structure organisationnelle", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle stratégie est souvent employée lors de la phase de déclin pour prolonger la durée de vie de l'organisation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Réduction des coûts et réorientation vers des segments de marché plus rentables", Truth = true },
                    new Option { Nom = "Augmentation des dépenses marketing pour revitaliser les produits existants", Truth = false },
                    new Option { Nom = "Expansion agressive dans de nouveaux marchés géographiques", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est un des signes précoces de la phase de déclin dans une organisation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Une diminution des ventes et une baisse de la part de marché", Truth = true },
                    new Option { Nom = "Une augmentation de la demande pour les produits ou services", Truth = false },
                    new Option { Nom = "Une croissance rapide des revenus et des bénéfices", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel rôle joue l'innovation dans la phase de croissance du cycle de vie des organisations ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Elle permet de différencier les produits et de conquérir de nouveaux segments de marché", Truth = true },
                    new Option { Nom = "Elle n'a pas un impact significatif sur la croissance", Truth = false },
                    new Option { Nom = "Elle est principalement utilisée pour réduire les coûts de production", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Comment les organisations peuvent-elles gérer les risques associés à la phase de maturité ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "En mettant en œuvre des stratégies d'efficacité et d'amélioration continue", Truth = true },
                    new Option { Nom = "En augmentant agressivement les investissements dans la recherche et le développement", Truth = false },
                    new Option { Nom = "En réduisant les efforts marketing et en se concentrant sur les produits existants", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est un des principaux défis de la phase de création pour une nouvelle organisation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Établir une base solide de clients et créer des processus efficaces", Truth = true },
                    new Option { Nom = "Gérer des opérations à grande échelle et optimiser les ressources", Truth = false },
                    new Option { Nom = "Faire face à des problèmes de surcapacité et de coûts élevés", Truth = false }
                }
            }
        }
    }, ChapitreNum = 3, Nom = "Analyse du Cycle de Vie des Organisations", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" },
                        new Chapitre {Quiz = new Quiz(){
        Nom = "Responsabilité Sociale des Entreprises",
        Statue = ObjectStatus.Approuver,
        Questions = new List<Question>()
        {
            new Question()
            {
                Nom = "Quel est le principal objectif de la Responsabilité Sociale des Entreprises (RSE) ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Améliorer l'impact social et environnemental des activités de l'entreprise", Truth = true },
                    new Option { Nom = "Maximiser les profits à court terme", Truth = false },
                    new Option { Nom = "Réduire le nombre d'employés pour augmenter l'efficacité", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est l'une des principales dimensions de la RSE ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "L'éthique et la transparence des affaires", Truth = true },
                    new Option { Nom = "L'optimisation des processus de production", Truth = false },
                    new Option { Nom = "L'expansion rapide sur de nouveaux marchés", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel rôle jouent les parties prenantes dans la RSE ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Elles influencent les décisions de l'entreprise et contribuent à ses objectifs de RSE", Truth = true },
                    new Option { Nom = "Elles sont principalement concernées par les aspects financiers de l'entreprise", Truth = false },
                    new Option { Nom = "Elles sont uniquement impliquées dans la gestion des opérations internes", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle pratique est typiquement associée à la RSE en matière d'environnement ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Réduction des émissions de gaz à effet de serre et gestion des déchets", Truth = true },
                    new Option { Nom = "Augmentation de la production sans considération pour les ressources", Truth = false },
                    new Option { Nom = "Maximisation des profits à court terme", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est un des avantages de la mise en œuvre de pratiques de RSE pour une entreprise ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Amélioration de l'image de marque et de la réputation", Truth = true },
                    new Option { Nom = "Augmentation immédiate des marges bénéficiaires", Truth = false },
                    new Option { Nom = "Réduction des coûts de production à court terme", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est un exemple d'initiative de RSE axée sur les employés ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Programmes de développement professionnel et de bien-être au travail", Truth = true },
                    new Option { Nom = "Réduction des coûts de production en diminuant les salaires", Truth = false },
                    new Option { Nom = "Externalisation des tâches pour réduire les coûts de main-d'œuvre", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'un des défis courants liés à la mise en œuvre de la RSE ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Aligner les pratiques de RSE avec les objectifs financiers de l'entreprise", Truth = true },
                    new Option { Nom = "Augmenter les ventes rapidement", Truth = false },
                    new Option { Nom = "Réduire le nombre d'employés", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle norme est souvent utilisée pour guider les entreprises dans leurs efforts de RSE ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La norme ISO 26000", Truth = true },
                    new Option { Nom = "La norme ISO 9001", Truth = false },
                    new Option { Nom = "La norme ISO 14001", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Comment la RSE peut-elle affecter les relations avec les clients ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "En renforçant la confiance et la fidélité des clients", Truth = true },
                    new Option { Nom = "En réduisant les interactions avec les clients", Truth = false },
                    new Option { Nom = "En augmentant les coûts des produits de manière significative", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est une pratique éthique courante dans le cadre de la RSE ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Assurer la transparence et la responsabilité dans les opérations", Truth = true },
                    new Option { Nom = "Maximiser les profits en réduisant les dépenses", Truth = false },
                    new Option { Nom = "Éviter les régulations gouvernementales", Truth = false }
                }
            }
        }
    }, ChapitreNum = 4, Nom = "Responsabilité Sociale des Entreprises", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" }
                    }
                }
            }
        },
                        new NiveauScolaire
        {
            Nom = "Semestre 2",
            Modules = new List<Module>
            {
                new Module
                {
                    Nom = "Langues et Communication 2",
                    Chapitres = new List<Chapitre>
                    {
                        new Chapitre {Quiz  = new Quiz()
    {
        Nom = "Communication Écrite Avancée",
        Statue = ObjectStatus.Approuver,
        Questions = new List<Question>()
        {
            new Question()
            {
                Nom = "Quelle est la principale caractéristique d'une communication écrite efficace ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Clarté et concision", Truth = true },
                    new Option { Nom = "Utilisation de jargon complexe", Truth = false },
                    new Option { Nom = "Longueur excessive des documents", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est un des objectifs d'une lettre professionnelle formelle ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Présenter des informations de manière structurée et polie", Truth = true },
                    new Option { Nom = "Exprimer des émotions personnelles de manière informelle", Truth = false },
                    new Option { Nom = "Inclure des détails non pertinents pour le sujet", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle technique est recommandée pour améliorer la lisibilité d'un document écrit ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Utiliser des titres et des sous-titres", Truth = true },
                    new Option { Nom = "Écrire de longs paragraphes sans espaces", Truth = false },
                    new Option { Nom = "Éviter l'utilisation de listes à puces", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est l'importance de la révision dans la communication écrite ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Elle permet de corriger les erreurs et d'améliorer la qualité du texte", Truth = true },
                    new Option { Nom = "Elle est moins importante que la rédaction initiale", Truth = false },
                    new Option { Nom = "Elle ralentit le processus de rédaction et n'est pas nécessaire", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le rôle de la voix active dans la communication écrite ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Rendre les phrases plus directes et dynamiques", Truth = true },
                    new Option { Nom = "Rendre les phrases plus passives et compliquées", Truth = false },
                    new Option { Nom = "Ajouter de la longueur aux phrases sans ajouter de clarté", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Comment les transitions entre les paragraphes contribuent-elles à la communication écrite ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Elles aident à maintenir un flux logique et cohérent", Truth = true },
                    new Option { Nom = "Elles compliquent la lecture du texte", Truth = false },
                    new Option { Nom = "Elles sont inutiles et peuvent être omises", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est la meilleure pratique pour utiliser les données dans une communication écrite ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Présenter les données de manière claire et soutenue par des explications", Truth = true },
                    new Option { Nom = "Inclure de nombreuses données sans explication", Truth = false },
                    new Option { Nom = "Éviter d'utiliser des données pour ne pas alourdir le texte", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'impact de l'utilisation d'un ton approprié dans la communication écrite ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Il aide à établir une relation positive avec le lecteur", Truth = true },
                    new Option { Nom = "Il n'a pas d'impact significatif sur le message", Truth = false },
                    new Option { Nom = "Il rend le message plus difficile à comprendre", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est l'importance de l'usage correct de la ponctuation dans la communication écrite ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Elle clarifie le sens et structure les phrases", Truth = true },
                    new Option { Nom = "Elle est secondaire par rapport au contenu", Truth = false },
                    new Option { Nom = "Elle n'a pas d'importance si le texte est court", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le rôle des exemples et des illustrations dans un texte écrit ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Ils aident à clarifier et à renforcer les points abordés", Truth = true },
                    new Option { Nom = "Ils sont superflus et peuvent distraire le lecteur", Truth = false },
                    new Option { Nom = "Ils allongent inutilement le texte", Truth = false }
                }
            }
        }
    }, ChapitreNum = 1, Nom = "Communication Écrite Avancée", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" },
                        new Chapitre {Quiz  = new Quiz(){
        Nom = "Stratégies de Communication Internationale",
        Statue = ObjectStatus.Approuver,
        Questions = new List<Question>()
        {
            new Question()
            {
                Nom = "Quelle est la principale considération dans la communication internationale ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Comprendre et respecter les différences culturelles", Truth = true },
                    new Option { Nom = "Uniformiser les messages pour tous les marchés", Truth = false },
                    new Option { Nom = "Utiliser un langage technique complexe", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est un des défis courants dans la communication interculturelle ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Les différences dans les styles de communication et les interprétations", Truth = true },
                    new Option { Nom = "Le manque de données disponibles pour les campagnes", Truth = false },
                    new Option { Nom = "Une trop grande uniformité des messages à l'échelle mondiale", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle stratégie est souvent utilisée pour adapter les messages à différents marchés internationaux ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Localisation des contenus pour refléter les préférences culturelles locales", Truth = true },
                    new Option { Nom = "Utilisation d'un message global uniforme sans adaptation", Truth = false },
                    new Option { Nom = "Éviter de modifier les messages pour maintenir la cohérence", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est un des avantages de la communication bilingue dans les marchés internationaux ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Augmenter la portée et l'efficacité de la communication auprès de diverses audiences", Truth = true },
                    new Option { Nom = "Rendre les messages plus compliqués à comprendre", Truth = false },
                    new Option { Nom = "Réduire le coût des traductions", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est un aspect important de la recherche préalable dans la communication internationale ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Comprendre les normes et les valeurs culturelles des marchés cibles", Truth = true },
                    new Option { Nom = "Utiliser des messages identiques pour tous les marchés", Truth = false },
                    new Option { Nom = "Limiter l'analyse aux aspects techniques du produit", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Comment les différences dans les styles de communication peuvent-elles affecter les campagnes internationales ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Elles peuvent influencer la manière dont les messages sont reçus et interprétés", Truth = true },
                    new Option { Nom = "Elles n'ont pas d'impact sur l'efficacité des messages", Truth = false },
                    new Option { Nom = "Elles simplifient la création des messages pour tous les marchés", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel rôle joue la traduction dans la communication internationale ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Assurer que les messages sont compréhensibles et appropriés pour les audiences locales", Truth = true },
                    new Option { Nom = "Uniformiser les messages pour qu'ils soient identiques dans toutes les langues", Truth = false },
                    new Option { Nom = "Éviter de tenir compte des nuances culturelles", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est une bonne pratique pour gérer les différences culturelles dans les réunions internationales ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Être conscient des différences de communication et adapter son approche en conséquence", Truth = true },
                    new Option { Nom = "Adopter une approche uniforme sans tenir compte des différences culturelles", Truth = false },
                    new Option { Nom = "Éviter les discussions sur les différences culturelles", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'impact de la culture sur la négociation internationale ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Elle influence les attentes, les comportements et les stratégies de négociation", Truth = true },
                    new Option { Nom = "Elle n'a pas d'impact sur le processus de négociation", Truth = false },
                    new Option { Nom = "Elle simplifie les négociations en réduisant les différences entre les parties", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est un des défis de la communication non verbale dans un contexte international ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Les gestes et expressions peuvent avoir des significations différentes dans différentes cultures", Truth = true },
                new Option { Nom = "Les signes non verbaux sont universels et donc ne posent pas de problème", Truth = false },
                new Option { Nom = "La communication non verbale est moins importante que la communication verbale", Truth = false }
                }
            }
        }
    }, ChapitreNum = 2, Nom = "Stratégies de Communication Internationale", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" },
                        new Chapitre {Quiz  = new Quiz()
    {
        Nom = "Techniques de Présentation",
        Statue = ObjectStatus.Approuver,
        Questions = new List<Question>()
        {
            new Question()
            {
                Nom = "Quelle est l'importance de connaître son public avant une présentation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Adapter le contenu et le style de présentation aux besoins et aux attentes du public", Truth = true },
                    new Option { Nom = "Utiliser le même contenu pour tous les publics sans adaptation", Truth = false },
                    new Option { Nom = "Ignorer les préférences du public pour se concentrer sur ses propres intérêts", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est un des éléments clés pour maintenir l'attention du public pendant une présentation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Utiliser des visuels attrayants et des exemples concrets", Truth = true },
                    new Option { Nom = "Lire directement des diapositives sans interaction", Truth = false },
                    new Option { Nom = "Parler de manière monotone et sans pauses", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Comment structurer efficacement une présentation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Avec une introduction claire, un corps bien organisé et une conclusion forte", Truth = true },
                    new Option { Nom = "En suivant un ordre aléatoire sans structure définie", Truth = false },
                    new Option { Nom = "En se concentrant uniquement sur la conclusion", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est l'importance des anecdotes et des histoires dans une présentation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Elles aident à rendre le contenu plus engageant et mémorable", Truth = true },
                    new Option { Nom = "Elles peuvent distraire et rendre la présentation moins professionnelle", Truth = false },
                    new Option { Nom = "Elles ne sont pas nécessaires et peuvent être omises", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle technique est efficace pour gérer le stress avant une présentation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Pratiquer et se préparer à l'avance", Truth = true },
                    new Option { Nom = "Ignorer les symptômes de stress et espérer qu'ils disparaissent d'eux-mêmes", Truth = false },
                    new Option { Nom = "Changer de sujet au dernier moment pour éviter la préparation", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le rôle des questions-réponses dans une présentation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Permettre une interaction avec le public et clarifier les points", Truth = true },
                    new Option { Nom = "Interrompre le flux de la présentation", Truth = false },
                    new Option { Nom = "Éviter de répondre aux questions pour maintenir le contrôle", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est une bonne pratique pour utiliser les diapositives pendant une présentation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Utiliser des diapositives comme support visuel pour renforcer les points clés", Truth = true },
                    new Option { Nom = "Lire les diapositives mot pour mot", Truth = false },
                    new Option { Nom = "Inclure trop de texte ou de détails sur chaque diapositive", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Comment utiliser le langage corporel pour améliorer une présentation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Utiliser des gestes ouverts et un contact visuel pour renforcer le message", Truth = true },
                    new Option { Nom = "Éviter le contact visuel et rester immobile", Truth = false },
                    new Option { Nom = "Se concentrer uniquement sur le contenu verbal et ignorer le langage corporel", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'impact d'une bonne gestion du temps pendant une présentation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Elle permet de couvrir tous les points importants sans dépasser le temps imparti", Truth = true },
                    new Option { Nom = "Elle n'a pas d'impact significatif sur l'efficacité de la présentation", Truth = false },
                    new Option { Nom = "Elle peut être ignorée si le contenu est intéressant", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Pourquoi est-il important de pratiquer une présentation avant de la donner ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Pour améliorer la fluidité, le timing et la confiance en soi", Truth = true },
                    new Option { Nom = "Pour se familiariser avec le matériel au dernier moment", Truth = false },
                    new Option { Nom = "Pour découvrir des points à éviter pendant la présentation", Truth = false }
                }
            }
        }}, ChapitreNum = 3, Nom = "Techniques de Présentation", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" },
                        new Chapitre {Quiz  = new Quiz()
    {
        Nom = "Analyse des Médias",
        Statue = ObjectStatus.Approuver,
        Questions = new List<Question>()
        {
            new Question()
            {
                Nom = "Quel est le principal objectif de l'analyse des médias ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Évaluer l'impact et l'efficacité des messages médiatiques", Truth = true },
                    new Option { Nom = "Augmenter le nombre de publications médiatiques", Truth = false },
                    new Option { Nom = "Créer du contenu sans évaluer sa portée", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est une méthode courante pour mesurer l'impact des campagnes médiatiques ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Analyser les indicateurs clés de performance (KPI) tels que les taux de couverture et d'engagement", Truth = true },
                    new Option { Nom = "Augmenter le budget publicitaire sans mesurer les résultats", Truth = false },
                    new Option { Nom = "Ignorer les statistiques et se fier aux impressions subjectives", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est un aspect important de la veille médiatique ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Surveiller les mentions de la marque et les tendances dans les médias", Truth = true },
                    new Option { Nom = "Publier des communiqués de presse sans surveiller les retours", Truth = false },
                    new Option { Nom = "Limiter la recherche aux médias traditionnels uniquement", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Comment l'analyse de sentiment est-elle utilisée dans l'analyse des médias ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Pour évaluer le ton et les émotions associées aux mentions médiatiques", Truth = true },
                    new Option { Nom = "Pour augmenter le volume de contenu publié sans évaluation", Truth = false },
                    new Option { Nom = "Pour ignorer les réactions négatives et se concentrer uniquement sur les positives", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le rôle des rapports d'analyse dans la gestion des campagnes médiatiques ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Fournir des informations détaillées pour ajuster les stratégies et améliorer les performances", Truth = true },
                    new Option { Nom = "Créer des documents uniquement à des fins de documentation", Truth = false },
                    new Option { Nom = "Éviter les détails pour se concentrer uniquement sur les résultats globaux", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Pourquoi est-il important d'analyser les concurrents dans l'analyse des médias ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Pour comprendre leurs stratégies et identifier des opportunités d'amélioration", Truth = true },
                    new Option { Nom = "Pour copier leurs stratégies sans adaptation", Truth = false },
                    new Option { Nom = "Pour ignorer les tendances du marché et se concentrer uniquement sur ses propres campagnes", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est une bonne pratique pour interpréter les données médiatiques ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Comparer les données avec les objectifs fixés et les benchmarks de l'industrie", Truth = true },
                    new Option { Nom = "Se concentrer uniquement sur les données qui montrent des résultats positifs", Truth = false },
                    new Option { Nom = "Éviter les comparaisons pour simplifier l'analyse", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est un des avantages de l'utilisation des outils d'analyse des médias ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Automatiser la collecte et l'analyse des données pour une évaluation plus rapide", Truth = true },
                    new Option { Nom = "Rendre l'analyse plus complexe et moins accessible", Truth = false },
                    new Option { Nom = "Remplacer entièrement les évaluations humaines", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est une technique pour analyser les tendances médiatiques ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Utiliser des outils de suivi pour identifier les sujets populaires et les changements dans les mentions", Truth = true },
                    new Option { Nom = "Focaliser l'analyse uniquement sur les médias sociaux", Truth = false },
                    new Option { Nom = "Ignorer les nouvelles tendances et se concentrer uniquement sur les données historiques", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'impact de l'analyse des médias sur les décisions stratégiques ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Elle aide à orienter les stratégies en fonction des retours et des résultats médiatiques", Truth = true },
                    new Option { Nom = "Elle est principalement utilisée pour des rapports internes sans impact stratégique", Truth = false },
                    new Option { Nom = "Elle n'a pas d'impact significatif sur les décisions stratégiques", Truth = false }
                }
            }
        }
    },ChapitreNum = 4, Nom = "Analyse des Médias", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" }
                    }
                },
                new Module
                {
                    Nom = "Management des organisations 1",
                    Chapitres = new List<Chapitre>
                    {
                        new Chapitre {Quiz  = new Quiz()
    {
        Nom = "Théories de l'Organisation",
        Statue = ObjectStatus.Approuver,
        Questions = new List<Question>()
        {
            new Question()
            {
                Nom = "Quelle théorie de l'organisation se concentre sur les structures formelles et les règles pour coordonner le travail ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La théorie classique de l'organisation", Truth = true },
                    new Option { Nom = "La théorie des systèmes ouverts", Truth = false },
                    new Option { Nom = "La théorie du capital humain", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est l'idée principale de la théorie des systèmes ouverts ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Les organisations doivent interagir avec leur environnement pour survivre et s'adapter", Truth = true },
                    new Option { Nom = "Les organisations doivent se concentrer uniquement sur leurs processus internes", Truth = false },
                    new Option { Nom = "Les structures organisationnelles doivent être rigides et uniformes", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le concept clé de la théorie de la contingence ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Il n'existe pas de structure organisationnelle unique adaptée à toutes les situations", Truth = true },
                    new Option { Nom = "Les organisations doivent suivre un modèle rigide pour être efficaces", Truth = false },
                    new Option { Nom = "Les processus organisationnels doivent être indépendants de l'environnement externe", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle théorie met l'accent sur l'importance des relations informelles et de la culture organisationnelle ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La théorie des relations humaines", Truth = true },
                    new Option { Nom = "La théorie de l'agence", Truth = false },
                    new Option { Nom = "La théorie des ressources humaines", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Selon la théorie de l'agence, quel est le principal problème à résoudre dans les organisations ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Le conflit d'intérêts entre les agents et les propriétaires", Truth = true },
                    new Option { Nom = "La surcharge d'information des employés", Truth = false },
                    new Option { Nom = "La résistance au changement organisationnel", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle théorie considère les organisations comme des systèmes complexes avec des interactions entre diverses parties ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La théorie des systèmes", Truth = true },
                    new Option { Nom = "La théorie de la gestion scientifique", Truth = false },
                    new Option { Nom = "La théorie de la bureaucratie", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le concept fondamental de la théorie de la gestion scientifique de Frederick Taylor ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "L'optimisation des processus de travail par la standardisation et l'efficacité", Truth = true },
                    new Option { Nom = "La promotion de la flexibilité organisationnelle et des relations humaines", Truth = false },
                    new Option { Nom = "L'importance des dynamiques sociales et culturelles dans les organisations", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle théorie se concentre sur la répartition des pouvoirs et des responsabilités dans une organisation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La théorie de la bureaucratie", Truth = true },
                    new Option { Nom = "La théorie des jeux", Truth = false },
                    new Option { Nom = "La théorie de l'innovation", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est un des principaux objectifs de la théorie du capital humain ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Investir dans le développement des compétences et des connaissances des employés pour améliorer la performance organisationnelle", Truth = true },
                    new Option { Nom = "Réduire les coûts opérationnels en minimisant les investissements dans les ressources humaines", Truth = false },
                    new Option { Nom = "Éliminer la hiérarchie pour favoriser une structure plate et sans leadership", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Dans quelle théorie l'accent est-il mis sur la gestion des relations avec les parties prenantes ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La théorie des parties prenantes", Truth = true },
                    new Option { Nom = "La théorie des contraintes", Truth = false },
                    new Option { Nom = "La théorie du leadership transformationnel", Truth = false }
                }
            }
        }
    },ChapitreNum = 1, Nom = "Théories de l'Organisation", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" },
                        new Chapitre {Quiz = new Quiz()
    {
        Nom = "Gestion des Ressources Humaines",
        Statue = ObjectStatus.Approuver,
        Questions = new List<Question>()
        {
            new Question()
            {
                Nom = "Quel est l'objectif principal de la gestion des ressources humaines (GRH) ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Optimiser le capital humain pour améliorer la performance organisationnelle", Truth = true },
                    new Option { Nom = "Réduire le nombre d'employés pour diminuer les coûts", Truth = false },
                    new Option { Nom = "Ignorer les compétences et le développement des employés", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel processus est crucial pour recruter les bons candidats pour un poste ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Le processus de sélection et d'entretiens", Truth = true },
                    new Option { Nom = "L'augmentation des budgets de recrutement sans analyse", Truth = false },
                    new Option { Nom = "La publication d'annonces uniquement sur les réseaux sociaux", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est l'importance de l'évaluation des performances des employés ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Fournir des retours constructifs pour le développement professionnel et les promotions", Truth = true },
                    new Option { Nom = "Accroître la pression sur les employés pour améliorer les performances", Truth = false },
                    new Option { Nom = "Utiliser l'évaluation pour justifier des réductions de salaire uniquement", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle approche est essentielle pour la gestion des conflits au sein d'une organisation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Adopter une approche proactive de résolution des conflits et de médiation", Truth = true },
                    new Option { Nom = "Ignorer les conflits et espérer qu'ils se résoudront d'eux-mêmes", Truth = false },
                    new Option { Nom = "Encourager la compétition entre les employés pour renforcer la performance", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le rôle de la formation et du développement dans la gestion des ressources humaines ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Améliorer les compétences et les connaissances des employés pour leur progression professionnelle", Truth = true },
                    new Option { Nom = "Réduire le nombre d'employés pour diminuer les coûts de formation", Truth = false },
                    new Option { Nom = "Limiter la formation uniquement aux nouvelles recrues", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est une des fonctions clés des politiques de gestion des ressources humaines ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Établir des directives claires pour la gestion des employés et des pratiques de travail", Truth = true },
                    new Option { Nom = "Permettre une flexibilité totale dans les pratiques de gestion", Truth = false },
                    new Option { Nom = "Éviter toute forme de réglementation pour simplifier la gestion", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Comment la gestion de la rémunération contribue-t-elle à la gestion des ressources humaines ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "En offrant des salaires compétitifs et des avantages pour attirer et retenir les talents", Truth = true },
                    new Option { Nom = "En réduisant les salaires pour minimiser les coûts", Truth = false },
                    new Option { Nom = "En accordant des augmentations uniquement sur la base de l'ancienneté", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est la fonction du bien-être au travail dans la gestion des ressources humaines ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Promouvoir un environnement de travail sain et équilibré pour améliorer la satisfaction et la productivité", Truth = true },
                    new Option { Nom = "Éviter les initiatives de bien-être pour concentrer les ressources sur la performance", Truth = false },
                    new Option { Nom = "Se concentrer uniquement sur les récompenses financières pour motiver les employés", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'objectif de la gestion des talents dans une organisation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Identifier, développer et retenir les meilleurs talents pour soutenir la stratégie organisationnelle", Truth = true },
                    new Option { Nom = "Réduire les coûts liés aux talents en évitant les formations et le développement", Truth = false },
                    new Option { Nom = "Concentrer les efforts uniquement sur les talents internes sans rechercher des talents externes", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le rôle des relations de travail dans la gestion des ressources humaines ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Gérer les relations entre les employés et les employeurs pour assurer une coopération harmonieuse", Truth = true },
                    new Option { Nom = "Éviter les interactions entre employés et direction pour minimiser les conflits", Truth = false },
                    new Option { Nom = "Ignorer les préoccupations des employés pour maintenir l'autorité", Truth = false }
                }
            }
        }
    },ChapitreNum = 2, Nom = "Gestion des Ressources Humaines", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" },
                        new Chapitre {Quiz  = new Quiz()
    {
        Nom = "Management Stratégique",
        Statue = ObjectStatus.Approuver,
        Questions = new List<Question>()
        {
            new Question()
            {
                Nom = "Quel est l'objectif principal du management stratégique ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Développer et mettre en œuvre des stratégies pour atteindre les objectifs à long terme de l'organisation", Truth = true },
                    new Option { Nom = "Réduire les coûts opérationnels à court terme", Truth = false },
                    new Option { Nom = "Optimiser les processus opérationnels au quotidien", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle analyse est souvent utilisée pour évaluer l'environnement externe d'une organisation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Analyse PESTEL", Truth = true },
                    new Option { Nom = "Analyse SWOT", Truth = false },
                    new Option { Nom = "Analyse des forces de Porter", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le but de l'analyse SWOT dans le management stratégique ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Identifier les forces, faiblesses, opportunités et menaces de l'organisation", Truth = true },
                    new Option { Nom = "Évaluer les coûts de production et de distribution", Truth = false },
                    new Option { Nom = "Optimiser les processus internes uniquement", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'objectif principal de la stratégie de différenciation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Créer un produit ou service perçu comme unique par les consommateurs", Truth = true },
                    new Option { Nom = "Réduire les coûts de production au minimum", Truth = false },
                    new Option { Nom = "Augmenter la part de marché en réduisant les prix", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel modèle stratégique évalue la position concurrentielle d'une entreprise en fonction des forces concurrentielles ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Le modèle des cinq forces de Porter", Truth = true },
                    new Option { Nom = "Le modèle BCG", Truth = false },
                    new Option { Nom = "La courbe d'expérience", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est l'importance des objectifs SMART dans la gestion stratégique ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Assurer que les objectifs sont Spécifiques, Mesurables, Atteignables, Réalistes et Temporels", Truth = true },
                    new Option { Nom = "Garantir que les objectifs sont uniquement axés sur les résultats financiers", Truth = false },
                    new Option { Nom = "Éviter la planification à long terme pour se concentrer sur les tâches quotidiennes", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle approche stratégique se concentre sur l'innovation et la recherche de nouveaux marchés ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La stratégie de croissance", Truth = true },
                    new Option { Nom = "La stratégie de stabilisation", Truth = false },
                    new Option { Nom = "La stratégie de désinvestissement", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le rôle de la gestion stratégique des ressources dans l'atteinte des objectifs organisationnels ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Optimiser l'utilisation des ressources humaines, financières et matérielles pour réaliser la stratégie", Truth = true },
                    new Option { Nom = "Réduire les ressources pour diminuer les coûts", Truth = false },
                    new Option { Nom = "Concentrer les ressources uniquement sur les aspects opérationnels de l'entreprise", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'objectif d'une stratégie de diversification ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "S'étendre dans de nouveaux marchés ou secteurs d'activité pour réduire les risques", Truth = true },
                    new Option { Nom = "Réduire la portée des opérations pour se concentrer sur un seul produit", Truth = false },
                    new Option { Nom = "Maintenir les opérations existantes sans expansion", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est l'importance de la veille stratégique dans le management stratégique ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Fournir des informations pertinentes pour ajuster les stratégies en fonction des changements du marché", Truth = true },
                    new Option { Nom = "Concentrer les efforts uniquement sur les aspects financiers", Truth = false },
                    new Option { Nom = "Éviter les ajustements stratégiques pour maintenir la stabilité", Truth = false }
                }
            }
        }
    }, ChapitreNum = 3, Nom = "Management Stratégique", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" },
                        new Chapitre {Quiz  = new Quiz()
    {
        Nom = "Gestion de la Performance",
        Statue = ObjectStatus.Approuver,
        Questions = new List<Question>()
        {
            new Question()
            {
                Nom = "Quel est l'objectif principal de la gestion de la performance dans une organisation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Améliorer la performance individuelle et organisationnelle en établissant des objectifs clairs et en fournissant un retour d'information", Truth = true },
                    new Option { Nom = "Réduire le nombre d'employés pour diminuer les coûts opérationnels", Truth = false },
                    new Option { Nom = "Éviter les évaluations de performance pour minimiser le stress chez les employés", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le rôle des objectifs SMART dans la gestion de la performance ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Assurer que les objectifs sont Spécifiques, Mesurables, Atteignables, Réalistes et Temporels pour faciliter la mesure de la performance", Truth = true },
                    new Option { Nom = "Éviter la fixation d'objectifs précis pour réduire la pression sur les employés", Truth = false },
                    new Option { Nom = "Concentrer les objectifs uniquement sur les résultats financiers sans considérer d'autres aspects", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle méthode est couramment utilisée pour évaluer la performance des employés ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "L'évaluation à 360 degrés", Truth = true },
                    new Option { Nom = "La réduction de la charge de travail", Truth = false },
                    new Option { Nom = "Les réunions informelles sans critères définis", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le principal avantage de la rétroaction continue dans la gestion de la performance ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Permettre aux employés de s'ajuster rapidement et d'améliorer leur performance en temps réel", Truth = true },
                    new Option { Nom = "Éviter toute forme de feedback pour maintenir un environnement de travail sans stress", Truth = false },
                    new Option { Nom = "Limiter les discussions sur la performance aux évaluations annuelles uniquement", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle approche de gestion de la performance se concentre sur la reconnaissance des réussites et des contributions des employés ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La gestion par les objectifs (MBO)", Truth = true },
                    new Option { Nom = "La gestion des coûts", Truth = false },
                    new Option { Nom = "La gestion des crises", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'un des principaux défis de la gestion de la performance ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Assurer l'objectivité et l'équité dans l'évaluation de la performance", Truth = true },
                    new Option { Nom = "Augmenter le nombre d'évaluations pour couvrir tous les aspects", Truth = false },
                    new Option { Nom = "Réduire le feedback pour éviter les conflits", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le but des plans de développement personnel dans la gestion de la performance ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Aider les employés à acquérir de nouvelles compétences et à atteindre leurs objectifs de carrière", Truth = true },
                    new Option { Nom = "Éviter la formation pour réduire les coûts", Truth = false },
                    new Option { Nom = "Se concentrer uniquement sur les objectifs financiers de l'organisation", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle méthode de gestion de la performance implique des discussions formelles entre le manager et l'employé pour définir les objectifs et évaluer les progrès ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Les entretiens annuels de performance", Truth = true },
                    new Option { Nom = "Les réunions informelles quotidiennes", Truth = false },
                    new Option { Nom = "Les rapports financiers trimestriels", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le rôle des indicateurs clés de performance (KPI) dans la gestion de la performance ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Mesurer et suivre les performances par rapport aux objectifs fixés", Truth = true },
                    new Option { Nom = "Éviter la mesure des performances pour se concentrer uniquement sur les résultats", Truth = false },
                    new Option { Nom = "Limiter les KPI aux aspects financiers uniquement", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est un des principaux avantages d'une gestion efficace de la performance ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Améliorer la motivation et l'engagement des employés en alignant les objectifs individuels avec ceux de l'organisation", Truth = true },
                    new Option { Nom = "Réduire les évaluations de performance pour éviter le stress chez les employés", Truth = false },
                    new Option { Nom = "Augmenter les charges de travail pour tester les limites des employés", Truth = false }
                }
            }
        }
    }, ChapitreNum = 4, Nom = "Gestion de la Performance", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" }
                    }
                },
                new Module
                {
                    Nom = "Instruments quantitatifs 2",
                    Chapitres = new List<Chapitre>
                    {
                        new Chapitre {Quiz  = new Quiz()
    {
        Nom = "Instruments Quantitatifs 2",
        Statue = ObjectStatus.Approuver,
        Questions = new List<Question>()
        {
            new Question()
            {
                Nom = "Quel est l'objectif principal de l'analyse de variance (ANOVA) ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Comparer les moyennes de plusieurs groupes pour déterminer s'il y a des différences significatives entre eux", Truth = true },
                    new Option { Nom = "Mesurer la dispersion des données au sein d'un seul groupe", Truth = false },
                    new Option { Nom = "Calculer les probabilités associées à une variable aléatoire", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est la principale différence entre une régression linéaire simple et une régression linéaire multiple ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La régression linéaire simple utilise une seule variable indépendante, tandis que la régression linéaire multiple en utilise plusieurs", Truth = true },
                    new Option { Nom = "La régression linéaire simple est utilisée uniquement pour les données catégorielles", Truth = false },
                    new Option { Nom = "La régression linéaire multiple ne nécessite pas d'évaluation de la qualité de l'ajustement", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le but de la méthode des moindres carrés dans la régression ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Minimiser la somme des carrés des écarts entre les valeurs observées et les valeurs prédites", Truth = true },
                    new Option { Nom = "Maximiser les écarts entre les valeurs observées et les valeurs prédites", Truth = false },
                    new Option { Nom = "Calculer la moyenne des erreurs de prévision", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'indicateur de performance principal dans une analyse de régression ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Le coefficient de détermination (R²)", Truth = true },
                    new Option { Nom = "Le coefficient de corrélation de Spearman", Truth = false },
                    new Option { Nom = "L'écart type de la variable dépendante", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'objectif principal d'une analyse de corrélation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Mesurer la force et la direction de la relation entre deux variables", Truth = true },
                    new Option { Nom = "Comparer les moyennes de plusieurs groupes", Truth = false },
                    new Option { Nom = "Évaluer la dispersion des données au sein d'un groupe", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Qu'est-ce qu'une variable aléatoire dans une distribution de probabilité ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Une variable dont les valeurs sont déterminées par un processus aléatoire", Truth = true },
                    new Option { Nom = "Une variable qui ne change jamais et est donc constante", Truth = false },
                    new Option { Nom = "Une variable dont les valeurs sont fixes et prévisibles", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est la principale caractéristique d'une distribution normale ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Elle est symétrique autour de la moyenne, avec une forme en cloche", Truth = true },
                    new Option { Nom = "Elle a une forme asymétrique avec des queues épaisses", Truth = false },
                    new Option { Nom = "Elle est uniformément distribuée sans pic central", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le but de la méthode de Monte Carlo dans les analyses quantitatives ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Utiliser des simulations aléatoires pour estimer des résultats complexes et des probabilités", Truth = true },
                    new Option { Nom = "Calculer des statistiques descriptives à partir de données échantillonnées", Truth = false },
                    new Option { Nom = "Mesurer la dispersion des données à l'aide d'une méthode analytique", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le rôle des intervalles de confiance en statistiques ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Fournir une estimation de l'intervalle dans lequel une statistique de population se trouve avec un certain niveau de confiance", Truth = true },
                    new Option { Nom = "Déterminer les valeurs extrêmes des données", Truth = false },
                    new Option { Nom = "Évaluer les relations causales entre les variables", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'objectif de la normalisation des données dans les analyses quantitatives ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Mettre les données à une échelle commune pour faciliter la comparaison et l'analyse", Truth = true },
                    new Option { Nom = "Éliminer les valeurs aberrantes sans considération", Truth = false },
                    new Option { Nom = "Augmenter la variance des données pour les rendre plus représentatives", Truth = false }
                }
            }
        }
    }, ChapitreNum = 1, Nom = "Statistiques Inférentielles", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" },
                        new Chapitre {Quiz  = new Quiz()
    {
        Nom = "Analyse Multivariée",
        Statue = ObjectStatus.Approuver,
        Questions = new List<Question>()
        {
            new Question()
            {
                Nom = "Quel est le but principal de l'analyse en composantes principales (ACP) ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Réduire la dimensionnalité des données tout en préservant le maximum de variance", Truth = true },
                    new Option { Nom = "Augmenter le nombre de variables pour mieux expliquer les données", Truth = false },
                    new Option { Nom = "Évaluer la relation entre deux variables", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est la principale différence entre l'analyse en composantes principales (ACP) et l'analyse factorielle ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "L'ACP est principalement utilisée pour la réduction de la dimensionnalité, tandis que l'analyse factorielle est utilisée pour identifier des structures latentes dans les données", Truth = true },
                    new Option { Nom = "L'ACP est utilisée pour identifier des groupes de données, tandis que l'analyse factorielle est utilisée pour réduire la dimensionnalité", Truth = false },
                    new Option { Nom= "Il n'y a aucune différence significative entre l'ACP et l'analyse factorielle", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le principal objectif de la régression multiple ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Modéliser la relation entre une variable dépendante et plusieurs variables indépendantes", Truth = true },
                    new Option { Nom = "Comparer les moyennes de plusieurs groupes", Truth = false },
                    new Option { Nom = "Évaluer la dispersion des données au sein d'un groupe", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le rôle des valeurs propres dans l'analyse en composantes principales (ACP) ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Mesurer la quantité de variance expliquée par chaque composante principale", Truth = true },
                    new Option { Nom = "Déterminer les relations causales entre les variables", Truth = false },
                    new Option { Nom = "Calculer les coefficients de corrélation entre les variables", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Qu'est-ce qu'une analyse discriminante ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Une technique utilisée pour classer des observations en groupes prédéfinis en fonction de leurs caractéristiques", Truth = true },
                    new Option { Nom = "Une méthode pour réduire la dimensionnalité des données en combinant des variables corrélées", Truth = false },
                    new Option { Nom = "Un processus pour mesurer la dispersion des données autour de la moyenne", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le but de l'analyse de cluster ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Regrouper des observations similaires en clusters ou groupes basés sur des caractéristiques communes", Truth = true },
                    new Option { Nom = "Déterminer les relations causales entre les variables", Truth = false },
                    new Option { Nom = "Comparer les moyennes de plusieurs groupes", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Dans une analyse multivariée, quel est le rôle du test de Bartlett ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Tester l'hypothèse que les matrices de covariance des groupes sont égales", Truth = true },
                    new Option { Nom = "Évaluer la normalité des données", Truth = false },
                    new Option { Nom = "Mesurer la dispersion des données dans une seule dimension", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'objectif de la régression logistique dans l'analyse multivariée ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Modéliser la probabilité d'un événement binaire en fonction de plusieurs variables indépendantes", Truth = true },
                    new Option { Nom = "Évaluer la relation linéaire entre deux variables continues", Truth = false },
                    new Option { Nom = "Comparer les moyennes de plusieurs groupes", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est la principale utilisation de la méthode des moindres carrés en régression multiple ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Minimiser la somme des carrés des erreurs entre les valeurs observées et les valeurs prédites", Truth = true },
                    new Option { Nom = "Maximiser la somme des carrés des erreurs pour tester les hypothèses", Truth = false },
                    new Option { Nom = "Calculer les coefficients de corrélation entre les variables", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'objectif de l'analyse des correspondances multiples (ACM) ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Explorer et visualiser les associations entre plusieurs variables qualitatives", Truth = true },
                    new Option { Nom = "Comparer les moyennes de plusieurs groupes de données quantitatives", Truth = false },
                    new Option { Nom = "Mesurer la variance au sein d'un seul groupe de données", Truth = false }
                }
            }
        }
    },ChapitreNum = 2, Nom = "Analyse Multivariée", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" },
                        new Chapitre {Quiz  = new Quiz()
    {
        Nom = "Optimisation et Recherche Opérationnelle",
        Statue = ObjectStatus.Approuver,
        Questions = new List<Question>()
        {
            new Question()
            {
                Nom = "Quel est l'objectif principal de la programmation linéaire ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Optimiser une fonction objective sous des contraintes linéaires", Truth = true },
                    new Option { Nom = "Évaluer les coûts des opérations sans contraintes", Truth = false },
                    new Option { Nom = "Maximiser la variance des variables", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel algorithme est couramment utilisé pour résoudre les problèmes de programmation linéaire ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "L'algorithme du simplexe", Truth = true },
                    new Option { Nom = "L'algorithme de K-means", Truth = false },
                    new Option { Nom = "L'algorithme de Dijkstra", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est la principale différence entre la programmation linéaire et la programmation entière ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La programmation entière exige que certaines ou toutes les variables soient des entiers", Truth = true },
                    new Option { Nom = "La programmation linéaire est utilisée uniquement pour les variables discrètes", Truth = false },
                    new Option { Nom = "La programmation entière n'a pas de fonction objective à optimiser", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'objectif de l'analyse de sensibilité dans la programmation linéaire ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Évaluer comment les changements dans les paramètres affectent la solution optimale", Truth = true },
                    new Option { Nom = "Trouver des solutions optimales sans contraintes", Truth = false },
                    new Option { Nom = "Minimiser la variance des solutions possibles", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le but de la méthode des points intérieurs en optimisation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Résoudre des problèmes de programmation linéaire en utilisant des techniques de points intérieurs", Truth = true },
                    new Option { Nom = "Maximiser le nombre de variables dans les contraintes", Truth = false },
                    new Option { Nom = "Calculer les dérivées secondes de la fonction objectif", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est la principale utilisation des modèles de transport en recherche opérationnelle ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Optimiser le coût de transport entre plusieurs sources et destinations", Truth = true },
                    new Option { Nom = "Évaluer la demande de production pour différents produits", Truth = false },
                    new Option { Nom = "Calculer les prévisions de ventes", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le principal objectif des algorithmes de séparation et évaluation (cutting-plane methods) ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Trouver des solutions optimales pour des problèmes de programmation entière en ajoutant des contraintes", Truth = true },
                    new Option { Nom = "Éliminer les variables non pertinentes de l'analyse", Truth = false },
                    new Option { Nom = "Augmenter le nombre de variables dans le modèle", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le but de l'algorithme de branch-and-bound en optimisation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Trouver la solution optimale à des problèmes de programmation entière en explorant les sous-problèmes", Truth = true },
                    new Option { Nom = "Optimiser une fonction objective en utilisant des techniques de points intérieurs", Truth = false },
                    new Option { Nom = "Calculer les gradients de la fonction objectif", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'objectif de l'algorithme de recherche tabou ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Éviter les solutions déjà explorées et améliorer les solutions locales en recherche heuristique", Truth = true },
                    new Option { Nom = "Résoudre des problèmes de programmation linéaire en utilisant des techniques de points intérieurs", Truth = false },
                    new Option { Nom= "Maximiser la variance dans les ensembles de données", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle technique est utilisée pour résoudre des problèmes d'optimisation combinatoire où la solution est constituée de différentes combinaisons d'éléments ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La programmation dynamique", Truth = true },
                    new Option { Nom = "La programmation linéaire", Truth = false },
                    new Option { Nom = "L'analyse en composantes principales", Truth = false }
                }
            }
        }
    },ChapitreNum = 3, Nom = "Optimisation et Recherche Opérationnelle", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" },
                        new Chapitre {Quiz  = new Quiz()
    {
        Nom = "Méthodes Quantitatives en Finance",
        Statue = ObjectStatus.Approuver,
        Questions = new List<Question>()
        {
            new Question()
            {
                Nom = "Quel est le principal objectif du modèle de Black-Scholes ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Évaluer le prix des options financières", Truth = true },
                    new Option { Nom = "Prédire les mouvements de marché à court terme", Truth = false },
                    new Option { Nom = "Calculer les rendements historiques des actions", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le rôle de la volatilité implicite dans la valorisation des options ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Mesurer les attentes du marché concernant la volatilité future des actifs", Truth = true },
                    new Option { Nom = "Évaluer les rendements passés d'un actif", Truth = false },
                    new Option { Nom = "Déterminer les taux d'intérêt futurs", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le but de la méthode de Monte Carlo en finance ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Simuler des scénarios de prix futurs pour évaluer les instruments financiers", Truth = true },
                    new Option { Nom = "Calculer les rendements historiques de portefeuilles", Truth = false },
                    new Option { Nom = "Déterminer les taux d'intérêt des obligations", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est la principale différence entre la Value at Risk (VaR) et l'Expected Shortfall (ES) ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La VaR mesure la perte maximale attendue avec un certain niveau de confiance, tandis que l'ES mesure la perte moyenne au-delà de la VaR", Truth = true },
                    new Option { Nom = "La VaR est utilisée pour évaluer les rendements historiques, tandis que l'ES mesure la volatilité des actifs", Truth = false },
                    new Option { Nom = "La VaR et l'ES sont des termes interchangeables dans l'évaluation du risque", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'objectif principal du modèle CAPM (Capital Asset Pricing Model) ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Déterminer le rendement attendu d'un actif en fonction de son risque systématique", Truth = true },
                    new Option { Nom = "Évaluer la performance historique d'un portefeuille", Truth = false },
                    new Option { Nom = "Calculer les dividendes futurs des actions", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Dans quel contexte est utilisé le modèle de GARCH (Generalized Autoregressive Conditional Heteroskedasticity) ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Pour modéliser la volatilité conditionnelle des séries temporelles financières", Truth = true },
                    new Option { Nom = "Pour prédire les rendements futurs des actions", Truth = false },
                    new Option { Nom = "Pour évaluer la corrélation entre les actifs", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est la principale utilisation de l'arbitrage dans les marchés financiers ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Exploiter les écarts de prix entre différents marchés ou instruments pour réaliser un profit sans risque", Truth = true },
                    new Option { Nom = "Prédire les mouvements futurs du marché à partir des tendances passées", Truth = false },
                    new Option { Nom = "Évaluer la performance des gestionnaires de fonds", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le rôle de la théorie du portefeuille de Markowitz ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Optimiser la composition d'un portefeuille pour maximiser le rendement pour un niveau de risque donné", Truth = true },
                    new Option { Nom = "Déterminer les prix des options financières", Truth = false },
                    new Option { Nom = "Prédire les taux d'intérêt futurs", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Dans le modèle de diffusion de Black-Scholes, quel processus est utilisé pour modéliser les prix des actifs ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Le processus de diffusion géométrique Brownien", Truth = true },
                    new Option { Nom = "Le processus de marche aléatoire simple", Truth = false },
                    new Option { Nom = "Le processus de Poisson", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est l'utilité principale des tests de stress dans la gestion des risques financiers ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Évaluer la résilience des portefeuilles ou des institutions face à des scénarios de crise extrêmes", Truth = true },
                    new Option { Nom = "Calculer les rendements attendus des actifs", Truth = false },
                    new Option { Nom = "Mesurer la volatilité quotidienne des marchés", Truth = false }
                }
            }
        }
    }, ChapitreNum = 4, Nom = "Méthodes Quantitatives en Finance", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" }
                    }
                },
                new Module
                {
                    Nom = "Environnement des organisations 2",
                    Chapitres = new List<Chapitre>
                    {
                        new Chapitre {Quiz  = new Quiz()
    {
        Nom = "Économie de l'Entreprise",
        Statue = ObjectStatus.Approuver,
        Questions = new List<Question>()
        {
            new Question()
            {
                Nom = "Quel est l'objectif principal de l'analyse des coûts dans une entreprise ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Identifier et contrôler les coûts pour maximiser les profits", Truth = true },
                    new Option { Nom = "Augmenter le volume de production sans considération des coûts", Truth = false },
                    new Option { Nom = "Évaluer uniquement les dépenses marketing", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Qu'est-ce que la théorie des jeux permet d'analyser dans un contexte économique ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Les interactions stratégiques entre plusieurs acteurs économiques", Truth = true },
                    new Option { Nom = "Les rendements historiques des investissements", Truth = false },
                    new Option { Nom = "Les coûts de production des biens", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'objectif de l'analyse de la chaîne de valeur selon Michael Porter ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Décomposer les activités de l'entreprise pour identifier les sources de valeur et d'avantage concurrentiel", Truth = true },
                    new Option { Nom = "Mesurer la rentabilité des produits à partir des ventes", Truth = false },
                    new Option { Nom = "Évaluer les coûts de production en comparaison avec les concurrents", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le rôle du budget prévisionnel dans la gestion d'une entreprise ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Planifier les ressources financières et opérationnelles pour atteindre les objectifs de l'entreprise", Truth = true },
                    new Option { Nom = "Analyser les performances passées de l'entreprise uniquement", Truth = false },
                    new Option { Nom = "Évaluer la satisfaction des clients", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est la principale différence entre coûts fixes et coûts variables ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Les coûts fixes restent constants quelle que soit la production, tandis que les coûts variables changent avec le niveau de production", Truth = true },
                    new Option { Nom = "Les coûts fixes changent avec la production, tandis que les coûts variables restent constants", Truth = false },
                    new Option { Nom = "Les coûts fixes incluent uniquement les coûts de matériel, tandis que les coûts variables incluent uniquement les coûts de main-d'œuvre", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'objectif principal de l'analyse SWOT pour une entreprise ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Identifier les forces, faiblesses, opportunités et menaces pour élaborer des stratégies", Truth = true },
                    new Option { Nom = "Analyser les tendances de consommation dans le marché", Truth = false },
                    new Option { Nom = "Calculer le retour sur investissement des campagnes marketing", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est la principale fonction d'un tableau de bord de gestion dans une entreprise ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Fournir une vue d'ensemble des performances de l'entreprise à travers des indicateurs clés", Truth = true },
                    new Option { Nom = "Calculer les salaires des employés", Truth = false },
                    new Option { Nom = "Évaluer les coûts de production par produit", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel concept économique est utilisé pour évaluer l'efficacité d'une entreprise dans l'utilisation de ses ressources ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La productivité", Truth = true },
                    new Option { Nom = "Le revenu brut", Truth = false },
                    new Option { Nom = "Le bénéfice net", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le but de l'analyse des coûts de revient dans une entreprise ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Déterminer le coût total de production d'un bien ou service pour fixer les prix et améliorer la rentabilité", Truth = true },
                    new Option { Nom = "Calculer les marges bénéficiaires sur les ventes", Truth = false },
                    new Option { Nom = "Évaluer les coûts de publicité", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est la principale utilité des indicateurs financiers pour une entreprise ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Évaluer la santé financière de l'entreprise et prendre des décisions informées", Truth = true },
                    new Option { Nom = "Analyser les préférences des consommateurs", Truth = false },
                    new Option { Nom = "Mesurer la satisfaction des employés", Truth = false }
                }
            }
        }
    }, ChapitreNum = 1, Nom = "Économie de l'Entreprise", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" },
                        new Chapitre {Quiz  = new Quiz()
    {
        Nom = "Environnement Juridique des Affaires",
        Statue = ObjectStatus.Approuver,
        Questions = new List<Question>()
        {
            new Question()
            {
                Nom = "Quel est l'objectif principal du droit des sociétés ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Régir les relations entre les actionnaires, les dirigeants et les autres parties prenantes", Truth = true },
                    new Option { Nom = "Déterminer les prix des produits sur le marché", Truth = false },
                    new Option { Nom = "Fixer les taux d'intérêt des prêts bancaires", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le rôle du contrat de travail dans la relation employeur-employé ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Définir les droits et obligations des parties concernant les conditions de travail et la rémunération", Truth = true },
                    new Option { Nom = "Évaluer la performance financière de l'entreprise", Truth = false },
                    new Option { Nom = "Fixer les stratégies marketing de l'entreprise", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Qu'est-ce que la responsabilité civile des entreprises ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "L'obligation de réparer les dommages causés à autrui par les activités de l'entreprise", Truth = true },
                    new Option { Nom = "La capacité de l'entreprise à investir dans des projets de développement", Truth = false },
                    new Option { Nom = "La gestion des ressources humaines et des opérations internes", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'objectif principal de la législation sur la concurrence ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Prévenir les pratiques anticoncurrentielles et promouvoir une concurrence équitable sur le marché", Truth = true },
                    new Option { Nom = "Réguler les salaires des employés dans les grandes entreprises", Truth = false },
                    new Option { Nom = "Établir des normes de qualité pour les produits", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est la fonction principale des brevets ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Protéger les inventions et les innovations en accordant des droits exclusifs à leur inventeur", Truth = true },
                    new Option { Nom = "Réguler les pratiques commerciales et les prix des produits", Truth = false },
                    new Option { Nom = "Déterminer les conditions de travail et de rémunération des employés", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'objectif du droit de la consommation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Protéger les droits des consommateurs contre les pratiques commerciales trompeuses et injustes", Truth = true },
                    new Option { Nom = "Fixer les normes de production pour les entreprises", Truth = false },
                    new Option { Nom = "Évaluer les performances financières des entreprises", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Qu'est-ce qu'une société à responsabilité limitée (SARL) ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Une forme juridique d'entreprise où la responsabilité des associés est limitée au montant de leurs apports", Truth = true },
                    new Option { Nom = "Un type de société où les actionnaires ont une responsabilité illimitée", Truth = false },
                    new Option { Nom = "Un contrat de travail à durée déterminée", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'objectif principal de la réglementation sur les fusions et acquisitions ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Assurer que les fusions et acquisitions ne créent pas des monopoles ou n'affectent pas négativement la concurrence", Truth = true },
                    new Option { Nom = "Fixer les prix des actions lors de la fusion ou de l'acquisition", Truth = false },
                    new Option { Nom = "Déterminer les stratégies de marketing post-fusion", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le but des clauses de non-concurrence dans un contrat de travail ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Interdire à un employé de travailler pour une entreprise concurrente pendant une certaine période après la fin de son emploi", Truth = true },
                    new Option { Nom = "Définir les conditions de remboursement des frais professionnels", Truth = false },
                    new Option { Nom = "Évaluer les performances de l'employé", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est la principale fonction de la loi sur la transparence financière ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Assurer la divulgation complète et honnête des informations financières des entreprises aux investisseurs et au public", Truth = true },
                    new Option { Nom = "Établir des normes pour les produits de consommation", Truth = false },
                    new Option { Nom = "Fixer les taux d'intérêt des prêts bancaires", Truth = false }
                }
            }
        }
    }, ChapitreNum = 2, Nom = "Environnement Juridique des Affaires", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" },
                        new Chapitre {Quiz  = new Quiz()
    {
        Nom = "Gestion de l'Innovation",
        Statue = ObjectStatus.Approuver,
        Questions = new List<Question>()
        {
            new Question()
            {
                Nom = "Quel est l'objectif principal de la gestion de l'innovation dans une entreprise ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Encourager et diriger les activités innovantes pour créer de la valeur et un avantage concurrentiel", Truth = true },
                    new Option { Nom = "Réduire les coûts de production uniquement", Truth = false },
                    new Option { Nom = "Analyser les performances passées des produits", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Qu'est-ce que le processus d'innovation ouverte ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Collaborer avec des partenaires externes pour développer des innovations", Truth = true },
                    new Option { Nom = "Lancer de nouveaux produits sans consultation externe", Truth = false },
                    new Option { Nom = "Concentrer l'innovation uniquement sur les produits existants", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est la principale différence entre une innovation incrémentale et une innovation radicale ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "L'innovation incrémentale améliore progressivement les produits existants, tandis que l'innovation radicale introduit des changements majeurs et disruptifs", Truth = true },
                    new Option { Nom = "L'innovation incrémentale concerne les nouveaux produits, tandis que l'innovation radicale concerne les améliorations de processus", Truth = false },
                    new Option { Nom = "L'innovation incrémentale est une stratégie de marketing, tandis que l'innovation radicale est une stratégie de gestion des ressources humaines", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le rôle des incubateurs d'entreprises dans la gestion de l'innovation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Fournir un soutien et des ressources aux startups pour les aider à développer et commercialiser leurs innovations", Truth = true },
                    new Option { Nom = "Contrôler les coûts de production des grandes entreprises", Truth = false },
                    new Option { Nom = "Analyser les tendances financières du marché", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est la méthode de gestion de l'innovation qui consiste à générer des idées créatives en impliquant tous les membres de l'organisation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Brainstorming collectif", Truth = true },
                    new Option { Nom = "Analyse SWOT", Truth = false },
                    new Option { Nom = "Planification stratégique", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'objectif de la phase de validation dans le processus d'innovation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Tester et évaluer la viabilité de l'idée innovante avant de procéder à sa mise en œuvre", Truth = true },
                    new Option { Nom = "Lancer le produit sur le marché sans évaluation préalable", Truth = false },
                    new Option { Nom = "Évaluer uniquement les coûts de production", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le rôle du prototypage dans le processus d'innovation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Créer des versions préliminaires du produit pour tester et affiner les concepts", Truth = true },
                    new Option { Nom = "Analyser les ventes historiques des produits", Truth = false },
                    new Option { Nom = "Déterminer les prix de vente des produits", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'objectif de la gestion du portefeuille d'innovation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Évaluer et prioriser les projets d'innovation pour optimiser les ressources et maximiser les retours", Truth = true },
                    new Option { Nom = "Réduire les coûts de marketing uniquement", Truth = false },
                    new Option { Nom = "Fixer les prix des produits de l'entreprise", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel concept est utilisé pour évaluer la valeur de l'innovation en termes de retour sur investissement ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Analyse du retour sur investissement (ROI)", Truth = true },
                    new Option { Nom = "Analyse SWOT", Truth = false },
                    new Option { Nom = "Analyse des tendances de consommation", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le but de la culture d'innovation dans une organisation ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Encourager et soutenir un environnement où les idées nouvelles sont valorisées et expérimentées", Truth = true },
                    new Option { Nom = "Réduire les coûts de production uniquement", Truth = false },
                    new Option { Nom = "Déterminer les prix de vente des produits", Truth = false }
                }
            }
        }
    },ChapitreNum = 3, Nom = "Gestion de l'Innovation", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" },
                        new Chapitre {Quiz  = new Quiz()
    {
        Nom = "Environnement Écologique et Développement Durable",
        Statue = ObjectStatus.Approuver,
        Questions = new List<Question>()
        {
            new Question()
            {
                Nom = "Quel est l'objectif principal du développement durable ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Répondre aux besoins présents sans compromettre la capacité des générations futures à répondre aux leurs", Truth = true },
                    new Option { Nom = "Maximiser la production industrielle à court terme", Truth = false },
                    new Option { Nom = "Réduire les coûts de production des entreprises uniquement", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le rôle des énergies renouvelables dans le développement durable ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Réduire les émissions de gaz à effet de serre en fournissant des alternatives aux énergies fossiles", Truth = true },
                    new Option { Nom = "Augmenter la consommation des ressources non renouvelables", Truth = false },
                    new Option { Nom = "Évaluer uniquement les coûts de production", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Qu'est-ce que l'empreinte écologique ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Une mesure de l'impact des activités humaines sur l'environnement", Truth = true },
                    new Option { Nom = "Un indicateur de la croissance économique d'un pays", Truth = false },
                    new Option { Nom = "Une évaluation des ressources humaines d'une entreprise", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quelle est la principale source de pollution de l'air ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Les émissions des véhicules et des industries", Truth = true },
                    new Option { Nom = "Les activités agricoles uniquement", Truth = false },
                    new Option { Nom = "Les émissions de gaz domestiques", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le but des accords internationaux sur le climat comme l'Accord de Paris ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Limiter le réchauffement climatique en réduisant les émissions de gaz à effet de serre", Truth = true },
                    new Option { Nom = "Augmenter la production d'énergie fossile", Truth = false },
                    new Option { Nom = "Fixer des normes de production pour les biens de consommation", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Qu'est-ce que le principe de précaution en matière environnementale ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Adopter des mesures pour éviter les risques environnementaux potentiels, même en l'absence de preuves scientifiques concluantes", Truth = true },
                    new Option { Nom = "Maximiser la production industrielle sans évaluer les impacts environnementaux", Truth = false },
                    new Option { Nom = "Limiter les investissements dans les technologies propres", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'impact principal de la déforestation sur l'environnement ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La perte de biodiversité et l'augmentation des émissions de CO2", Truth = true },
                    new Option { Nom = "La réduction des prix des matières premières", Truth = false },
                    new Option { Nom = "L'amélioration des conditions climatiques", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est le concept de l'économie circulaire ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Un modèle économique visant à réduire les déchets et à recycler les matériaux pour créer une boucle fermée", Truth = true },
                    new Option { Nom = "Un modèle économique basé sur l'augmentation continue de la production", Truth = false },
                    new Option { Nom = "Une approche centrée uniquement sur la réduction des coûts de production", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'objectif des certificats de carbone ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "Permettre aux entreprises de compenser leurs émissions de CO2 en investissant dans des projets de réduction des émissions", Truth = true },
                    new Option { Nom = "Évaluer les performances financières des entreprises", Truth = false },
                    new Option { Nom = "Fixer les prix des produits sur le marché", Truth = false }
                }
            },
            new Question()
            {
                Nom = "Quel est l'impact de la surpêche sur les écosystèmes marins ?",
                Options = new List<Option>()
                {
                    new Option { Nom = "La réduction des populations de poissons et la perturbation des écosystèmes marins", Truth = true },
                    new Option { Nom = "L'augmentation de la biodiversité marine", Truth = false },
                    new Option { Nom = "La stabilisation des écosystèmes côtiers", Truth = false }
                }
            }
        }
    },ChapitreNum = 4, Nom = "Environnement Écologique et Développement Durable", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" }
                    }
                }
            }
        },
                        new NiveauScolaire
    {
        Nom = "Semestre 3",
        Modules = new List<Module>
        {
            new Module
            {
                Nom = "Langues et Communication 3",
                Chapitres = new List<Chapitre>
                {
                    new Chapitre {Quiz  = new Quiz()
{
    Nom = "Communication Interculturelle",
    Statue = ObjectStatus.Approuver,
    Questions = new List<Question>()
    {
        new Question()
        {
            Nom = "Quel est l'objectif principal de la communication interculturelle ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Faciliter la compréhension et la collaboration entre personnes de cultures différentes", Truth = true },
                new Option { Nom = "Promouvoir une seule culture au détriment des autres", Truth = false },
                new Option { Nom = "Limiter les interactions entre différentes cultures", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est un des principaux défis de la communication interculturelle ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Les différences dans les styles de communication et les normes culturelles", Truth = true },
                new Option { Nom = "Les similitudes entre les cultures", Truth = false },
                new Option { Nom = "L'uniformité des valeurs culturelles", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce que le 'high-context' dans la communication interculturelle ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Une communication où le contexte et les relations sont importants pour comprendre le message", Truth = true },
                new Option { Nom = "Une communication directe et explicite où les informations sont clairement exprimées", Truth = false },
                new Option { Nom = "Une communication uniquement verbale sans utilisation de gestes ou de symboles", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est un exemple de barrière culturelle dans la communication ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Les différences de langue, de croyances et de comportements non verbaux", Truth = true },
                new Option { Nom = "La présence d'un langage commun", Truth = false },
                new Option { Nom = "L'uniformité des styles de communication", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce que la compétence interculturelle ?",
            Options = new List<Option>()
            {
                new Option { Nom = "La capacité à comprendre, respecter et adapter ses comportements en fonction de diverses cultures", Truth = true },
                new Option { Nom = "La maîtrise d'une langue étrangère", Truth = false },
                new Option { Nom = "La connaissance des traditions d'une seule culture", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Pourquoi est-il important de comprendre les différences culturelles dans un contexte professionnel international ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Pour éviter les malentendus et améliorer la collaboration efficace entre équipes multiculturelles", Truth = true },
                new Option { Nom = "Pour imposer des normes culturelles spécifiques aux autres", Truth = false },
                new Option { Nom = "Pour réduire les coûts de communication uniquement", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est un exemple de différence dans les styles de communication entre cultures ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Les cultures 'high-context' utilisent plus d'implicite et de nuances, tandis que les cultures 'low-context' sont plus explicites", Truth = true },
                new Option { Nom = "Toutes les cultures utilisent les mêmes styles de communication verbale", Truth = false },
                new Option { Nom = "Les styles de communication sont identiques dans les interactions professionnelles et personnelles", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce que l'ethnocentrisme et comment cela affecte-t-il la communication interculturelle ?",
            Options = new List<Option>()
            {
                new Option { Nom = "La tendance à évaluer d'autres cultures selon les critères de sa propre culture, ce qui peut entraîner des préjugés et des malentendus", Truth = true },
                new Option { Nom = "L'ouverture d'esprit et l'acceptation des différences culturelles", Truth = false },
                new Option { Nom = "La capacité à comprendre et à adopter toutes les normes culturelles", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel rôle jouent les valeurs culturelles dans la communication interculturelle ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Les valeurs culturelles influencent la manière dont les messages sont interprétés et les comportements sont perçus", Truth = true },
                new Option { Nom = "Les valeurs culturelles n'ont aucun impact sur la communication", Truth = false },
                new Option { Nom = "Les valeurs culturelles sont identiques dans toutes les cultures", Truth = false }
            }
        }
    }
}, ChapitreNum = 1, Nom = "Communication Interculturelle", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" },
                    new Chapitre {Quiz  = new Quiz()
{
    Nom = "Communication Numérique et Réseaux Sociaux",
    Statue = ObjectStatus.Approuver,
    Questions = new List<Question>()
    {
        new Question()
        {
            Nom = "Quel est l'objectif principal de la communication numérique ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Utiliser des plateformes et des outils numériques pour diffuser des messages et interagir avec les audiences", Truth = true },
                new Option { Nom = "Limiter les interactions en ligne pour éviter les distractions", Truth = false },
                new Option { Nom = "Concentrer uniquement sur la communication face-à-face", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est un des avantages principaux des réseaux sociaux pour les entreprises ?",
            Options = new List<Option>()
            {
                new Option { Nom = "La possibilité d'atteindre un large public et d'interagir directement avec les clients", Truth = true },
                new Option { Nom = "L'élimination des coûts de marketing", Truth = false },
                new Option { Nom = "La réduction des besoins en communication interne", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est le rôle du SEO (Search Engine Optimization) dans la communication numérique ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Améliorer la visibilité et le classement d'un site web dans les résultats des moteurs de recherche", Truth = true },
                new Option { Nom = "Créer des campagnes publicitaires payantes", Truth = false },
                new Option { Nom = "Analyser les tendances de consommation", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est un des défis majeurs de la communication sur les réseaux sociaux ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Gérer et répondre aux commentaires et avis en temps réel", Truth = true },
                new Option { Nom = "Éviter les interactions avec les clients", Truth = false },
                new Option { Nom = "Réduire le nombre de publications pour éviter le surmenage", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce que le 'content marketing' dans le contexte de la communication numérique ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Créer et partager du contenu pertinent et de qualité pour attirer et engager l'audience cible", Truth = true },
                new Option { Nom = "Envoyer des emails promotionnels uniquement", Truth = false },
                new Option { Nom = "Limiter la publication de contenu à des périodes spécifiques", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Comment les entreprises utilisent-elles les 'analytics' pour améliorer leur stratégie sur les réseaux sociaux ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Analyser les données sur l'engagement, les performances des publications et les comportements des utilisateurs pour ajuster les stratégies", Truth = true },
                new Option { Nom = "Ignorer les données pour se concentrer sur la créativité uniquement", Truth = false },
                new Option { Nom = "Limiter les rapports d'analyse aux périodes de haute saison", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est l'impact des commentaires et avis des utilisateurs sur les réseaux sociaux ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Ils peuvent influencer la réputation de l'entreprise et affecter la perception de ses produits ou services", Truth = true },
                new Option { Nom = "Ils n'ont aucun impact sur la stratégie de l'entreprise", Truth = false },
                new Option { Nom = "Ils sont ignorés en raison de leur faible pertinence", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est le but de la 'réputation en ligne' pour une entreprise ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Maintenir une image positive et gérer les perceptions publiques en ligne", Truth = true },
                new Option { Nom = "Réduire les interactions avec les clients", Truth = false },
                new Option { Nom = "Limiter les publications sur les réseaux sociaux", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce que la 'publicité ciblée' dans la communication numérique ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Diffuser des annonces spécifiques à des groupes d'audience basés sur leurs intérêts, comportements ou données démographiques", Truth = true },
                new Option { Nom = "Diffuser les mêmes annonces à un public général sans segmentation", Truth = false },
                new Option { Nom = "Éviter les campagnes publicitaires en ligne", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est un des objectifs de la gestion de crise sur les réseaux sociaux ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Gérer rapidement et efficacement les situations négatives pour protéger la réputation de l'entreprise", Truth = true },
                new Option { Nom = "Ignorer les problèmes et espérer qu'ils disparaissent d'eux-mêmes", Truth = false },
                new Option { Nom = "Limiter la communication en ligne pendant les crises", Truth = false }
            }
        }
    }
},ChapitreNum = 2, Nom = "Communication Numérique et Réseaux Sociaux", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" },
                    new Chapitre {Quiz  = new Quiz()
{
    Nom = "Techniques de Négociation",
    Statue = ObjectStatus.Approuver,
    Questions = new List<Question>()
    {
        new Question()
        {
            Nom = "Quel est l'objectif principal de la négociation ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Atteindre un accord mutuellement acceptable entre les parties", Truth = true },
                new Option { Nom = "Imposer ses propres conditions sans tenir compte des autres", Truth = false },
                new Option { Nom = "Éviter toute forme de compromis", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est le rôle de l'écoute active dans la négociation ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Comprendre pleinement les besoins et les préoccupations de l'autre partie", Truth = true },
                new Option { Nom = "Répondre uniquement avec des arguments préétablis", Truth = false },
                new Option { Nom = "Ignorer les commentaires de l'autre partie", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quelle technique de négociation consiste à faire des concessions réciproques ?",
            Options = new List<Option>()
            {
                new Option { Nom = "La technique du donnant-donnant", Truth = true },
                new Option { Nom = "La technique de l'affirmation de soi", Truth = false },
                new Option { Nom = "La technique de l'intimidation", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce qu'une 'BATNA' (Best Alternative to a Negotiated Agreement) dans la négociation ?",
            Options = new List<Option>()
            {
                new Option { Nom = "La meilleure alternative disponible si les négociations échouent", Truth = true },
                new Option { Nom = "Une tactique de négociation agressive", Truth = false },
                new Option { Nom = "Une offre initiale qui est toujours acceptée", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est l'avantage d'utiliser des 'zones de possible accord' (ZOPA) dans la négociation ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Définir une gamme de valeurs où les parties peuvent potentiellement trouver un accord", Truth = true },
                new Option { Nom = "Limiter la négociation à un seul point de vue", Truth = false },
                new Option { Nom = "Éviter de fixer des objectifs clairs", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est l'objectif de la technique de 'l'ancrage' dans la négociation ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Établir une première offre pour influencer les négociations futures", Truth = true },
                new Option { Nom = "Proposer plusieurs options en même temps", Truth = false },
                new Option { Nom = "Éviter toute forme d'offre initiale", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce que la 'négociation distributive' ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Une négociation où les parties se disputent une ressource fixe, souvent appelée 'gagnant-perdant'", Truth = true },
                new Option { Nom = "Une négociation visant à trouver des solutions créatives qui bénéficient à toutes les parties", Truth = false },
                new Option { Nom = "Une technique pour éviter les négociations", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quelle est la principale différence entre la négociation distributive et la négociation intégrative ?",
            Options = new List<Option>()
            {
                new Option { Nom = "La négociation distributive se concentre sur le partage d'une ressource fixe, tandis que la négociation intégrative cherche des solutions gagnant-gagnant", Truth = true },
                new Option { Nom = "La négociation distributive est toujours plus efficace que la négociation intégrative", Truth = false },
                new Option { Nom = "Les deux types de négociation utilisent les mêmes techniques", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est le rôle de la 'préparation' dans la négociation ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Collecter des informations, définir des objectifs et préparer des arguments solides avant la négociation", Truth = true },
                new Option { Nom = "Commencer la négociation sans aucune préparation", Truth = false },
                new Option { Nom = "Limiter les informations disponibles pendant la négociation", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce que la 'négociation basée sur les intérêts' ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Se concentrer sur les besoins et les motivations sous-jacentes des parties plutôt que sur leurs positions initiales", Truth = true },
                new Option { Nom = "S'appuyer uniquement sur les positions déclarées des parties", Truth = false },
                new Option { Nom = "Éviter toute discussion sur les intérêts personnels", Truth = false }
            }
        }
    }
},ChapitreNum = 3, Nom = "Techniques de Négociation", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" },
                    new Chapitre {Quiz  = new Quiz()
{
    Nom = "Rédaction de Contenus Professionnels",
    Statue = ObjectStatus.Approuver,
    Questions = new List<Question>()
    {
        new Question()
        {
            Nom = "Quel est l'objectif principal de la rédaction de contenus professionnels ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Communiquer clairement et efficacement des informations pertinentes à un public cible", Truth = true },
                new Option { Nom = "Écrire des textes uniquement pour remplir des espaces", Truth = false },
                new Option { Nom = "Utiliser un langage complexe pour impressionner le lecteur", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est un des éléments clés d'un bon contenu professionnel ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Clarté et concision dans la présentation des informations", Truth = true },
                new Option { Nom = "Utilisation excessive de jargon et de termes techniques", Truth = false },
                new Option { Nom = "Longueur excessive pour démontrer l'expertise", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce que la 'structuration' dans la rédaction de contenus ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Organiser le contenu de manière logique avec des titres, sous-titres et paragraphes pour améliorer la lisibilité", Truth = true },
                new Option { Nom = "Utiliser des phrases longues et complexes pour impressionner le lecteur", Truth = false },
                new Option { Nom = "Ajouter des éléments graphiques sans rapport avec le contenu", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Pourquoi est-il important de connaître son audience lors de la rédaction de contenus professionnels ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Pour adapter le ton, le style et le niveau de détail en fonction des besoins et des attentes du public cible", Truth = true },
                new Option { Nom = "Pour écrire le contenu de manière à ce qu'il soit compréhensible uniquement pour les experts", Truth = false },
                new Option { Nom = "Pour utiliser des termes techniques qui ne sont pas nécessairement pertinents pour l'audience", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel rôle joue la 'révision' dans la rédaction de contenus professionnels ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Corriger les erreurs, améliorer la clarté et garantir que le contenu répond aux objectifs fixés", Truth = true },
                new Option { Nom = "Ignorer les erreurs pour accélérer le processus de publication", Truth = false },
                new Option { Nom = "Modifier le contenu pour qu'il soit plus long", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce qu'un 'appel à l'action' (CTA) dans un contenu professionnel ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Une instruction claire et concise demandant au lecteur de réaliser une action spécifique, comme s'inscrire ou acheter", Truth = true },
                new Option { Nom = "Une phrase décorative sans but précis", Truth = false },
                new Option { Nom = "Une partie du contenu sans interaction prévue avec le lecteur", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Comment la 'tonalité' du contenu affecte-t-elle sa réception ?",
            Options = new List<Option>()
            {
                new Option { Nom = "La tonalité influence la perception et l'engagement du lecteur en fonction du style et de l'émotion transmis", Truth = true },
                new Option { Nom = "La tonalité n'a pas d'impact sur la compréhension du contenu", Truth = false },
                new Option { Nom = "La tonalité doit toujours être formelle et distante", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Pourquoi est-il important d'utiliser des exemples concrets dans le contenu professionnel ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Pour illustrer des points complexes et rendre le contenu plus accessible et engageant", Truth = true },
                new Option { Nom = "Pour ajouter des détails non pertinents au texte", Truth = false },
                new Option { Nom = "Pour remplir l'espace et allonger le texte", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est l'impact de l'optimisation pour les moteurs de recherche (SEO) sur la rédaction de contenus professionnels ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Améliorer la visibilité du contenu sur les moteurs de recherche en utilisant des mots-clés et des techniques de référencement", Truth = true },
                new Option { Nom = "Augmenter la longueur du contenu sans ajouter de valeur", Truth = false },
                new Option { Nom = "Réduire la clarté pour inclure davantage de mots-clés", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est le rôle des 'métadonnées' dans le contenu professionnel en ligne ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Fournir des informations sur le contenu, telles que les titres, descriptions et mots-clés, pour améliorer le référencement et la gestion des informations", Truth = true },
                new Option { Nom = "Ajouter des détails non pertinents pour le contenu", Truth = false },
                new Option { Nom = "Réduire la visibilité du contenu en ligne", Truth = false }
            }
        }
    }
}, ChapitreNum = 4, Nom = "Rédaction de Contenus Professionnels", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" }
                }
            },
            new Module
            {
                Nom = "Environnement économique",
                Chapitres = new List<Chapitre>
                {
                    new Chapitre {Quiz  = new Quiz()
{
    Nom = "Concepts Fondamentaux de l'Économie",
    Statue = ObjectStatus.Approuver,
    Questions = new List<Question>()
    {
        new Question()
        {
            Nom = "Quel est le principe de l'offre et de la demande en économie ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Les prix sont déterminés par l'interaction entre l'offre des producteurs et la demande des consommateurs", Truth = true },
                new Option { Nom = "Les prix sont fixés par les autorités gouvernementales indépendamment de l'offre et de la demande", Truth = false },
                new Option { Nom = "Les prix sont déterminés uniquement par la demande des consommateurs", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce que le produit intérieur brut (PIB) mesure ?",
            Options = new List<Option>()
            {
                new Option { Nom = "La valeur totale de tous les biens et services produits dans un pays pendant une période donnée", Truth = true },
                new Option { Nom = "La quantité de monnaie en circulation dans une économie", Truth = false },
                new Option { Nom = "Le niveau des prix des biens et services dans une économie", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est le rôle de la 'politique monétaire' dans une économie ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Influencer la quantité de monnaie en circulation et les taux d'intérêt pour contrôler l'inflation et stimuler la croissance économique", Truth = true },
                new Option { Nom = "Réguler directement les prix des biens et services", Truth = false },
                new Option { Nom = "Fixer les salaires minimaux dans l'économie", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce que l'inflation ?",
            Options = new List<Option>()
            {
                new Option { Nom = "L'augmentation générale et continue des prix des biens et services dans une économie", Truth = true },
                new Option { Nom = "La diminution du niveau de la production économique", Truth = false },
                new Option { Nom = "La baisse du taux de chômage", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce que le 'coût d'opportunité' en économie ?",
            Options = new List<Option>()
            {
                new Option { Nom = "La valeur de la meilleure alternative à laquelle on renonce en prenant une décision économique", Truth = true },
                new Option { Nom = "Le coût total de production d'un bien ou service", Truth = false },
                new Option { Nom = "Le montant des impôts payés sur un revenu", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est le concept de 'rente économique' ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Le surplus de revenu qu'un producteur obtient au-delà de ce qui est nécessaire pour le maintenir en production", Truth = true },
                new Option { Nom = "Le revenu total généré par la vente d'un produit", Truth = false },
                new Option { Nom = "Le montant des dépenses publiques pour un projet économique", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce que le 'marché' en économie ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Un lieu où se rencontrent les acheteurs et les vendeurs pour échanger des biens et des services", Truth = true },
                new Option { Nom = "Une institution gouvernementale qui fixe les prix des produits", Truth = false },
                new Option { Nom = "Un document de politique économique", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est l'objectif principal de la 'politique budgétaire' ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Influencer l'économie en ajustant les niveaux de dépenses publiques et de taxation", Truth = true },
                new Option { Nom = "Fixer les taux d'intérêt pour contrôler la masse monétaire", Truth = false },
                new Option { Nom = "Réguler le marché du travail et les conditions de travail", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce que le 'multiplicateur' en économie ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Le concept selon lequel un changement dans les dépenses publiques ou privées peut entraîner un changement plus que proportionnel dans le revenu national", Truth = true },
                new Option { Nom = "Le taux de croissance de la population active", Truth = false },
                new Option { Nom = "la différence entre les importations et les exportations", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est le rôle des 'marchés financiers' dans une économie ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Faciliter l'échange de capitaux entre les investisseurs et les emprunteurs", Truth = true },
                new Option { Nom = "Fixer les taux de change des devises", Truth = false },
                new Option { Nom = "Gérer directement la production de biens et services", Truth = false }
            }
        }
    }}, ChapitreNum = 1, Nom = "Concepts Fondamentaux de l'Économie", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" },
                    new Chapitre {Quiz  = new Quiz()
{
    Nom = "Analyse Microéconomique",
    Statue = ObjectStatus.Approuver,
    Questions = new List<Question>()
    {
        new Question()
        {
            Nom = "Qu'est-ce que la 'loi de la demande' ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Lorsque le prix d'un bien augmente, la quantité demandée de ce bien diminue, et inversement", Truth = true },
                new Option { Nom = "Lorsque le prix d'un bien augmente, la quantité demandée de ce bien augmente également", Truth = false },
                new Option { Nom = "La demande d'un bien est indépendante de son prix", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est le concept de 'l'élasticité-prix de la demande' ?",
            Options = new List<Option>()
            {
                new Option { Nom = "La mesure de la sensibilité de la quantité demandée d'un bien par rapport à un changement de prix", Truth = true },
                new Option { Nom = "La relation entre le coût de production et le prix de vente d'un bien", Truth = false },
                new Option { Nom = "La variation du revenu d'un consommateur en réponse à un changement de prix", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce que la 'courbe d'offre' ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Une représentation graphique de la relation entre le prix d'un bien et la quantité offerte par les producteurs", Truth = true },
                new Option { Nom = "Une courbe montrant la relation entre le coût de production et la demande d'un bien", Truth = false },
                new Option { Nom = "Une courbe indiquant la quantité totale demandée par les consommateurs", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est l'effet de la 'concurrence parfaite' sur les prix ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Les prix tendent vers le niveau de coût marginal et reflètent la pleine information disponible", Truth = true },
                new Option { Nom = "Les prix sont fixés par un monopoleur qui peut influencer le marché", Truth = false },
                new Option { Nom = "Les prix fluctuent fortement en raison du manque d'information", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce qu'une 'externalité' en microéconomie ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Un effet secondaire d'une activité économique qui affecte des tiers non impliqués dans cette activité", Truth = true },
                new Option { Nom = "Un coût direct lié à la production d'un bien", Truth = false },
                new Option { Nom = "Le revenu généré par une entreprise", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce qu'un 'monopole' ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Une situation de marché où un seul producteur contrôle l'offre d'un bien ou service", Truth = true },
                new Option { Nom = "Un marché avec de nombreux producteurs offrant des biens identiques", Truth = false },
                new Option { Nom = "Un marché où les prix sont déterminés par l'interaction entre l'offre et la demande", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est le concept de 'coût marginal' ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Le coût additionnel pour produire une unité supplémentaire d'un bien", Truth = true },
                new Option { Nom = "Le coût total de production d'une unité d'un bien", Truth = false },
                new Option { Nom = "Le coût fixe total qui ne change pas avec la production", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce que la 'théorie des jeux' ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Une analyse des décisions stratégiques où les résultats dépendent des choix des autres participants", Truth = true },
                new Option { Nom = "Une étude des préférences des consommateurs pour divers biens", Truth = false },
                new Option { Nom = "Une théorie sur les effets des politiques monétaires sur l'économie", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est l'impact des 'subventions' sur le marché ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Elles augmentent généralement l'offre en réduisant le coût de production pour les producteurs", Truth = true },
                new Option { Nom = "Elles augmentent le prix des biens pour les consommateurs", Truth = false },
                new Option { Nom = "Elles diminuent la demande des consommateurs en augmentant les prix", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce qu'un 'bien public' en économie ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Un bien qui est non exclusif et non rival, c'est-à-dire que sa consommation par une personne ne réduit pas sa disponibilité pour d'autres", Truth = true },
            new Option { Nom = "Un bien dont la consommation est limitée à un groupe spécifique de personnes", Truth = false },
            new Option { Nom = "Un bien qui est uniquement disponible pour l'achat dans des magasins spécialisés", Truth = false }
            }
        }
    }
}, ChapitreNum = 2, Nom = "Analyse Microéconomique", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" },
                    new Chapitre {Quiz  = new Quiz()
{
    Nom = "Analyse Macroeconomique",
    Statue = ObjectStatus.Approuver,
    Questions = new List<Question>()
    {
        new Question()
        {
            Nom = "Qu'est-ce que le produit national brut (PNB) mesure ?",
            Options = new List<Option>()
            {
                new Option { Nom = "La valeur totale des biens et services produits par les résidents d'un pays, qu'ils soient à l'intérieur ou à l'extérieur du pays", Truth = true },
                new Option { Nom = "La valeur totale des biens et services produits à l'intérieur d'un pays, indépendamment de la nationalité des producteurs", Truth = false },
                new Option { Nom = "La quantité de monnaie en circulation dans une économie", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est l'objectif principal de la 'politique fiscale' ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Influencer l'économie en ajustant les niveaux de dépenses publiques et de taxation", Truth = true },
                new Option { Nom = "Contrôler les taux d'intérêt pour réguler l'offre de monnaie", Truth = false },
                new Option { Nom = "Réguler le marché du travail et les conditions de travail", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce que le 'taux de chômage' ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Le pourcentage de la population active qui est sans emploi et qui cherche activement un travail", Truth = true },
                new Option { Nom = "Le taux de croissance du PIB par rapport à l'année précédente", Truth = false },
                new Option { Nom = "Le pourcentage de la population totale qui reçoit des aides gouvernementales", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce que la 'courbe de Phillips' représente ?",
            Options = new List<Option>()
            {
                new Option { Nom = "La relation inverse entre le taux de chômage et le taux d'inflation dans une économie", Truth = true },
                new Option { Nom = "La relation entre la croissance économique et les dépenses publiques", Truth = false },
                new Option { Nom = "La relation entre la consommation et l'investissement des entreprises", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est le concept de 'déficit budgétaire' ?",
            Options = new List<Option>()
            {
                new Option { Nom = "La situation où les dépenses publiques dépassent les recettes publiques", Truth = true },
                new Option { Nom = "Le montant total des recettes publiques dans un budget", Truth = false },
                new Option { Nom = "La différence entre les importations et les exportations d'un pays", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce que la 'théorie de la croissance économique' ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Une théorie qui examine les facteurs déterminants de la croissance à long terme du PIB", Truth = true },
                new Option { Nom = "Une analyse des fluctuations à court terme de l'économie", Truth = false },
                new Option { Nom = " l'impact des politiques monétaires sur le marché du travail", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est le rôle des 'réserves obligatoires' dans la politique monétaire ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Les réserves que les banques doivent conserver pour limiter leur capacité à prêter et influencer l'offre de monnaie", Truth = true },
                new Option { Nom = "Les fonds que le gouvernement utilise pour financer des projets d'infrastructure", Truth = false },
                new Option { Nom = "Les revenus générés par les taxes et les impôts", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce que le 'taux d'intérêt nominal' ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Le taux d'intérêt avant ajustement pour l'inflation", Truth = true },
                new Option { Nom = "Le taux d'intérêt ajusté pour l'inflation", Truth = false },
                new Option { Nom = "l'inflation annuelle moyenne", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce que le 'multiplieur keynésien' ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Le concept selon lequel une augmentation des dépenses publiques peut entraîner une augmentation plus que proportionnelle du revenu national", Truth = true },
                new Option { Nom = "Le taux de croissance du PIB en réponse à un changement de politique monétaire", Truth = false },
                new Option { Nom =" l'effet des taxes sur la consommation des ménages", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est l'impact de l' 'ouverture commerciale' sur l'économie ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Elle peut entraîner une spécialisation et une augmentation de l'efficacité économique grâce à la concurrence internationale", Truth = true },
                new Option { Nom = "Elle entraîne toujours une augmentation des barrières commerciales et des tarifs douaniers", Truth = false },
                new Option { Nom = "Elle réduit le niveau des investissements étrangers", Truth = false }
            }
        }
    }
}, ChapitreNum = 3, Nom = "Analyse Macroeconomique", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" },
                    new Chapitre {Quiz  = new Quiz()
{
    Nom = "Politiques Économiques et Régulation",
    Statue = ObjectStatus.Approuver,
    Questions = new List<Question>()
    {
        new Question()
        {
            Nom = "Quel est l'objectif principal de la politique monétaire ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Contrôler la masse monétaire et les taux d'intérêt pour stabiliser l'économie", Truth = true },
                new Option { Nom = "Fixer les salaires et réguler les conditions de travail", Truth = false },
                new Option { Nom = "Déterminer les prix des biens et services sur le marché", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce que la 'politique budgétaire' ?",
            Options = new List<Option>()
            {
                new Option { Nom = "L'utilisation des dépenses publiques et des impôts pour influencer l'économie", Truth = true },
                new Option { Nom = "La régulation des prix des biens et services", Truth = false },
                new Option { Nom = "la gestion de la masse monétaire par les banques centrales", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est l'impact d'une politique de 'quantitative easing' ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Augmenter la masse monétaire en achetant des actifs financiers pour stimuler l'économie", Truth = true },
                new Option { Nom = "Réduire les dépenses publiques pour diminuer le déficit budgétaire", Truth = false },
                new Option { Nom = "l'augmentation des taux d'intérêt pour réduire l'inflation", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce que la 'régulation anti-trust' ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Les lois et politiques visant à prévenir les pratiques anticoncurrentielles et les monopoles", Truth = true },
                new Option { Nom = "Les politiques visant à encourager la concentration des industries pour plus d'efficacité", Truth = false },
                new Option { Nom = "la gestion des crises financières par les gouvernements", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est le rôle des 'organismes de régulation' dans une économie ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Assurer le respect des règles et lois économiques pour maintenir un marché compétitif et stable", Truth = true },
                new Option { Nom = "Fixer les prix des biens et services pour protéger les consommateurs", Truth = false },
                new Option { Nom = "la gestion des réserves de change pour influencer les taux de change", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce qu'une 'politique de subventions' ?",
            Options = new List<Option>()
            {
                new Option { Nom = "L'octroi d'aides financières aux entreprises ou aux individus pour encourager certaines activités économiques", Truth = true },
                new Option { Nom = "La fixation des salaires minimaux par le gouvernement", Truth = false },
                new Option { Nom = "la régulation des taux d'intérêt par les banques centrales", Truth = false }
            }
        },
            new Question()
            {
                Nom = "Quel est l'objectif d'une 'politique de désinflation' ?",
                Options = new List<Option>()
            {
                new Option { Nom = "Réduire le taux d'inflation pour stabiliser l'économie", Truth = true },
                new Option { Nom = "Augmenter la masse monétaire pour stimuler la croissance économique", Truth = false },
                new Option { Nom = "l'augmentation des impôts pour réduire le déficit budgétaire", Truth = false }
            }
        },
                new Question()
                {
                    Nom = "Qu'est-ce que la 'régulation des marchés financiers' ?",
                    Options = new List<Option>()
            {
                new Option { Nom = "Les lois et régulations visant à garantir la transparence et la stabilité des marchés financiers", Truth = true },
                new Option { Nom = "La gestion des prix des biens et services sur les marchés de consommation", Truth = false },
                new Option { Nom = "la politique de création de monnaie pour stimuler l'économie", Truth = false }
            }
        },
                    new Question()
                    {
                        Nom = "Quel est le but des 'contrôles de change' ?",
                        Options = new List<Option>()
            {
                new Option { Nom = "Limiter les mouvements de capitaux étrangers et stabiliser la monnaie locale", Truth = true },
                new Option { Nom = "Fixer les salaires pour tous les travailleurs dans une économie", Truth = false },
                new Option { Nom =" la régulation des marchés de produits pour éviter les pénuries", Truth = false }
            }
        },
                        new Question()
                        {
                            Nom = "Qu'est-ce qu'une 'politique de rigueur' ?",
                            Options = new List<Option>()
            {
                new Option { Nom = "Une politique économique visant à réduire les déficits budgétaires et à contrôler l'inflation par des mesures d'austérité", Truth = true },
                new Option { Nom = "Une politique visant à augmenter les dépenses publiques pour stimuler la croissance économique", Truth = false },
                new Option { Nom =" la fixation des prix des produits de base pour les rendre accessibles", Truth = false }
            }
        }
                        }
                    },                    ChapitreNum = 4, Nom = "Politiques Économiques et Régulation", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" }
                }
            },
            new Module
            {
                Nom = "Management des organisations 2",
                Chapitres = new List<Chapitre>
                {
                    new Chapitre {Quiz  = new Quiz()
{
    Nom = "Gestion du Changement Organisationnel",
    Statue = ObjectStatus.Approuver,
    Questions = new List<Question>()
    {
        new Question()
        {
            Nom = "Quel est l'objectif principal de la gestion du changement organisationnel ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Assurer une transition efficace et minimiser les perturbations lors de l'introduction de changements dans une organisation", Truth = true },
                new Option { Nom = "Augmenter les bénéfices immédiats de l'organisation", Truth = false },
                new Option { Nom = "Réduire le nombre d'employés dans l'organisation", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est le rôle des 'leaders du changement' ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Diriger et inspirer les équipes pour accepter et s'adapter aux changements organisationnels", Truth = true },
                new Option { Nom = "Évaluer la performance financière des employés", Truth = false },
                new Option { Nom = "Fixer les objectifs de ventes à court terme", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce que le 'modèle de Kotter' en gestion du changement ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Un cadre en 8 étapes pour conduire le changement et gérer la transition efficacement", Truth = true },
                new Option { Nom = "Un modèle pour l'analyse des états financiers des entreprises", Truth = false },
                new Option { Nom = "Un processus en 5 étapes pour recruter de nouveaux employés", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quelle est la première étape du modèle de Kotter pour le changement organisationnel ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Créer un sentiment d'urgence", Truth = true },
                new Option { Nom = "Développer une vision et une stratégie", Truth = false },
                new Option { Nom = "Consolider les gains et produire plus de changement", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est l'objectif de la 'communication du changement' ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Informer et engager les parties prenantes sur le changement à venir et ses impacts", Truth = true },
                new Option { Nom = "Réduire les coûts opérationnels de l'organisation", Truth = false },
                new Option { Nom = "Fixer des objectifs de vente et de production", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce qu'un 'champion du changement' ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Une personne qui soutient activement et promeut le changement au sein de l'organisation", Truth = true },
                new Option { Nom = "Un employé qui résiste au changement", Truth = false },
                new Option { Nom = "Un consultant externe engagé pour analyser les finances de l'organisation", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est le rôle de la 'formation' dans la gestion du changement ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Préparer les employés aux nouvelles compétences et processus nécessaires pour s'adapter au changement", Truth = true },
                new Option { Nom = "Augmenter les salaires des employés pour compenser le changement", Truth = false },
                new Option { Nom = "Développer de nouveaux produits et services", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce que le 'feedback' dans le processus de gestion du changement ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Les commentaires et les réactions des employés sur le changement, utilisés pour ajuster et améliorer le processus", Truth = true },
                new Option { Nom = "Les évaluations de performance des employés après le changement", Truth = false },
                new Option { Nom = "Les données financières sur les résultats du changement", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quelle est l'importance de la 'résistance au changement' ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Comprendre et gérer les objections des employés pour faciliter l'acceptation du changement", Truth = true },
                new Option { Nom = "Augmenter la charge de travail des employés pour tester leur endurance", Truth = false },
                new Option { Nom = "Réduire les dépenses de formation pour les nouvelles technologies", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est l'objectif de la 'soutien continu' dans la gestion du changement ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Assurer une assistance continue aux employés pour renforcer les nouvelles pratiques et surmonter les défis", Truth = true },
                new Option { Nom = "Réduire les coûts opérationnels immédiatement après le changement", Truth = false },
                new Option { Nom = "Évaluer les résultats financiers du changement", Truth = false }
            }
        }
    }
},ChapitreNum = 1, Nom = "Gestion du Changement Organisationnel", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" },
                    new Chapitre {Quiz  = new Quiz()
{
    Nom = "Management de Projet",
    Statue = ObjectStatus.Approuver,
    Questions = new List<Question>()
    {
        new Question()
        {
            Nom = "Quel est le premier processus du cycle de vie d'un projet ?",
            Options = new List<Option>()
            {
                new Option { Nom = "La définition des objectifs et du périmètre du projet", Truth = true },
                new Option { Nom = "La planification des ressources", Truth = false },
                new Option { Nom = "La gestion des risques", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est l'objectif de la 'charte de projet' ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Définir le périmètre, les objectifs, et les parties prenantes du projet", Truth = true },
                new Option { Nom = "Établir le budget et le calendrier du projet", Truth = false },
                new Option { Nom = "Créer des rapports de performance du projet", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce que le 'chemin critique' dans la gestion de projet ?",
            Options = new List<Option>()
            {
                new Option { Nom = "La séquence de tâches qui détermine la durée totale du projet", Truth = true },
                new Option { Nom = "Les tâches qui peuvent être exécutées en parallèle", Truth = false },
                new Option { Nom = "Les tâches avec des coûts fixes et non variables", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est le but de la 'planification des ressources' ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Assurer que les ressources nécessaires sont disponibles et correctement allouées pour le projet", Truth = true },
                new Option { Nom = "Déterminer les exigences techniques du projet", Truth = false },
                new Option { Nom = "Évaluer les risques potentiels du projet", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce que le 'budget de projet' inclut généralement ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Les coûts estimés pour toutes les phases et tâches du projet", Truth = true },
                new Option { Nom = "Les bénéfices attendus du projet", Truth = false },
                new Option { Nom = "Les prévisions de ventes et de revenus", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est le rôle du 'responsable de projet' ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Coordonner, superviser et diriger toutes les activités du projet pour atteindre les objectifs fixés", Truth = true },
                new Option { Nom = "Évaluer les performances des employés de l'organisation", Truth = false },
                new Option { Nom = "Développer des stratégies marketing pour le produit final", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce qu'une 'analyse des parties prenantes' ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Identifier et évaluer les attentes et l'impact des parties prenantes sur le projet", Truth = true },
                new Option { Nom = "Analyser les coûts et les bénéfices du projet", Truth = false },
                new Option { Nom = "Évaluer la performance financière des fournisseurs", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est l'objectif de la 'gestion des risques' dans un projet ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Identifier, évaluer et atténuer les risques potentiels pour minimiser leur impact sur le projet", Truth = true },
                new Option { Nom = "Maximiser les bénéfices du projet", Truth = false },
                new Option { Nom = "Accélérer le calendrier du projet", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce qu'une 'évaluation des performances du projet' ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Mesurer et analyser la progression et l'efficacité du projet par rapport aux objectifs fixés", Truth = true },
                new Option { Nom = "Déterminer le prix de vente du produit final", Truth = false },
                new Option { Nom = "Établir les conditions de travail pour les employés", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est le but du 'contrôle de qualité' dans un projet ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Assurer que le produit final répond aux exigences et aux normes de qualité spécifiées", Truth = true },
                new Option { Nom = "Augmenter le budget du projet pour améliorer les ressources", Truth = false },
                new Option { Nom = "Réduire le temps de développement en accélérant les processus", Truth = false }
            }
        }
    }
},ChapitreNum = 2, Nom = "Management de Projet", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" },
                    new Chapitre {Quiz  = new Quiz()
{
    Nom = "Gestion des Conflits et Négociations",
    Statue = ObjectStatus.Approuver,
    Questions = new List<Question>()
    {
        new Question()
        {
            Nom = "Quel est l'objectif principal de la gestion des conflits ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Résoudre les différends de manière constructive et maintenir des relations positives", Truth = true },
                new Option { Nom = "Éliminer tous les conflits au sein de l'organisation", Truth = false },
                new Option { Nom = "Ignorer les conflits pour éviter les perturbations", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quelle est la première étape du processus de résolution de conflit ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Identifier et comprendre les causes du conflit", Truth = true },
                new Option { Nom = "Proposer des solutions de compromis", Truth = false },
                new Option { Nom = "Médiation entre les parties en conflit", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce que la 'négociation intégrative' ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Une approche de négociation visant à trouver des solutions gagnant-gagnant où toutes les parties bénéficient", Truth = true },
                new Option { Nom = "Une approche de négociation où chaque partie cherche à maximiser ses propres avantages, souvent au détriment de l'autre partie", Truth = false },
                new Option { Nom = "Un processus formel de résolution de conflit par un arbitrage externe", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quelle est la principale différence entre la négociation distributive et la négociation intégrative ?",
            Options = new List<Option>()
            {
                new Option { Nom = "La négociation distributive se concentre sur la répartition d'une ressource fixe, tandis que la négociation intégrative cherche à créer de la valeur supplémentaire", Truth = true },
                new Option { Nom = "La négociation distributive implique plusieurs parties, tandis que la négociation intégrative est un processus unilatéral", Truth = false },
                new Option { Nom = "La négociation distributive est informelle, tandis que la négociation intégrative est toujours formelle", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est l'objectif de la médiation dans la gestion des conflits ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Faciliter la communication entre les parties en conflit pour parvenir à un accord mutuel", Truth = true },
                new Option { Nom = "Imposer une solution aux parties en conflit", Truth = false },
                new Option { Nom = "Évaluer les fautes des parties impliquées", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quelle compétence est essentielle pour un négociateur efficace ?",
            Options = new List<Option>()
            {
                new Option { Nom = "La capacité d'écouter activement et de comprendre les besoins des autres parties", Truth = true },
                new Option { Nom = "La capacité à ignorer les objections pour obtenir ce que l'on veut", Truth = false },
                new Option { Nom = "La capacité à éviter toute forme de compromis", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce que la 'négociation basée sur les intérêts' ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Une approche qui se concentre sur les intérêts sous-jacents des parties plutôt que sur leurs positions initiales", Truth = true },
                new Option { Nom = "Une approche qui fixe des conditions strictes dès le début de la négociation", Truth = false },
                new Option { Nom = "Une approche où les parties ne font aucune concession", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quelle est la méthode de résolution de conflit qui utilise une tierce partie pour rendre une décision contraignante ?",
            Options = new List<Option>()
            {
                new Option { Nom = "L'arbitrage", Truth = true },
                new Option { Nom = "La médiation", Truth = false },
                new Option { Nom = "La négociation", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quelle technique peut être utilisée pour désamorcer un conflit avant qu'il ne s'aggrave ?",
            Options = new List<Option>()
            {
                new Option { Nom = "La communication ouverte et honnête", Truth = true },
                new Option { Nom = "L'isolement des parties en conflit", Truth = false },
                new Option { Nom = "La mise en œuvre de sanctions immédiates", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est le rôle des 'compromis' dans la résolution des conflits ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Trouver une solution où chaque partie fait des concessions pour parvenir à un accord", Truth = true },
                new Option { Nom = "Imposer des conditions aux parties en conflit sans négociation", Truth = false },
                new Option { Nom = "Éviter toute forme de négociation ou de concession", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce que l'approche 'collaborative' dans la gestion des conflits ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Travailler ensemble pour trouver une solution qui satisfait les besoins de toutes les parties", Truth = true },
                new Option { Nom = "Résoudre le conflit en prenant une position dominante", Truth = false },
                new Option { Nom = "Éviter de discuter du conflit pour ne pas aggraver la situation", Truth = false }
            }
        }
    }
}, ChapitreNum = 3, Nom = "Gestion des Conflits et Négociations", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" },
                    new Chapitre {Quiz  = new Quiz()
{
    Nom = "Approches Modernes du Leadership",
    Statue = ObjectStatus.Approuver,
    Questions = new List<Question>()
    {
        new Question()
        {
            Nom = "Quelle est la principale caractéristique du leadership transformationnel ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Inspirer et motiver les employés à dépasser leurs propres intérêts pour le bien de l'organisation", Truth = true },
                new Option { Nom = "Focaliser uniquement sur les tâches et les résultats", Truth = false },
                new Option { Nom = "Imposer des directives strictes sans consulter les employés", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est le but du leadership servant ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Mettre les besoins des employés en premier et les aider à se développer et à réussir", Truth = true },
                new Option { Nom = "Concentrer le pouvoir et les décisions entre les mains du leader", Truth = false },
                new Option { Nom = "Exiger la conformité sans offrir de soutien ou de direction", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce que le leadership situationnel implique ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Adapter le style de leadership en fonction de la situation et des besoins des employés", Truth = true },
                new Option { Nom = "Utiliser un seul style de leadership pour toutes les situations", Truth = false },
                new Option { Nom = "Imposer des décisions sans tenir compte des circonstances", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est l'accent principal du leadership authentique ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Être transparent, honnête et fidèle à ses valeurs personnelles tout en dirigeant", Truth = true },
                new Option { Nom = "Adapter constamment son comportement pour plaire aux autres", Truth = false },
                new Option { Nom = "Éviter les conflits et maintenir un consensus à tout prix", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est le concept clé du leadership participatif ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Impliquer les employés dans le processus de prise de décision", Truth = true },
                new Option { Nom = "Prendre toutes les décisions de manière unilatérale", Truth = false },
                new Option { Nom = "Diriger uniquement par des directives et des ordres", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce que le leadership charismatique ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Utiliser son charme personnel et son charisme pour influencer et motiver les autres", Truth = true },
                new Option { Nom = "Focaliser uniquement sur les résultats financiers de l'organisation", Truth = false },
                new Option { Nom = "Éviter les interactions personnelles avec les employés", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est le rôle principal du leader dans le modèle de leadership transformationnel ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Créer une vision inspirante et encourager les employés à adopter des objectifs ambitieux", Truth = true },
                new Option { Nom = "Contrôler minutieusement chaque tâche et activité", Truth = false },
                new Option { Nom = "Minimiser les interactions avec les employés pour éviter les distractions", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est l'objectif principal du leadership adaptatif ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Aider les individus et les équipes à s'ajuster aux changements complexes et incertains", Truth = true },
                new Option { Nom = "Maintenir un style de leadership rigide malgré les changements", Truth = false },
                new Option { Nom = "Focaliser sur la stabilité et éviter les risques", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce que le leadership situationnel nécessite de la part du leader ?",
            Options = new List<Option>()
            {
                new Option { Nom = "La flexibilité pour ajuster le style de leadership en fonction des besoins des membres de l'équipe et de la situation", Truth = true },
                new Option { Nom = "Une approche uniforme pour toutes les situations et tous les membres de l'équipe", Truth = false },
                new Option { Nom = "La concentration sur les tâches et la délégation des responsabilités sans interaction", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quelle approche de leadership se concentre sur la création d'un environnement où les employés se sentent valorisés et soutenus ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Le leadership servant", Truth = true },
                new Option { Nom = "Le leadership transactionnel", Truth = false },
                new Option { Nom = "Le leadership autocratique", Truth = false }
            }
        }
    }
}, ChapitreNum = 4, Nom = "Approches Modernes du Leadership", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" }
                }
            },
            new Module
            {
                Nom = "Techniques quantitatives 1",
                Chapitres = new List<Chapitre>
                {
                    new Chapitre {Quiz  = new Quiz()
{
    Nom = "Introduction à l'Analyse de Données",
    Statue = ObjectStatus.Approuver,
    Questions = new List<Question>()
    {
        new Question()
        {
            Nom = "Qu'est-ce que l'analyse de données ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Le processus d'inspection, de nettoyage et de modélisation des données pour découvrir des informations utiles", Truth = true },
                new Option { Nom = "La collecte de données sans objectif précis", Truth = false },
                new Option { Nom = "L'enregistrement des données sans les analyser", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quelle est la première étape de l'analyse de données ?",
            Options = new List<Option>()
            {
                new Option { Nom = "La collecte de données", Truth = true },
                new Option { Nom = "La visualisation des données", Truth = false },
                new Option { Nom = "L'application de techniques d'apprentissage automatique", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quelle méthode est couramment utilisée pour détecter les valeurs aberrantes dans un jeu de données ?",
            Options = new List<Option>()
            {
                new Option { Nom = "L'analyse de la boîte à moustaches (boxplot)", Truth = true },
                new Option { Nom = "L'utilisation d'un tableau croisé dynamique", Truth = false },
                new Option { Nom = "L'application d'un filtre passe-bas", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quelle technique est utilisée pour réduire la dimensionnalité d'un jeu de données ?",
            Options = new List<Option>()
            {
                new Option { Nom = "L'analyse en composantes principales (PCA)", Truth = true },
                new Option { Nom = "La classification k-means", Truth = false },
                new Option { Nom = "Le calcul des fréquences", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel type de graphique est le mieux adapté pour représenter la distribution d'une variable continue ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Un histogramme", Truth = true },
                new Option { Nom = "Un diagramme de Venn", Truth = false },
                new Option { Nom = "Un graphique en secteurs (camembert)", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quelle est l'une des principales utilisations des diagrammes de dispersion (scatter plots) ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Visualiser la relation entre deux variables continues", Truth = true },
                new Option { Nom = "Afficher la répartition des catégories dans un échantillon", Truth = false },
                new Option { Nom = "Comparer les proportions de différentes parties d'un tout", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce qu'une valeur manquante dans un jeu de données ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Une valeur absente pour une variable dans un ou plusieurs enregistrements", Truth = true },
                new Option { Nom = "Une valeur extrêmement élevée par rapport aux autres valeurs", Truth = false },
                new Option { Nom = "Une valeur qui se répète fréquemment dans le jeu de données", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quelle technique est utilisée pour estimer la relation entre une variable dépendante et une ou plusieurs variables indépendantes ?",
            Options = new List<Option>()
            {
                new Option { Nom = "La régression linéaire", Truth = true },
                new Option { Nom = "La classification hiérarchique", Truth = false },
                new Option { Nom = "L'analyse de cohorte", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel outil est souvent utilisé pour la visualisation de données ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Tableau", Truth = true },
                new Option { Nom = "SQL", Truth = false },
                new Option { Nom = "Python", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est l'objectif principal du nettoyage des données ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Améliorer la qualité des données en supprimant ou corrigeant les erreurs", Truth = true },
                new Option { Nom = "Augmenter le volume des données collectées", Truth = false },
                new Option { Nom = "Réduire la taille des fichiers de données", Truth = false }
            }
        }
    }
},ChapitreNum = 1, Nom = "Introduction à l'Analyse de Données", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" },
                    new Chapitre {Quiz  = new Quiz()
{
    Nom = "Algèbre Linéaire et Applications",
    Statue = ObjectStatus.Approuver,
    Questions = new List<Question>()
    {
        new Question()
        {
            Nom = "Qu'est-ce qu'un vecteur dans l'algèbre linéaire ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Un objet qui a une magnitude et une direction", Truth = true },
                new Option { Nom = "Une matrice carrée", Truth = false },
                new Option { Nom = "Un scalaire", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Que représente une matrice dans l'algèbre linéaire ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Un tableau de nombres arrangé en lignes et colonnes", Truth = true },
                new Option { Nom = "Un nombre unique", Truth = false },
                new Option { Nom = "Un vecteur de dimension 1", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce qu'un espace vectoriel ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Un ensemble de vecteurs qui peuvent être additionnés entre eux et multipliés par des scalaires", Truth = true },
                new Option { Nom = "Un ensemble de matrices", Truth = false },
                new Option { Nom = "Un ensemble de nombres complexes", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est le déterminant d'une matrice 2x2 [ [a, b], [c, d] ] ?",
            Options = new List<Option>()
            {
                new Option { Nom = "ad - bc", Truth = true },
                new Option { Nom = "a + d", Truth = false },
                new Option { Nom = "ac + bd", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est le produit matriciel de deux matrices A et B ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Une matrice obtenue en multipliant les lignes de A par les colonnes de B", Truth = true },
                new Option { Nom = "Une matrice obtenue en additionnant les éléments correspondants de A et B", Truth = false },
                new Option { Nom = "Une matrice obtenue en soustrayant les éléments de B de ceux de A", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce qu'un vecteur propre d'une matrice ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Un vecteur qui ne change pas de direction sous l'application de la matrice", Truth = true },
                new Option { Nom = "Un vecteur dont les éléments sont tous positifs", Truth = false },
                new Option { Nom = "Un vecteur dont la norme est égale à 1", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce que la décomposition de valeurs singulières (SVD) d'une matrice ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Une décomposition d'une matrice en trois matrices : une matrice orthogonale, une matrice diagonale, et une autre matrice orthogonale", Truth = true },
                new Option { Nom = "Une décomposition d'une matrice en une somme de matrices triangulaires", Truth = false },
                new Option { Nom = "Une méthode pour trouver le déterminant d'une matrice", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quelle est l'application de la méthode de Gauss-Jordan en algèbre linéaire ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Résoudre des systèmes d'équations linéaires", Truth = true },
                new Option { Nom = "Calculer le déterminant d'une matrice", Truth = false },
                new Option { Nom = "Trouver les valeurs propres d'une matrice", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quelle propriété doit avoir une matrice pour être inversible ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Avoir un déterminant non nul", Truth = true },
                new Option { Nom = "Être une matrice diagonale", Truth = false },
                new Option { Nom = "Avoir des éléments positifs", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Que représente le rang d'une matrice ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Le nombre maximal de lignes ou de colonnes linéairement indépendantes", Truth = true },
                new Option { Nom = "Le nombre de zéros dans la matrice", Truth = false },
                new Option { Nom = "Le produit des éléments diagonaux", Truth = false }
            }
        }
    }
}, ChapitreNum = 2, Nom = "Algèbre Linéaire et Applications", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" },
                    new Chapitre {Quiz  = new Quiz()
{
    Nom = "Méthodes de Prédiction et de Prévision",
    Statue = ObjectStatus.Approuver,
    Questions = new List<Question>()
    {
        new Question()
        {
            Nom = "Qu'est-ce que la prévision dans le contexte de l'analyse de données ?",
            Options = new List<Option>()
            {
                new Option { Nom = "L'utilisation de modèles statistiques pour estimer des valeurs futures basées sur des données historiques", Truth = true },
                new Option { Nom = "L'analyse des corrélations entre variables", Truth = false },
                new Option { Nom = "La collecte de nouvelles données", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quelle est la méthode la plus courante de prévision pour des données temporelles ?",
            Options = new List<Option>()
            {
                new Option { Nom = "La méthode des moyennes mobiles", Truth = true },
                new Option { Nom = "L'analyse factorielle", Truth = false },
                new Option { Nom = "La classification k-means", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est le principal avantage des modèles ARIMA en prévision ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Ils peuvent capturer à la fois les tendances et les saisonnalités dans les séries temporelles", Truth = true },
                new Option { Nom = "Ils nécessitent peu de données historiques", Truth = false },
                new Option { Nom = "Ils sont simples à interpréter", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quelle est une application typique des techniques de régression en prévision ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Estimer la demande future de produits", Truth = true },
                new Option { Nom = "Identifier des clusters dans des données", Truth = false },
                new Option { Nom = "Réduire la dimensionnalité des données", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce qu'un modèle de régression linéaire simple ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Un modèle qui prédit une variable dépendante à partir d'une seule variable indépendante", Truth = true },
                new Option { Nom = "Un modèle qui utilise plusieurs variables indépendantes", Truth = false },
                new Option { Nom = "Un modèle qui n'utilise aucune variable indépendante", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quel est le rôle des coefficients dans un modèle de régression linéaire ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Ils déterminent l'importance relative des variables indépendantes dans la prédiction de la variable dépendante", Truth = true },
                new Option { Nom = "Ils indiquent le nombre de prédictions correctes", Truth = false },
                new Option { Nom = "Ils servent de labels pour les données", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quelle méthode est utilisée pour évaluer les performances des modèles de prévision ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Le calcul de l'erreur quadratique moyenne (MSE)", Truth = true },
                new Option { Nom = "La validation croisée k-fold", Truth = false },
                new Option { Nom = "L'analyse de variance (ANOVA)", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce que la saisonnalité dans les données temporelles ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Des motifs ou cycles qui se répètent à intervalles réguliers dans les données", Truth = true },
                new Option { Nom = "Des variations aléatoires dans les données", Truth = false },
                new Option { Nom = "Une tendance à long terme dans les données", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quelle est l'utilisation des méthodes de lissage exponentiel dans les prévisions ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Pour réduire les variations et mettre en évidence les tendances dans les séries temporelles", Truth = true },
                new Option { Nom = "Pour segmenter les données en clusters", Truth = false },
                new Option { Nom = "Pour générer des arbres de décision", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quelle technique est souvent utilisée pour valider un modèle de prévision ?",
            Options = new List<Option>()
            {
                new Option { Nom = "La validation croisée", Truth = true },
                new Option { Nom = "L'analyse en composantes principales (PCA)", Truth = false },
                new Option { Nom = "Le clustering hiérarchique", Truth = false }
            }
        }
    }
},ChapitreNum = 3, Nom = "Méthodes de Prédiction et de Prévision", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" },
                    new Chapitre {Quiz  = new Quiz()
{
    Nom = "Techniques de Modélisation Statistique",
    Statue = ObjectStatus.Approuver,
    Questions = new List<Question>()
    {
        new Question()
        {
            Nom = "Qu'est-ce que la modélisation statistique ?",
            Options = new List<Option>()
            {
                new Option { Nom = "L'utilisation de techniques statistiques pour créer des modèles qui expliquent ou prédisent des phénomènes", Truth = true },
                new Option { Nom = "La collecte de données brutes", Truth = false },
                new Option { Nom = "L'analyse descriptive des données", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quelle est la différence entre un modèle de régression linéaire simple et multiple ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Un modèle de régression linéaire simple utilise une seule variable indépendante, tandis qu'un modèle de régression linéaire multiple en utilise plusieurs", Truth = true },
                new Option { Nom = "Un modèle de régression linéaire simple est utilisé pour des données temporelles, tandis qu'un modèle multiple ne l'est pas", Truth = false },
                new Option { Nom = "Un modèle de régression linéaire multiple est toujours non linéaire", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce qu'une variable dépendante dans un modèle statistique ?",
            Options = new List<Option>()
            {
                new Option { Nom = "La variable que l'on cherche à prédire ou expliquer", Truth = true },
                new Option { Nom = "La variable que l'on contrôle ou manipule", Truth = false },
                new Option { Nom = "Une variable qui est toujours constante", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quelle est l'hypothèse clé d'un modèle de régression linéaire ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Il existe une relation linéaire entre la variable dépendante et les variables indépendantes", Truth = true },
                new Option { Nom = "Les variables doivent être indépendantes les unes des autres", Truth = false },
                new Option { Nom = "Les données doivent suivre une distribution normale", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce qu'une valeur résiduelle dans un modèle de régression ?",
            Options = new List<Option>()
            {
                new Option { Nom = "La différence entre les valeurs observées et les valeurs prédites par le modèle", Truth = true },
                new Option { Nom = "Une valeur qui n'est pas utilisée dans le modèle", Truth = false },
                new Option { Nom = "Une variable indépendante", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quelle technique statistique est utilisée pour prédire une variable qualitative ?",
            Options = new List<Option>()
            {
                new Option { Nom = "La régression logistique", Truth = true },
                new Option { Nom = "La régression linéaire", Truth = false },
                new Option { Nom = "L'analyse de variance (ANOVA)", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce que l'analyse de variance (ANOVA) ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Une technique utilisée pour comparer les moyennes de plusieurs groupes", Truth = true },
                new Option { Nom = "Une méthode pour réduire la dimensionnalité des données", Truth = false },
                new Option { Nom = "Une technique de clustering", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quelle est l'utilité des coefficients de détermination (R²) dans la régression ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Ils mesurent la proportion de la variance de la variable dépendante qui est expliquée par les variables indépendantes", Truth = true },
                new Option { Nom = "Ils indiquent la fiabilité des prédictions futures", Truth = false },
                new Option { Nom = "Ils calculent la corrélation entre les variables indépendantes", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Qu'est-ce qu'un modèle de régression non linéaire ?",
            Options = new List<Option>()
            {
                new Option { Nom = "Un modèle où la relation entre les variables dépendantes et indépendantes n'est pas une ligne droite", Truth = true },
                new Option { Nom = "Un modèle qui utilise des variables indépendantes catégorielles", Truth = false },
                new Option { Nom = "Un modèle qui ne nécessite pas de validation", Truth = false }
            }
        },
        new Question()
        {
            Nom = "Quelle méthode est souvent utilisée pour évaluer la performance d'un modèle de classification ?",
            Options = new List<Option>()
            {
                new Option { Nom = "La matrice de confusion", Truth = true },
                new Option { Nom = "L'analyse en composantes principales (PCA)", Truth = false },
                new Option { Nom = "Le test t de Student", Truth = false }
            }
        }
    }
}, ChapitreNum = 4, Nom = "Techniques de Modélisation Statistique", Premium = true, Statue = ObjectStatus.Approuver, VideoPath = "/video/0f4c864f-bb6a-49ed-bc40-7a9088161219_University Promo Video Template (Editable).mp4", CoursPdfPath = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Schema = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf", Synthese = "/controle/02dca4f8-fa80-4319-9c71-dc201d0bab6a_6df57240-a274-4909-9112-855996c47734_Apercu_generale.pdf" }
                }
            }
        }
    }
                    }
                    };


                    await context.institutions.AddRangeAsync(institution1);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
