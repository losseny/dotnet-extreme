using Data.Common.Interfaces;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Persistence.Configurations;

public class ReservationDateConfiguration : IComplexPropertyConfiguration<ReservationDate>
{
    public ComplexPropertyBuilder<ReservationDate> Configure(ComplexPropertyBuilder<ReservationDate> builder)
    {
        builder.Property(date => date.Date).HasColumnName("date");

        return builder;
    }
}
