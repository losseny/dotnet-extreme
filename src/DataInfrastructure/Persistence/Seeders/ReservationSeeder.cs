using System.Collections.Immutable;
using Data.Common.Interfaces;
using Domain.Common.Designs.ReservationFactory;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Data.Persistence.Seeders;

public class ReservationSeeder(IApplicationDbContext context, ILogger<ApplicationDbContextInitializer> logger) : ISeeder<SeedReservationWrapper>
{
    public SeedReservationWrapper Seed()
    {
        if (context.Reservations.Any()) return new SeedReservationWrapper();
        var now = DateTime.Now;
        const int userRange = 100;
        const int studyGroupRange = 4;
        var random = new Random();
        var timeSlots = Enumerable.Range(8, 10)
            .Select(num => new TimeSlot { Start = num, End = num + 1 })
            .ToArray();

        var usersRange = Enumerable
            .Range(1, userRange)
            .Select(_ => new IdWrapper { Id = Guid.NewGuid() })
            .ToList();
        var studyGroupsRange = Enumerable.Range(1, studyGroupRange)
            .Select(_ => new IdWrapper {Id = Guid.NewGuid()})
            .ToList();

        var freeReservations = CreateReservations(
            usersRange,
            ReservationType.Free,
            random,
            timeSlots,
            now
        );
        var studyReservations = CreateReservations(
            studyGroupsRange,
            ReservationType.Study,
            random,
            timeSlots,
            now,
            20
        );

        logger.Log(
            LogLevel.Information,
            "Free: {Free}; Study:{Study}; Total: {Total}",
            freeReservations.Count, studyReservations.Count,
            freeReservations.Count + studyReservations.Count
        );

        return new SeedReservationWrapper(
            freeReservations,
            studyReservations
        );
    }

    private static IImmutableList<Reservation> CreateReservations(IList<IdWrapper> wrappers, ReservationType type, Random random, TimeSlot[] timeSlots, DateTime now, int amount = 10)
    {
        IReservationFactory factory = new ReservationFactory();

        return wrappers.SelectMany(value =>
        {
            return Enumerable.Range(1, random.Next(amount)).Select(_ =>
            {
                var timeSlot = timeSlots[random.Next(timeSlots.Length)];
                return factory.CreateReservation(
                    type,
                    ReservationDate.InstanceFromDate(DateOnly.FromDateTime(now.AddDays(random.Next(100)))),
                    ReservationPeriod.InstanceFromTime(
                        TimeOnly.Parse($"{timeSlot.Start}:00"),
                        TimeOnly.Parse($"{timeSlot.End}:00")
                    ),
                    value.Id
                );
            });
        }).ToImmutableList();
    }
}


internal struct IdWrapper
{
    public Guid Id { get; init; }
}

internal struct TimeSlot
{
    public int Start { get; init; }
    public int End { get; init; }
}
