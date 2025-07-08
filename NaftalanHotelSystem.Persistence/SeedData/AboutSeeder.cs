using Microsoft.EntityFrameworkCore;
using NaftalanHotelSystem.Domain.Entites;
using NaftalanHotelSystem.Persistence.DataAccessLayer;

namespace NaftalanHotelSystem.Persistence.SeedData;

public class AboutSeeder : ISeeder
{
    public async Task SeedAsync(AppDbContext context)
    {
        if (!await context.Abouts.AnyAsync())
        {
            await context.Abouts.AddAsync(new About
            {
                Title = "Haqqımızda",
                Description = "Biz 2020-ci ildən fəaliyyət göstəririk.",
                MiniTitle = "Title",
                VideoLink ="asdad"
            });

            await context.SaveChangesAsync();
        }
    }
}
