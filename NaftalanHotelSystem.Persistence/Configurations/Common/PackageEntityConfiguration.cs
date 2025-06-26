using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NaftalanHotelSystem.Domain.Entites;

namespace NaftalanHotelSystem.Persistence.Configurations.Common;

public class PackageEntityConfiguration : IEntityTypeConfiguration<Package>
{
    public void Configure(EntityTypeBuilder<Package> builder)
    {
        builder.ConfigureBaseEntity();

        builder.Property(e => e.Name).IsRequired();
        builder.Property(e => e.RoomType).IsRequired();
        builder.Property(e => e.DurationDay).IsRequired();
        builder.Property(e => e.Price).IsRequired();


        builder.HasMany(e => e.PackageTranslations).WithOne(s => s.Packages).HasForeignKey(s => s.PackageId);

        builder.HasMany(e => e.TreatmentMethods).WithMany(e => e.Packages);
    }
}
public class PackageTranslationEntityConfiguration : IEntityTypeConfiguration<PackageTranslation>
{
    public void Configure(EntityTypeBuilder<PackageTranslation> builder)
    {
        builder.ConfigureBaseEntity();

        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.Language).HasConversion<string>().IsRequired();

        builder.HasOne(rt => rt.Packages)
        .WithMany(r => r.PackageTranslations)
        .HasForeignKey(rt => rt.PackageId);
    }
}
