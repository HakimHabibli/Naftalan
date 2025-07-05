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

        // TreatmentMethod ilə əlaqə
        builder.HasOne(t => t.TreatmentMethod)
               .WithMany(m => m.Translations)
               .HasForeignKey(t => t.TreatmentMethodId);

        // TreatmentCategory ilə əlaqə
        builder.HasOne(t => t.Category)
               .WithMany(c => c.TreatmentMethodTranslations)
               .HasForeignKey(t => t.TreatmentCategoryId)
               .OnDelete(DeleteBehavior.Restrict);  // silmə məhdudlaşdırıla bilər
    }
}

public class TreatmentCategoryConfiguration : IEntityTypeConfiguration<TreatmentCategory>
{
    public void Configure(EntityTypeBuilder<TreatmentCategory> builder)
    {
        builder.ConfigureBaseEntity();

        // Category -> CategoryTranslation (one-to-many)
        builder.HasMany(c => c.Translations)
               .WithOne(t => t.TreatmentCategory)
               .HasForeignKey(t => t.TreatmentCategoryId)
               .OnDelete(DeleteBehavior.Cascade);

        // Category -> TreatmentMethodTranslation (one-to-many)
        builder.HasMany(c => c.TreatmentMethodTranslations)
               .WithOne(t => t.Category)
               .HasForeignKey(t => t.TreatmentCategoryId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
public class TreatmentCategoryTranslationConfiguration : IEntityTypeConfiguration<TreatmentCategoryTranslation>
{
    public void Configure(EntityTypeBuilder<TreatmentCategoryTranslation> builder)
    {
        builder.ConfigureBaseEntity();

        builder.Property(t => t.Name).IsRequired();
        builder.Property(t => t.Language)
               .HasConversion<string>()
               .IsRequired();

        builder.HasOne(t => t.TreatmentCategory)
               .WithMany(c => c.Translations)
               .HasForeignKey(t => t.TreatmentCategoryId);
    }
}
