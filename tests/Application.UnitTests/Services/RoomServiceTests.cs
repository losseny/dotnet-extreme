using Application.Common.Dto.Reservation;
using Application.Common.Mappings;
using Application.Services;
using Application.Services.Interfaces;
using AutoMapper;
using Data.Agents;
using Data.Agents.Dto;
using Data.Repository;
using Domain.Entities;
using Domain.Exceptions;
using Domain.ValueObjects;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Application.UnitTests.Services;

public class RoomServiceTests
{
    private Mock<IRoomRepository> _repositoryMock;
    private Mock<IReserveringService> _reserveringServiceMock;
    private Mock<ILogger<RoomService>> _loggerMock;
    private RoomService _roomService;
    private readonly IMapper _mapper;

    public RoomServiceTests()
    {
        IConfigurationProvider configuration = new MapperConfiguration(config =>
            config.AddProfile<MappingProfile>());

        _mapper = configuration.CreateMapper();
    }

    [SetUp]
    public void SetUp()
    {
        _repositoryMock = new Mock<IRoomRepository>();
        _reserveringServiceMock = new Mock<IReserveringService>();
        _loggerMock = new Mock<ILogger<RoomService>>();

        _roomService = new RoomService(
            _repositoryMock.Object,
            _reserveringServiceMock.Object,
            _mapper,
            _loggerMock.Object);
    }

    [Test]
    public async Task CreateReservation_ShouldAddReservationToExistingRoom()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        var reservationDto = new CreateReservationDto();
        var reservation = new FreeReservation(
            ReservationDate.InstanceFromDate(DateOnly.FromDateTime(DateTime.Today.AddDays(1))),
            ReservationPeriod.InstanceFromTime(new TimeOnly(9, 0), new TimeOnly(10, 0)),
            Guid.NewGuid()
        );

        var existingRoom = new Room("A", 1, 101);

        _reserveringServiceMock.Setup(s => s.CreateReservation(reservationDto))
            .ReturnsAsync(reservation);
        _repositoryMock.Setup(r => r.FindRoomById(roomId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRoom);

        // Act
        await _roomService.CreateReservation(roomId, reservationDto);

        // Assert
        existingRoom.Reservations.Should().ContainSingle()
            .Which.Should().Be(reservation);
        _repositoryMock.Verify(r => r.SaveRoom(existingRoom, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task CreateReservation_ShouldFetchRoomFromAgent_WhenRoomNotFoundInDatabase()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        var reservationDto = new CreateReservationDto();
        var reservation = new StudyReservation(
            ReservationDate.InstanceFromDate(DateOnly.FromDateTime(DateTime.Today.AddDays(1))),
            ReservationPeriod.InstanceFromTime(new TimeOnly(10, 0), new TimeOnly(11, 0)),
            Guid.NewGuid()
        );

        var agentDto = new RoomAgentDto
        {
            Id = roomId,
            BuildingPrefix = "B",
            FloorNumber = 2,
            RoomNumber = 202
        };

        _reserveringServiceMock.Setup(s => s.CreateReservation(reservationDto))
            .ReturnsAsync(reservation);
        _repositoryMock.Setup(r => r.FindRoomById(roomId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new NotFoundException());

        var agentMock = new Mock<IAgent>();
        agentMock.Setup(a => a.GetResource<RoomAgentDto>($"http://dummyhost/api/room/{roomId}"))
            .ReturnsAsync(agentDto);

        var roomService = new RoomService(
            _repositoryMock.Object,
            _reserveringServiceMock.Object,
            _mapper,
            _loggerMock.Object
        );

        // Act
        var createdRoom = await roomService.CreateReservation(roomId, reservationDto);

        // Assert
        createdRoom.Reservations.Should().ContainSingle()
            .Which.Should().Be(reservation);
        _repositoryMock.Verify(r => r.SaveRoom(createdRoom, It.IsAny<CancellationToken>()), Times.Once);
    }
}
