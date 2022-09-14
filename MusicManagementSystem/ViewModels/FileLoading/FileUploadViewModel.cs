using MusicManagementSystem.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace MusicManagementSystem.ViewModels.FileLoading
{
    public class FileUploadViewModel
    {
        [Required(ErrorMessage = "Please, select a file")]
        [DataType(DataType.Upload)]
        [MaxFileSize(15 * 1024 * 1024)]
        public IFormFileCollection FormFileCollection { get; set; }
    }
}
