using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Infrastructure.Data.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder
            .HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(GenerateComments());
    }

    private Comment[] GenerateComments()
    {
        ICollection<Comment> comments = new HashSet<Comment>();

        comments.Add(new Comment
        {
            Id = 1,
            Content = "Great article! I learned a lot about the whisky types.",
            PostedDate = new DateTime(2024, 3, 17),
            ArticleId = 1,
            UserId = "1cf4a321-6128-459e-8e4e-e4615c85d30f"
        });

        comments.Add(new Comment
        {
            Id = 2,
            Content = "I completely agree with your list! Can't wait to try these whiskies.",
            PostedDate = new DateTime(2024, 3, 16),
            ArticleId = 1,
            UserId = "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4"
        });

        comments.Add(new Comment
        {
            Id = 3,
            Content = "Auchentoshan Three Wood is one of the my favourite whiskies!",
            PostedDate = new DateTime(2024, 4, 2),
            ArticleId = 3,
            UserId = "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4"
        });

        comments.Add(new Comment
        {
            Id = 4,
            Content = "In some aspect i agree with you, but overall i don't!",
            PostedDate = new DateTime(2024, 4, 3),
            ArticleId = 3,
            UserId = "1cf4a321-6128-459e-8e4e-e4615c85d30f"
        });


        return comments.ToArray();
    }
}
