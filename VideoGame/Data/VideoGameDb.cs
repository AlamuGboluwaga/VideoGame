using Microsoft.EntityFrameworkCore;
using VideoGame.Models.Domain;

namespace VideoGame.Data
{
    public class VideoGameDb(DbContextOptions options) : DbContext( options)
    {
        public DbSet<VideoGames> VideoGames { get; set; }
    }
}
