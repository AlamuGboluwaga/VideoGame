using Microsoft.EntityFrameworkCore;
using VideoGame.Data;
using FluentValidation;
using System;
using System.Threading.Tasks;

namespace VideoGame
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. ADD CORS POLICY DEFINITION (Crucial for React frontend to hit your endpoints)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", policy =>
                {
                    policy.WithOrigins("http://localhost:3000") // Standard React development port
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<VideoGameDb>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);
            builder.Services.AddHttpClient();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // This now works perfectly because the policy is defined above
            app.UseCors("AllowReactApp");

            app.UseAuthorization();
            app.MapControllers();

            // 2. MIGRATE & SEED (Moved down right before running the application)
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<VideoGameDb>();
                    // Applies migrations and runs your Bogus UseAsyncSeeding method
                    await context.Database.MigrateAsync();
                }
                catch (Exception ex)
                {
                    // If the database connection fails, this stops Swagger from disappearing completely 
                    // without giving you a reason in your logs.
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating or seeding the database.");
                }
            }

            await app.RunAsync();
        }
    }
}