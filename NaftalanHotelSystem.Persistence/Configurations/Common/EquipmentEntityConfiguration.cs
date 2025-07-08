using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using NaftalanHotelSystem.Domain.Entites;

namespace NaftalanHotelSystem.Persistence.Configurations.Common;

public class EquipmentEntityConfiguration : IEntityTypeConfiguration<Equipment>
{
    public void Configure(EntityTypeBuilder<Equipment> builder)
    {
        builder.ConfigureBaseEntity();

        builder.HasMany(e => e.EquipmentTranslations)
               .WithOne(et => et.Equipment)
               .HasForeignKey(et => et.EquipmentId);

        builder.HasMany(e => e.RoomEquipments)
               .WithOne(re => re.Equipment)
               .HasForeignKey(re => re.EquipmentId);
    }
}
public class AboutEntityConfiguration : IEntityTypeConfiguration<About>
{
    public void Configure(EntityTypeBuilder<About> builder)
    {
        builder.ConfigureBaseEntity();

        builder.Property(x=>x.Title).IsRequired();
        builder.Property(x=>x.MiniTitle).IsRequired();
        builder.Property(x=>x.Description).IsRequired();
        builder.Property(x=>x.VideoLink).IsRequired();

    }
}
public class ContactEntityConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.ConfigureBaseEntity();

        builder.Property(x=>x.Number).IsRequired();
        builder.Property(x=>x.Adress).IsRequired();
        builder.Property(x=>x.Mail).IsRequired();
        builder.Property(x=>x.YoutubeLink).IsRequired();
        builder.Property(x=>x.FacebookLink).IsRequired();
        builder.Property(x=>x.TiktokLink).IsRequired();
        builder.Property(x=>x.WhatsappNumber).IsRequired();
        builder.Property(x=>x.InstagramLink).IsRequired();
    }
}
