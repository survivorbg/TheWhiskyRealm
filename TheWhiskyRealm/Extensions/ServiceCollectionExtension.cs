using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Infrastructure.Data;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection");

        services.AddDbContext<TheWhiskyRealmDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddDatabaseDeveloperPageExceptionFilter();

        return services;
    }

    public static IServiceCollection AddApplicationIdentity(this IServiceCollection services, IConfiguration config)
    {
        services.AddDefaultIdentity<IdentityUser>(options =>
            {

            })
            .AddEntityFrameworkStores<TheWhiskyRealmDbContext>();

        return services;
    }
}
