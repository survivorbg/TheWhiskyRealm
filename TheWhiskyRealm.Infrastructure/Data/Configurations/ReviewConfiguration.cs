using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Infrastructure.Data.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasData(GenerateReviews());
        }

        private Review[] GenerateReviews()
        {
            ICollection<Review> reviews = new List<Review>();

            reviews.Add(new Review
            {
                Id = 1,
                Title = "Fantastic flavor!",
                Content = "This whisky has an amazing taste profile, rich and complex.",
                Recommend = true,
                WhiskyId = 1,
                UserId = "1cf4a321-6128-459e-8e4e-e4615c85d30f"
            });

            reviews.Add(new Review
            {
                Id = 2,
                Title = "Smooth and enjoyable",
                Content = "Really enjoyed sipping on this whisky, smooth with a nice finish.",
                Recommend = true,
                WhiskyId = 2,
                UserId = "7dfb241e-f8a5-4ba4-a5aa-5becf035c442"
            });

            reviews.Add(new Review
            {
                Id = 3,
                Title = "Great!",
                Content = "This whisky is unbeatable. Smooth and easy to drink.",
                Recommend = true,
                WhiskyId = 3,
                UserId = "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4"
            });

            reviews.Add(new Review
            {
                Id = 4,
                Title = "Disappointing",
                Content = "Expected more from this whisky. Found it lacking in flavor.",
                Recommend = false,
                WhiskyId = 4,
                UserId = "1cf4a321-6128-459e-8e4e-e4615c85d30f"
            });

            reviews.Add(new Review
            {
                Id = 5,
                Title = "A real treat",
                Content = "This whisky is a real treat for the senses. Highly recommended.",
                Recommend = true,
                WhiskyId = 5,
                UserId = "7dfb241e-f8a5-4ba4-a5aa-5becf035c442"
            });

            reviews.Add(new Review
            {
                Id = 6,
                Title = "Smooth and elegant",
                Content = "Smooth and elegant, with a lovely finish. A delightful whisky.",
                Recommend = true,
                WhiskyId = 6,
                UserId = "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4"
            });

            reviews.Add(new Review
            {
                Id = 7,
                Title = "Not bad",
                Content = "Decent whisky, but nothing extraordinary. Would drink again though.",
                Recommend = true,
                WhiskyId = 7,
                UserId = "1cf4a321-6128-459e-8e4e-e4615c85d30f"
            });

            reviews.Add(new Review
            {
                Id = 8,
                Title = "Complex flavors",
                Content = "Really enjoyed the complexity of flavors in this whisky. A must-try.",
                Recommend = true,
                WhiskyId = 8,
                UserId = "7dfb241e-f8a5-4ba4-a5aa-5becf035c442"
            });

            reviews.Add(new Review
            {
                Id = 9,
                Title = "Perfect for special occasions",
                Content = "Save this one for special occasions. Truly a special whisky.",
                Recommend = true,
                WhiskyId = 9,
                UserId = "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4"
            });

            reviews.Add(new Review
            {
                Id = 10,
                Title = "Not impressed",
                Content = "Was not impressed with this whisky. Expected more.",
                Recommend = false,
                WhiskyId = 10,
                UserId = "1cf4a321-6128-459e-8e4e-e4615c85d30f"
            });

            reviews.Add(new Review
            {
                Id = 11,
                Title = "Very good!",
                Content = "The whisky of my dreams!",
                Recommend = true,
                WhiskyId = 1,
                UserId = "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4"
            });

            reviews.Add(new Review
            {
                Id = 12,
                Title = "Nothing special",
                Content = "Honestly I dont't know what is to like about it.",
                Recommend = false,
                WhiskyId = 1,
                UserId = "7dfb241e-f8a5-4ba4-a5aa-5becf035c442"
            });

            reviews.Add(new Review
            {
                Id = 13,
                Title = "Yep , yep good",
                Content = "Like it! A lot!",
                Recommend = true,
                WhiskyId = 2,
                UserId = "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4"
            });

            return reviews.ToArray();
        }
    }
}
