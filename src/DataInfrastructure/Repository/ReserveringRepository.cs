using Data.Common.Extensions;
using Data.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository;

public class ReserveringRepository(IApplicationDbContext context) : IReserveringRepository
{
    public async Task<IEnumerable<Reservation>> FetchAllRosterOfUserByWeek(IEnumerable<DateOnly> dates, Guid userId,
        Guid studyGroupId, CancellationToken cancellationToken = default)
    {
        var studyReservations = context.Reservations.OfType<StudyReservation>()
            .Where(r => r.StudyGroupId == studyGroupId && dates.Contains(r.ReservationDate.Date))
            .AsNoTracking();

        var freeReservations = context.Reservations.OfType<FreeReservation>()
            .Where(r => r.BookerId == userId && dates.Contains(r.ReservationDate.Date))
            .AsNoTracking();

        var studyReservationsList = await studyReservations.ToListAsync(cancellationToken);
        var freeReservationsList = await freeReservations.ToListAsync(cancellationToken);

        return studyReservationsList
            .Concat<Reservation>(freeReservationsList)
            .OrderBy(r => r.ReservationDate.Date);
    }

    public async Task<Reservation> FindReservationById(Guid id, CancellationToken cancellationToken = default) =>
        await context.Reservations
            .FindEntityAsync(id, cancellationToken);

    // Is sending back null a good idea? Maybe refactor or do an Optional type?
    public async Task<Reservation?> FindReservation(Reservation reservation, CancellationToken cancellationToken = default)
    {
        var query = context.Reservations.AsNoTracking()
                .Where(r => r.ReservationDate.Date.Equals(reservation.ReservationDate.Date))
                .Where(r => r.Period.Start == reservation.Period.Start)
                .Where(r => r.Period.End == reservation.Period.End);

        switch (reservation)
        {
            case StudyReservation studyReservation:
                query = query
                    .OfType<StudyReservation>()
                    .Where(r => r.StudyGroupId == studyReservation.StudyGroupId);
                break;
            case FreeReservation freeReservation:
                query = query
                    .OfType<FreeReservation>()
                    .Where(r => r.BookerId == freeReservation.BookerId);
                break;
        }

        return await query.FirstOrDefaultAsync(cancellationToken);
    }
}
