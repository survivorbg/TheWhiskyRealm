using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheWhiskyRealm.Infrastructure.Data
{
    public partial class EventTableAvailableSpotsNotNullRemovedDuration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationInHours",
                table: "Events");

            migrationBuilder.AlterColumn<int>(
                name: "AvailableSpots",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "AvailableSpots",
                value: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AvailableSpots",
                table: "Events",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DurationInHours",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "The duration of the event in hours.");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AvailableSpots", "DurationInHours" },
                values: new object[] { null, 3 });
        }
    }
}
