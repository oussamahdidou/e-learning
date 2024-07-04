using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.extensions
{
    public static class FilesExtensions
    {
        public static async Task<string> UploadCoursPdf(this IFormFile formFile, IWebHostEnvironment webHostEnvironment)
        {

            if (webHostEnvironment == null)
            {

                return null;
            }

            try
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "cours");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + formFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }

                return "/cours/" + uniqueFileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UploadImage Exception: {ex.Message}");
                return null;
            }
        }

        public static async Task<string> UploadVideo(this IFormFile formFile, IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment == null)
            {

                return null;
            }

            try
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "video");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + formFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }

                return "/video/" + uniqueFileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UploadImage Exception: {ex.Message}");
                return null;
            }
        }

        public static async Task<string> UploadSynthese(this IFormFile formFile, IWebHostEnvironment webHostEnvironment)
        {

            if (webHostEnvironment == null)
            {

                return null;
            }

            try
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "synthese");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + formFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }

                return "/synthese/" + uniqueFileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UploadImage Exception: {ex.Message}");
                return null;
            }
        }

        public static async Task<string> UploadSchema(this IFormFile formFile, IWebHostEnvironment webHostEnvironment)
        {

            if (webHostEnvironment == null)
            {

                return null;
            }

            try
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "schema");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + formFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }

                return "/schema/" + uniqueFileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UploadImage Exception: {ex.Message}");
                return null;
            }
        }

        public static async Task<string> UploadControle(this IFormFile formFile, IWebHostEnvironment webHostEnvironment)
        {

            if (webHostEnvironment == null)
            {

                return null;
            }

            try
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "controle");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + formFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }

                return "/controle/" + uniqueFileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UploadImage Exception: {ex.Message}");
                return null;
            }
        }

        public static async Task<string> UploadControleSolution(this IFormFile formFile, IWebHostEnvironment webHostEnvironment)
        {

            if (webHostEnvironment == null)
            {

                return null;
            }

            try
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "controlesolution");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + formFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }

                return "/controlesolution/" + uniqueFileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UploadImage Exception: {ex.Message}");
                return null;
            }
        }

        public static async Task<string> UploadControleReponse(this IFormFile formFile, IWebHostEnvironment webHostEnvironment)
        {

            if (webHostEnvironment == null)
            {

                return null;
            }

            try
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "controlereponse");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + formFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }

                return "/controlereponse/" + uniqueFileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UploadImage Exception: {ex.Message}");
                return null;
            }
        }


    }
}