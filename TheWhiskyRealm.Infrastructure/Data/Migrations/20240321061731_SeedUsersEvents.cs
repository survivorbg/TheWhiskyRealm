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
                values: new object[] { 1, "1cf4a321-6128-459e-8e4e-e4615c85d30f" });

            migrationBuilder.InsertData(
                table: "UsersEvents",
                columns: new[] { "EventId", "UserId" },
                values: new object[] { 1, "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UsersEvents",
                keyColumns: new[] { "EventId", "UserId" },
                keyValues: new object[] { 1, "1cf4a321-6128-459e-8e4e-e4615c85d30f" });

            migrationBuilder.DeleteData(
                table: "UsersEvents",
                keyColumns: new[] { "EventId", "UserId" },
                keyValues: new object[] { 1, "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4" });
        }
    }
}
