using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.interfaces
{
    public interface IBlobStorageService
    {
        Task<string> UploadImageVideoAsync(Stream fileStream, string folderName, string fileName);
        Task<bool> DeleteImageVideoAsync(string folderName, string fileName);
        Task<string> UploadFileAsync(Stream fileStream, string containerName, string fileName);
        Task<bool> DeleteFileAsync(string containerName, string fileName);

        // string GenerateSasToyken(string containerName, string fileName, TimeSpan tokenDuration);
    }
}