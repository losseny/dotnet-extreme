using Data.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Persistence.Configurations;

public class ReservationDbConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable("reservation");

        builder.ComplexProperty(reservation => reservation.ReservationDate)
            .Configure(new ReservationDateConfiguration());
        builder.ComplexProperty(reservation => reservation.Period)
            .Configure(new ReservationPeriodConfiguration());

        builder.HasDiscriminator<string>("reservation")
            .HasValue<FreeReservation>(nameof(FreeReservation))
            .HasValue<StudyReservation>(nameof(StudyReservation));
    }
}
