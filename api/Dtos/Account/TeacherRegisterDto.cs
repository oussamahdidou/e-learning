using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Account
{
    public class TeacherRegisterDto
    {
        [Required]
        public string? Teacher_Nom {get; set;} 
        [Required]
        public string? Teacher_Prenom {get; set;} 
        [Required]
        public DateTime? Teacher_DateDeNaissance {get; set;} 
        [Required]
        public string? Teacher_Etablissement {get; set;} 
        [Required]
        public IFormFile? JustificatifDeLaProfession {get; set;} 
        [Required]
        public string? UserName {get; set;} 
        [Required]
        [EmailAddress]
        public string? Email {get; set;}
        [Required]
        public string?Status {get; set;}
        [Required]
        public string? Specialite {get; set;}
        [Required]
        public string? PhoneNumber {get; set;}
        public string? Password {get; set;}

        [Compare ("Password", ErrorMessage ="The password and confirmation password are not the same.")]
        public string? ConfirmPassword {get; set;}




        
    }
}