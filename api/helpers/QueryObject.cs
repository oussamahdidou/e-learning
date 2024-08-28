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
        private int _pageNumber = 1;
         public int pageNumber 
        { 
            get => _pageNumber; 
            set => _pageNumber = (value < 1) ? 1 : value; 
        }

        private int _pageSize = 20;
        public int pageSize 
        { 
            get => _pageSize; 
            set => _pageSize = (value < 1) ? 20 : value; 
        }
    }
}