using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Model
{
    public class CoursParagraphe
    {
        public string? Paragraphe { get; set; }
        public Chapitre? Chapitre { get; set; }
        public int ChapitreId { get; set; }
    }
}