using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Model
{
    public class AppUser : IdentityUser
    {
        // public string? Nom { get; set; }
        // public string? Prenom { get; set; }
        // public DateTime? DateDeNaissance { get; set; }
        // public string? Etablissement { get; set; }
        // public string? Branche { get; set; }
        // public string? Niveaus { get; set; }
        // public string? Teacher_Nom { get; set; }
        // public string? Teacher_Prenom { get; set; }
        // public DateTime? Teacher_DateDeNaissance { get; set; }
        // public string? Teacher_Etablissement { get; set; }
        // public string? JustificatifDeLaProfession { get; set; }
        public bool Granted { get; set; } = false;
        public List<Poste> Postes { get; set; } = new List<Poste>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}