using TheWhiskyRealm.Infrastructure.Data.Configurations;

namespace Microsoft.AspNetCore.Builder;

public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Seeds the user roles into the application's Identity.
    /// </summary>
    /// <param name="app">The application builder.</param>
    public async static Task SeedUserRoles(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<UserRoleSeeder>();
        await seeder.AssignRolesAsync();
    }
}
