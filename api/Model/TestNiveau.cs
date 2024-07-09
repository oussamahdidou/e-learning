using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Model
{
    public class TestNiveau
    {
        public string? StudentId { get; set; }
        public int ModuleId { get; set; }
        public Student? Student { get; set; }
        public Module? Module { get; set; }
        public double Note { get; set; }
    }
}