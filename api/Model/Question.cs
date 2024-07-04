using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Model
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public string Nom { get; set; } = "";
        public int QuizId { get; set; }
        public Quiz? Quiz { get; set; }
        public List<Option> Options { get; set; } = new List<Option>();

    }
}