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
                    { 1, 45, 47, 54, "7dfb241e-f8a5-4ba4-a5aa-5becf035c442", 10 },
                    { 2, 43, 39, 87, "1cf4a321-6128-459e-8e4e-e4615c85d30f", 11 },
                    { 3, 88, 78, 77, "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4", 12 },
                    { 4, 59, 59, 59, "7dfb241e-f8a5-4ba4-a5aa-5becf035c442", 13 },
                    { 5, 87, 99, 78, "1cf4a321-6128-459e-8e4e-e4615c85d30f", 14 },
                    { 6, 31, 54, 42, "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4", 15 },
                    { 7, 51, 44, 55, "7dfb241e-f8a5-4ba4-a5aa-5becf035c442", 16 },
                    { 8, 81, 77, 63, "1cf4a321-6128-459e-8e4e-e4615c85d30f", 17 },
                    { 9, 45, 12, 18, "7dfb241e-f8a5-4ba4-a5aa-5becf035c442", 18 },
                    { 10, 49, 49, 59, "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4", 19 }
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
