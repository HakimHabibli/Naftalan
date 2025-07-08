using Microsoft.EntityFrameworkCore;
using NaftalanHotelSystem.Domain.Entites;
using NaftalanHotelSystem.Persistence.DataAccessLayer;

namespace NaftalanHotelSystem.Persistence.SeedData;

public class ContactSeeder : ISeeder
{
    public async Task SeedAsync(AppDbContext context)
    {
        if (!await context.Contacts.AnyAsync())
        {
            await context.Contacts.AddAsync(new Contact
            {
                Number = new List<string> { "+994", "+994" },
                Adress ="asdasd",
                FacebookLink ="asdasd",
                InstagramLink ="sadad",
                Mail ="asdads",
                TiktokLink ="sadasd",
                WhatsappNumber = "info@example.com",
                YoutubeLink = "sadsa"
            });

            await context.SaveChangesAsync();
        }
    }
}
