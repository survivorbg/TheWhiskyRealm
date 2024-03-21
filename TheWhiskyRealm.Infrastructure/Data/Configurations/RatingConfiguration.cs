using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheWhiskyRealm.Infrastructure.Data.Models;
using static TheWhiskyRealm.Infrastructure.Constants.RatingDataConstants;


namespace TheWhiskyRealm.Infrastructure.Data.Configurations;

public class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        //Rating configuration
        builder
       .HasCheckConstraint("CK_Nose_Min", $"[Nose] >= {MinNoseValue}")
       .HasCheckConstraint("CK_Nose_Max", $"[Nose] <= {MaxNoseValue}")
       .HasCheckConstraint("CK_Taste_Min", $"[Taste] >= {MinTasteValue}")
       .HasCheckConstraint("CK_Taste_Max", $"[Taste] <= {MaxTasteValue}")
       .HasCheckConstraint("CK_Finish_Min", $"[Finish] >= {MinFinishValue}")
       .HasCheckConstraint("CK_Finish_Max", $"[Finish] <= {MaxFinishValue}");

        builder.HasData(GenerateRatings());
    }

    private Rating[] GenerateRatings()
    {
        ICollection<Rating> ratings = new HashSet<Rating>();

        ratings.Add(new Rating
        {
            Id = 1,
            Nose = 47,
            Taste = 54,
            Finish = 45,
            WhiskyId = 10,
            UserId = "a8909756-a101-47c5-8d52-085322ffa6e6"
        });

        ratings.Add(new Rating
        {
            Id = 2,
            Nose = 39,
            Taste = 87,
            Finish = 43,
            WhiskyId = 11,
            UserId = "bca5356a-d5d8-47d7-b314-e74901211b99"
        });

        ratings.Add(new Rating
        {
            Id = 3,
            Nose = 78,
            Taste = 77,
            Finish = 88,
            WhiskyId = 12,
            UserId = "d9ef08e4-4307-4a78-8b8c-afaea5d8a0d2"
        });

        ratings.Add(new Rating
        {
            Id = 4,
            Nose = 59,
            Taste = 59,
            Finish = 59,
            WhiskyId = 13,
            UserId = "a8909756-a101-47c5-8d52-085322ffa6e6"
        });

        ratings.Add(new Rating
        {
            Id = 5,
            Nose = 99,
            Taste = 78,
            Finish = 87,
            WhiskyId = 14,
            UserId = "bca5356a-d5d8-47d7-b314-e74901211b99"
        });

        ratings.Add(new Rating
        {
            Id = 6,
            Nose = 54,
            Taste = 42,
            Finish = 31,
            WhiskyId = 15,
            UserId = "d9ef08e4-4307-4a78-8b8c-afaea5d8a0d2"
        });

        ratings.Add(new Rating
        {
            Id = 7,
            Nose = 44,
            Taste = 55,
            Finish = 51,
            WhiskyId = 16,
            UserId = "a8909756-a101-47c5-8d52-085322ffa6e6"
        });

        ratings.Add(new Rating
        {
            Id = 8,
            Nose = 77,
            Taste = 63,
            Finish = 81,
            WhiskyId = 17,
            UserId = "bca5356a-d5d8-47d7-b314-e74901211b99"
        });

        ratings.Add(new Rating
        {
            Id = 9,
            Nose = 12,
            Taste = 18,
            Finish = 45,
            WhiskyId = 18,
            UserId = "d9ef08e4-4307-4a78-8b8c-afaea5d8a0d2"
        });

        ratings.Add(new Rating
        {
            Id = 10,
            Nose = 49,
            Taste = 59,
            Finish = 49,
            WhiskyId = 19,
            UserId = "a8909756-a101-47c5-8d52-085322ffa6e6"
        });


        return ratings.ToArray();
    }
}
