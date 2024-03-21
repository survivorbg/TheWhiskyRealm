using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheWhiskyRealm.Infrastructure.Data
{
    public partial class AddedColumnMedalTypeToAwards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MedalType",
                table: "Awards",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "The type of the medal.");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MedalType",
                table: "Awards");
        }
    }
}
