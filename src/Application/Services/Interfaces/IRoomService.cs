using Application.Common.Dto.Reservation;
using Domain.Entities;

namespace Application.Services.Interfaces;

public interface IRoomService
{
    Task<Room> CreateReservation(Guid roomId, CreateReservationDto reservation, CancellationToken cancellationToken = default);
}
