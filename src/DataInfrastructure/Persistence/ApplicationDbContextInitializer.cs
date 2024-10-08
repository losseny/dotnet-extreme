using Data.Common.Extensions;
using Data.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data.Persistence;

public class ApplicationDbContextInitializer(ApplicationDbContext context, ILogger<ApplicationDbContextInitializer> logger): IContextInitializer
{

    public async Task InitialiseAsync()
    {
        try
        {
            logger.LogInformation("Initialising the database");
            if (context.Database.IsNpgsql() && await context.Database.CanConnectAsync())
            {
                await context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initialising the database");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        logger.LogInformation("Seeding the database");
        await InitialiseAsync();
        await context.SeedDevelop(logger);
    }
}
