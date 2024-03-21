using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheWhiskyRealm.Infrastructure.Data
{
    public partial class SeedRatings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "Finish", "Nose", "Taste", "UserId", "WhiskyId" },
                values: new object[,]
                {
                    { 1, 45, 47, 54, "a8909756-a101-47c5-8d52-085322ffa6e6", 10 },
                    { 2, 43, 39, 87, "bca5356a-d5d8-47d7-b314-e74901211b99", 11 },
                    { 3, 88, 78, 77, "d9ef08e4-4307-4a78-8b8c-afaea5d8a0d2", 12 },
                    { 4, 59, 59, 59, "a8909756-a101-47c5-8d52-085322ffa6e6", 13 },
                    { 5, 87, 99, 78, "bca5356a-d5d8-47d7-b314-e74901211b99", 14 },
                    { 6, 31, 54, 42, "d9ef08e4-4307-4a78-8b8c-afaea5d8a0d2", 15 },
                    { 7, 51, 44, 55, "a8909756-a101-47c5-8d52-085322ffa6e6", 16 },
                    { 8, 81, 77, 63, "bca5356a-d5d8-47d7-b314-e74901211b99", 17 },
                    { 9, 45, 12, 18, "d9ef08e4-4307-4a78-8b8c-afaea5d8a0d2", 18 },
                    { 10, 49, 49, 59, "a8909756-a101-47c5-8d52-085322ffa6e6", 19 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
