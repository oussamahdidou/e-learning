using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.interfaces
{
    public interface IBlobStorageService
    {
        Task<string> UploadFileAsync(Stream fileStream, string containerName, string fileName);
        Task<bool> DeleteFileAsync(string containerName, string fileName);
    }
}