using System;
<<<<<<< HEAD
=======
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
>>>>>>> manall

namespace api.Dtos.Chapitre
{
    public class CreateChapitreDto
    {
        public int ChapitreNum { get; set; }
        public string Nom { get; set; } = "";
<<<<<<< HEAD
        public string Statue { get; set; } = "";
        public string CoursPdfPath { get; set; } = "";
        public string VideoPath { get; set; } = "";
        public string Synthese { get; set; } = "";
        public string Schema { get; set; } = "";
        public bool Premium { get; set; } = true;
        public int QuizId { get; set; }
        public int ModuleId { get; set; }
        public int ControleId { get; set; }
        // public List<CheckChapterDto> CheckChapters { get; set; } = new List<CheckChapterDto>();
    }
}
=======
        public IFormFile CoursPdf { get; set; }
        public IFormFile Video { get; set; }
        public IFormFile Synthese { get; set; }
        public IFormFile Schema { get; set; }
        public bool Premium { get; set; }
        public int ModuleId { get; set; }
    }
}
>>>>>>> manall
