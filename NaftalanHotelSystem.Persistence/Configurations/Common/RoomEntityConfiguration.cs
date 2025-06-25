using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NaftalanHotelSystem.Domain.Entites;

namespace NaftalanHotelSystem.Persistence.Configurations.Common;

public class RoomEntityConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ConfigureBaseEntity();

        builder.Property(x=>x.Area).IsRequired();
        builder.Property(x=>x.Price).IsRequired();
        builder.Property(x=>x.Member).IsRequired();
        builder.Property(x=>x.Category).IsRequired();

        builder.HasMany(r=>r.RoomTranslations).WithOne(rt=>rt.Room).HasForeignKey(rt => rt.RoomId);
        builder.HasMany(r => r.Equipments).WithOne(e => e.Room).HasForeignKey(e=>e.RoomId);

    }
}
