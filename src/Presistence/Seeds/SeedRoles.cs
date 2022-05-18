using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Presistence.Seeds
{
    public class SeedRoles
    {
        public static async Task Seed(RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in Role.AllRoles)
            {
                var roleExsts = await roleManager.RoleExistsAsync(role);
                if (!roleExsts)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}