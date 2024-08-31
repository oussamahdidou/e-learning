using api.interfaces;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using System;
using System.IO;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

public class BlobStorageService : IBlobStorageService
{
    private readonly Cloudinary _cloudinary;
    private readonly IConfiguration _configuration;
    public BlobStorageService(Cloudinary cloudinary,IConfiguration configuration)
    {
        _cloudinary = cloudinary;
        _configuration = configuration;
    }

    public async Task<string> UploadImageVideoAsync(Stream fileStream, string folderName, string fileName)
    {
        Console.WriteLine(fileName);
        var uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
        var publicId = string.IsNullOrEmpty(folderName) ? fileName : $"{folderName}/{uniqueFileName}";
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(fileName, fileStream),
            UseFilename = true,
            UniqueFilename = false,
            Overwrite = true,
            PublicId = publicId
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);
        if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
        {
            Console.WriteLine(uploadResult.JsonObj);
            return uploadResult.SecureUrl.ToString();
        }
        throw new Exception("File upload failed: " + uploadResult.Error?.Message);
    }
    public async Task<string> UploadFileAsync(Stream fileStream, string folderName, string fileName)
    {
        var uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
        var publicId = string.IsNullOrEmpty(folderName) ? fileName : $"{folderName}/{uniqueFileName}";
        Console.WriteLine(publicId);
        var uploadParams = new RawUploadParams()
        {
            File = new FileDescription(@uniqueFileName, fileStream),
            UseFilename = true,
            UniqueFilename = false,
            Overwrite = true,
            PublicId = publicId
        };
        var uploadResult = await _cloudinary.UploadAsync(uploadParams);
        if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
        {
            Console.WriteLine(uploadResult.JsonObj);
            return uploadResult.SecureUrl.ToString();
        }
        throw new Exception("File upload failed: " + uploadResult.Error?.Message);
    }

    public async Task<bool> DeleteFileAsync(string folderName, string fileName)
    {
        var publicId = string.IsNullOrEmpty(folderName) ? fileName : $"{folderName}/{fileName}";
        Console.WriteLine(publicId);
        var deletionParams = new DeletionParams(publicId){
            ResourceType = ResourceType.Raw
        };
        var deletionResult = await _cloudinary.DestroyAsync(deletionParams);
        return deletionResult.Result == "ok";
    }
    public async Task<bool> DeleteImageVideoAsync(string folderName, string fileName)
    {
        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
        var publicId = string.IsNullOrEmpty(folderName) ? fileName : $"{folderName}/{fileNameWithoutExtension}";
        Console.WriteLine(publicId);
        Console.WriteLine(fileNameWithoutExtension);
        var deletionParams = new DeletionParams(publicId){
            ResourceType = ResourceType.Image
        };
        var deletionResult = await _cloudinary.DestroyAsync(deletionParams);
        return deletionResult.Result == "ok";
    }

    // public string GenerateSasToken(string folderName, string fileName, TimeSpan tokenDuration)
    // {
    //     var publicId = string.IsNullOrEmpty(folderName) ? fileName : $"{folderName}/{fileName}";
    //     var expirationTime = DateTime.UtcNow.Add(tokenDuration);
    //     var expirationUnix = new DateTimeOffset(expirationTime).ToUnixTimeSeconds();
    //     var url = _cloudinary.Api.Url
    //         .BuildUrl(publicId)
    //         .ToString();
    //     var signedUrl = $"{url}&expires_at={expirationUnix}";
    //     return signedUrl;
    // }
}
