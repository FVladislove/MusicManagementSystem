using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Options;
using MusicManagementSystem.Models.NotMapped.SecretsModels;

namespace MusicManagementSystem.Services.CloudStorage
{
    public class GoogleCloudStorage : ICloudStorage
    {
        private readonly GoogleCredential googleCredential;
        private readonly StorageClient storageClient;
        private readonly string bucketName;

        public GoogleCloudStorage(IOptions<GoogleCloudStorageModel> options)
        {
            googleCredential = GoogleCredential.FromFile(options.Value.CredentialFile);
            storageClient = StorageClient.Create(googleCredential);
            bucketName = options.Value.BucketName;
        }

        public async Task DeleteFileAsync(string fileNameForStorage)
        {
            await storageClient.DeleteObjectAsync(bucketName, fileNameForStorage);
        }

        public async Task<string> UploadFileAsync(IFormFile formFile, string fileNameForStorage)
        {
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                var dataObject = await storageClient.UploadObjectAsync(
                    bucketName, fileNameForStorage, null, memoryStream);
                return dataObject.MediaLink;
            }
        }
    }
}
