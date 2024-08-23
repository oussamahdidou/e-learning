using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Module
{
    public class CreateNiveauScolaireModuleDto
    {
        public int ModuleId { get; set; }
        public int NiveauScolaireId { get; set; }
    }
}