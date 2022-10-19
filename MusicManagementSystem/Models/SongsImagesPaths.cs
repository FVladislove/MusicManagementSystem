namespace MusicManagementSystem.Models
{
    public class SongsImagesPaths
    {
        public int SongId { get; set; }
        public Songs Song { get; set; }

        public int ImagePathId { get; set; }
        public ImagesPaths ImagesPaths { get; set; }
    }
}
