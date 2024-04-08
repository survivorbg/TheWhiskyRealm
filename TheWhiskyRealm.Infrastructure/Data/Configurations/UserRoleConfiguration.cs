using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TheWhiskyRealm.Infrastructure.Data.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(GeneratesUsersRoles());
    }

    private IdentityUserRole<string>[] GeneratesUsersRoles()
    {
        ICollection<IdentityUserRole<string>> roles = new HashSet<IdentityUserRole<string>>();

        roles.Add(new IdentityUserRole<string>
        {
            UserId = "f99c5e20-d91e-4a5e-9b73-fdb38b89ffc3",
            RoleId = "44a539b2-223a-4c1b-9d1c-954ef8d889ff"
        });

        roles.Add(new IdentityUserRole<string>
        {
            UserId = "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4",
            RoleId = "dc3cf4ec-f90c-4915-b749-4ab01863fdf6"
        });

        roles.Add(new IdentityUserRole<string>
        {
            UserId = "7dfb241e-f8a5-4ba4-a5aa-5becf035c442",
            RoleId = "dc3cf4ec-f90c-4915-b749-4ab01863fdf6"
        });

        roles.Add(new IdentityUserRole<string>
        {
            UserId = "1cf4a321-6128-459e-8e4e-e4615c85d30f",
            RoleId = "dc3cf4ec-f90c-4915-b749-4ab01863fdf6"
        });


        return roles.ToArray();
    }
}
