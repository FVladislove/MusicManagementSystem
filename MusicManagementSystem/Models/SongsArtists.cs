namespace MusicManagementSystem.Models
{
    public class SongsArtists
    {
        public int SongId { get; set; }
        public Songs Song { get; set; }

        public int ArtistId { get; set; }
        public Artists Artist { get; set; }
    }
}
