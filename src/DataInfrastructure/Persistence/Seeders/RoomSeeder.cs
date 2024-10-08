using System.Collections.Immutable;
using Data.Common.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Data.Persistence.Seeders;

public class RoomSeeder(IApplicationDbContext context, SeedReservationWrapper reservationsWrapper, ILogger<ApplicationDbContextInitializer> logger): ISeeder<ICollection<Room>>
{
    public ICollection<Room> Seed()
    {

        if (context.Rooms.Any()) return [];
        var random = new Random();
        var buildings = new[] { "A", "B", "C", "D" };
        var floors = new[] { 1, 2, 3};
        var freeCopy = new List<Reservation>(reservationsWrapper.FreeReservations);
        var studyCopy = new List<Reservation>(reservationsWrapper.StudyReservations);

        var rooms = Enumerable.Range(0, buildings.Length).SelectMany(building =>
        {
            var floorAmount = floors[random.Next(floors.Length)];
            return Enumerable.Range(0, floorAmount).SelectMany(floor =>
            {
                return Enumerable.Range(0, 30).Select(roomNumber =>
                {
                    var choice = random.Next(1, 3);
                    var reservations = choice == 1 ? freeCopy : studyCopy;

                    var room = new Room(buildings[building], floor, roomNumber);
                    var took = reservations.Take(new Range(0, random.Next(0, 30))).Select(Reservation (r) =>
                    {
                        return choice switch
                        {
                            1 when r is FreeReservation free => new FreeReservation(r.ReservationDate, r.Period,
                                free.BookerId),
                            2 when r is StudyReservation study => new StudyReservation(r.ReservationDate, r.Period,
                                study.StudyGroupId),
                            _ => throw new InvalidOperationException("Invalid reservation type")
                        };
                    }).ToArray();
                    var notAdded = room.AddAllReservation(took);
                    logger.Log(
                        LogLevel.Information,
                        "Room {RoomBuildingPrefix}-{RoomFloorNumber}-{RoomRoomNumber} has {RoomReservationsCount} reservations of type {FullName} with {ReservationsCount} reservations and {TookLength} taken - {NotAddedCount} not added",
                        room.BuildingPrefix, room.FloorNumber,
                        room.RoomNumber, room.Reservations.Count,
                        choice, reservations.Count,
                        took.Length, notAdded.Count
                    );
                    return room;
                });
            });
        }).ToImmutableList();

        context.Rooms.AddRange(rooms);
        return rooms;
    }
}
