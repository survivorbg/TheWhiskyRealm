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
                values: new object[] { 1, 1, "Great article! I learned a lot about the whisky types.", new DateTime(2024, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "a8909756-a101-47c5-8d52-085322ffa6e6" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "ArticleId", "Content", "PostedDate", "UserId" },
                values: new object[] { 2, 1, "I completely agree with your list! Can't wait to try these whiskies.", new DateTime(2024, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "2d730ec7-1b14-4bf5-9265-3522e35c06d5" });
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
