using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheWhiskyRealm.Infrastructure.Data.Models;
using static TheWhiskyRealm.Infrastructure.Constants.RatingDataConstants;


namespace TheWhiskyRealm.Infrastructure.Data.Configurations;

public class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder
       .HasCheckConstraint("CK_Nose_Min", $"[Nose] >= {RatingMinNoseValue}")
       .HasCheckConstraint("CK_Nose_Max", $"[Nose] <= {RatingMaxNoseValue}")
       .HasCheckConstraint("CK_Taste_Min", $"[Taste] >= {RatingMinTasteValue}")
       .HasCheckConstraint("CK_Taste_Max", $"[Taste] <= {RatingMaxTasteValue}")
       .HasCheckConstraint("CK_Finish_Min", $"[Finish] >= {RatingMinFinishValue}")
       .HasCheckConstraint("CK_Finish_Max", $"[Finish] <= {RatingMaxFinishValue}");

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
            UserId = "7dfb241e-f8a5-4ba4-a5aa-5becf035c442"
        });

        ratings.Add(new Rating
        {
            Id = 2,
            Nose = 39,
            Taste = 87,
            Finish = 43,
            WhiskyId = 11,
            UserId = "1cf4a321-6128-459e-8e4e-e4615c85d30f"
        });

        ratings.Add(new Rating
        {
            Id = 3,
            Nose = 78,
            Taste = 77,
            Finish = 88,
            WhiskyId = 12,
            UserId = "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4"
        });

        ratings.Add(new Rating
        {
            Id = 4,
            Nose = 59,
            Taste = 59,
            Finish = 59,
            WhiskyId = 13,
            UserId = "7dfb241e-f8a5-4ba4-a5aa-5becf035c442"
        });

        ratings.Add(new Rating
        {
            Id = 5,
            Nose = 99,
            Taste = 78,
            Finish = 87,
            WhiskyId = 14,
            UserId = "1cf4a321-6128-459e-8e4e-e4615c85d30f"
        });

        ratings.Add(new Rating
        {
            Id = 6,
            Nose = 54,
            Taste = 42,
            Finish = 31,
            WhiskyId = 15,
            UserId = "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4"
        });

        ratings.Add(new Rating
        {
            Id = 7,
            Nose = 44,
            Taste = 55,
            Finish = 51,
            WhiskyId = 16,
            UserId = "7dfb241e-f8a5-4ba4-a5aa-5becf035c442"
        });

        ratings.Add(new Rating
        {
            Id = 8,
            Nose = 77,
            Taste = 63,
            Finish = 81,
            WhiskyId = 17,
            UserId = "1cf4a321-6128-459e-8e4e-e4615c85d30f"
        });

        ratings.Add(new Rating
        {
            Id = 9,
            Nose = 12,
            Taste = 18,
            Finish = 45,
            WhiskyId = 18,
            UserId = "7dfb241e-f8a5-4ba4-a5aa-5becf035c442"
        });

        ratings.Add(new Rating
        {
            Id = 10,
            Nose = 49,
            Taste = 59,
            Finish = 49,
            WhiskyId = 19,
            UserId = "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4"
        });

        ratings.Add(new Rating
        {
            Id = 11,
            Nose = 47,
            Taste = 54,
            Finish = 45,
            WhiskyId = 1,
            UserId = "7dfb241e-f8a5-4ba4-a5aa-5becf035c442"
        });

        ratings.Add(new Rating
        {
            Id = 12,
            Nose = 39,
            Taste = 87,
            Finish = 43,
            WhiskyId = 1,
            UserId = "1cf4a321-6128-459e-8e4e-e4615c85d30f"
        });

        ratings.Add(new Rating
        {
            Id = 13,
            Nose = 87,
            Taste = 77,
            Finish = 88,
            WhiskyId = 1,
            UserId = "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4"
        });

        ratings.Add(new Rating
        {
            Id = 14,
            Nose = 37,
            Taste = 84,
            Finish = 45,
            WhiskyId = 2,
            UserId = "7dfb241e-f8a5-4ba4-a5aa-5becf035c442"
        });

        ratings.Add(new Rating
        {
            Id = 15,
            Nose = 45,
            Taste = 87,
            Finish = 43,
            WhiskyId = 2,
            UserId = "1cf4a321-6128-459e-8e4e-e4615c85d30f"
        });

        ratings.Add(new Rating
        {
            Id = 16,
            Nose = 87,
            Taste = 45,
            Finish = 37,
            WhiskyId = 2,
            UserId = "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4"
        });

        return ratings.ToArray();
    }
}
