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
                    { 1, "This whisky has an amazing taste profile, rich and complex.", true, "Fantastic flavor!", "a8909756-a101-47c5-8d52-085322ffa6e6", 1 },
                    { 2, "Really enjoyed sipping on this whisky, smooth with a nice finish.", true, "Smooth and enjoyable", "bca5356a-d5d8-47d7-b314-e74901211b99", 2 },
                    { 3, "This whisky is unbeatable. Smooth and easy to drink.", true, "Great!", "d9ef08e4-4307-4a78-8b8c-afaea5d8a0d2", 3 },
                    { 4, "Expected more from this whisky. Found it lacking in flavor.", false, "Disappointing", "a8909756-a101-47c5-8d52-085322ffa6e6", 4 },
                    { 5, "This whisky is a real treat for the senses. Highly recommended.", true, "A real treat", "bca5356a-d5d8-47d7-b314-e74901211b99", 5 },
                    { 6, "Smooth and elegant, with a lovely finish. A delightful whisky.", true, "Smooth and elegant", "d9ef08e4-4307-4a78-8b8c-afaea5d8a0d2", 6 },
                    { 7, "Decent whisky, but nothing extraordinary. Would drink again though.", true, "Not bad", "a8909756-a101-47c5-8d52-085322ffa6e6", 7 },
                    { 8, "Really enjoyed the complexity of flavors in this whisky. A must-try.", true, "Complex flavors", "bca5356a-d5d8-47d7-b314-e74901211b99", 8 },
                    { 9, "Save this one for special occasions. Truly a special whisky.", true, "Perfect for special occasions", "d9ef08e4-4307-4a78-8b8c-afaea5d8a0d2", 9 },
                    { 10, "Was not impressed with this whisky. Expected more.", false, "Not impressed", "a8909756-a101-47c5-8d52-085322ffa6e6", 10 }
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
