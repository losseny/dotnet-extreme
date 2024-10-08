using System.Diagnostics.CodeAnalysis;
using Application.Common.Dto.Reservation;
using Application.Common.Dto.Room;
using Application.Common.Mappings;
using Application.Services.Interfaces;
using AutoMapper;
using Data.Agents;
using Data.Agents.Dto;
using Data.Repository;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class RoomService(IRoomRepository repository, IReserveringService reserveringService, IMapper mapper, ILogger<RoomService> logger): IRoomService
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]

    private readonly IAgent agent = new DummyRoomAgent();

    public async Task<Room> CreateReservation(Guid roomId, CreateReservationDto reservationDto, CancellationToken cancellationToken = default)
    {
        Room room;

        var reservation = await reserveringService.CreateReservation(reservationDto);
        try
        {
            logger.LogInformation("Fetching room from database");
            room = await repository.FindRoomById(roomId, cancellationToken);
        }
        catch (NotFoundException)
        {
            logger.LogInformation("Room not found in database, fetching from agent");
            room = ToEntity(await agent.GetResource<RoomAgentDto>($"http://dummyhost/api/room/{roomId}"));
        }
        logger.LogInformation("Adding reservation to room");
        room.AddAllReservation(reservation);

        await repository.SaveRoom(room, cancellationToken);
        logger.LogInformation("Reservation created for room {RoomId}", room.Id);
        return room;
    }

    // Change to RoomDto when other services work
    private Room ToEntity(RoomAgentDto agentDto)
    {
        var dto = new RoomDto
        {
            Id = agentDto.Id,
            BuildingPrefix = agentDto.BuildingPrefix,
            FloorNumber = agentDto.FloorNumber,
            RoomNumber = agentDto.RoomNumber
        };
        return dto.MapTo<RoomDto, Room>(mapper.ConfigurationProvider);
    }
}
