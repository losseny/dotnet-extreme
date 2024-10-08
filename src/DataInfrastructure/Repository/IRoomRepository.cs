using Domain.Entities;

namespace Data.Repository;

public interface IRoomRepository
{
    Task<Guid> SaveRoom(Room room, CancellationToken cancellationToken);
    Task<Guid> UpdateRoom(Room room, CancellationToken cancellationToken);
    Task<Room> FindRoomById(Guid id, CancellationToken cancellationToken);
}
