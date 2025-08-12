using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using NaftalanHotelSystem.Domain.Entites;

namespace NaftalanHotelSystem.Persistence.Configurations.Common;

public class RoomEntityConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ConfigureBaseEntity();

        builder.Property(x => x.Area).IsRequired();
        builder.Property(x => x.Price).IsRequired();
        builder.Property(x => x.Member).IsRequired();
        builder.Property(x => x.Category).IsRequired();

        builder.HasMany(r => r.RoomTranslations)
               .WithOne(rt => rt.Room)
               .HasForeignKey(rt => rt.RoomId);

        builder.HasMany(r => r.Equipments)
               .WithOne(e => e.Room)
               .HasForeignKey(e => e.RoomId);

        builder.HasMany(r => r.RoomChildren)
                   .WithOne(rc => rc.Room)
                   .HasForeignKey(rc => rc.RoomId);

        builder.HasMany(r => r.RoomPricesByOccupancy)
              .WithOne(p => p.Room);

    }
}
public class RoomPriceByOccupancyEntityConfiguration : IEntityTypeConfiguration<RoomPriceByOccupancy>
{
    public void Configure(EntityTypeBuilder<RoomPriceByOccupancy> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Occupancy).IsRequired();
        builder.Property(p => p.Price).IsRequired();


        builder.HasOne(p => p.Room)
               .WithMany(r => r.RoomPricesByOccupancy)
               .HasForeignKey(p => p.RoomId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}

public class ChildEntityConfiguration : IEntityTypeConfiguration<Child>
{
    public void Configure(EntityTypeBuilder<Child> builder)
    {
        builder.ConfigureBaseEntity();

        builder.Property(x => x.HasTreatment).IsRequired();
        builder.Property(x => x.Price).IsRequired();
        builder.Property(x => x.AgeRange).IsRequired();

        builder.HasMany(c => c.RoomChildren)
                 .WithOne(rc => rc.Child)
                 .HasForeignKey(rc => rc.ChildId);

    }
}
public class RoomChildEntityConfiguration : IEntityTypeConfiguration<RoomChild>
{
    public void Configure(EntityTypeBuilder<RoomChild> builder)
    {
        builder.HasKey(rc => new { rc.RoomId, rc.ChildId });

        builder.HasOne(rc => rc.Room)
               .WithMany(r => r.RoomChildren)
               .HasForeignKey(rc => rc.RoomId);

        builder.HasOne(rc => rc.Child)
               .WithMany(c => c.RoomChildren)
               .HasForeignKey(rc => rc.ChildId);
    }
}