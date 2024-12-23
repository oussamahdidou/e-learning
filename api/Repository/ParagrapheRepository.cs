using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.generique;
using api.helpers;
using api.interfaces;
using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ParagrapheRepository : IParagrapheRepository
    {
        private readonly apiDbContext apiDbContext;
        private readonly IBlobStorageService _blobStorageService;
        private string pdfContainer = "pdf-container";
        public ParagrapheRepository(apiDbContext apiDbContext, IBlobStorageService blobStorageService)
        {
            this.apiDbContext = apiDbContext;
            _blobStorageService = blobStorageService;
        }

        public async Task<bool> DeletePAragraphe(int id)
        {
            // Retrieve the Paragraphe by its Id
            Paragraphe? paragraphe = await apiDbContext.paragraphes.FirstOrDefaultAsync(x => x.Id == id);

            if (paragraphe == null)
            {
                return false;
            }

            // Delete Paragraphe Contenu if exists
            if (!string.IsNullOrEmpty(paragraphe.Contenu))
            {
                await _blobStorageService.DeleteFileAsync(pdfContainer, CloudinaryUrlHelper.ExtractFileName(paragraphe.Contenu));
            }

            // Remove the Paragraphe from the database
            apiDbContext.paragraphes.Remove(paragraphe);
            await apiDbContext.SaveChangesAsync();

            return true;
        }


        public async Task<Result<Paragraphe>> GetCourById(int id)
        {
            Paragraphe? paragraphe = await apiDbContext.paragraphes.FirstOrDefaultAsync(x => x.Id == id);

            if (paragraphe == null)
            {
                return Result<Paragraphe>.Failure("Cour Not Found");
            }

            // paragraphe.Contenu = _blobStorageService.GenerateSasToken(pdfContainer, Path.GetFileName(new Uri(paragraphe.Contenu).LocalPath), TimeSpan.FromMinutes(5));

            return Result<Paragraphe>.Success(paragraphe);
        }
    }
}