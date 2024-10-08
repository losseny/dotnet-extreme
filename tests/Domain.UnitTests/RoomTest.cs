using Domain.Entities;
using Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace Domain.UnitTests;

public class RoomTest
{
    [Test]
    public void AddAllReservation_ShouldAddAllNonDuplicateReservations()
    {
        // Arrange
        var room = new Room("B", 3, 202);

        var reservation1 = new FreeReservation(
            ReservationDate.InstanceFromDate(DateOnly.FromDateTime(DateTime.Today.AddDays(1))),
            ReservationPeriod.InstanceFromTime(new TimeOnly(9, 0), new TimeOnly(10, 0)),
            Guid.NewGuid()
        );

        var reservation2 = new StudyReservation(
            ReservationDate.InstanceFromDate(DateOnly.FromDateTime(DateTime.Today.AddDays(1))),
            ReservationPeriod.InstanceFromTime(new TimeOnly(10, 0), new TimeOnly(11, 0)),
            Guid.NewGuid()
        );

        var reservation3 = new FreeReservation(
            ReservationDate.InstanceFromDate(DateOnly.FromDateTime(DateTime.Today.AddDays(1))),
            ReservationPeriod.InstanceFromTime(new TimeOnly(9, 0), new TimeOnly(10, 0)),
            Guid.NewGuid()
        );

        // Act
        var notAdded = room.AddAllReservation(reservation1, reservation2, reservation3);

        // Assert
        room.Reservations.Should().HaveCount(2);
        room.Reservations.Should().Contain(reservation1);
        room.Reservations.Should().Contain(reservation2);
        notAdded.Should().ContainSingle().Which.Should().Be(reservation3);
    }

    [Test]
    public void AddAllReservation_ShouldReturnAllReservations_WhenTheyAreAllDuplicates()
    {
        // Arrange
        var room = new Room("C", 1, 303);
        var reservation = new StudyReservation(
            ReservationDate.InstanceFromDate(DateOnly.FromDateTime(DateTime.Today.AddDays(1))),
            ReservationPeriod.InstanceFromTime(new TimeOnly(12, 0), new TimeOnly(13, 0)),
            Guid.NewGuid()
        );
        room.AddAllReservation(reservation);

        // Act
        var notAdded = room.AddAllReservation(reservation);

        // Assert
        room.Reservations.Should().HaveCount(1);
        notAdded.Should().ContainSingle().Which.Should().Be(reservation);
    }

    [Test]
    public void AddAllReservation_ShouldNotModifyOriginalList_WhenNoReservationsAreAdded()
    {
        // Arrange
        var room = new Room("D", 4, 404);
        var existingReservation = new FreeReservation(
            ReservationDate.InstanceFromDate(DateOnly.FromDateTime(DateTime.Today.AddDays(1))),
            ReservationPeriod.InstanceFromTime(new TimeOnly(14, 0), new TimeOnly(15, 0)),
            Guid.NewGuid()
        );
        room.AddAllReservation(existingReservation);

        // Act
        var reservationToBeAdded = new FreeReservation(
            ReservationDate.InstanceFromDate(DateOnly.FromDateTime(DateTime.Today.AddDays(1))),
            ReservationPeriod.InstanceFromTime(new TimeOnly(14, 0), new TimeOnly(15, 0)),
            Guid.NewGuid()
        );
        var notAdded = room.AddAllReservation(reservationToBeAdded);

        // Assert
        room.Reservations.Should().HaveCount(1);
        notAdded.Should().ContainSingle().Which.Should().Be(reservationToBeAdded);
    }
}
