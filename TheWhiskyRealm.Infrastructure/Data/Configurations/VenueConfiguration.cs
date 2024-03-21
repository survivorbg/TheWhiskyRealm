using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheWhiskyRealm.Infrastructure.Data.Models;
using static TheWhiskyRealm.Infrastructure.Constants.VenueDataConstants;

namespace TheWhiskyRealm.Infrastructure.Data.Configurations;

public class VenueConfiguration : IEntityTypeConfiguration<Venue>
{
    public void Configure(EntityTypeBuilder<Venue> builder)
    {
        builder
            .HasCheckConstraint("CK_Capacity_Min", $"[Capacity] >= {MinCapacity}")
            .HasCheckConstraint("CK_Capacity_Max", $"[Capacity] <= {MaxCapacity}");

        builder.HasData(GenerateVenues());
    }

    private Venue[] GenerateVenues()
    {
        ICollection<Venue> venues = new HashSet<Venue>();

        venues.Add(new Venue
        {
            Id = 1,
            Name = "Masterpiece Whisky Bar",
            CityId = 1,
            Capacity = 7,
        });

        venues.Add(new Venue
        {
            Id = 2,
            Name = "Bar Caldo",
            CityId = 1,
            Capacity = 6,
        });

        venues.Add(new Venue
        {
            Id = 4,
            Name = "Hotel Marinela",
            CityId = 1,
            Capacity = 100,
        });

        venues.Add(new Venue
        {
            Id = 5,
            Name = "Bar Sandaka",
            CityId = 2,
            Capacity = 3,
        });

        venues.Add(new Venue
        {
            Id = 6,
            Name = "The Whisky Library",
            CityId = 2,
            Capacity = 10,
        });

        venues.Add(new Venue
        {
            Id = 7,
            Name = "Hotel Imperial",
            CityId = 2,
            Capacity = 40,
        });

        venues.Add(new Venue
        {
            Id = 8,
            Name = "Tasting Room",
            CityId = 3,
            Capacity = 10,
        });

        return venues.ToArray();
    }
}
