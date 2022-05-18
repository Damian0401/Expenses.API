using Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace Presistence.Seeds
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            await SeedRoles.Seed(roleManager);
            await SeedUsers.Seed(userManager);
        }
    }
}