using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Infrastructure.Data;

public class TheWhiskyRealmDbContext : IdentityDbContext
{
    public TheWhiskyRealmDbContext(DbContextOptions<TheWhiskyRealmDbContext> options)
        : base(options)
    {

    }

    public DbSet<Whisky> Whiskies { get; set; } = null!;
    public DbSet<Distillery> Distilleries { get; set; } = null!;
    public DbSet<Region> Regions { get; set; } = null!;
    public DbSet<Country> Countries { get; set; } = null!;
    public DbSet<Review> Reviews { get; set; } = null!;
    public DbSet<Rating> Ratings { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //builder.Entity<Distillery>()
        //.HasCheckConstraint("CK_Year_Min", "[YearFounded] >= 1500")
        //.HasCheckConstraint("CK_Year_Max", "[YearFounded] <= 2024");

        base.OnModelCreating(builder);
    }
}
