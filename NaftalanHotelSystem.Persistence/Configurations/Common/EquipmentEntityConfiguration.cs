using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NaftalanHotelSystem.Domain.Entites;

namespace NaftalanHotelSystem.Persistence.Configurations.Common;

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
