using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.ResultControle;
using api.Model;

namespace api.Mappers
{
    public static class ResultControleMapper
    {
        public static ResultControleDto ToResultControleDto (this ResultControle resultControleMapper){
            return new ResultControleDto
            {
                Controle = resultControleMapper.Controle,
                Reponse = resultControleMapper.Reponse
            };
        }
    }
}