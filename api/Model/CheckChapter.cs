using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Model
{
    public class CheckChapter
    {
        public string? StudentId { get; set; }
        public int ChapitreId { get; set; }
        public Student? Student { get; set; }
        public Chapitre? Chapitre { get; set; }
        public string? Comment { get; set; }
    }
}