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
            UserId = "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4",
            EventId = 1
        });

        usersEvents.Add(new UserEvent
        {
            UserId = "1cf4a321-6128-459e-8e4e-e4615c85d30f",
            EventId = 1
        });

        return usersEvents.ToArray();
    }
}
