﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TheWhiskyRealm.Infrastructure.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(GeneratesRoles());
    }
    private IdentityRole[] GeneratesRoles()
    {
        ICollection<IdentityRole> roles = new HashSet<IdentityRole>();

        roles.Add(new IdentityRole
        {
            Name = "Administrator",
            NormalizedName = "ADMINISTRATOR",
            Id = "44a539b2-223a-4c1b-9d1c-954ef8d889ff",
            ConcurrencyStamp = "eb8f0668-2e21-4903-81a3-b858513bb59c"

        });

        roles.Add(new IdentityRole()
        {
            Name = "User",
            NormalizedName = "USER",
            Id = "dc3cf4ec-f90c-4915-b749-4ab01863fdf6",
            ConcurrencyStamp = "1c72eeac-aa45-4a8e-8606-6bbd1dca9a73"
        });

        return roles.ToArray();
    }
}