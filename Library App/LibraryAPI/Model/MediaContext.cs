using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Model
{
    public class MediaContext: DbContext
    {
        public DbSet<Media> Medias { get; set; }

        public MediaContext(DbContextOptions<MediaContext> options) : base(options) { }
    }
}
