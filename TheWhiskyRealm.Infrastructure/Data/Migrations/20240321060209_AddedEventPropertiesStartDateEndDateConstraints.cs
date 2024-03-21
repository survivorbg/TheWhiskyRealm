using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheWhiskyRealm.Infrastructure.Data
{
    public partial class AddedEventPropertiesStartDateEndDateConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_Event_EndDate",
                table: "Events",
                sql: "[EndDate] > [StartDate]");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Event_StartDate",
                table: "Events",
                sql: "[StartDate] < [EndDate]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Event_EndDate",
                table: "Events");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Event_StartDate",
                table: "Events");
        }
    }
}
