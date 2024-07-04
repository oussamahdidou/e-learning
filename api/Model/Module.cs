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
        public List<Chapitre> Chapitres { get; set; } = new List<Chapitre>();
        public int NiveauScolaireId { get; set; }
        public NiveauScolaire? NiveauScolaire { get; set; }
        public List<ModuleRequirement> ModuleRequirements { get; set; } = new List<ModuleRequirement>();
        public List<ModuleRequirement> ModulesRequiredIn { get; set; } = new List<ModuleRequirement>();
        public List<TestNiveau> TestNiveaus { get; set; } = new List<TestNiveau>();

    }
}