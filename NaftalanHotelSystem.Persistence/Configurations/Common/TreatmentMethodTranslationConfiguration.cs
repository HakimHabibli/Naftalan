using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NaftalanHotelSystem.Domain.Entites;

namespace NaftalanHotelSystem.Persistence.Configurations.Common;

public class TreatmentMethodTranslationConfiguration : IEntityTypeConfiguration<TreatmentMethodTranslation>
{
    public void Configure(EntityTypeBuilder<TreatmentMethodTranslation> builder)
    {
        builder.ConfigureBaseEntity();

        builder.Property(t => t.Name).IsRequired();
        builder.Property(t => t.Description).IsRequired();
        builder.Property(t => t.Language)
               .HasConversion<string>()
               .IsRequired();

        builder.HasOne(t => t.TreatmentMethod)
               .WithMany(m => m.Translations)
               .HasForeignKey(t => t.TreatmentMethodId);

       
    }
}

