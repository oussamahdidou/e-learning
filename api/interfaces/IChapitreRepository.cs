using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Chapitre;
using api.generique;
using api.Model;

namespace api.interfaces
{
    public interface IChapitreRepository
    {

        Task<Result<Chapitre>> GetChapitreById(int id);
        Task<Result<Chapitre>> CreateChapitre(CreateChapitreDto createChapitreDto);
        Task<Result<Chapitre>> UpdateChapitrePdf(UpdateChapitrePdfDto updateChapitrePdfDto);
        Task<Result<Chapitre>> UpdateChapitreVideo(UpdateChapitreVideoDto updateChapitreVideoDto);
        Task<Result<Chapitre>> UpdateChapitreSchema(UpdateChapitreSchemaDto updateChapitreSchemaDto);
        Task<Result<Chapitre>> UpdateChapitreSynthese(UpdateChapitreSyntheseDto updateChapitreSyntheseDto);
    }
}