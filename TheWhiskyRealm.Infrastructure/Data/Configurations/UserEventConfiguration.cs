using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Infrastructure.Data.Configurations;

public class UserEventConfiguration : IEntityTypeConfiguration<UserEvent>
{
    public void Configure(EntityTypeBuilder<UserEvent> builder)
    {
        
        builder
            .HasKey(ue => new { ue.EventId, ue.UserId });

        builder
            .HasOne(ue => ue.Event)
            .WithMany(e => e.UsersEvents)
            .HasForeignKey(ue => ue.EventId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(GenerateUsersEvents());
    }

    private UserEvent[] GenerateUsersEvents()
    {
        ICollection<UserEvent> usersEvents = new HashSet<UserEvent>();

        usersEvents.Add(new UserEvent
        {
            UserId = "2d730ec7-1b14-4bf5-9265-3522e35c06d5",
            EventId = 1
        });

        usersEvents.Add(new UserEvent
        {
            UserId = "bca5356a-d5d8-47d7-b314-e74901211b99",
            EventId = 1
        });

        usersEvents.Add(new UserEvent
        {
            UserId = "d9ef08e4-4307-4a78-8b8c-afaea5d8a0d2",
            EventId = 1
        });

        return usersEvents.ToArray();
    }
}
