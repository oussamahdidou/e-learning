using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Model
{
    public class Controle
    {
        [Key]
        public int Id { get; set; }
        public string Nom { get; set; } = "";
        public string Ennonce { get; set; } = "";
        public string Solution { get; set; } = "";
        public string Status { get; set; } = "";
        public List<Chapitre> Chapitres { get; set; } = new List<Chapitre>();
        public List<ResultControle> ResultControles { get; set; } = new List<ResultControle>();

    }
}