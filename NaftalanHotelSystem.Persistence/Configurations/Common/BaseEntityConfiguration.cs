using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NaftalanHotelSystem.Domain.Common;
using NaftalanHotelSystem.Domain.Entites;

namespace NaftalanHotelSystem.Persistence.Configurations.Common;

public static class BaseEntityConfiguration
{
    public static void ConfigureBaseEntity<T>(this EntityTypeBuilder<T> builder) where T : BaseEntity
    {
        builder.HasKey(x => x.Id);
    }
}

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

public class RoomTranslationEntityConfiguration : IEntityTypeConfiguration<RoomTranslation>
{
    public void Configure(EntityTypeBuilder<RoomTranslation> builder)
    {
        builder.ConfigureBaseEntity();

        builder.Property(x => x.Service).IsRequired();
        builder.Property(x=>x.Description).IsRequired();
        builder.Property(x=>x.Language).HasConversion<string>().IsRequired();
        builder.HasOne(rt => rt.Room)
        .WithMany(r => r.RoomTranslations)
        .HasForeignKey(rt => rt.RoomId);
    }
}
public class RoomEquipmentEntityConfiguration : IEntityTypeConfiguration<RoomEquipment>
{
    public void Configure(EntityTypeBuilder<RoomEquipment> builder)
    {
        builder.HasKey(r => new { r.RoomId, r.EquipmentId });

        builder.HasOne(re => re.Room)
               .WithMany(r => r.Equipments)
               .HasForeignKey(re => re.RoomId);

        builder.HasOne(re => re.Equipment)
               .WithMany(e => e.RoomEquipments)
               .HasForeignKey(re => re.EquipmentId);
    }
}
public class EquipmentEntityConfiguration : IEntityTypeConfiguration<Equipment>
{
    public void Configure(EntityTypeBuilder<Equipment> builder)
    {
        builder.ConfigureBaseEntity();

        builder.HasMany(e => e.EquipmentTranslations)
               .WithOne(et => et.Equipment)
               .HasForeignKey(et => et.EquipmentId);

        builder.HasMany(e => e.RoomEquipments)
               .WithOne(re => re.Equipment)
               .HasForeignKey(re => re.EquipmentId);
    }
}

public class EquipmentTranslationEntityConfiguration : IEntityTypeConfiguration<EquipmentTranslation>
{
    public void Configure(EntityTypeBuilder<EquipmentTranslation> builder)
    {
        builder.ConfigureBaseEntity();

        builder.Property(et => et.Name)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(et => et.Language)
               .HasConversion<string>()
               .IsRequired();

        builder.HasOne(et => et.Equipment)
               .WithMany(e => e.EquipmentTranslations)
               .HasForeignKey(et => et.EquipmentId);
    }
}
