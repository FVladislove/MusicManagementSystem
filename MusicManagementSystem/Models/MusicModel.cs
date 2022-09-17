using System.Drawing;

namespace MusicManagementSystem.Models
{
    public class MusicModel
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IEnumerable<string>? Genres { get; set; }
        public string? AlbumName { get; set; }
        public IEnumerable<string>? Artists { get; set; }
        public string? Lyrics { get; set; }
        public int? Length { get; set; }
        public IEnumerable<Image>? Images { get; set; }
    }
}
