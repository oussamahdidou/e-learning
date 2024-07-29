using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.dashboard
{
    public class GetChapitresToUpdateControlesDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool Checked { get; set; } = false;
    }
}