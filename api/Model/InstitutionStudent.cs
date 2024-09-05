using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Model
{
    public class InstitutionStudent
    {
        public int InstitutionId { get; set; }
        public string? StudentId { get; set; }
        public Institution? Institution { get; set; }
        public Student? Student { get; set; }

    }
}