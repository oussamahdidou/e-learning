using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.helpers
{
    public class QueryObject
    {
        public string? Query { get; set; }
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 20;

        public string SortBy { get; set; } = "recent";
    }
}