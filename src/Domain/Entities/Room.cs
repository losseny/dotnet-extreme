using Domain.Common.Extensions;

namespace Domain.Entities;

public class Room: BaseEntity
{
    public string BuildingPrefix { get; private set; }
    public int FloorNumber { get; private set; }
    public int RoomNumber { get; private set; }
    public ICollection<Reservation> Reservations { get; private init; } = new List<Reservation>();

    private Room() { }

    public Room(string buildingPrefix, int floorNumber, int roomNumber)
    {
        BuildingPrefix = buildingPrefix;
        FloorNumber = floorNumber;
        RoomNumber = roomNumber;
    }

    public ICollection<Reservation> AddAllReservation(params Reservation[] reservation) =>
        Reservations.AddAll(reservation, (l, res) => l.Any(r => r.ReservationDate.Equals(res.ReservationDate) && r.Period.Equals(res.Period)));

}
