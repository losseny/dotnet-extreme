using System.Text.Json.Serialization;
using Application.Common.Converters;
using Data.Persistence;
using Presentation.Services;
using Presentation.Services.Interfaces;

namespace Presentation;

public static class ConfigurePresentationServices
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services)
    {
        var controllersBuilder = services.AddControllers(options => options.Conventions.Add(new CustomRouteToken()));

        controllersBuilder.AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.Converters.Add(new TimeOnlyConverter());
        });

        services.AddScoped<IApplicationContext, ApplicationContext>();
        services.AddTransient<SeedService>();

        return services;
    }

    public static void InitialiseAndSeedDatabase(this WebApplication app)
    {
        var task = Task.Run(async () =>
        {
            using var scope = app.Services.CreateScope();
            var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
            await initializer.InitialiseAsync();
        });
        task.Wait();
    }
}
