using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Infrastructure.Data;

public class TheWhiskyRealmDbContext : IdentityDbContext
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


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
