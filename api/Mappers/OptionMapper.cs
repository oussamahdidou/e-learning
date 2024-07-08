using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Option;
using api.Model;

namespace api.Mappers
{
    public static class OptionMapper
    {
        public static OptionDto ToOptionDto(this Option option )
        {
            return new OptionDto
            {
                Id = option.Id,
                Nom = option.Nom,
                Truth = option.Truth
            };
        }        
    }
}