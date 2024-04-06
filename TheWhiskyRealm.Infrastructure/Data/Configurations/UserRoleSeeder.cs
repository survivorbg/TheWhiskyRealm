using Microsoft.AspNetCore.Identity;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Infrastructure.Data.Configurations;

public class UserRoleSeeder
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;

    public UserRoleSeeder(UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    public async Task AssignRolesAsync()
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

        var adminUser = await userManager.FindByNameAsync("admin@gmail.com");
        if (adminUser != null)
        {
            await userManager.AddToRoleAsync(adminUser, adminRole.Name);
        }

        var userOne = await userManager.FindByNameAsync("test@gmail.com");
        if (userOne != null)
        {
            await userManager.AddToRoleAsync(userOne, userRole.Name);
        }

        var userTwo = await userManager.FindByNameAsync("sober@gmail.com");
        if (userTwo != null)
        {
            await userManager.AddToRoleAsync(userTwo, userRole.Name);
        }

        var userThree = await userManager.FindByNameAsync("noToAlcohol@gmail.com");
        if (userThree != null)
        {
            await userManager.AddToRoleAsync(userThree, userRole.Name);
        }
    }
}