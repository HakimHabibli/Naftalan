using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NaftalanHotelSystem.Domain.Entites;

namespace NaftalanHotelSystem.Persistence.Configurations.Common;

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
