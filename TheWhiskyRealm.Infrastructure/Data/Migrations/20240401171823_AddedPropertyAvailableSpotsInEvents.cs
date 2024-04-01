using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheWhiskyRealm.Infrastructure.Data
{
    public partial class AddedPropertyAvailableSpotsInEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvailableSpots",
                table: "Events",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableSpots",
                table: "Events");
        }
    }
}
