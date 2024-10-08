using Data.Persistence;

namespace Presentation.Services;

public class SeedService(ApplicationDbContextInitializer initializer)
{
    public async Task InitialiseAndSeedDatabase()
    {
        await initializer.SeedAsync();
    }

}
