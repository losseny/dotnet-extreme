using Data.Persistence;
using Data.Persistence.Seeders;
using Microsoft.Extensions.Logging;

namespace Data.Common.Extensions;

public static class SetupScenario
{
    public static async Task SeedDevelop(this ApplicationDbContext context, ILogger<ApplicationDbContextInitializer> logger)
    {
        var reservationSeeder = new ReservationSeeder(context, logger);
        var roomSeeder = new RoomSeeder(context, reservationSeeder.Seed(), logger);
        roomSeeder.Seed();
        logger.Log(LogLevel.Information, "Seeding completed");
        await context.SaveChangesAsync();
    }

}
