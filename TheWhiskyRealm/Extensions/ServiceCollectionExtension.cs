using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Services;
using TheWhiskyRealm.Infrastructure.Data;
using TheWhiskyRealm.Infrastructure.Data.Common;
using TheWhiskyRealm.Infrastructure.Data.Configurations;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace Microsoft.Extensions.DependencyInjection;


/// <summary>
/// Provides extension methods for IServiceCollection to add application services, DbContext and Identity.
/// </summary>
public static class ServiceCollectionExtension
{

    /// <summary>
    /// Adds application services to the IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    /// <returns>The same service collection so that multiple calls can be chained.</returns>
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddScoped<IWhiskyService, WhiskyService>();
        services.AddScoped<IRatingService, RatingService>();
        services.AddScoped<IWhiskyTypeService, WhiskyTypeService>();
        services.AddScoped<IRegionService, RegionService>();
        services.AddScoped<IDistilleryService, DistilleryService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<IAwardService, AwardService>();
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<IVenueService, VenueService>();
        services.AddScoped<IArticleService, ArticleService>();
        services.AddScoped<ICommentService, CommentService>();

        return services;
    }


    /// <summary>
    /// Adds the application's DbContext to the IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the DbContext to.</param>
    /// <param name="config">The application configuration, which includes the connection string.</param>
    /// <returns>The same service collection so that multiple calls can be chained.</returns>
    public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection");

        services.AddDbContext<TheWhiskyRealmDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddDatabaseDeveloperPageExceptionFilter();
        services.AddScoped<IRepository, Repository>();
        return services;
    }

    /// <summary>
    /// Adds the application's Identity to the IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the Identity to.</param>
    /// <param name="config">The application configuration, which includes the Identity settings.</param>
    /// <returns>The same service collection so that multiple calls can be chained.</returns>
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
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<TheWhiskyRealmDbContext>();

        return services;
    }
}
