using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.generique;

namespace api.extensions
{
    public static class FilesExtensions
    {
        public static async Task<Result<string>> UploadCoursPdf(this IFormFile formFile, IWebHostEnvironment webHostEnvironment)
        {

            if (webHostEnvironment == null)
            {

                return Result<string>.Failure("web host envirenement error");
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

                return Result<string>.Success("/cours/" + uniqueFileName);
            }
            catch (Exception ex)
            {
                return Result<string>.Failure($"UploadImage Exception: {ex.Message}");

            }
        }

        public static async Task<Result<string>> UploadVideo(this IFormFile formFile, IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment == null)
            {

                return Result<string>.Failure("web host envirenement error");
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
                return Result<string>.Success("/video/" + uniqueFileName);


            }
            catch (Exception ex)
            {
                return Result<string>.Failure($"UploadImage Exception: {ex.Message}");
            }
        }

        public static async Task<Result<string>> UploadSynthese(this IFormFile formFile, IWebHostEnvironment webHostEnvironment)
        {

            if (webHostEnvironment == null)
            {

                return Result<string>.Failure("web host envirenement error");
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
                return Result<string>.Success("/synthese/" + uniqueFileName);


            }
            catch (Exception ex)
            {

                return Result<string>.Failure($"UploadImage Exception: {ex.Message}");
            }
        }

        public static async Task<Result<string>> UploadSchema(this IFormFile formFile, IWebHostEnvironment webHostEnvironment)
        {

            if (webHostEnvironment == null)
            {

                return Result<string>.Failure("web host envirenement error");

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

                return Result<string>.Success("/schema/" + uniqueFileName);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"UploadImage Exception: {ex.Message}");
                return Result<string>.Failure($"UploadImage Exception: {ex.Message}");
            }
        }

        public static async Task<Result<string>> UploadControle(this IFormFile formFile, IWebHostEnvironment webHostEnvironment)
        {

            if (webHostEnvironment == null)
            {

                return Result<string>.Failure("web host envirenement error");
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

                return Result<string>.Success("/controle/" + uniqueFileName);
            }
            catch (Exception ex)
            {

                return Result<string>.Failure($"UploadImage Exception: {ex.Message}");

            }
        }

        public static async Task<Result<string>> UploadControleSolution(this IFormFile formFile, IWebHostEnvironment webHostEnvironment)
        {

            if (webHostEnvironment == null)
            {

                return Result<string>.Failure("web host envirenement error");
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

                return Result<string>.Success("/controlesolution/" + uniqueFileName);
            }
            catch (Exception ex)
            {

                return Result<string>.Failure($"UploadImage Exception: {ex.Message}");
            }
        }

        public static async Task<Result<string>> UploadControleReponse(this IFormFile formFile, IWebHostEnvironment webHostEnvironment)
        {

            if (webHostEnvironment == null)
            {

                return Result<string>.Failure("web host envirenement error");
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

                return Result<string>.Success("/controlereponse/" + uniqueFileName);
            }
            catch (Exception ex)
            {

                return Result<string>.Failure($"UploadImage Exception: {ex.Message}");
            }
        }
        public static Result<string> DeleteFile(this string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return Result<string>.Failure("file path is null");
            }

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return Result<string>.Success(filePath);

            }
            else
            {
                return Result<string>.Failure($"The file specified in the path was not found. {filePath}");
            }
        }

    }
}