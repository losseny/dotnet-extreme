using Microsoft.AspNetCore.Mvc;
using Presentation.Services;

namespace Presentation.Controllers;

public class PublicController(SeedService seedService): ApiControllerBase
{

    [HttpGet("ping")]
    public ActionResult Ping()
    {
        return Ok(new { Message = "Pong" });
    }

    [HttpPost("seed")]
    public async Task<ActionResult> Seed()
    {
        // In a real-world scenario, I would check if this is a development environment
        // and only allow seeding in development environments to avoid accidental data loss
        // and to prevent seeding in production environments
        // Or use an already seeded database as an image in development environments
        // but for the sake of this example, I will allow seeding in all environments
        await seedService.InitialiseAndSeedDatabase();
        return Ok(new { Message = "Database initialized and seeded" });
    }
}
