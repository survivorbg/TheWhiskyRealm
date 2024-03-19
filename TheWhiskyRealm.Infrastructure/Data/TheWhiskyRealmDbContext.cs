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


    protected override void OnModelCreating(ModelBuilder builder)
    {
        //Event configurations
        builder.Entity<Event>()
            .Property(e => e.Price)
            .HasPrecision(18, 2);

        //UserEvent configuration
        builder.Entity<UserEvent>()
            .HasKey(ue => new { ue.EventId, ue.UserId });

        builder.Entity<UserEvent>()
            .HasOne(ue => ue.Event)
            .WithMany(e => e.UsersEvents)
            .HasForeignKey(ue => ue.EventId)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(builder);
    }
}
