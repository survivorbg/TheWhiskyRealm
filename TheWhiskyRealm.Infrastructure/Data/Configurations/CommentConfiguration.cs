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
            UserId = "a8909756-a101-47c5-8d52-085322ffa6e6"
        });

        comments.Add(new Comment
        {
            Id= 2,
            Content = "I completely agree with your list! Can't wait to try these whiskies.",
            PostedDate = new DateTime(2024, 3, 16),
            ArticleId = 1,
            UserId = "2d730ec7-1b14-4bf5-9265-3522e35c06d5"
        });



        return comments.ToArray();
    }
}
