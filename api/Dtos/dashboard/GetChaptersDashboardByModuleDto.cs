using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.dashboard
{
    public class GetChaptersDashboardByModuleDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public required string Name { get; set; }
        public required string module { get; set; }

    }
}