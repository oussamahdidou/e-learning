using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.helpers
{
    public class QueryObject
    {
        public string? titre { get; set; } = null;

        public int pageNumber {get; set;} = 1;

        public int pageSize { get; set; } = 20;
    }
}