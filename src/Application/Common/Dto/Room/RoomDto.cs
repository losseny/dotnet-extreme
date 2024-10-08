using Application.Common.Mappings;

namespace Application.Common.Dto.Room;

public record RoomDto: IMapToFrom<Domain.Entities.Room>
{
    public Guid Id { get; set; }
    public string BuildingPrefix { get; set; }
    public int FloorNumber { get; set; }
    public int RoomNumber { get; set; }
}
