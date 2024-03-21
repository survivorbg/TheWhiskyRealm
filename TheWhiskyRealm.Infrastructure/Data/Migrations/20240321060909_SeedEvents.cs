﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheWhiskyRealm.Infrastructure.Data
{
    public partial class SeedEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Description", "DurationInHours", "EndDate", "OrganiserId", "Price", "StartDate", "Title", "VenueId" },
                values: new object[] { 1, "Join us for an evening of whisky tasting and discovery.", 3, new DateTime(2024, 3, 25, 21, 0, 0, 0, DateTimeKind.Unspecified), "a8909756-a101-47c5-8d52-085322ffa6e6", 25.99m, new DateTime(2024, 3, 25, 18, 0, 0, 0, DateTimeKind.Unspecified), "Whisky Tasting Evening", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}