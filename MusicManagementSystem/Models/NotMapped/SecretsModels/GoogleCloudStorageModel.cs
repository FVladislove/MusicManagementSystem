namespace MusicManagementSystem.Models.NotMapped.SecretsModels
{
    public class GoogleCloudStorageModel : ISecretsModel
    {
        public string SectionName => "CloudStorage:Google";
        public string CredentialFile { get; set; }
        public string BucketName { get; set; }
    }
}
