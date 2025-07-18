using NaftalanHotelSystem.Persistence.DataAccessLayer;

namespace NaftalanHotelSystem.Persistence.SeedData;

public class SeederManager
{
    private readonly IEnumerable<ISeeder> _seeders;

    public SeederManager(IEnumerable<ISeeder> seeders)
    {
        _seeders = seeders;
    }

    public async Task SeedAsync(AppDbContext context)
    {
        foreach (var seeder in _seeders)
        {
            await seeder.SeedAsync(context);
        }
    }
}
