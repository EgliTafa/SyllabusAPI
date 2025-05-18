using Microsoft.AspNetCore.Identity;
using Syllabus.Domain.Users;

namespace Syllabus.Infrastructure.Data;

public static class RoleSeeder
{
    public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        foreach (UserRole role in Enum.GetValues(typeof(UserRole)))
        {
            string roleName = role.ToString();
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
} 