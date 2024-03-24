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

        regions.Add(new Region { Id = 1, Name = "Lowland", CountryId = 1 });
        regions.Add(new Region { Id = 2, Name = "Highland", CountryId = 1 });
        regions.Add(new Region { Id = 3, Name = "Speyside", CountryId = 1 });
        regions.Add(new Region { Id = 4, Name = "Island", CountryId = 1 });
        regions.Add(new Region { Id = 5, Name = "Campbeltown", CountryId = 1 });
        regions.Add(new Region { Id = 6, Name = "Islay", CountryId = 1 });
        regions.Add(new Region { Id = 7, Name = "County Mayo", CountryId = 2 });
        regions.Add(new Region { Id = 8, Name = "County Kilkenny", CountryId = 2 });
        regions.Add(new Region { Id = 9, Name = "County Donegal", CountryId = 2 });
        regions.Add(new Region { Id = 10, Name = "County Waterford", CountryId = 2 });
        regions.Add(new Region { Id = 11, Name = "County Meath", CountryId = 2 });
        regions.Add(new Region { Id = 12, Name = "County Fermanagh", CountryId = 2 });
        regions.Add(new Region { Id = 13, Name = "County Clare", CountryId = 2 });
        regions.Add(new Region { Id = 14, Name = "County Cork", CountryId = 2 });
        regions.Add(new Region { Id = 15, Name = "County Louth", CountryId = 2 });
        regions.Add(new Region { Id = 16, Name = "County Down", CountryId = 2 });
        regions.Add(new Region { Id = 17, Name = "County Kerry", CountryId = 2 });
        regions.Add(new Region { Id = 18, Name = "County Wicklow", CountryId = 2 });
        regions.Add(new Region { Id = 19, Name = "County Westmeath", CountryId = 2 });
        regions.Add(new Region { Id = 20, Name = "County Sligo", CountryId = 2 });
        regions.Add(new Region { Id = 21, Name = "County Antrim", CountryId = 2 });
        regions.Add(new Region { Id = 22, Name = "County Carlow", CountryId = 2 });
        regions.Add(new Region { Id = 23, Name = "County Leitrim", CountryId = 2 });
        regions.Add(new Region { Id = 24, Name = "County Tipperary", CountryId = 2 });
        regions.Add(new Region { Id = 25, Name = "County Offaly", CountryId = 2 });
        regions.Add(new Region { Id = 26, Name = "Kentucky", CountryId = 3 });
        regions.Add(new Region { Id = 27, Name = "Tennessee", CountryId = 3 });
        regions.Add(new Region { Id = 28, Name = "Saitama", CountryId = 4 });
        regions.Add(new Region { Id = 29, Name = "Hyogo", CountryId = 4 });
        regions.Add(new Region { Id = 30, Name = "Shizuoka", CountryId = 4 });
        regions.Add(new Region { Id = 31, Name = "Yamanashi", CountryId = 4 });
        regions.Add(new Region { Id = 32, Name = "Nagano", CountryId = 4 });
        regions.Add(new Region { Id = 33, Name = "Miyagi", CountryId = 4 });
        regions.Add(new Region { Id = 34, Name = "Osaka", CountryId = 4 });
        regions.Add(new Region { Id = 35, Name = "Hokkaido", CountryId = 4 });
        regions.Add(new Region { Id = 36, Name = "Yilan County", CountryId = 5 });
        regions.Add(new Region { Id = 37, Name = "Karnataka", CountryId = 6 });
        regions.Add(new Region { Id = 38, Name = "Ontario", CountryId = 7 });


        return regions.ToArray();
    }
}
