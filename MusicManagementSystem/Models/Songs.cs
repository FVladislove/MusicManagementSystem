using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace MusicManagementSystem.Models
{
    public class Songs
    {
        public int Id { get; set; }

        [MaxLength(56)]
        public string? Title { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        [MaxLength(56)]
        public string? AlbumName { get; set; }

        [MaxLength(6666)]
        public string? Lyrics { get; set; }

        public int? Length { get; set; }

        public virtual IList<SongsArtists>? SongsArtistsPairs { get; set; }

        public virtual IList<SongsGenres>? SongsGenresPairs { get; set; }

        public virtual IList<SongsImagesPaths>? SongsImagesPathsPairs { get; set; }
    }
}
