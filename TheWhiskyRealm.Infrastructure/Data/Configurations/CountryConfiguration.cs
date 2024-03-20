using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Infrastructure.Data.Configurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasData(GenerateCountries());
    }

    private Country[] GenerateCountries()
    {
        ICollection<Country> countries = new HashSet<Country>();

        countries.Add(new Country
        {
            Id = 1,
            Name = "Scotland"
        });

        countries.Add(new Country
        {
            Id = 2,
            Name = "Ireland"
        });

        countries.Add(new Country
        {
            Id = 3,
            Name = "United States"
        });

        countries.Add(new Country
        {
            Id = 4,
            Name = "Japan"
        });

        countries.Add(new Country
        {
            Id = 5,
            Name = "Taiwan"
        });

        countries.Add(new Country
        {
            Id = 6,
            Name = "India"
        });

        countries.Add(new Country
        {
            Id = 7,
            Name = "Canada"
        });
        countries.Add(new Country
        {
            Id = 8,
            Name = "Germany"
        });
        countries.Add(new Country
        {
            Id = 9,
            Name = "Finland"
        });
        countries.Add(new Country
        {
            Id = 10,
            Name = "Australia"
        });
        countries.Add(new Country
        {
            Id = 11,
            Name = "Bulgaria"
        });

        return countries.ToArray();
    }
}
