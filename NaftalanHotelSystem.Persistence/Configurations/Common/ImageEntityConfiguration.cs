using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NaftalanHotelSystem.Domain.Entites;

namespace NaftalanHotelSystem.Persistence.Configurations.Common;

public class ImageEntityConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.ConfigureBaseEntity();

        builder.Property(x => x.Url)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.Entity)
               .IsRequired()
               .HasConversion<string>(); 

        builder.Property(x => x.RelatedEntityId)
               .IsRequired();
    }
}
