using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Model;

namespace api.Dtos.RequiredModules
{
    public class RequiredModulesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NiveauScolaire { get; set; }
        public string Institution { get; set; }
        public double Seuill { get; set; }

    }
}