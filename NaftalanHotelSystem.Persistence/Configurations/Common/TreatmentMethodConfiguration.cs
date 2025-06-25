using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NaftalanHotelSystem.Domain.Entites;

namespace NaftalanHotelSystem.Persistence.Configurations.Common;

public class TreatmentMethodConfiguration : IEntityTypeConfiguration<TreatmentMethod>
{
    public void Configure(EntityTypeBuilder<TreatmentMethod> builder)
    {
        builder.ConfigureBaseEntity();

        builder.HasMany(t => t.Translations)
            .WithOne(t => t.TreatmentMethod)
            .HasForeignKey(t => t.TreatmentMethodId);
    }
}
