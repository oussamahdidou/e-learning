using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.dashboard
{
    public class ObjectsDto
    {
        public List<Model.Chapitre> chapitres { get; set; } = new List<Model.Chapitre>();
        public List<Model.Controle> controles { get; set; } = new List<Model.Controle>();
    }
}