using System.ComponentModel.DataAnnotations.Schema;

namespace MusicManagementSystem.Models.NotMapped.SecretsModels
{
    [NotMapped]
    public class ConnectionStringsModel : ISecretsModel, IStaticType
    {
        public string? MusicManagementSystemContext { get; set; }

        public string SectionName => "ConnectionStrings";

        public Type GetStaticType => typeof(ConnectionStringsModel);
    }
}
