using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheWhiskyRealm.Infrastructure.Data
{
    public partial class VenuesSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Venues",
                columns: new[] { "Id", "Capacity", "CityId", "Name" },
                values: new object[,]
                {
                    { 1, 7, 1, "Masterpiece Whisky Bar" },
                    { 2, 6, 1, "Bar Caldo" },
                    { 4, 100, 1, "Hotel Marinela" },
                    { 5, 3, 2, "Bar Sandaka" },
                    { 6, 10, 2, "The Whisky Library" },
                    { 7, 40, 2, "Hotel Imperial" },
                    { 8, 10, 3, "Tasting Room" }
                });

            migrationBuilder.AddCheckConstraint(
                name: "CK_Capacity_Max",
                table: "Venues",
                sql: "[Capacity] <= 100");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Capacity_Min",
                table: "Venues",
                sql: "[Capacity] >= 3");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Capacity_Max",
                table: "Venues");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Capacity_Min",
                table: "Venues");

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
