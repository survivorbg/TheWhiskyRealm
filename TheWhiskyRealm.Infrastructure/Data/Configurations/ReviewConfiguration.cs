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
                UserId = "a8909756-a101-47c5-8d52-085322ffa6e6"
            });

            reviews.Add(new Review
            {
                Id = 2,
                Title = "Smooth and enjoyable",
                Content = "Really enjoyed sipping on this whisky, smooth with a nice finish.",
                Recommend = true,
                WhiskyId = 2,
                UserId = "bca5356a-d5d8-47d7-b314-e74901211b99"
            });

            reviews.Add(new Review
            {
                Id = 3,
                Title = "Great!",
                Content = "This whisky is unbeatable. Smooth and easy to drink.",
                Recommend = true,
                WhiskyId = 3,
                UserId = "d9ef08e4-4307-4a78-8b8c-afaea5d8a0d2"
            });

            reviews.Add(new Review
            {
                Id = 4,
                Title = "Disappointing",
                Content = "Expected more from this whisky. Found it lacking in flavor.",
                Recommend = false,
                WhiskyId = 4,
                UserId = "a8909756-a101-47c5-8d52-085322ffa6e6"
            });

            reviews.Add(new Review
            {
                Id = 5,
                Title = "A real treat",
                Content = "This whisky is a real treat for the senses. Highly recommended.",
                Recommend = true,
                WhiskyId = 5,
                UserId = "bca5356a-d5d8-47d7-b314-e74901211b99"
            });

            reviews.Add(new Review
            {
                Id = 6,
                Title = "Smooth and elegant",
                Content = "Smooth and elegant, with a lovely finish. A delightful whisky.",
                Recommend = true,
                WhiskyId = 6,
                UserId = "d9ef08e4-4307-4a78-8b8c-afaea5d8a0d2"
            });

            reviews.Add(new Review
            {
                Id = 7,
                Title = "Not bad",
                Content = "Decent whisky, but nothing extraordinary. Would drink again though.",
                Recommend = true,
                WhiskyId = 7,
                UserId = "a8909756-a101-47c5-8d52-085322ffa6e6"
            });

            reviews.Add(new Review
            {
                Id = 8,
                Title = "Complex flavors",
                Content = "Really enjoyed the complexity of flavors in this whisky. A must-try.",
                Recommend = true,
                WhiskyId = 8,
                UserId = "bca5356a-d5d8-47d7-b314-e74901211b99"
            });

            reviews.Add(new Review
            {
                Id = 9,
                Title = "Perfect for special occasions",
                Content = "Save this one for special occasions. Truly a special whisky.",
                Recommend = true,
                WhiskyId = 9,
                UserId = "d9ef08e4-4307-4a78-8b8c-afaea5d8a0d2"
            });

            reviews.Add(new Review
            {
                Id = 10,
                Title = "Not impressed",
                Content = "Was not impressed with this whisky. Expected more.",
                Recommend = false,
                WhiskyId = 10,
                UserId = "a8909756-a101-47c5-8d52-085322ffa6e6"
            });

            return reviews.ToArray();
        }
    }
}
