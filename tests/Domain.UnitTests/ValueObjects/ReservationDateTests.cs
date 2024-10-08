using Domain.ValueObjects;
using NUnit.Framework;
using FluentAssertions;

namespace Domain.UnitTests.ValueObjects;

[TestFixture]
public class ReservationDateTests
{
    [Test]
    public void InstanceFromDate_ShouldCreateReservationDate_WhenDateIsInTheFuture()
    {
        // Arrange
        var futureDate = DateOnly.FromDateTime(DateTime.Today.AddDays(1));

        // Act
        var reservationDate = ReservationDate.InstanceFromDate(futureDate);

        // Assert
        reservationDate.Should().NotBeNull();
        reservationDate.Date.Should().Be(futureDate);
    }

    [Test]
    public void InstanceFromDate_ShouldCreateReservationDate_WhenDateIsToday()
    {
        // Arrange
        var todayDate = DateOnly.FromDateTime(DateTime.Today);

        // Act
        var reservationDate = ReservationDate.InstanceFromDate(todayDate);

        // Assert
        reservationDate.Should().NotBeNull();
        reservationDate.Date.Should().Be(todayDate);
    }

    [Test]
    public void InstanceFromDate_ShouldThrowArgumentException_WhenDateIsInThePast()
    {
        // Arrange
        var pastDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-1));

        // Act
        Action act = () => ReservationDate.InstanceFromDate(pastDate);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Reservation date cannot be in the past");
    }

}
