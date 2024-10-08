using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Reservation> Reservations { get; }
    DbSet<Room> Rooms { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
