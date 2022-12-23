namespace MusicManagementSystem.Services.CloudStorage
{
    public interface ICloudStorage
    {
        Task<string> UploadFileAsync(IFormFile formFile, string fileNameForStorage);
        Task DeleteFileAsync(string fileNameForStorage);
    }
}
