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
        Task<Result<Paragraphe>> CreateParagraphe(CreateParagrapheDto createParagrapheDto);
        Task<Result<Paragraphe>> GetParagrapheByid(int id);
        Task<Result<Paragraphe>> UpdateParagraphe(UpdateParagrapheDto updateParagrapheDto);

        Task<Result<Chapitre>> UpdateChapitrePdf(UpdateChapitrePdfDto updateChapitrePdfDto);
        Task<Result<Video>> UpdateChapitreVideo(UpdateChapitreVideoDto updateChapitreVideoDto);
        Task<Result<Video>> UpdateChapitreVideoLink(UpdateChapitreVideoLinkDto updateChapitreVideoLinkDto);
        Task<Result<Video>> AddVideo(UpdateChapitreVideoDto updateChapitreVideoDto);
        Task<Result<Video>> AddVideoLink(UpdateChapitreVideoLinkDto updateChapitreVideoLinkDto);
        Task<Result<Schema>> AddChapitreSchema(UpdateChapitreSchemaDto updateChapitreSchemaDto);
        Task<Result<Synthese>> AddChapitreSynthese(UpdateChapitreSyntheseDto updateChapitreSyntheseDto);

        Task<Result<Video>> GetVideoById(int Id);
        Task<Result<Schema>> UpdateChapitreSchema(UpdateChapitreSchemaDto updateChapitreSchemaDto);
        Task<Result<Synthese>> GetSyntheseById(int Id);
        Task<Result<Synthese>> UpdateChapitreSynthese(UpdateChapitreSyntheseDto updateChapitreSyntheseDto);
        Task<Result<Schema>> GetSchemaById(int Id);
        Task<Result<Chapitre>> UpdateChapterName(UpdateChapitreNameDto updateChapitreNameDto);
        Task<Result<Chapitre>> Approuver(int id);
        Task<Result<Chapitre>> Refuser(int id);
        Task<bool> DeleteChapitre(int id);
        Task<bool> DeleteVideo(int id);
        Task<bool> DeleteSchema(int id);
        Task<bool> DeleteSynthese(int id);

    }
}