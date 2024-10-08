using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Persistence.Configurations;

public class RoomDbConfiguration: IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("room");

        builder.Property(r => r.BuildingPrefix)
            .HasColumnName("building_prefix");
        builder.Property(r => r.FloorNumber)
            .HasColumnName("floor_number");
        builder.Property(r => r.RoomNumber)
            .HasColumnName("room_number");


        builder.HasMany(r => r.Reservations)
            .WithOne(r => r.Room)
            .HasForeignKey(r => r.RoomId);
    }
}
