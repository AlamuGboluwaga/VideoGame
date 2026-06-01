using Microsoft.EntityFrameworkCore;
using VideoGame.Data;
using FluentValidation;
using System;
using System.Threading.Tasks; // Required for Task

namespace VideoGame
{
    public class Program
    {
        // Changed from "public static void Main" to "async Task"
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.

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
            app.UseCors("AllowReactApp");

            app.UseAuthorization();

            app.MapControllers();

            // This scope block safely triggers your DbContext's UseAsyncSeeding method automatically 
            // right after migrating.
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<VideoGameDb>();

                // Automatically applies any pending migrations and runs UseAsyncSeeding
                await context.Database.MigrateAsync();
            }

            await app.RunAsync(); // Upgraded to RunAsync to stay fully asynchronous
        }
    }
}