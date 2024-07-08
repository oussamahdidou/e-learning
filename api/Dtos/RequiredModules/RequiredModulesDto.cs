using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Model;

namespace api.Dtos.RequiredModules
{
    public class RequiredModulesDto
    {
        public Module Module { get; set; }
        public double Seuill { get; set; }

    }
}