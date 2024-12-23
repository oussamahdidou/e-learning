using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Account
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage ="Password is required")]
        public string? Password {get; set;}

        [Compare ("Password", ErrorMessage ="The password and confirmation password are not the same.")]
        public string? ConfirmPassword {get; set;}

        public string? Email {get; set;}
        public string? Token {get; set;}
    }
}