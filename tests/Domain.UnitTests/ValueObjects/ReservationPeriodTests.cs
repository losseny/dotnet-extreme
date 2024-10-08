using Domain.ValueObjects;
using NUnit.Framework;
using FluentAssertions;

namespace Domain.UnitTests.ValueObjects;

[TestFixture]
public class ReservationPeriodTests
{
    [Test]
    public void InstanceFromTime_WithValidTimeOnly_ShouldCreateReservationPeriod()
    {
        // Arrange
        var startTime = new TimeOnly(9, 0);
        var endTime = new TimeOnly(10, 0);

        // Act
        var reservationPeriod = ReservationPeriod.InstanceFromTime(startTime, endTime);

        // Assert
        reservationPeriod.Should().NotBeNull();
        reservationPeriod.Start.Should().Be(startTime);
        reservationPeriod.End.Should().Be(endTime);
    }

    [Test]
    public void InstanceFromTime_WithEndTimeBeforeStartTime_ShouldThrowArgumentException()
    {
        // Arrange
        var startTime = new TimeOnly(10, 0);
        var endTime = new TimeOnly(9, 0);

        // Act
        Action act = () => ReservationPeriod.InstanceFromTime(startTime, endTime);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Start time cannot be after end time");
    }

    [Test]
    public void InstanceFromTime_WithValidDateTime_ShouldCreateReservationPeriod()
    {
        // Arrange
        var startTime = DateTime.Today.AddHours(9);
        var endTime = DateTime.Today.AddHours(10);

        // Act
        var reservationPeriod = ReservationPeriod.InstanceFromTime(startTime, endTime);

        // Assert
        reservationPeriod.Should().NotBeNull();
        reservationPeriod.Start.Should().Be(TimeOnly.FromDateTime(startTime));
        reservationPeriod.End.Should().Be(TimeOnly.FromDateTime(endTime));
    }

    [Test]
    public void InstanceFromTime_WithEndTimeBeforeStartTimeSameDay_ShouldThrowArgumentException()
    {
        // Arrange
        var startTime = DateTime.Today.AddHours(10);
        var endTime = DateTime.Today.AddHours(9);

        // Act
        Action act = () => ReservationPeriod.InstanceFromTime(startTime, endTime);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Start time cannot be after end time");
    }

    [Test]
    public void InstanceFromTime_WithDifferentDays_ShouldCreateReservationPeriod()
    {
        // Arrange
        var startTime = DateTime.Today.AddHours(23);
        var endTime = DateTime.Today.AddDays(1).AddHours(1);

        // Act
        var reservationPeriod = ReservationPeriod.InstanceFromTime(startTime, endTime);

        // Assert
        reservationPeriod.Should().NotBeNull();
        reservationPeriod.Start.Should().Be(TimeOnly.FromDateTime(startTime));
        reservationPeriod.End.Should().Be(TimeOnly.FromDateTime(endTime));
    }

}
