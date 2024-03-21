using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheWhiskyRealm.Infrastructure.Data
{
    public partial class AddedConstraintsForWhiskyAgeAndABV : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_ABV_Max",
                table: "Whiskies",
                sql: "[AlcoholPercentage] <= 94.8");

            migrationBuilder.AddCheckConstraint(
                name: "CK_ABV_Min",
                table: "Whiskies",
                sql: "[AlcoholPercentage] >= 40");

            migrationBuilder.AddCheckConstraint(
                name: "CK_WhiskyAge_Max",
                table: "Whiskies",
                sql: "[Age] <= 99");

            migrationBuilder.AddCheckConstraint(
                name: "CK_WhiskyAge_Min",
                table: "Whiskies",
                sql: "[Age] >= 2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_ABV_Max",
                table: "Whiskies");

            migrationBuilder.DropCheckConstraint(
                name: "CK_ABV_Min",
                table: "Whiskies");

            migrationBuilder.DropCheckConstraint(
                name: "CK_WhiskyAge_Max",
                table: "Whiskies");

            migrationBuilder.DropCheckConstraint(
                name: "CK_WhiskyAge_Min",
                table: "Whiskies");
        }
    }
}
