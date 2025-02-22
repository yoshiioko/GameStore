using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    public static async Task InitializeDbAsync(this WebApplication app)
    {
        await app.MigrateDbAsync();
        await app.SeedDbAsync();
    }

    private static async Task MigrateDbAsync(this WebApplication app)
    {
        // Manually provision a scope for DbContext
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        await dbContext.Database.MigrateAsync();
    }

    private static async Task SeedDbAsync(this WebApplication app)
    {
        // Manually provision a scope for DbContext
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

        if (!dbContext.Genres.Any())
        {
            dbContext.Genres.AddRange(
                new Genre { Name = "Fighting" },
                new Genre { Name = "Kids and Family" },
                new Genre { Name = "Racing" },
                new Genre { Name = "Roleplaying" },
                new Genre { Name = "Sports" }
            );

            await dbContext.SaveChangesAsync();
        }
    }
}