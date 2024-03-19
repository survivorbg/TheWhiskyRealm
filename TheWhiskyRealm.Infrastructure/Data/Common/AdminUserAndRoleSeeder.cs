using Microsoft.AspNetCore.Identity;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Infrastructure.Data.Common;

public class AdminUserAndRoleSeeder
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;

    public AdminUserAndRoleSeeder(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    public async Task SeedAsync()
    {
        var adminRole = new IdentityRole("Administrator");
        var userRole = new IdentityRole("User");

        if (!await roleManager.RoleExistsAsync(adminRole.Name))
        {
            await roleManager.CreateAsync(adminRole);
        }

        if (!await roleManager.RoleExistsAsync(userRole.Name))
        {
            await roleManager.CreateAsync(userRole);
        }

        var adminUser = new ApplicationUser
        {
            UserName = "admin@gmail.com",
            Email = "admin@gmail.com",
            Age = 29
        };

        var user = await userManager.FindByNameAsync(adminUser.UserName);

        if (user == null)
        {
            await userManager.CreateAsync(adminUser, "admin123");
            await userManager.AddToRoleAsync(adminUser, adminRole.Name);
        }

    }
}
