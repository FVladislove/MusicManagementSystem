using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicManagementSystem.Models
{
    public class Artists
    {
        public int Id { get; set; }

        [MaxLength(56)]
        public string Name { get; set; }

        [MaxLength(777)]
        public string? SpotifyLink { get; set; }

        public virtual IList<SongsArtists> SongsAuthorsPairs { get; set; }
    }
}
