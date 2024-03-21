using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheWhiskyRealm.Infrastructure.Data
{
    public partial class AddedColumnAwardsCeremonyToAwards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AwardsCeremony",
                table: "Awards",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                comment: "The award ceremony this award was won by the whisky.");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Year_Max",
                table: "Awards",
                sql: "[Year] <= 2024");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Year_Min",
                table: "Awards",
                sql: "[Year] >= 1690");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Year_Max",
                table: "Awards");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Year_Min",
                table: "Awards");

            migrationBuilder.DropColumn(
                name: "AwardsCeremony",
                table: "Awards");
        }
    }
}
