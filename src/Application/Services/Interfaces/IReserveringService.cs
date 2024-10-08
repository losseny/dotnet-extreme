using Application.Common.Dto.Reservation;
using Domain.Entities;

namespace Application.Services.Interfaces;

public interface IReserveringService
{
    Task<IEnumerable<ReservationDto>> FetchAllRosterOfBookerByWeek(Guid id, int week = default);
    Task<ReservationDto> FindReservationById(Guid id);
    Task<Reservation> CreateReservation(CreateReservationDto reservation);

}
