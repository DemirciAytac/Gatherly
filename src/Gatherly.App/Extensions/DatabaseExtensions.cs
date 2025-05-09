using Gatherly.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Gatherly.App.Extensions
{
    public static class DatabaseExtensions
    {
        public static async Task ApplyMigrationAsync(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                try
                {
                    await db.Database.MigrateAsync();
                    app.Logger.LogInformation("Database migration applied successfully.");
                }
                catch (Exception)
                {
                    app.Logger.LogError("An error occurred while applying database migration.");
                    throw;
                }
            }
        }
    }
}
