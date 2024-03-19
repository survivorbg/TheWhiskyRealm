using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheWhiskyRealm.Infrastructure.Data
{
    public partial class AddedEntityRatingFinishConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Nose_Max",
                table: "Ratings");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Nose_Min",
                table: "Ratings");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Finish_Max",
                table: "Ratings",
                sql: "[Finish] <= 100");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Finish_Min",
                table: "Ratings",
                sql: "[Finish] >= 1");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Nose_Max",
                table: "Ratings",
                sql: "[Nose] <= 100");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Nose_Min",
                table: "Ratings",
                sql: "[Nose] >= 1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Finish_Max",
                table: "Ratings");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Finish_Min",
                table: "Ratings");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Nose_Max",
                table: "Ratings");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Nose_Min",
                table: "Ratings");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Nose_Max",
                table: "Ratings",
                sql: "[Finish] <= 100");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Nose_Min",
                table: "Ratings",
                sql: "[Finish] >= 1");
        }
    }
}
