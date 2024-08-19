using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Model
{
    public class NiveauScolaireModule
    {
        public int NiveauScolaireId { get; set; }
        public NiveauScolaire? NiveauScolaire { get; set; }
        public int ModuleId { get; set; }
        public Module? Module { get; set; }
    }
}