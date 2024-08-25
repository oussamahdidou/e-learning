using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Model
{
    public class AppUser : IdentityUser
    {
        public List<Poste> Postes { get; set; } = new List<Poste>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}