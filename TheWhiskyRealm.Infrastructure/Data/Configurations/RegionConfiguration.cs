using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Infrastructure.Data.Configurations;

public class RegionConfiguration : IEntityTypeConfiguration<Region>
{
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        builder.HasData(GenerateRegions());
    }
    private Region[] GenerateRegions()
    {
        ICollection<Region> regions = new HashSet<Region>();

        regions.Add(new Region
        {
            Id = 1,
            Name = "Lowland",
            CountryId = 1,
        });

        regions.Add(new Region
        {
            Id = 2,
            Name = "Highland",
            CountryId = 1,
        });

        regions.Add(new Region
        {
            Id = 3,
            Name = "Speyside",
            CountryId = 1,
        });

        regions.Add(new Region
        {
            Id = 4,
            Name = "Islands",
            CountryId = 1,
        });

        regions.Add(new Region
        {
            Id = 5,
            Name = "Campbeltown",
            CountryId = 1,
        });

        regions.Add(new Region
        {
            Id = 6,
            Name = "Islay",
            CountryId = 1,
        });

        regions.Add(new Region
        {
            Id = 7,
            Name = "Antrim County",
            CountryId = 2,
        });

        regions.Add(new Region
        {
            Id = 8,
            Name = "Cork County ",
            CountryId = 2,
        });

        regions.Add(new Region
        {
            Id = 9,
            Name = "Dublin County ",
            CountryId = 2,
        });
        
        regions.Add(new Region
        {
            Id = 10,
            Name = "Offaly County ",
            CountryId = 2,
        });
        regions.Add(new Region
        {
            Id = 11,
            Name = "Kentucky",
            CountryId = 3,
        });

        regions.Add(new Region
        {
            Id = 12,
            Name = "Tennessee",
            CountryId = 3,
        });

        regions.Add(new Region
        {
            Id = 13,
            Name = "Osaka Prefecture",
            CountryId = 4,
        });

        regions.Add(new Region
        {
            Id = 14,
            Name = "Yilan County",
            CountryId = 5,
        });

        regions.Add(new Region
        {
            Id = 15,
            Name = "Karnataka",
            CountryId = 6,
        });

        regions.Add(new Region
        {
            Id = 16,
            Name = "Ontario",
            CountryId = 7,
        });

        return regions.ToArray();
    }
}
