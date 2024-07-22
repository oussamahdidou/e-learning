using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Chapitre;
using api.Dtos.Control;

namespace api.Dtos.Module
{
    public class ModuleDto
    {

    public int Id { get; set; }
    public string Nom { get; set; }
    public List<ChapitreDto> Chapitres { get; set; }
    public List<ControleDto> Controles { get; set; }

    }
}