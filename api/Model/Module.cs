using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Model
{
    public class Module
    {
        [Key]
        public int Id { get; set; }
        public string Nom { get; set; } = "";
        public string ModuleImg { get; set; } = "https://blog.coursify.me/wp-content/uploads/2018/08/plan-your-online-course.jpg";
        public string Description { get; set; } = "module description";
        public string? CourseProgram { get; set; }
        public List<Chapitre> Chapitres { get; set; } = new List<Chapitre>();
        public List<ModuleRequirement> ModuleRequirements { get; set; } = new List<ModuleRequirement>();
        public List<ModuleRequirement> ModulesRequiredIn { get; set; } = new List<ModuleRequirement>();
        public List<TestNiveau> TestNiveaus { get; set; } = new List<TestNiveau>();
        public ExamFinal? ExamFinal { get; set; }
        public int? ExamFinalId { get; set; }
        public List<NiveauScolaireModule> NiveauScolaireModules { get; set; } = new List<NiveauScolaireModule>();

    }
}