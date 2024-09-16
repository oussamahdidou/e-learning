using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.helpers;

namespace api.Dtos.Chapitre
{
    public class UpdateChapitreSyntheseDto
    {
        public int Id { get; set; }
        [Required]
        public required IFormFile File { get; set; }
        public string Statue { get; set; } = ObjectStatus.Pending;
        public string? TeacherId { get; set; }

    }
}