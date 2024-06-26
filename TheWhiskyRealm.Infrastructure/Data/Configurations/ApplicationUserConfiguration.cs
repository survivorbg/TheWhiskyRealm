﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Infrastructure.Data.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder
       .HasCheckConstraint("CheckDateOfBirth", "DateOfBirth >= '1900-01-01' AND DateOfBirth <= DATEADD(YEAR, -18, GETDATE())");

        builder.HasData(GenerateUsers());

    }

    private ApplicationUser[] GenerateUsers()
    {
        ICollection<ApplicationUser> users = new HashSet<ApplicationUser>();

        var hasher = new PasswordHasher<ApplicationUser>();

        var userOne = new ApplicationUser()
        {
            Id = "f99c5e20-d91e-4a5e-9b73-fdb38b89ffc3",
            UserName = "admin@gmail.com",
            NormalizedUserName = "ADMIN@GMAIL.COM",
            Email = "admin@gmail.com",
            NormalizedEmail = "ADMIN@GMAIL.COM",
            DateOfBirth = new DateTime(1980, 1, 1),
            EmailConfirmed = true,
            LockoutEnabled = false
        };

        userOne.PasswordHash = hasher.HashPassword(userOne, "admin123");
        users.Add(userOne);

        var userTwo = new ApplicationUser()
        {
            Id = "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4",
            UserName = "test@gmail.com",
            NormalizedUserName = "TEST@GMAIL.COM",
            Email = "test@gmail.com",
            NormalizedEmail = "TEST@GMAIL.COM",
            DateOfBirth = new DateTime(1994, 9, 23),
            EmailConfirmed = true,
            LockoutEnabled = true
        };

        userTwo.PasswordHash = hasher.HashPassword(userTwo, "password123");

        users.Add(userTwo);

        var userThree = new ApplicationUser()
        {

            Id = "7dfb241e-f8a5-4ba4-a5aa-5becf035c442",
            UserName = "sober@gmail.com",
            NormalizedUserName = "SOBER@GMAIL.COM",
            Email = "sober@gmail.com",
            NormalizedEmail = "SOBER@GMAIL.COM",
            DateOfBirth = new DateTime(1995, 10, 24),
            EmailConfirmed = true,
            LockoutEnabled = true
        };

        userThree.PasswordHash = hasher.HashPassword(userThree, "password123");

        users.Add(userThree);

        var userFour = new ApplicationUser()
        {

            Id = "1cf4a321-6128-459e-8e4e-e4615c85d30f",
            UserName = "noToAlcohol@gmail.com",
            NormalizedUserName = "NOTOALCOHOL@GMAIL.COM",
            Email = "noToAlcohol@gmail.com",
            NormalizedEmail = "NOTOALCOHOL@GMAIL.COM",
            DateOfBirth = new DateTime(2004, 1, 2),
            EmailConfirmed = true,
            LockoutEnabled = true
        };

        userFour.PasswordHash = hasher.HashPassword(userFour, "password123");

        users.Add(userFour);

        return users.ToArray();
    }
}
