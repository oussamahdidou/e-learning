using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Model
{
    public class Institution
    {
        [Key]
        public int Id { get; set; }
        public string Nom { get; set; } = "";
        public List<NiveauScolaire> NiveauScolaires { get; set; } = new List<NiveauScolaire>();
        public List<InstitutionStudent> InstitutionStudents { get; set; } = new List<InstitutionStudent>();

    }
}