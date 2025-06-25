using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NaftalanHotelSystem.Domain.Common;

namespace NaftalanHotelSystem.Persistence.Configurations.Common;

public static class BaseEntityConfiguration
{
    public static void ConfigureBaseEntity<T>(this EntityTypeBuilder<T> builder) where T : BaseEntity
    {
        builder.HasKey(x => x.Id);
    }
}
