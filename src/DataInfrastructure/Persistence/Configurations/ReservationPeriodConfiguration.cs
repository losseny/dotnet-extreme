using Data.Common.Interfaces;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Persistence.Configurations;

public class ReservationPeriodConfiguration : IComplexPropertyConfiguration<ReservationPeriod>
{
    public ComplexPropertyBuilder<ReservationPeriod> Configure(ComplexPropertyBuilder<ReservationPeriod> builder)
    {
        builder.Property(period => period.Start).HasColumnName("start");
        builder.Property(period => period.End).HasColumnName("end");

        return builder;
    }
}
