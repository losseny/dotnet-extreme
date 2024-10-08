using System.Reflection;
using Data.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext() { }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<Reservation> Reservations => Set<Reservation>();
    public DbSet<Room> Rooms => base.Set<Room>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
