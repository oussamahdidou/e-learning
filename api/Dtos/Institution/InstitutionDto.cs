using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using api.Model;
using api.Dtos.NiveauScolaires;


namespace api.Dtos.Institution
{
    public class InstitutionDto
    {
      public int Id { get; set; } 
        public string Nom { get; set; } = "";
        public List<NiveauScolaireDto> NiveauScolaires { get; set; } 
         public InstitutionDto()
        {
            NiveauScolaires = new List<NiveauScolaireDto>();
        } 
    }
}