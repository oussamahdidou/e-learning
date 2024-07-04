using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Model
{
    public class Teacher : AppUser
    {
        public bool Granted { get; set; } = false;
    }
}