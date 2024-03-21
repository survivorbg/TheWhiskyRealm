using Microsoft.AspNetCore.Identity;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Infrastructure.Data.Configurations;

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

        if (await userManager.FindByNameAsync(adminUser.UserName) == null)
        {
            await userManager.CreateAsync(adminUser, "admin123");
            await userManager.AddToRoleAsync(adminUser, adminRole.Name);
        }

        var userOne = new ApplicationUser
        {
            UserName = "test@gmail.com",
            Email = "test@gmail.com",
            Age = 18
        };

        if (await userManager.FindByNameAsync(userOne.UserName) == null)
        {
            await userManager.CreateAsync(userOne, "test123");
            await userManager.AddToRoleAsync(userOne, userRole.Name);

        }

        var userTwo = new ApplicationUser
        {
            UserName = "sober@gmail.com",
            Email = "sober@gmail.com",
            Age = 24
        };

        if (await userManager.FindByNameAsync(userTwo.UserName) == null)
        {
            await userManager.CreateAsync(userTwo, "test123");
            await userManager.AddToRoleAsync(userTwo, userRole.Name);

        }

        var userThree = new ApplicationUser
        {
            UserName = "noToAlcohol@gmail.com",
            Email = "noToAlcohol@gmail.com",
            Age = 64
        };

        if (await userManager.FindByNameAsync(userThree.UserName) == null)
        {
            await userManager.CreateAsync(userThree, "test123");
            await userManager.AddToRoleAsync(userThree, userRole.Name);
        }
    }
}
