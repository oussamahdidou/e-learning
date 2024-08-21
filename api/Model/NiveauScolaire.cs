using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Model
{
    public class NiveauScolaire
    {
        [Key]
        public int Id { get; set; }
        public string Nom { get; set; } = "";
        public int InstitutionId { get; set; }
        public Institution? Institution { get; set; }
        public List<NiveauScolaireModule> NiveauScolaireModules { get; set; } = new List<NiveauScolaireModule>();
    }
}