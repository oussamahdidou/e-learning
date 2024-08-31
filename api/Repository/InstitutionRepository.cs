using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Institution;
using api.generique;
using api.interfaces;
using api.Model;
//using api.Dtos.Institution;
//using api.generique;
using Microsoft.EntityFrameworkCore;
//using api.Dtos.NiveauScolaire;
using api.Dtos.NiveauScolaire;
using api.helpers;

namespace api.Repository
{
    public class InstitutionRepository : IinstitutionRepository
    {
        private readonly apiDbContext apiDbContext;
        private readonly IBlobStorageService blobStorageService;
        private string pdfContainer = "pdf-container";
        private string videoContainer = "video-container";
        private string schemaContainer = "schema-container";
        private string syntheseContainer = "synthese-container";
        private string controleContainer = "controle-container";
        private string imageContainer = "image-container";
        private string programContainer = "program-container";
        public InstitutionRepository(apiDbContext apiDbContext, IBlobStorageService blobStorageService)
        {
            this.apiDbContext = apiDbContext;
            this.blobStorageService = blobStorageService;
        }
        public async Task<Result<Institution>> CreateInstitution(string InstitutionName)
        {
            try
            {
                Institution institution = new Institution()
                {
                    Nom = InstitutionName,
                };
                await apiDbContext.institutions.AddAsync(institution);
                await apiDbContext.SaveChangesAsync();
                return Result<Institution>.Success(institution);

            }
            catch (Exception ex)
            {

                return Result<Institution>.Failure($"{ex.Message}");

            }
        }

        public async Task<bool> DeleteInstitution(int institutionId)
        {
            try
            {
                var institution = await apiDbContext.institutions
                    .Include(i => i.NiveauScolaires)
                        .ThenInclude(ns => ns.NiveauScolaireModules)
                    .Include(i => i.NiveauScolaires)
                        .ThenInclude(ns => ns.ElementPedagogiques)
                    .FirstOrDefaultAsync(i => i.Id == institutionId);

                if (institution != null)
                {
                    // Delete Element Pedagogique Links
                    foreach (var niveauScolaire in institution.NiveauScolaires)
                    {
                        foreach (var elementPedagogique in niveauScolaire.ElementPedagogiques)
                        {
                            if (!string.IsNullOrEmpty(elementPedagogique.Lien))
                            {
                                await blobStorageService.DeleteFileAsync(imageContainer, CloudinaryUrlHelper.ExtractFileName(elementPedagogique.Lien));
                            }
                        }
                    }

                    // Remove the institution from the database
                    apiDbContext.institutions.Remove(institution);
                    await apiDbContext.SaveChangesAsync();

                    // Find and delete orphaned modules (not linked to any NiveauScolaireModules)
                    var modulesToDelete = await apiDbContext.modules
                        .Include(m => m.Chapitres)
                            .ThenInclude(c => c.Cours)
                                .ThenInclude(c => c.Paragraphes)
                        .Include(m => m.Chapitres)
                            .ThenInclude(c => c.Controle)
                        .Include(m => m.ExamFinal)
                        .Where(m => !apiDbContext.niveauScolaireModules.Any(nsm => nsm.ModuleId == m.Id))
                        .ToListAsync();

                    // Delete associated files for each module
                    foreach (var module in modulesToDelete)
                    {
                        if (!string.IsNullOrEmpty(module.ModuleImg))
                        {
                            await blobStorageService.DeleteImageVideoAsync(imageContainer, CloudinaryUrlHelper.ExtractFileName(module.ModuleImg));
                        }
                        if (!string.IsNullOrEmpty(module.CourseProgram))
                        {
                            await blobStorageService.DeleteFileAsync(programContainer, CloudinaryUrlHelper.ExtractFileName(module.CourseProgram));
                        }
                        // Delete ExamFinal files
                        if (module.ExamFinal != null)
                        {
                            if (!string.IsNullOrEmpty(module.ExamFinal.Ennonce))
                            {
                                await blobStorageService.DeleteFileAsync(controleContainer, CloudinaryUrlHelper.ExtractFileName(module.ExamFinal.Ennonce));
                            }
                            if (!string.IsNullOrEmpty(module.ExamFinal.Solution))
                            {
                                await blobStorageService.DeleteFileAsync(controleContainer, CloudinaryUrlHelper.ExtractFileName(module.ExamFinal.Solution));
                            }
                        }

                        // Delete Chapitre files
                        foreach (var chapitre in module.Chapitres)
                        {
                            if (!string.IsNullOrEmpty(chapitre.VideoPath))
                            {
                                await blobStorageService.DeleteImageVideoAsync(videoContainer, CloudinaryUrlHelper.ExtractFileName(chapitre.VideoPath));
                            }
                            if (!string.IsNullOrEmpty(chapitre.Schema))
                            {
                                await blobStorageService.DeleteFileAsync(schemaContainer, CloudinaryUrlHelper.ExtractFileName(chapitre.Schema));
                            }
                            if (!string.IsNullOrEmpty(chapitre.Synthese))
                            {
                                await blobStorageService.DeleteFileAsync(syntheseContainer, CloudinaryUrlHelper.ExtractFileName(chapitre.Synthese));
                            }

                            // Delete Cours Paragraphes files
                            foreach (var cours in chapitre.Cours)
                            {
                                foreach (var paragraphe in cours.Paragraphes)
                                {
                                    if (!string.IsNullOrEmpty(paragraphe.Contenu))
                                    {
                                        await blobStorageService.DeleteFileAsync(pdfContainer, CloudinaryUrlHelper.ExtractFileName(paragraphe.Contenu));
                                    }
                                }
                            }

                            // Delete Controle files
                            if (chapitre.Controle != null)
                            {
                                if (!string.IsNullOrEmpty(chapitre.Controle.Ennonce))
                                {
                                    await blobStorageService.DeleteFileAsync(controleContainer, CloudinaryUrlHelper.ExtractFileName(chapitre.Controle.Ennonce));
                                }
                                if (!string.IsNullOrEmpty(chapitre.Controle.Solution))
                                {
                                    await blobStorageService.DeleteFileAsync(controleContainer, CloudinaryUrlHelper.ExtractFileName(chapitre.Controle.Solution));
                                }
                            }
                        }
                    }

                    // Remove the orphaned modules from the database
                    apiDbContext.modules.RemoveRange(modulesToDelete);
                    await apiDbContext.SaveChangesAsync();

                    return true;
                }
                return false;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<Result<Institution>> GetInstitutionById(int id)
        {
            try
            {
                Institution? institution = await apiDbContext.institutions.Include(x => x.NiveauScolaires).FirstOrDefaultAsync(x => x.Id == id);
                if (institution == null)
                {
                    return Result<Institution>.Failure("institution notfound");

                }
                return Result<Institution>.Success(institution);
            }
            catch (Exception ex)
            {

                return Result<Institution>.Failure($"{ex.Message}");
            }
        }

        public async Task<Result<List<Institution>>> GetInstitutions()
        {
            try
            {
                List<Institution> institutions = await apiDbContext.institutions.ToListAsync();
                return Result<List<Institution>>.Success(institutions);
            }
            catch (Exception ex)
            {

                return Result<List<Institution>>.Failure($"{ex.Message}");
            }
        }

        public async Task<Result<Institution>> UpdateInstitution(UpdateInstitutionDto updateInstitutionDto)
        {
            try
            {
                Institution? institution = await apiDbContext.institutions.FirstOrDefaultAsync(x => x.Id == updateInstitutionDto.Id);
                if (institution == null)
                {
                    return Result<Institution>.Failure("institution notfound");

                }
                institution.Nom = updateInstitutionDto.Nom;
                await apiDbContext.SaveChangesAsync();
                return Result<Institution>.Success(institution);
            }
            catch (System.Exception ex)
            {

                return Result<Institution>.Failure($"{ex.Message}");
            }
        }


    }
}