using Domain.Models;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Presistence.Seeds
{
    public class SeedUsers
    {
        public static async Task Seed(UserManager<ApplicationUser> userManager)
        {
            if (!await userManager.Users.AnyAsync())
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "Admin",
                    Email = "admin@email.com",
                    FirstName = "Admin",
                    LastName = "Admin"
                };

                await userManager.CreateAsync(adminUser, "Admin123!");
                await userManager.AddToRoleAsync(adminUser, Role.Administrator);
            }
        }
    }
}
