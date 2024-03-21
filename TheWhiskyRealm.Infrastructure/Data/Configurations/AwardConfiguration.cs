using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheWhiskyRealm.Infrastructure.Data.Models;
using static TheWhiskyRealm.Infrastructure.Constants.AwardDataConstants;

namespace TheWhiskyRealm.Infrastructure.Data.Configurations;

public class AwardConfiguration : IEntityTypeConfiguration<Award>
{
    public void Configure(EntityTypeBuilder<Award> builder)
    {
        builder
            .HasCheckConstraint("CK_Year_Min", $"[Year] >= {MinYearValue}")
            .HasCheckConstraint("CK_Year_Max", $"[Year] <= {MaxYearValue}");

        builder.HasData(GenerateAwards());
    }

    private Award[] GenerateAwards()
    {
        ICollection<Award> awards = new HashSet<Award>();

        awards.Add(new Award
        {
            Id = 1,
            Title = "Distillers' Single Malts 12 years and under",
            AwardsCeremony = "International Spirits Challenge",
            MedalType = Enums.MedalType.Gold,
            Year = 2020,
            WhiskyId = 1
        });

        awards.Add(new Award
        {
            Id = 2,
            Title = "Scotch Single Malt - Highland",
            AwardsCeremony = "International Wine & Spirit Competition",
            MedalType = Enums.MedalType.Gold,
            Year = 2017,
            WhiskyId = 2
        });

        awards.Add(new Award
        {
            Id = 3,
            Title = "Scotch Single Malt - Highland",
            AwardsCeremony = "International Wine & Spirit Competition",
            MedalType = Enums.MedalType.Gold,
            Year = 2014,
            WhiskyId = 2
        });

        awards.Add(new Award
        {
            Id = 9,
            Title = "Single Malt Scotch - to 12 Yrs",
            AwardsCeremony = "San Francisco World Spirits Competition",
            MedalType = Enums.MedalType.Gold,
            Year = 2022,
            WhiskyId = 4
        });

        awards.Add(new Award
        {
            Id = 10,
            Title = "Single Malt Under 12 Year Old",
            AwardsCeremony = "Scottish Whisky Awards",
            MedalType = Enums.MedalType.Gold,
            Year = 2019,
            WhiskyId = 4
        });

        awards.Add(new Award
        {
            Id = 11,
            Title = "Scotch Single Malt - Islay",
            AwardsCeremony = "International Wine & Spirit Competition",
            MedalType = Enums.MedalType.Gold,
            Year = 2019,
            WhiskyId = 4
        });

        awards.Add(new Award
        {
            Id = 12,
            Title = "Scotch Single Malt - Islay",
            AwardsCeremony = "International Wine & Spirit Competition",
            MedalType = Enums.MedalType.Silver,
            Year = 2017,
            WhiskyId = 4
        });

        awards.Add(new Award
        {
            Id = 13,
            Title = "Daily Dram",
            AwardsCeremony = "Malt Maniacs Awards",
            MedalType = Enums.MedalType.Bronze,
            Year = 2014,
            WhiskyId = 5
        });

        awards.Add(new Award
        {
            Id = 14,
            Title = "Single Malt Scotch - 13 to 19 Yrs",
            AwardsCeremony = "San Francisco World Spirits Competition",
            MedalType = Enums.MedalType.Gold,
            Year = 2022,
            WhiskyId = 6
        });

        awards.Add(new Award
        {
            Id = 15,
            Title = "Distillers' Single Malts between 13 and 20 years old",
            AwardsCeremony = "International Spirits Challenge",
            MedalType = Enums.MedalType.Gold,
            Year = 2020,
            WhiskyId = 6
        });

        awards.Add(new Award
        {
            Id = 16,
            Title = "Highlands & Islands 13-18yo",
            AwardsCeremony = "The Scotch Whisky Masters (The Spirits Business)",
            MedalType = Enums.MedalType.Gold,
            Year = 2020,
            WhiskyId = 6
        });

        awards.Add(new Award
        {
            Id = 17,
            Title = "Highlands & Islands 13-18yo",
            AwardsCeremony = "The Scotch Whisky Masters (The Spirits Business)",
            MedalType = Enums.MedalType.Gold,
            Year = 2019,
            WhiskyId = 6
        });

        awards.Add(new Award
        {
            Id = 18,
            Title = "Highlands & Islands 13-18yo",
            AwardsCeremony = "The Scotch Whisky Masters (The Spirits Business)",
            MedalType = Enums.MedalType.Gold,
            Year = 2018,
            WhiskyId = 6
        });

        awards.Add(new Award
        {
            Id = 19,
            Title = "Distillers' Single Malts between 13 and 20 years old",
            AwardsCeremony = "International Spirits Challenge",
            MedalType = Enums.MedalType.Silver,
            Year = 2019,
            WhiskyId = 6
        });

        awards.Add(new Award
        {
            Id = 20,
            Title = "Single Malt 17-20 Year Old",
            AwardsCeremony = "Scottish Whisky Awards",
            MedalType = Enums.MedalType.Bronze,
            Year = 2019,
            WhiskyId = 6
        });

        awards.Add(new Award
        {
            Id = 21,
            Title = "Irish Blended - Standard",
            AwardsCeremony = "The Irish Whisky Masters (The Spirits Business)",
            MedalType = Enums.MedalType.Gold,
            Year = 2013,
            WhiskyId = 7
        });

        awards.Add(new Award
        {
            Id = 22,
            Title = "Blended Malted Irish Whiskey",
            AwardsCeremony = "San Francisco World Spirits Competition",
            MedalType = Enums.MedalType.Gold,
            Year = 2013,
            WhiskyId = 7
        });

        awards.Add(new Award
        {
            Id = 23,
            Title = "Irish Blended - Premium",
            AwardsCeremony = "The Irish Whisky Masters (The Spirits Business)",
            MedalType = Enums.MedalType.Silver,
            Year = 2014,
            WhiskyId = 7
        });

        awards.Add(new Award
        {
            Id = 24,
            Title = "Irish Whiskey",
            AwardsCeremony = "Wizards of Whisky Awards",
            MedalType = Enums.MedalType.Bronze,
            Year = 2014,
            WhiskyId = 7
        });

        awards.Add(new Award
        {
            Id = 25,
            Title = "Irish Blend of the Year",
            AwardsCeremony = "Jim Murray's Whisky Bible",
            MedalType = Enums.MedalType.Gold,
            Year = 2014,
            WhiskyId = 8
        });

        awards.Add(new Award
        {
            Id = 26,
            Title = "Irish Blend of the Year",
            AwardsCeremony = "Jim Murray's Whisky Bible",
            MedalType = Enums.MedalType.Gold,
            Year = 2013,
            WhiskyId = 8
        });

        awards.Add(new Award
        {
            Id = 27,
            Title = "Irish Blended - Premium",
            AwardsCeremony = "The Irish Whisky Masters (The Spirits Business)",
            MedalType = Enums.MedalType.Gold,
            Year = 2020,
            WhiskyId = 8
        });

        awards.Add(new Award
        {
            Id = 28,
            Title = "Irish Blended - Premium",
            AwardsCeremony = "The Irish Whisky Masters (The Spirits Business)",
            MedalType = Enums.MedalType.Gold,
            Year = 2018,
            WhiskyId = 8
        });

        awards.Add(new Award
        {
            Id = 29,
            Title = "Irish Blended - Standard",
            AwardsCeremony = "The Irish Whisky Masters (The Spirits Business)",
            MedalType = Enums.MedalType.Silver,
            Year = 2019,
            WhiskyId = 8
        });

        awards.Add(new Award
        {
            Id = 30,
            Title = "Irish Blended - Standard",
            AwardsCeremony = "The Irish Whisky Masters (The Spirits Business)",
            MedalType = Enums.MedalType.Silver,
            Year = 2017,
            WhiskyId = 8
        });

        awards.Add(new Award
        {
            Id = 31,
            Title = "Irish Blended - Premium",
            AwardsCeremony = "The Irish Whisky Masters (The Spirits Business)",
            MedalType = Enums.MedalType.Silver,
            Year = 2013,
            WhiskyId = 8
        });

        awards.Add(new Award
        {
            Id = 32,
            Title = "Irish Whiskey - Blended",
            AwardsCeremony = "International Wine & Spirit Competition",
            MedalType = Enums.MedalType.Silver,
            Year = 2014,
            WhiskyId = 8
        });

        awards.Add(new Award
        {
            Id = 33,
            Title = "Blended Irish Whiskey",
            AwardsCeremony = "San Francisco World Spirits Competition",
            MedalType = Enums.MedalType.Bronze,
            Year = 2013,
            WhiskyId = 8
        });

        awards.Add(new Award
        {
            Id = 34,
            Title = "Straight Bourbon",
            AwardsCeremony = "San Francisco World Spirits Competition",
            MedalType = Enums.MedalType.Silver,
            Year = 2019,
            WhiskyId = 10
        });

        awards.Add(new Award
        {
            Id = 35,
            Title = "Straight Bourbon 10 years old and under",
            AwardsCeremony = "International Spirits Challenge",
            MedalType = Enums.MedalType.Silver,
            Year = 2019,
            WhiskyId = 10
        });

        awards.Add(new Award
        {
            Id = 36,
            Title = "Asian Whisky",
            AwardsCeremony = "Wizards of Whisky Awards",
            MedalType = Enums.MedalType.Gold,
            Year = 2014,
            WhiskyId = 13
        });

        awards.Add(new Award
        {
            Id = 37,
            Title = "Single Malt No Age Statement",
            AwardsCeremony = "International Spirits Challenge",
            MedalType = Enums.MedalType.Gold,
            Year = 2021,
            WhiskyId = 16
        });

        awards.Add(new Award
        {
            Id = 38,
            Title = "Single Malt No Age Statement",
            AwardsCeremony = "International Spirits Challenge",
            MedalType = Enums.MedalType.Gold,
            Year = 2020,
            WhiskyId = 16
        });

        awards.Add(new Award
        {
            Id = 39,
            Title = "Single Malt Scotch - to 12 Yrs",
            AwardsCeremony = "San Francisco World Spirits Competition",
            MedalType = Enums.MedalType.Gold,
            Year = 2013,
            WhiskyId = 16
        });

        awards.Add(new Award
        {
            Id = 40,
            Title = "Single Malt Scotch - No Age Statement",
            AwardsCeremony = "San Francisco World Spirits Competition",
            MedalType = Enums.MedalType.Gold,
            Year = 2021,
            WhiskyId = 16
        });

        awards.Add(new Award
        {
            Id = 41,
            Title = "Scotch Whisky - Single Malt - NAS",
            AwardsCeremony = "The Asian Spirits Masters (The Spirits Business)",
            MedalType = Enums.MedalType.Gold,
            Year = 2018,
            WhiskyId = 16
        });

        awards.Add(new Award
        {
            Id = 42,
            Title = "Scotch Single Malt - Lowland",
            AwardsCeremony = "International Wine & Spirit Competition",
            MedalType = Enums.MedalType.Silver,
            Year = 2017,
            WhiskyId = 16
        });

        awards.Add(new Award
        {
            Id = 43,
            Title = "Distillers' Single Malts 12 years and under",
            AwardsCeremony = "International Spirits Challenge",
            MedalType = Enums.MedalType.Silver,
            Year = 2014,
            WhiskyId = 16
        });

        awards.Add(new Award
        {
            Id = 44,
            Title = "Scotch Single Malt - Lowland",
            AwardsCeremony = "International Wine & Spirit Competition",
            MedalType = Enums.MedalType.Silver,
            Year = 2014,
            WhiskyId = 16
        });

        awards.Add(new Award
        {
            Id = 45,
            Title = "Scotch Single Malt - Highland",
            AwardsCeremony = "International Wine & Spirit Competition",
            MedalType = Enums.MedalType.Gold,
            Year = 2019,
            WhiskyId = 17
        });

        awards.Add(new Award
        {
            Id = 46,
            Title = "Distillers' Single Malts 12 years and under",
            AwardsCeremony = "International Spirits Challenge",
            MedalType = Enums.MedalType.Gold,
            Year = 2020,
            WhiskyId = 17
        });

        awards.Add(new Award
        {
            Id = 47,
            Title = "Single Malt Scotch - to 12 Yrs",
            AwardsCeremony = "San Francisco World Spirits Competition",
            MedalType = Enums.MedalType.Silver,
            Year = 2022,
            WhiskyId = 17
        });

        awards.Add(new Award
        {
            Id = 48,
            Title = "Distillers' Single Malts 12 years and under",
            AwardsCeremony = "International Spirits Challenge",
            MedalType = Enums.MedalType.Silver,
            Year = 2022,
            WhiskyId = 17
        });

        awards.Add(new Award
        {
            Id = 49,
            Title = "Distillers' Single Malts 12 years and under",
            AwardsCeremony = "International Spirits Challenge",
            MedalType = Enums.MedalType.Silver,
            Year = 2019,
            WhiskyId = 17
        });

        awards.Add(new Award
        {
            Id = 50,
            Title = "Scotch Single Malt - Highland",
            AwardsCeremony = "International Wine & Spirit Competition",
            MedalType = Enums.MedalType.Silver,
            Year = 2017,
            WhiskyId = 17
        });

        awards.Add(new Award
        {
            Id = 51,
            Title = "Scotch Whisky - Single Malt - NAS",
            AwardsCeremony = "The Asian Spirits Masters (The Spirits Business)",
            MedalType = Enums.MedalType.Gold,
            Year = 2018,
            WhiskyId = 19
        });

        awards.Add(new Award
        {
            Id = 52,
            Title = "Scotch Single Malt - Islay",
            AwardsCeremony = "International Wine & Spirit Competition",
            MedalType = Enums.MedalType.Gold,
            Year = 2017,
            WhiskyId = 19
        });

        awards.Add(new Award
        {
            Id = 53,
            Title = "Distillers' Single Malts 12 years and under",
            AwardsCeremony = "International Spirits Challenge",
            MedalType = Enums.MedalType.Silver,
            Year = 2022,
            WhiskyId = 19
        });

        awards.Add(new Award
        {
            Id = 54,
            Title = "Best Single Malt Scotch (Islay)",
            AwardsCeremony = "International Whisky Competition",
            MedalType = Enums.MedalType.Bronze,
            Year = 2015,
            WhiskyId = 19
        });

        awards.Add(new Award
        {
            Id = 55,
            Title = "Best Single Malt NAS",
            AwardsCeremony = "International Whisky Competition",
            MedalType = Enums.MedalType.Bronze,
            Year = 2015,
            WhiskyId = 19
        });

        awards.Add(new Award
        {
            Id = 56,
            Title = "Scotch Single Malt - Islay",
            AwardsCeremony = "International Wine & Spirit Competition",
            MedalType = Enums.MedalType.Silver,
            Year = 2014,
            WhiskyId = 19
        });

        awards.Add(new Award
        {
            Id = 57,
            Title = "Scotch Single Malt - Islay",
            AwardsCeremony = "International Wine & Spirit Competition",
            MedalType = Enums.MedalType.Silver,
            Year = 2013,
            WhiskyId = 19
        });

        awards.Add(new Award
        {
            Id = 58,
            Title = "Single Malt Scotch - to 12 Yrs",
            AwardsCeremony = "San Francisco World Spirits Competition",
            MedalType = Enums.MedalType.Silver,
            Year = 2013,
            WhiskyId = 19
        });

        awards.Add(new Award
        {
            Id = 59,
            Title = "Scotch Single Malt - Islay",
            AwardsCeremony = "International Wine & Spirit Competition",
            MedalType = Enums.MedalType.Silver,
            Year = 2019,
            WhiskyId = 19
        });

        awards.Add(new Award
        {
            Id = 60,
            Title = "Single Malt Scotch - to 12 Yrs",
            AwardsCeremony = "San Francisco World Spirits Competition",
            MedalType = Enums.MedalType.Gold,
            Year = 2022,
            WhiskyId = 21
        });

        awards.Add(new Award
        {
            Id = 61,
            Title = "Distillers' Single Malts 12 years and under",
            AwardsCeremony = "International Spirits Challenge",
            MedalType = Enums.MedalType.Gold,
            Year = 2020,
            WhiskyId = 21
        });

        awards.Add(new Award
        {
            Id = 62,
            Title = "Highlands & Islands up to 12yo",
            AwardsCeremony = "The Scotch Whisky Masters (The Spirits Business)",
            MedalType = Enums.MedalType.Gold,
            Year = 2020,
            WhiskyId = 21
        });

        awards.Add(new Award
        {
            Id = 63,
            Title = "Highlands & Islands up to 12yo",
            AwardsCeremony = "The Scotch Whisky Masters (The Spirits Business)",
            MedalType = Enums.MedalType.Gold,
            Year = 2019,
            WhiskyId = 21
        });

        awards.Add(new Award
        {
            Id = 64,
            Title = "Highlands & Islands up to 12yo",
            AwardsCeremony = "The Scotch Whisky Masters (The Spirits Business)",
            MedalType = Enums.MedalType.Gold,
            Year = 2018,
            WhiskyId = 21
        });

        awards.Add(new Award
        {
            Id = 65,
            Title = "Distillers' Single Malts 12 years and under",
            AwardsCeremony = "International Spirits Challenge",
            MedalType = Enums.MedalType.Silver,
            Year = 2022,
            WhiskyId = 21
        });

        awards.Add(new Award
        {
            Id = 66,
            Title = "Distillers' Single Malts 12 years and under",
            AwardsCeremony = "International Spirits Challenge",
            MedalType = Enums.MedalType.Silver,
            Year = 2019,
            WhiskyId = 21
        });

        awards.Add(new Award
        {
            Id = 67,
            Title = "Scotch Whisky - Single Malt - Age Statement",
            AwardsCeremony = "The Asian Spirits Masters (The Spirits Business)",
            MedalType = Enums.MedalType.Silver,
            Year = 2018,
            WhiskyId = 21
        });

        awards.Add(new Award
        {
            Id = 68,
            Title = "Scotch Single Malt - Island",
            AwardsCeremony = "International Wine & Spirit Competition",
            MedalType = Enums.MedalType.Silver,
            Year = 2017,
            WhiskyId = 21
        });

        awards.Add(new Award
        {
            Id = 69,
            Title = "Irish Blend of the Year",
            AwardsCeremony = "Jim Murray's Whisky Bible",
            MedalType = Enums.MedalType.Gold,
            Year = 2018,
            WhiskyId = 22
        });

        awards.Add(new Award
        {
            Id = 70,
            Title = "Irish Blended - Premium",
            AwardsCeremony = "The Irish Whisky Masters (The Spirits Business)",
            MedalType = Enums.MedalType.Gold,
            Year = 2013,
            WhiskyId = 22
        });

        awards.Add(new Award
        {
            Id = 71,
            Title = "Irish Whiskey",
            AwardsCeremony = "Wizards of Whisky Awards",
            MedalType = Enums.MedalType.Silver,
            Year = 2014,
            WhiskyId = 22
        });

        awards.Add(new Award
        {
            Id = 72,
            Title = "Irish Blended - Premium",
            AwardsCeremony = "The Irish Whisky Masters (The Spirits Business)",
            MedalType = Enums.MedalType.Silver,
            Year = 2014,
            WhiskyId = 22
        });

        awards.Add(new Award
        {
            Id = 73,
            Title = "Blended Malted Irish Whiskey",
            AwardsCeremony = "San Francisco World Spirits Competition",
            MedalType = Enums.MedalType.Silver,
            Year = 2013,
            WhiskyId = 22
        });

        awards.Add(new Award
        {
            Id = 74,
            Title = "Irish Blended - Super-Premium",
            AwardsCeremony = "The Irish Whisky Masters (The Spirits Business)",
            MedalType = Enums.MedalType.Silver,
            Year = 2019,
            WhiskyId = 23
        });

        awards.Add(new Award
        {
            Id = 75,
            Title = "Irish Blended - Standard",
            AwardsCeremony = "The Irish Whisky Masters (The Spirits Business)",
            MedalType = Enums.MedalType.Silver,
            Year = 2017,
            WhiskyId = 23
        });

        awards.Add(new Award
        {
            Id = 76,
            Title = "Straight Bourbon",
            AwardsCeremony = "San Francisco World Spirits Competition",
            MedalType = Enums.MedalType.Silver,
            Year = 2019,
            WhiskyId = 25
        });

        awards.Add(new Award
        {
            Id = 77,
            Title = "Straight Bourbon 10 years old and under",
            AwardsCeremony = "International Spirits Challenge",
            MedalType = Enums.MedalType.Silver,
            Year = 2019,
            WhiskyId = 25
        });

        awards.Add(new Award
        {
            Id = 78,
            Title = "Daily Dram",
            AwardsCeremony = "Malt Maniacs Awards",
            MedalType = Enums.MedalType.Bronze,
            Year = 2013,
            WhiskyId = 26
        });

        awards.Add(new Award
        {
            Id = 84,
            Title = "Whiskies Worldwide",
            AwardsCeremony = "International Spirits Challenge",
            MedalType = Enums.MedalType.Silver,
            Year = 2019,
            WhiskyId = 27
        });

        awards.Add(new Award
        {
            Id = 85,
            Title = "Asian Single Malt",
            AwardsCeremony = "The World Whisky Masters (The Spirits Business)",
            MedalType = Enums.MedalType.Silver,
            Year = 2019,
            WhiskyId = 27
        });

        awards.Add(new Award
        {
            Id = 86,
            Title = "Asian Single Malt",
            AwardsCeremony = "The World Whisky Masters (The Spirits Business)",
            MedalType = Enums.MedalType.Silver,
            Year = 2018,
            WhiskyId = 27
        });



        return awards.ToArray();
    }
}
