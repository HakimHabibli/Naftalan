using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NaftalanHotelSystem.Domain.Entites;

namespace NaftalanHotelSystem.Persistence.Configurations.Common;

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
