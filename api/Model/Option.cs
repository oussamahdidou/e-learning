using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Model
{
    public class Option
    {
        [Key]
        public int Id { get; set; }
        public string Nom { get; set; } = "";
        public bool Truth { get; set; } = false;
        public int QuestionId { get; set; }
        public Question? Question { get; set; }
    }
}