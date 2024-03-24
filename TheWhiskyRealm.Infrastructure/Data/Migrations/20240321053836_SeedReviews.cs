using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheWhiskyRealm.Infrastructure.Data
{
    public partial class SeedReviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Content", "Recommend", "Title", "UserId", "WhiskyId" },
                values: new object[,]
                {
                    { 1, "This whisky has an amazing taste profile, rich and complex.", true, "Fantastic flavor!", "1cf4a321-6128-459e-8e4e-e4615c85d30f", 1 },
                    { 2, "Really enjoyed sipping on this whisky, smooth with a nice finish.", true, "Smooth and enjoyable", "7dfb241e-f8a5-4ba4-a5aa-5becf035c442", 2 },
                    { 3, "This whisky is unbeatable. Smooth and easy to drink.", true, "Great!", "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4", 3 },
                    { 4, "Expected more from this whisky. Found it lacking in flavor.", false, "Disappointing", "1cf4a321-6128-459e-8e4e-e4615c85d30f", 4 },
                    { 5, "This whisky is a real treat for the senses. Highly recommended.", true, "A real treat", "7dfb241e-f8a5-4ba4-a5aa-5becf035c442", 5 },
                    { 6, "Smooth and elegant, with a lovely finish. A delightful whisky.", true, "Smooth and elegant", "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4", 6 },
                    { 7, "Decent whisky, but nothing extraordinary. Would drink again though.", true, "Not bad", "1cf4a321-6128-459e-8e4e-e4615c85d30f", 7 },
                    { 8, "Really enjoyed the complexity of flavors in this whisky. A must-try.", true, "Complex flavors", "7dfb241e-f8a5-4ba4-a5aa-5becf035c442", 8 },
                    { 9, "Save this one for special occasions. Truly a special whisky.", true, "Perfect for special occasions", "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4", 9 },
                    { 10, "Was not impressed with this whisky. Expected more.", false, "Not impressed", "1cf4a321-6128-459e-8e4e-e4615c85d30f", 10 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
