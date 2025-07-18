using Microsoft.AspNetCore.Identity;
using NaftalanHotelSystem.Domain.Entites;

namespace NaftalanHotelSystem.Persistence.SeedData;

public static class IdentitySeeder 
{
    
    public static async Task SeedAdminUserAsync(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        Console.WriteLine("--- Identity Seeding Started ---");

        // 1. "Admin" rolunu yarat (yalnız mövcud deyilsə)
        string adminRoleName = "Admin";
        if (!await roleManager.RoleExistsAsync(adminRoleName))
        {
            await roleManager.CreateAsync(new IdentityRole(adminRoleName));
            Console.WriteLine($"Role '{adminRoleName}' created successfully.");
        }
        else
        {
            Console.WriteLine($"Role '{adminRoleName}' already exists.");
        }

     
        string adminEmail1 = "admin1@naftalan.com";
        var adminUser1 = await userManager.FindByEmailAsync(adminEmail1);
        if (adminUser1 == null)
        {
            adminUser1 = new ApplicationUser
            {
                UserName = adminEmail1, 
                Email = adminEmail1,
                FirstName = "System",
                LastName = "Admin1",
                EmailConfirmed = true 
            };
           
            var result = await userManager.CreateAsync(adminUser1, "AdminPass123!"); 
            if (result.Succeeded)
            {
                
                await userManager.AddToRoleAsync(adminUser1, adminRoleName);
                Console.WriteLine($"Admin user '{adminEmail1}' created and assigned to '{adminRoleName}' role.");
            }
            else
            {
                Console.WriteLine($"Error creating admin user '{adminEmail1}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
        else
        {
            Console.WriteLine($"Admin user '{adminEmail1}' already exists.");
           
            if (!await userManager.IsInRoleAsync(adminUser1, adminRoleName))
            {
                await userManager.AddToRoleAsync(adminUser1, adminRoleName);
                Console.WriteLine($"Admin user '{adminEmail1}' assigned to '{adminRoleName}' role (was missing).");
            }
        }

       
        string adminEmail2 = "admin2@naftalan.com";
        var adminUser2 = await userManager.FindByEmailAsync(adminEmail2);
        if (adminUser2 == null)
        {
            adminUser2 = new ApplicationUser
            {
                UserName = adminEmail2,
                Email = adminEmail2,
                FirstName = "System",
                LastName = "Admin2",
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(adminUser2, "AdminPass456@");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser2, adminRoleName);
                Console.WriteLine($"Admin user '{adminEmail2}' created and assigned to '{adminRoleName}' role.");
            }
            else
            {
                Console.WriteLine($"Error creating admin user '{adminEmail2}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
        else
        {
            Console.WriteLine($"Admin user '{adminEmail2}' already exists.");
            if (!await userManager.IsInRoleAsync(adminUser2, adminRoleName))
            {
                await userManager.AddToRoleAsync(adminUser2, adminRoleName);
                Console.WriteLine($"Admin user '{adminEmail2}' assigned to '{adminRoleName}' role (was missing).");
            }
        }
       
    }
}