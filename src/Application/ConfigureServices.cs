using System.Reflection;
using Application.Services;
using Application.Services.Interfaces;
using Domain.Common.Designs.ReservationFactory;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ConfigureServices
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		services.AddAutoMapper(Assembly.GetExecutingAssembly());
		services.AddScoped<IReserveringService, ReserveringService>();
		services.AddScoped<IRoomService, RoomService>();

		services.AddScoped<IReservationFactory, ReservationFactory>();
		return services;
	}
}
