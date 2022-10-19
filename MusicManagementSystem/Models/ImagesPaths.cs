using System.ComponentModel.DataAnnotations;

namespace MusicManagementSystem.Models
{
    public class ImagesPaths
    {
        public int Id { get; set; }

        [MaxLength(777)]
        public string Path { get; set; }
    }
}
