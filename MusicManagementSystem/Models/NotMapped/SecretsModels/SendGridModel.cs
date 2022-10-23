namespace MusicManagementSystem.Models.NotMapped.SecretsModels
{
    public class SendGridModel : ISecretsModel, IStaticType
    {
        public string Key { get; set; }
        public string MailFrom { get; set; }

        public string SectionName => "SendGrid";

        public Type GetStaticType => typeof(SendGridModel);
    }
}
