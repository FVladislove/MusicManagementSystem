using System.ComponentModel.DataAnnotations;

namespace MusicManagementSystem.Models
{
    public class Genres
    {
        public int Id { get; set; }

        [MaxLength(56)]
        public string Name { get; set; }
    }
}
