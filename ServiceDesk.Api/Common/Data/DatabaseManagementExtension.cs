using ServiceDesk.Api;
using ServiceDesk.Infrastructure.Data.Seeds;

namespace ServiceDesk.Api.Common.Data
{
    public static class DatabaseManagementExtension
    {
        public static async Task UseDatabaseSeeding(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var initializer = services.GetRequiredService<DbInitializer>();

                await initializer.SeedAsync();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }
    }
}
