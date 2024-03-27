using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Infrastructure.Data.Configurations;

public class UserWhiskyConfiguration : IEntityTypeConfiguration<UserWhisky>
{
    public void Configure(EntityTypeBuilder<UserWhisky> builder)
    {

        builder
            .HasKey(ue => new { ue.UserId, ue.WhiskyId });

        builder.HasOne(uw => uw.Whisky)
            .WithMany(w => w.UsersWhiskies)
            .HasForeignKey(uw => uw.WhiskyId)
            .OnDelete(DeleteBehavior.Restrict); 
    }

    private UserEvent[] GenerateUsersWhiskies()
    {
        return new UserEvent[0];
    }
}
