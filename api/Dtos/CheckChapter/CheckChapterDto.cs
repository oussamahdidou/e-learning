using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.CheckChapter
{
    public class CheckChapterDto
    {
        public int Id { get; set; }
        public string avis { get; set; }
        public int ControleId { get; set; }
        public bool lastChapter { get; set; }
        public bool lastChapterExam { get; set; }
        public int ExamId { get; set; }
    }
} 