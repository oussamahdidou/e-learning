using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.TestNiveau
{
    public class TestNiveauResultDto
    {
        public int ModuleId { get; set; }
        public string StudentId { get; set; }
        public double Note { get; set; }
    }
}