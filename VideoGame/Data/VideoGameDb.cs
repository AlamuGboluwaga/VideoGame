using Microsoft.EntityFrameworkCore;
using VideoGame.Models.Domian;

namespace VideoGame.Data
{
    public class VideoGameDb(DbContextOptions options) : DbContext( options)
    {
        public DbSet<VideoGames> VideoGames { get; set; }
    }
}
