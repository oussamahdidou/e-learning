﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api.Data;

#nullable disable

namespace api.Migrations
{
    [DbContext(typeof(apiDbContext))]
    partial class apiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "e73fcf02-e570-4a69-9069-e3c31608b538",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "006975be-857f-4ac9-a4db-2b7054f168a7",
                            Name = "Teacher",
                            NormalizedName = "TEACHER"
                        },
                        new
                        {
                            Id = "2cceb3da-bea4-4e8c-9c42-635474128152",
                            Name = "Student",
                            NormalizedName = "STUDENT"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("api.Model.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("AppUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("api.Model.Chapitre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChapitreNum")
                        .HasColumnType("int");

                    b.Property<int?>("ControleId")
                        .HasColumnType("int");

                    b.Property<string>("CoursPdfPath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ModuleId")
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Premium")
                        .HasColumnType("bit");

                    b.Property<int?>("QuizId")
                        .HasColumnType("int");

                    b.Property<string>("Schema")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Statue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Synthese")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VideoPath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ControleId");

                    b.HasIndex("ModuleId");

                    b.HasIndex("QuizId");

                    b.ToTable("chapitres");
                });

            modelBuilder.Entity("api.Model.CheckChapter", b =>
                {
                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ChapitreId")
                        .HasColumnType("int");

                    b.HasKey("StudentId", "ChapitreId");

                    b.HasIndex("ChapitreId");

                    b.ToTable("checkChapters");
                });

            modelBuilder.Entity("api.Model.Controle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ennonce")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Solution")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("controles");
                });

            modelBuilder.Entity("api.Model.ExamFinal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ennonce")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Solution")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("examFinals");
                });

            modelBuilder.Entity("api.Model.Institution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("institutions");
                });

            modelBuilder.Entity("api.Model.Module", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CourseProgram")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ExamFinalId")
                        .HasColumnType("int");

                    b.Property<string>("ModuleImg")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NiveauScolaireId")
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExamFinalId");

                    b.HasIndex("NiveauScolaireId");

                    b.ToTable("modules");
                });

            modelBuilder.Entity("api.Model.ModuleRequirement", b =>
                {
                    b.Property<int>("RequiredModuleId")
                        .HasColumnType("int");

                    b.Property<int>("TargetModuleId")
                        .HasColumnType("int");

                    b.Property<double>("Seuill")
                        .HasColumnType("float");

                    b.HasKey("RequiredModuleId", "TargetModuleId");

                    b.HasIndex("TargetModuleId");

                    b.ToTable("moduleRequirements");
                });

            modelBuilder.Entity("api.Model.NiveauScolaire", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("InstitutionId")
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("InstitutionId");

                    b.ToTable("niveauScolaires");
                });

            modelBuilder.Entity("api.Model.Option", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<bool>("Truth")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("options");
                });

            modelBuilder.Entity("api.Model.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuizId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QuizId");

                    b.ToTable("questions");
                });

            modelBuilder.Entity("api.Model.Quiz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Statue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("quizzes");
                });

            modelBuilder.Entity("api.Model.QuizResult", b =>
                {
                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("QuizId")
                        .HasColumnType("int");

                    b.Property<double>("Note")
                        .HasColumnType("float");

                    b.HasKey("StudentId", "QuizId");

                    b.HasIndex("QuizId");

                    b.ToTable("quizResults");
                });

            modelBuilder.Entity("api.Model.ResultControle", b =>
                {
                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ControleId")
                        .HasColumnType("int");

                    b.Property<string>("Reponse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentId", "ControleId");

                    b.HasIndex("ControleId");

                    b.ToTable("resultControles");
                });

            modelBuilder.Entity("api.Model.ResultExam", b =>
                {
                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ExamFinalId")
                        .HasColumnType("int");

                    b.Property<string>("Reponse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentId", "ExamFinalId");

                    b.HasIndex("ExamFinalId");

                    b.ToTable("resultExams");
                });

            modelBuilder.Entity("api.Model.TestNiveau", b =>
                {
                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ModuleId")
                        .HasColumnType("int");

                    b.Property<double>("Note")
                        .HasColumnType("float");

                    b.HasKey("StudentId", "ModuleId");

                    b.HasIndex("ModuleId");

                    b.ToTable("testNiveaus");
                });

            modelBuilder.Entity("api.Model.Admin", b =>
                {
                    b.HasBaseType("api.Model.AppUser");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("api.Model.Student", b =>
                {
                    b.HasBaseType("api.Model.AppUser");

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("api.Model.Teacher", b =>
                {
                    b.HasBaseType("api.Model.AppUser");

                    b.Property<bool>("Granted")
                        .HasColumnType("bit");

                    b.HasDiscriminator().HasValue("Teacher");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("api.Model.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("api.Model.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Model.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("api.Model.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("api.Model.Chapitre", b =>
                {
                    b.HasOne("api.Model.Controle", "Controle")
                        .WithMany("Chapitres")
                        .HasForeignKey("ControleId");

                    b.HasOne("api.Model.Module", "Module")
                        .WithMany("Chapitres")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Model.Quiz", "Quiz")
                        .WithMany()
                        .HasForeignKey("QuizId");

                    b.Navigation("Controle");

                    b.Navigation("Module");

                    b.Navigation("Quiz");
                });

            modelBuilder.Entity("api.Model.CheckChapter", b =>
                {
                    b.HasOne("api.Model.Chapitre", "Chapitre")
                        .WithMany("CheckChapters")
                        .HasForeignKey("ChapitreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Model.Student", "Student")
                        .WithMany("CheckChapters")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chapitre");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("api.Model.Module", b =>
                {
                    b.HasOne("api.Model.ExamFinal", "ExamFinal")
                        .WithMany()
                        .HasForeignKey("ExamFinalId");

                    b.HasOne("api.Model.NiveauScolaire", "NiveauScolaire")
                        .WithMany("Modules")
                        .HasForeignKey("NiveauScolaireId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExamFinal");

                    b.Navigation("NiveauScolaire");
                });

            modelBuilder.Entity("api.Model.ModuleRequirement", b =>
                {
                    b.HasOne("api.Model.Module", "RequiredModule")
                        .WithMany("ModulesRequiredIn")
                        .HasForeignKey("RequiredModuleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("api.Model.Module", "TargetModule")
                        .WithMany("ModuleRequirements")
                        .HasForeignKey("TargetModuleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("RequiredModule");

                    b.Navigation("TargetModule");
                });

            modelBuilder.Entity("api.Model.NiveauScolaire", b =>
                {
                    b.HasOne("api.Model.Institution", "Institution")
                        .WithMany("NiveauScolaires")
                        .HasForeignKey("InstitutionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Institution");
                });

            modelBuilder.Entity("api.Model.Option", b =>
                {
                    b.HasOne("api.Model.Question", "Question")
                        .WithMany("Options")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("api.Model.Question", b =>
                {
                    b.HasOne("api.Model.Quiz", "Quiz")
                        .WithMany("Questions")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Quiz");
                });

            modelBuilder.Entity("api.Model.QuizResult", b =>
                {
                    b.HasOne("api.Model.Quiz", "Quiz")
                        .WithMany("QuizResults")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Model.Student", "Student")
                        .WithMany("QuizResults")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Quiz");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("api.Model.ResultControle", b =>
                {
                    b.HasOne("api.Model.Controle", "Controle")
                        .WithMany("ResultControles")
                        .HasForeignKey("ControleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Model.Student", "Student")
                        .WithMany("ResultControles")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Controle");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("api.Model.ResultExam", b =>
                {
                    b.HasOne("api.Model.ExamFinal", "ExamFinal")
                        .WithMany("ResultExams")
                        .HasForeignKey("ExamFinalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Model.Student", "Student")
                        .WithMany("ResultExams")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExamFinal");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("api.Model.TestNiveau", b =>
                {
                    b.HasOne("api.Model.Module", "Module")
                        .WithMany("TestNiveaus")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Model.Student", "Student")
                        .WithMany("TestNiveaus")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Module");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("api.Model.Chapitre", b =>
                {
                    b.Navigation("CheckChapters");
                });

            modelBuilder.Entity("api.Model.Controle", b =>
                {
                    b.Navigation("Chapitres");

                    b.Navigation("ResultControles");
                });

            modelBuilder.Entity("api.Model.ExamFinal", b =>
                {
                    b.Navigation("ResultExams");
                });

            modelBuilder.Entity("api.Model.Institution", b =>
                {
                    b.Navigation("NiveauScolaires");
                });

            modelBuilder.Entity("api.Model.Module", b =>
                {
                    b.Navigation("Chapitres");

                    b.Navigation("ModuleRequirements");

                    b.Navigation("ModulesRequiredIn");

                    b.Navigation("TestNiveaus");
                });

            modelBuilder.Entity("api.Model.NiveauScolaire", b =>
                {
                    b.Navigation("Modules");
                });

            modelBuilder.Entity("api.Model.Question", b =>
                {
                    b.Navigation("Options");
                });

            modelBuilder.Entity("api.Model.Quiz", b =>
                {
                    b.Navigation("Questions");

                    b.Navigation("QuizResults");
                });

            modelBuilder.Entity("api.Model.Student", b =>
                {
                    b.Navigation("CheckChapters");

                    b.Navigation("QuizResults");

                    b.Navigation("ResultControles");

                    b.Navigation("ResultExams");

                    b.Navigation("TestNiveaus");
                });
#pragma warning restore 612, 618
        }
    }
}
