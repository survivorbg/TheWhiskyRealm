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
            Id = "f99c5e20-d91e-4a5e-9b73-fdb38b89ffc3",
            UserName = "admin@gmail.com",
            Email = "admin@gmail.com"
        };

        if (await userManager.FindByNameAsync(adminUser.UserName) == null)
        {
            await userManager.CreateAsync(adminUser, "admin123");
            await userManager.AddToRoleAsync(adminUser, adminRole.Name);
        }

        var userOne = new ApplicationUser
        {
            Id = "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4",
            UserName = "test@gmail.com",
            Email = "test@gmail.com"
        };

        if (await userManager.FindByNameAsync(userOne.UserName) == null)
        {
            await userManager.CreateAsync(userOne, "test123");
            await userManager.AddToRoleAsync(userOne, userRole.Name);

        }

        var userTwo = new ApplicationUser
        {
            Id = "7dfb241e-f8a5-4ba4-a5aa-5becf035c442",
            UserName = "sober@gmail.com",
            Email = "sober@gmail.com"
        };

        if (await userManager.FindByNameAsync(userTwo.UserName) == null)
        {
            await userManager.CreateAsync(userTwo, "test123");
            await userManager.AddToRoleAsync(userTwo, userRole.Name);

        }

        var userThree = new ApplicationUser
        {
            Id = "1cf4a321-6128-459e-8e4e-e4615c85d30f",
            UserName = "noToAlcohol@gmail.com",
            Email = "noToAlcohol@gmail.com"
        };

        if (await userManager.FindByNameAsync(userThree.UserName) == null)
        {
            await userManager.CreateAsync(userThree, "test123");
            await userManager.AddToRoleAsync(userThree, userRole.Name);
        }
    }
}
