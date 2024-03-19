using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Infrastructure.Data.Models;
using static TheWhiskyRealm.Infrastructure.Constants.RatingDataConstants;

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

        //Rating configuration
        builder.Entity<Rating>()
       .HasCheckConstraint("CK_Nose_Min", $"[Nose] >= {MinNoseValue}")
       .HasCheckConstraint("CK_Nose_Max", $"[Nose] <= {MaxNoseValue}")
       .HasCheckConstraint("CK_Taste_Min", $"[Taste] >= {MinTasteValue}")
       .HasCheckConstraint("CK_Taste_Max", $"[Taste] <= {MaxTasteValue}")
       .HasCheckConstraint("CK_Finish_Min", $"[Finish] >= {MinFinishValue}")
       .HasCheckConstraint("CK_Finish_Max", $"[Finish] <= {MaxFinishValue}");

        //Comment configuration

        builder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c=>c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        //Application User
        builder.Entity<ApplicationUser>()
            .HasCheckConstraint("CK_Age_Min", "[Age] >= 18")
            .HasCheckConstraint("CK_Age_Max", "[Age] <= 119");

        base.OnModelCreating(builder);
    }
}
