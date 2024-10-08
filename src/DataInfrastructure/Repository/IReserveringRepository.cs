using Domain.Entities;

namespace Data.Repository;

public interface IReserveringRepository
{
    Task<IEnumerable<Reservation>> FetchAllRosterOfUserByWeek(IEnumerable<DateOnly> dates, Guid userId, Guid studyGroupId = default, CancellationToken cancellationToken = default);
    Task<Reservation> FindReservationById(Guid id, CancellationToken cancellationToken = default);
    Task<Reservation?> FindReservation(Reservation reservation, CancellationToken cancellationToken = default);
}
