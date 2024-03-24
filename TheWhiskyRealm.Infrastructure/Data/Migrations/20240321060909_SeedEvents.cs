using System;
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
                values: new object[] { 1, "Join us for an evening of whisky tasting and discovery.", 3, new DateTime(2024, 3, 25, 21, 0, 0, 0, DateTimeKind.Unspecified), "7dfb241e-f8a5-4ba4-a5aa-5becf035c442", 25.99m, new DateTime(2024, 3, 25, 18, 0, 0, 0, DateTimeKind.Unspecified), "Whisky Tasting Evening", 1 });
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
