using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Services;
using TheWhiskyRealm.Infrastructure.Data;
using TheWhiskyRealm.Infrastructure.Data.Common;
using TheWhiskyRealm.Infrastructure.Data.Configurations;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddScoped<IWhiskyService, WhiskyService>();
        services.AddScoped<IRatingService, RatingService>();
        services.AddScoped<IWhiskyTypeService, WhiskyTypeService>();
        services.AddScoped<IRegionService, RegionService>();
        services.AddScoped<IDistilleryService, DistilleryService>();

        return services;
    }

    public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection");

        services.AddDbContext<TheWhiskyRealmDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddDatabaseDeveloperPageExceptionFilter();
        services.AddScoped<IRepository, Repository>();
        return services;
    }

    public static IServiceCollection AddApplicationIdentity(this IServiceCollection services, IConfiguration config)
    {
        services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<TheWhiskyRealmDbContext>();

        services.AddScoped<AdminUserAndRoleSeeder>();

        return services;
    }

    public static void SeedUserData(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<AdminUserAndRoleSeeder>();
        seeder.SeedAsync().Wait();
    }
}
