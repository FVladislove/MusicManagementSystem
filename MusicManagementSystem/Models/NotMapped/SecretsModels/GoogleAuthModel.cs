namespace MusicManagementSystem.Models.NotMapped.SecretsModels
{
    public class GoogleAuthModel : ISecretsModel, IStaticType
    {
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public string SectionName => "Authentication:Google";

        public Type GetStaticType => typeof(GoogleAuthModel);
    }
}
