using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheWhiskyRealm.Infrastructure.Data
{
    public partial class SeedUsersEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UsersEvents",
                columns: new[] { "EventId", "UserId" },
                values: new object[] { 1, "2d730ec7-1b14-4bf5-9265-3522e35c06d5" });

            migrationBuilder.InsertData(
                table: "UsersEvents",
                columns: new[] { "EventId", "UserId" },
                values: new object[] { 1, "bca5356a-d5d8-47d7-b314-e74901211b99" });

            migrationBuilder.InsertData(
                table: "UsersEvents",
                columns: new[] { "EventId", "UserId" },
                values: new object[] { 1, "d9ef08e4-4307-4a78-8b8c-afaea5d8a0d2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UsersEvents",
                keyColumns: new[] { "EventId", "UserId" },
                keyValues: new object[] { 1, "2d730ec7-1b14-4bf5-9265-3522e35c06d5" });

            migrationBuilder.DeleteData(
                table: "UsersEvents",
                keyColumns: new[] { "EventId", "UserId" },
                keyValues: new object[] { 1, "bca5356a-d5d8-47d7-b314-e74901211b99" });

            migrationBuilder.DeleteData(
                table: "UsersEvents",
                keyColumns: new[] { "EventId", "UserId" },
                keyValues: new object[] { 1, "d9ef08e4-4307-4a78-8b8c-afaea5d8a0d2" });
        }
    }
}
