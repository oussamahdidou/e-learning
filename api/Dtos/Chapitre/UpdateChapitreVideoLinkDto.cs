using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.helpers;

namespace api.Dtos.Chapitre
{
    public class UpdateChapitreVideoLinkDto
    {
        public int chapitreId { get; set; }
        public required string Link { get; set; }
        public string Statue { get; set; } = ObjectStatus.Pending;
        public string? TeacherId { get; set; }
    }
}