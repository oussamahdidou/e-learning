using api.interfaces;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using System;
using System.IO;
using System.Threading.Tasks;

public class BlobStorageService : IBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly IConfiguration _configuration;
    public BlobStorageService(BlobServiceClient blobServiceClient,IConfiguration configuration)
    {
        _blobServiceClient = blobServiceClient;
        _configuration = configuration;
    }

    public async Task<string> UploadFileAsync(Stream fileStream, string containerName, string fileName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync();
        var blobClient = containerClient.GetBlobClient(fileName);
        await blobClient.UploadAsync(fileStream, true);
        return blobClient.Uri.ToString();
    }

    public async Task<bool> DeleteFileAsync(string containerName, string fileName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(fileName);
        return await blobClient.DeleteIfExistsAsync();
    }

     public string GenerateSasToken(string containerName, string fileName, TimeSpan tokenDuration)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(fileName);

        BlobSasBuilder sasBuilder = new BlobSasBuilder()
        {
            BlobContainerName = containerName,
            BlobName = fileName,
            Resource = "b",
            ExpiresOn = DateTimeOffset.UtcNow.Add(tokenDuration)
        };

        sasBuilder.SetPermissions(BlobSasPermissions.Read);

        var accountKey = _configuration["AzureBlobStorage:AccountKey"];
        var sasToken = sasBuilder.ToSasQueryParameters(new StorageSharedKeyCredential(_blobServiceClient.AccountName, accountKey));
        return $"{blobClient.Uri}?{sasToken}";
    }
}
