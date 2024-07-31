using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.dashboard
{
    public class StatsDto
    {
        public int StudentNmbr { get; set; }
        public int TeacherNmbr { get; set; }
        public int CoursesNmbr { get; set; }
        public int UnapprouvedNmbr { get; set; }

    }
}