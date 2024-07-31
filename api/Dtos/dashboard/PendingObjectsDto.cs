using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.dashboard
{
    public class PendingObjectsDto
    {
        public int Id { get; set; }
        public string? Nom { get; set; }
        public string? Type { get; set; }
    }
}