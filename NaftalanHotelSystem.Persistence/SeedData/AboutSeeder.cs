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
            var about = new About
            {
                VideoLink = "Test",
           
                AboutTranslations = new List<AboutTranslation>
                {
                    new AboutTranslation
                    {
                        Title = "Haqqımızda",
                        MiniTitle = "Naftalan Hotel",
                        Description = "Biz 2020-ci ildən fəaliyyət göstəririk.",
                        Language = Domain.Enums.Language.Az
                    },
                    new AboutTranslation
                    {
                        Title = "About Us",
                        MiniTitle = "Naftalan Hotel",
                        Description = "We have been operating since 2020.",
                        Language = Domain.Enums.Language.En
                    },
                       new AboutTranslation
                    {
                        Title = "О нас",
                        MiniTitle = "Нафталан Отель",
                        Description = "Мы - лечебный отель, расположенный в Нафталане.",
                        Language = Domain.Enums.Language.Ru
                    }

                }
            };

            await context.Abouts.AddAsync(about);
            await context.SaveChangesAsync();
        }
    }
}
