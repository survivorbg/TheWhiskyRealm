using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Infrastructure.Data;

public class TheWhiskyRealmDbContext : IdentityDbContext<ApplicationUser>
{
    public TheWhiskyRealmDbContext(DbContextOptions<TheWhiskyRealmDbContext> options)
        : base(options)
    {

    }

    public DbSet<Country> Countries { get; set; } = null!;
    public DbSet<Region> Regions { get; set; } = null!;
    public DbSet<Distillery> Distilleries { get; set; } = null!;
    public DbSet<Whisky> Whiskies { get; set; } = null!;
    public DbSet<WhiskyType> WhiskyTypes { get; set; } = null!;
    public DbSet<Award> Awards { get; set; } = null!;
    public DbSet<City> Cities { get; set; } = null!;
    public DbSet<Venue> Venues { get; set; } = null!;
    public DbSet<Event> Events { get; set; } = null!;
    public DbSet<UserEvent> UsersEvents { get; set; } = null!;
    public DbSet<Review> Reviews { get; set; } = null!;
    public DbSet<Rating> Ratings { get; set; } = null!;
    public DbSet<Article> Articles { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        Assembly cfg = Assembly.GetAssembly(typeof(TheWhiskyRealmDbContext)) ?? Assembly.GetExecutingAssembly();

        builder.ApplyConfigurationsFromAssembly(cfg);

        base.OnModelCreating(builder);
    }
}
