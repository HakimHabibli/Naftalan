using System.Reflection;
using System.Text.Json;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NaftalanHotelSystem.Domain.Entites;
using NaftalanHotelSystem.Persistence.Configurations.Common;

namespace NaftalanHotelSystem.Persistence.DataAccessLayer;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Room> Rooms { get; set; }
    public DbSet<Equipment> Equipments { get; set; }
    public DbSet<RoomEquipment> RoomEquipments { get; set; }
    public DbSet<RoomTranslation> RoomTranslations { get; set; }
    public DbSet<EquipmentTranslation> EquipmentTranslations { get; set; }
    public DbSet<Child> Children { get; set; }
    public DbSet<RoomChild> RoomChildren { get; set; }
    public DbSet<RoomPriceByOccupancy> RoomPricesByOccupancy { get; set; }

    public DbSet<TreatmentMethod> TreatmentMethods { get; set; }
    public DbSet<TreatmentMethodTranslation> TreatmentMethodTranslations { get; set; }

    public DbSet<TreatmentCategory> TreatmentCategories { get; set; }
    public DbSet<TreatmentCategoryTranslation> TreatmentCategoryTranslations { get; set; }

    public DbSet<Illness> Illnesses { get; set; }
    public DbSet<IllnessTranslation> IllnessesTranslation { get; set; }


    public DbSet<About> Abouts { get; set; }
    public DbSet<AboutTranslation> AboutTranslations { get; set; }
    public DbSet<Contact> Contacts { get; set; }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public DbSet<Package> Packages { get; set; }
    public DbSet<PackageTranslation> PackageTranslations { get; set; }

    public DbSet<Image> Images { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
       
        var stringListConverter = new ValueConverter<List<string>, string>
            (   
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null)
            );

        modelBuilder.Entity<Contact>()
            .Property(c => c.Number)
            .HasConversion(stringListConverter);

        base.OnModelCreating(modelBuilder);
    }
   
}
