using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
            Id = 1,
            Title = "Whisky Tasting Evening",
            Description = "Join us for an evening of whisky tasting and discovery.",
            OrganiserId = "7dfb241e-f8a5-4ba4-a5aa-5becf035c442",
            StartDate = new DateTime(2024, 3, 25, 18, 0, 0),
            EndDate = new DateTime(2024, 3, 25, 21, 0, 0),
            Price = 25.99m,
            VenueId = 5,
            AvailableSpots = 1
        });

        events.Add(new Event
        {
            Id = 2,
            Title = "Whisky Fest Plovdiv",
            Description = "Time for whiskey!\r\n\r\nThe 10th anniversary edition of Whisky Fest Sofia will feature over 35 whiskies, rums, and brandies at the stand. Over 250 different brands will be available for tasting over three days.\r\n\r\nWe'll turn back the hands of time to journey through the various years of whisky history from brands originating from Scotland, Ireland, America, Japan, Taiwan, France, Wales, Sweden, and over 10 other countries.",
            OrganiserId = "7dfb241e-f8a5-4ba4-a5aa-5becf035c442",
            StartDate = new DateTime(2024, 5, 25, 18, 0, 0),
            EndDate = new DateTime(2024, 5, 27, 21, 0, 0),
            VenueId = 5,
            AvailableSpots = 2
        });

        events.Add(new Event
        {
            Id = 3,
            Title = "Whisky Show Sofia",
            Description = "Whisky Show Bulgaria is the only festival event in Bulgaria and the region that is totally focused on pur favorite aged spirit – the Whisky. Once a year and in three days only the Whisky meets the local whisky community of enthusiasts, fans, aficionados, collectors, specialists, journalist, bar and restaurant owners, bloggers and whisky lovers. Whisky Show Bulgaria is an event created by and for whisky enthusiasts. A show where hundreds of exceptional whiskies that are usually described as special, independent, family-owned, exotic, limited, rare, old, single cask, small batch, produced by ghost distilleries, are to be tasted.",
            OrganiserId = "7dfb241e-f8a5-4ba4-a5aa-5becf035c442",
            StartDate = new DateTime(2024, 6, 3, 18, 0, 0),
            EndDate = new DateTime(2024, 6, 3, 21, 0, 0),
            Price = 15.00m,
            VenueId = 1,
            AvailableSpots = 7
        });

        events.Add(new Event
        {
            Id = 4,
            Title = "Irish Whiskey of things",
            Description = "The old & rare selection will be quite large and pleasing, thanks to the support of local collectors and partnering whisky bars like Caldo and Local.",
            OrganiserId = "7dfb241e-f8a5-4ba4-a5aa-5becf035c442",
            StartDate = new DateTime(2024, 5, 23, 16, 0, 0),
            EndDate = new DateTime(2024, 5, 23, 21, 0, 0),
            Price = 35.00m,
            VenueId = 2,
            AvailableSpots = 6
        });

        events.Add(new Event
        {
            Id = 5,
            Title = "Kavalan Masterclass",
            Description = "You will be able to taste Kavalan Solist Sherry, Kaval Solist Ex-Bourbon and Kavalan Concertmaster!",
            OrganiserId = "7dfb241e-f8a5-4ba4-a5aa-5becf035c442",
            StartDate = new DateTime(2024, 3, 23, 16, 0, 0),
            EndDate = new DateTime(2024, 3, 23, 21, 0, 0),
            Price = 20.00m,
            VenueId = 2,
            AvailableSpots = 6
        });

        return events.ToArray();
    }
}
