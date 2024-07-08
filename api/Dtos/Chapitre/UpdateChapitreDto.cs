using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Chapitre
{
   public class UpdateChapitreDto
    {
        public int? Id { get; set; }
        public int ChapitreNum { get; set; }
        public string Nom { get; set; } = "";
        public string Statue { get; set; } = "";
        public string CoursPdfPath { get; set; } = "";
        public string VideoPath { get; set; } = "";
        public string Synthese {get; set; } = "";
        public string Schema { get; set; } = "";
        public bool Premium { get; set; } = true;
        public int QuizId { get; set; }
        public int ModuleId { get; set; }
        public int ControleId { get; set; }
        // public List<CheckChapterDto> CheckChapters { get; set; } = new List<CheckChapterDto>();
    }}