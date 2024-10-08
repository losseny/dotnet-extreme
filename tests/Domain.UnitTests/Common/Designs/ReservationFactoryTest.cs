using Domain.Common.Designs.ReservationFactory;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using NUnit.Framework;
using FluentAssertions;

namespace Domain.UnitTests.Common.Designs;

[TestFixture]
public class ReservationFactoryTests
{
    private ReservationFactory _reservationFactory;

    [SetUp]
    public void SetUp()
    {
        _reservationFactory = new ReservationFactory();
    }

    [Test]
    public void CreateReservation_WithReservationTypeStudy_ShouldReturnStudyReservation()
    {
        // Arrange
        var reservationDate = ReservationDate.InstanceFromDate(DateOnly.FromDateTime(DateTime.Today.AddDays(1)));
        var period = ReservationPeriod.InstanceFromTime(new TimeOnly(9, 0), new TimeOnly(10, 0));
        var id = Guid.NewGuid();

        // Act
        var reservation = _reservationFactory.CreateReservation(ReservationType.Study, reservationDate, period, id);

        // Assert
        reservation.Should().BeOfType<StudyReservation>();
        var studyReservation = reservation as StudyReservation;
        studyReservation.Should().NotBeNull();
        studyReservation!.StudyGroupId.Should().Be(id);
    }

    [Test]
    public void CreateReservation_WithReservationTypeFree_ShouldReturnFreeReservation()
    {
        // Arrange
        var reservationDate = ReservationDate.InstanceFromDate(DateOnly.FromDateTime(DateTime.Today.AddDays(1)));
        var period = ReservationPeriod.InstanceFromTime(new TimeOnly(9, 0), new TimeOnly(10, 0));
        var id = Guid.NewGuid();

        // Act
        var reservation = _reservationFactory.CreateReservation(ReservationType.Free, reservationDate, period, id);

        // Assert
        reservation.Should().BeOfType<FreeReservation>();
        var freeReservation = reservation as FreeReservation;
        freeReservation.Should().NotBeNull();
        freeReservation!.BookerId.Should().Be(id);
    }

    [Test]
    public void CreateReservation_WithInvalidReservationType_ShouldThrowArgumentException()
    {
        // Arrange
        var reservationDate = ReservationDate.InstanceFromDate(DateOnly.FromDateTime(DateTime.Today.AddDays(1)));
        var period = ReservationPeriod.InstanceFromTime(new TimeOnly(9, 0), new TimeOnly(10, 0));
        var id = Guid.NewGuid();

        // Act
        Action act = () => _reservationFactory.CreateReservation((ReservationType)999, reservationDate, period, id);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Invalid reservation type");
    }

    [Test]
    public void CreateReservation_WithOnlyGroupId_ShouldReturnStudyReservation()
    {
        // Arrange
        var reservationDate = ReservationDate.InstanceFromDate(DateOnly.FromDateTime(DateTime.Today.AddDays(1)));
        var period = ReservationPeriod.InstanceFromTime(new TimeOnly(9, 0), new TimeOnly(10, 0));
        var groupId = Guid.NewGuid();

        // Act
        var reservation = _reservationFactory.CreateReservation(reservationDate, period, groupId, null);

        // Assert
        reservation.Should().BeOfType<StudyReservation>();
        var studyReservation = reservation as StudyReservation;
        studyReservation.Should().NotBeNull();
        studyReservation!.StudyGroupId.Should().Be(groupId);
    }

    [Test]
    public void CreateReservation_WithOnlyBookerId_ShouldReturnFreeReservation()
    {
        // Arrange
        var reservationDate = ReservationDate.InstanceFromDate(DateOnly.FromDateTime(DateTime.Today.AddDays(1)));
        var period = ReservationPeriod.InstanceFromTime(new TimeOnly(9, 0), new TimeOnly(10, 0));
        var bookerId = Guid.NewGuid();

        // Act
        var reservation = _reservationFactory.CreateReservation(reservationDate, period, null, bookerId);

        // Assert
        reservation.Should().BeOfType<FreeReservation>();
        var freeReservation = reservation as FreeReservation;
        freeReservation.Should().NotBeNull();
        freeReservation!.BookerId.Should().Be(bookerId);
    }

    [Test]
    public void CreateReservation_WithBothGroupIdAndBookerId_ShouldThrowArgumentException()
    {
        // Arrange
        var reservationDate = ReservationDate.InstanceFromDate(DateOnly.FromDateTime(DateTime.Today.AddDays(1)));
        var period = ReservationPeriod.InstanceFromTime(new TimeOnly(9, 0), new TimeOnly(10, 0));
        var groupId = Guid.NewGuid();
        var bookerId = Guid.NewGuid();

        // Act
        Action act = () => _reservationFactory.CreateReservation(reservationDate, period, groupId, bookerId);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Invalid reservation type");
    }

    [Test]
    public void CreateReservation_WithNoGroupIdOrBookerId_ShouldThrowArgumentException()
    {
        // Arrange
        var reservationDate = ReservationDate.InstanceFromDate(DateOnly.FromDateTime(DateTime.Today.AddDays(1)));
        var period = ReservationPeriod.InstanceFromTime(new TimeOnly(9, 0), new TimeOnly(10, 0));

        // Act
        Action act = () => _reservationFactory.CreateReservation(reservationDate, period, null, null);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Invalid reservation type");
    }
}
