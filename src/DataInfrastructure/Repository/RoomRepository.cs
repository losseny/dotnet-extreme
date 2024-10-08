using Data.Common.Extensions;
using Data.Common.Interfaces;
using Domain.Entities;

namespace Data.Repository;

public class RoomRepository(IApplicationDbContext context): IRoomRepository
{
    public async Task<Guid> SaveRoom(Room room, CancellationToken cancellationToken)
    {
        if (!context.Rooms.Any(r => r.Id == room.Id))
        {
            context.Rooms.Add(room);
        }
        await context.SaveChangesAsync(cancellationToken);

        return room.Id;
    }

    public async Task<Guid> UpdateRoom(Room room, CancellationToken cancellationToken)
    {
        return await SaveRoom(room, cancellationToken);
    }

    public async Task<Room> FindRoomById(Guid id, CancellationToken cancellationToken)
    {
        return await context.Rooms.FindEntityAsync(id, cancellationToken);
    }
}
