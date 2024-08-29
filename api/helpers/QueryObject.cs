using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.helpers
{
    public class QueryObject
    {
        public string? titre { get; set; } = null;


        public string? contenu {get; set;} = null;
        public int pageNumber = 1;

        public int pageSize = 20;

        public bool sortByMostComments { get; set; } = false;
    }
}