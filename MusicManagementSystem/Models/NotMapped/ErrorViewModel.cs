using System.ComponentModel.DataAnnotations.Schema;

namespace MusicManagementSystem.Models.NotMapped
{
    [NotMapped]
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}