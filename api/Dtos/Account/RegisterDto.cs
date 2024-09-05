using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Account
{
    public class RegisterDto
    {
        [Required]
        public string? Nom {get; set;} 
        [Required]
        public string? Prenom {get; set;} 
        
        public DateTime? DateDeNaissance {get; set;} 
        [Required]
        public int Etablissement {get; set;} 
        [Required]
        public string? Branche {get; set;} 
        [Required]
        public string? Niveaus {get; set;} 
        [Required]
        public string? UserName {get; set;} 
        [Required]
        [EmailAddress]
        public string? Email {get; set;}
        [EmailAddress]
        public string?TuteurMail {get; set;}
        public string? PhoneNumber {get; set;}
        [Required]
        public string? Password {get; set;}

        [Compare ("Password", ErrorMessage ="The password and confirmation password are not the same.")]
        public string? ConfirmPassword {get; set;}




        
    }
}