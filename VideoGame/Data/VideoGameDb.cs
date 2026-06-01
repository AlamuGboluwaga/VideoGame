using Microsoft.EntityFrameworkCore;
using VideoGame.Models.Domain;
using Bogus;

namespace VideoGame.Data
{
    public class VideoGameDb(DbContextOptions options) : DbContext(options)
    {
        public DbSet<VideoGames> VideoGames { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Attach the modern runtime seeding callback
            optionsBuilder.UseAsyncSeeding(async (context, _, cancellationToken) =>
            {
                // 1. Only seed if the table is completely empty
                if (!await context.Set<Product>().AnyAsync(cancellationToken))
                {
                    int totalRecords = 10000;
                    int batchSize = 2000;

                    // 2. Configure Bogus fake data generator matching your schema
                    var faker = new Faker<Product>()
                        .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                        .RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price(10, 1000)))
                        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
                        .RuleFor(p => p.CreatedAt, f => f.Date.Past(1));

                    // 3. Generate and save in chunks to protect DB memory
                    for (int i = 0; i < totalRecords; i += batchSize)
                    {
                        var batch = faker.Generate(batchSize);
                        await context.Set<Product>().AddRangeAsync(batch, cancellationToken);
                        await context.SaveChangesAsync(cancellationToken);

                        // Detach entities to free memory tracking for the next batch
                        context.ChangeTracker.Clear();
                    }
                }
            });
        }
    }
}
