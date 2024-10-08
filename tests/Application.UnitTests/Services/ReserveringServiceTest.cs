using Application.Common.Behaviours;
using Application.Common.Dto.Common;
using Application.Common.Dto.Reservation;
using Application.Common.Extensions;
using Application.Common.Mappings;
using Application.Services;
using AutoMapper;
using Data.Agents;
using Data.Agents.Dto;
using Data.Repository;
using Domain.Common.Designs.ReservationFactory;
using Domain.Entities;
using Domain.ValueObjects;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Application.UnitTests.Services;

public class ReserveringServiceTest
{
    private Mock<IReserveringRepository> _repositoryMock;
    private Mock<IReservationFactory> _factoryMock;
    private Mock<IAgent> _agentMock;
    private IMapper _mapper;
    private ReserveringService _reserveringService;

    [SetUp]
    public void SetUp()
    {
        _repositoryMock = new Mock<IReserveringRepository>();
        _factoryMock = new Mock<IReservationFactory>();
        _agentMock = new Mock<IAgent>();

        var configuration = new MapperConfiguration(config =>
            config.AddProfile<MappingProfile>());
        _mapper = configuration.CreateMapper();

        _reserveringService = new ReserveringService(
            _repositoryMock.Object,
            _factoryMock.Object,
            _mapper
        );
    }

    // Does not work for some reason
    public async Task FetchAllRosterOfBookerByWeek_ShouldReturnMappedReservations()
    {
        // Arrange
        var userId = Guid.NewGuid();
        const int week = 5;

        var studyGroupDto = new StudyGroupAgentDto { Id = Guid.NewGuid() };
        _agentMock.Setup(a => a.GetResource<StudyGroupAgentDto>($"http://dummyhost/api/studygroup/{userId}"))
            .ReturnsAsync(studyGroupDto);

        var dates = ChronosBehaviour.GetInstance().ConvertWeekToDate(week).GenerateDaysInWeek();

        var reservations = new List<Reservation>
        {
            new FreeReservation(
                ReservationDate.InstanceFromDate(DateOnly.FromDateTime(DateTime.Today.AddDays(1))),
                ReservationPeriod.InstanceFromTime(new TimeOnly(9, 0), new TimeOnly(10, 0)),
                userId
            )
        };

        _repositoryMock.Setup(r => r.FetchAllRosterOfUserByWeek(It.Is<List<DateOnly>>(d => d.SequenceEqual(dates)), userId, studyGroupDto.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(reservations);

        // Act
        var result = await _reserveringService.FetchAllRosterOfBookerByWeek(userId, week);
        result = result.ToList();

        // Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().HaveCount(reservations.Count);
        result.Should().AllBeOfType<ReservationDto>();
    }

    [Test]
    public async Task FindReservationById_ShouldReturnMappedReservationDto()
    {
        // Arrange
        var reservationId = Guid.NewGuid();
        var reservation = new StudyReservation(
            ReservationDate.InstanceFromDate(DateOnly.FromDateTime(DateTime.Today.AddDays(1))),
            ReservationPeriod.InstanceFromTime(new TimeOnly(10, 0), new TimeOnly(11, 0)),
            Guid.NewGuid()
        );

        _repositoryMock.Setup(r => r.FindReservationById(reservationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(reservation);


        // Act
        var result = await _reserveringService.FindReservationById(reservationId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ReservationDto>();
    }

    [Test]
    public async Task CreateReservation_ShouldReturnCreatedReservation_WhenNoDuplicateExists()
    {
        // Arrange
        var createReservationDto = new CreateReservationDto
        {
            ReservationDate = new ReservationDateDto { Date = DateOnly.FromDateTime(DateTime.Today.AddDays(1)) },
            Period = new ReservationPeriodDto { Start = new TimeOnly(9, 0), End = new TimeOnly(10, 0) },
            BookerId = Guid.NewGuid()
        };

        var reservation = new FreeReservation(
            ReservationDate.InstanceFromDate(createReservationDto.ReservationDate.Date),
            ReservationPeriod.InstanceFromTime(createReservationDto.Period.Start, createReservationDto.Period.End),
            createReservationDto.BookerId.Value
        );

        _factoryMock.Setup(f => f.CreateReservation(
                It.IsAny<ReservationDate>(),
                It.IsAny<ReservationPeriod>(),
                It.IsAny<Guid?>(),
                It.IsAny<Guid?>()
            ))
            .Returns(reservation);

        _repositoryMock.Setup(r => r.FindReservation(It.IsAny<Reservation>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Reservation)null);

        // Act
        var result = await _reserveringService.CreateReservation(createReservationDto);

        // Assert
        result.Should().Be(reservation);
    }

    [Test]
    public void CreateReservation_ShouldThrowException_WhenDuplicateReservationExists()
    {
        // Arrange
        var createReservationDto = new CreateReservationDto
        {
            ReservationDate = new ReservationDateDto { Date = DateOnly.FromDateTime(DateTime.Today.AddDays(1)) },
            Period = new ReservationPeriodDto { Start = new TimeOnly(9, 0), End = new TimeOnly(10, 0) },
            BookerId = Guid.NewGuid()
        };

        var reservation = new FreeReservation(
            ReservationDate.InstanceFromDate(createReservationDto.ReservationDate.Date),
            ReservationPeriod.InstanceFromTime(createReservationDto.Period.Start, createReservationDto.Period.End),
            createReservationDto.BookerId.Value
        );

        _factoryMock.Setup(f => f.CreateReservation(
                It.IsAny<ReservationDate>(),
                It.IsAny<ReservationPeriod>(),
                It.IsAny<Guid?>(),
                It.IsAny<Guid?>()
            ))
            .Returns(reservation);


        _repositoryMock.Setup(r => r.FindReservation(It.IsAny<Reservation>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(reservation);
        // Act
        Func<Task> act = async () => await _reserveringService.CreateReservation(createReservationDto);

        // Assert
        act.Should().ThrowAsync<Exception>()
            .WithMessage("Reservation already exists");
    }
}
