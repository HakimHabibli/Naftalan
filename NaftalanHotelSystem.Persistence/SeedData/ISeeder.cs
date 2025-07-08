using NaftalanHotelSystem.Persistence.DataAccessLayer;

namespace NaftalanHotelSystem.Persistence.SeedData;


public interface ISeeder
{
    public Task SeedAsync(AppDbContext context);
}
