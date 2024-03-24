using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheWhiskyRealm.Infrastructure.Data
{
    public partial class SeedComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "ArticleId", "Content", "PostedDate", "UserId" },
                values: new object[] { 1, 1, "Great article! I learned a lot about the whisky types.", new DateTime(2024, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "1cf4a321-6128-459e-8e4e-e4615c85d30f" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "ArticleId", "Content", "PostedDate", "UserId" },
                values: new object[] { 2, 1, "I completely agree with your list! Can't wait to try these whiskies.", new DateTime(2024, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 3, 21, 7, 16, 5, 47, DateTimeKind.Local).AddTicks(5139));
        }
    }
}
