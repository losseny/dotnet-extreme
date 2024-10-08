using System.Collections.Immutable;
using Domain.Entities;

namespace Data.Persistence.Seeders;

public class SeedReservationWrapper(
    IImmutableList<Reservation> freeReservations,
    IImmutableList<Reservation> studyReservations)
{
    public IImmutableList<Reservation> FreeReservations { get; } = freeReservations;
    public IImmutableList<Reservation> StudyReservations { get; } = studyReservations;


    public SeedReservationWrapper() : this(ImmutableList<Reservation>.Empty, ImmutableList<Reservation>.Empty)
    {
    }
}
