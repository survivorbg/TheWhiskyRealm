using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Infrastructure.Data.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder
            .Property(e => e.Price)
            .HasPrecision(18, 2);

        builder
            .HasCheckConstraint("CK_Event_StartDate", "[StartDate] < [EndDate]");

        builder.
            HasCheckConstraint("CK_Event_EndDate", "[EndDate] > [StartDate]");

        builder.HasData(GenerateEvents());
    }

    private Event[] GenerateEvents()
    {
        ICollection<Event> events = new HashSet<Event>();

        events.Add(new Event
        {
            Id =1,
            Title = "Whisky Tasting Evening",
            Description = "Join us for an evening of whisky tasting and discovery.",
            OrganiserId = "a8909756-a101-47c5-8d52-085322ffa6e6",
            StartDate = new DateTime(2024, 3, 25, 18, 0, 0),
            EndDate = new DateTime(2024, 3, 25, 21, 0, 0),
            DurationInHours = 3,
            Price = 25.99m,
            VenueId = 1
        });

        return events.ToArray();
    }
}
