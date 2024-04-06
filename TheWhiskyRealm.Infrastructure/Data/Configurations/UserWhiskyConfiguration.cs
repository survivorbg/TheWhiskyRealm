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

    private UserWhisky[] GenerateUsersWhiskies()
    {
        ICollection<UserWhisky> userWhiskies = new HashSet<UserWhisky>();

        userWhiskies.Add(new UserWhisky
        {
            UserId = "7dfb241e-f8a5-4ba4-a5aa-5becf035c442",
            WhiskyId = 1
        });

        userWhiskies.Add(new UserWhisky
        {
            UserId = "7dfb241e-f8a5-4ba4-a5aa-5becf035c442",
            WhiskyId = 3
        });

        userWhiskies.Add(new UserWhisky
        {
            UserId = "7dfb241e-f8a5-4ba4-a5aa-5becf035c442",
            WhiskyId = 5
        });

        userWhiskies.Add(new UserWhisky
        {
            UserId = "1cf4a321-6128-459e-8e4e-e4615c85d30f",
            WhiskyId = 3
        });

        userWhiskies.Add(new UserWhisky
        {
            UserId = "1cf4a321-6128-459e-8e4e-e4615c85d30f",
            WhiskyId = 2
        });

        userWhiskies.Add(new UserWhisky
        {
            UserId = "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4",
            WhiskyId = 19
        });

        return userWhiskies.ToArray();
    }
}
