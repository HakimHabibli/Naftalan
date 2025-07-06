using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NaftalanHotelSystem.Domain.Entites;

namespace NaftalanHotelSystem.Persistence.Configurations.Common;

public class TreatmentCategoryConfiguration : IEntityTypeConfiguration<TreatmentCategory>
{
    public void Configure(EntityTypeBuilder<TreatmentCategory> builder)
    {
        builder.ConfigureBaseEntity();

        builder.HasMany(c=>c.Translations)
            .WithOne(c => c.TreatmentCategory).HasForeignKey(c => c.TreatmentCategoryId);

        builder.HasMany(c => c.Illnesses)
            .WithOne(c => c.TreatmentCategory).HasForeignKey(c => c.TreatmentCategoryId);
    }
}
public class IllnessConfiguration : IEntityTypeConfiguration<Illness>
{
    public void Configure(EntityTypeBuilder<Illness> builder)
    {
        builder.ConfigureBaseEntity();

        builder.HasOne(i => i.TreatmentCategory)
               .WithMany(c => c.Illnesses)
               .HasForeignKey(i => i.TreatmentCategoryId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(i => i.Translations)
               .WithOne(t => t.Illness)
               .HasForeignKey(t => t.IllnessId)
               .OnDelete(DeleteBehavior.Cascade);
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
public class IllnessTranslationConfiguration : IEntityTypeConfiguration<IllnessTranslation>
{
    public void Configure(EntityTypeBuilder<IllnessTranslation> builder)
    {
        builder.ConfigureBaseEntity();

        builder.Property(t => t.Name).IsRequired();
        builder.Property(t => t.Description).IsRequired();
        builder.Property(t => t.Language)
               .HasConversion<string>()
               .IsRequired();

        builder.HasOne(t => t.Illness)
               .WithMany(i => i.Translations)
               .HasForeignKey(t => t.IllnessId);
    }
}