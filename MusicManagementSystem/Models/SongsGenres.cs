namespace MusicManagementSystem.Models
{
    public class SongsGenres
    {
        public int SongId { get; set; }
        public Songs Song { get; set; }

        public int GenreId { get; set; }
        public Genres Genre { get; set; }
    }
}
