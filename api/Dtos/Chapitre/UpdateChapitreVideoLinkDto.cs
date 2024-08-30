using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Chapitre
{
    public class UpdateChapitreVideoLinkDto
    {
        public int chapitreId { get; set; }
        public required string Link { get; set; }
    }
}