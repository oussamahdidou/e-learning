using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Chapitre
{
    public class UpdateChapitrePdfDto
    {
        public int Id { get; set; }
        [Required]
        public required IFormFile File { get; set; }
    }
}