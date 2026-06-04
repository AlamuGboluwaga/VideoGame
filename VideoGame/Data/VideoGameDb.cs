using Microsoft.EntityFrameworkCore;
using VideoGame.Models.Domain;
using Bogus;
using System;

namespace VideoGame.Data
{
    public class VideoGameDb(DbContextOptions options) : DbContext(options)
    {
        public DbSet<VideoGames> VideoGames { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<InstantSavings> InstantSavings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Attach the modern runtime seeding callback (EF Core 9+)
            optionsBuilder.UseAsyncSeeding(async (context, _, cancellationToken) =>
            {
                // ==========================================
                // SEED TASK 1: INSTANT SAVINGS
                // ==========================================
                if (!await context.Set<InstantSavings>().AnyAsync(cancellationToken))
                {
                    int totalRecords = 1000;
                    int batchSize = 1000; // Adjusted to match or be lower than totalRecords

                    var faker = new Faker<InstantSavings>()
                        .RuleFor(s => s.Id, f => f.Random.Guid())
                        .RuleFor(s => s.FullName, f => f.Name.FullName().ToUpper())
                        .RuleFor(s => s.DateOfBirth, f => f.Date.Past(40, DateTime.UtcNow.AddYears(-18)).ToString("ddMMyyyy"))
                        .RuleFor(s => s.Bvn, f => f.Random.ReplaceNumbers("###########"))
                        .RuleFor(s => s.Nin, f => f.Random.ReplaceNumbers("###########"))
                        .RuleFor(s => s.Gender, f => f.PickRandom("1", "2"))
                        .RuleFor(s => s.PhoneNumber, f => f.Phone.PhoneNumber("080########"))
                        .RuleFor(s => s.Email, f => f.Internet.Email())
                        .RuleFor(s => s.CustomerAddress, f => f.Address.FullAddress().ToUpper())
                        .RuleFor(s => s.Status, f => "SUCCESSFUL")
                        .RuleFor(s => s.AccountType, f => "INSTANT SAVING")
                        .RuleFor(s => s.Source, f => f.Internet.UserName())
                        .RuleFor(s => s.BranchCode, f => f.Random.Number(100, 999))
                        .RuleFor(s => s.LedCode, f => f.Random.Decimal(10, 100))
                        .RuleFor(s => s.AccountNumber, f => f.Random.ReplaceNumbers("00########"))
                        .RuleFor(s => s.AccountCurreny, f => "NGN")
                        .RuleFor(s => s.ProductType, f => f.Random.Number(1, 5))
                        .RuleFor(s => s.CreatedDate, f => f.Date.Past(1))
                        .RuleFor(s => s.CorporateId, f => Guid.Empty)
                        .RuleFor(s => s.MotherMaidenName, f => f.Name.LastName())
                        .RuleFor(s => s.StateOfOrigin, f => f.Address.State())
                        .RuleFor(s => s.Lga, f => f.Address.City())
                        .RuleFor(s => s.ExpectedMonthlyTurnover, f => f.Random.Decimal(50000, 500000))
                        .RuleFor(s => s.NextofKinName, f => f.Name.FullName())
                        .RuleFor(s => s.NextofKinPhoneNumber, f => f.Phone.PhoneNumber("081########"));

                    for (int i = 0; i < totalRecords; i += batchSize)
                    {
                        var batch = faker.Generate(batchSize);
                        await context.Set<InstantSavings>().AddRangeAsync(batch, cancellationToken);
                        await context.SaveChangesAsync(cancellationToken);
                        context.ChangeTracker.Clear();
                    }
                }

                // ==========================================
                // SEED TASK 2: PRODUCTS
                // ==========================================
                if (!await context.Set<Product>().AnyAsync(cancellationToken))
                {
                    int totalRecords = 10000;
                    int batchSize = 2000;

                    var faker = new Faker<Product>()
                        .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                        .RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price(10, 1000)))
                        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
                        .RuleFor(p => p.CreatedAt, f => f.Date.Past(1));

                    for (int i = 0; i < totalRecords; i += batchSize)
                    {
                        var batch = faker.Generate(batchSize);
                        await context.Set<Product>().AddRangeAsync(batch, cancellationToken);
                        await context.SaveChangesAsync(cancellationToken);
                        context.ChangeTracker.Clear();
                    }
                }
            });
        }

        // Fixes the Microsoft.EntityFrameworkCore.Model.Validation precision warnings
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<InstantSavings>()
                .Property(s => s.ExpectedMonthlyTurnover)
                .HasPrecision(18, 2);

            modelBuilder.Entity<InstantSavings>()
                .Property(s => s.LedCode)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);
        }
    }
}