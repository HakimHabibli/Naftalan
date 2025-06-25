using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NaftalanHotelSystem.Domain.Entites;

namespace NaftalanHotelSystem.Persistence.Configurations.Common;

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
