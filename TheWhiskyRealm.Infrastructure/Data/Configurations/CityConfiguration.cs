using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Infrastructure.Data.Configurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasData(GenerateCities());
    }

    private City[] GenerateCities()
    {
        ICollection<City> cities = new HashSet<City>();

        cities.Add(new City
        {
            Id = 1,
            Name = "Sofia",
            ZipCode = "1000",
            CountryId = 11
        });

        cities.Add(new City
        {
            Id = 2,
            Name = "Plovdiv",
            ZipCode = "4000",
            CountryId = 11
        });

        cities.Add(new City
        {
            Id = 3,
            Name = "Varna",
            ZipCode = "9000",
            CountryId = 11
        });


        


        return cities.ToArray();
    }
}
