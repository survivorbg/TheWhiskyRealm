using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheWhiskyRealm.Infrastructure.Data
{
    public partial class FixedUsersEventsMappingTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserEvents_AspNetUsers_UserId",
                table: "UserEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_UserEvents_Events_EventId",
                table: "UserEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserEvents",
                table: "UserEvents");

            migrationBuilder.RenameTable(
                name: "UserEvents",
                newName: "UsersEvents");

            migrationBuilder.RenameIndex(
                name: "IX_UserEvents_UserId",
                table: "UsersEvents",
                newName: "IX_UsersEvents_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersEvents",
                table: "UsersEvents",
                columns: new[] { "EventId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UsersEvents_AspNetUsers_UserId",
                table: "UsersEvents",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersEvents_Events_EventId",
                table: "UsersEvents",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersEvents_AspNetUsers_UserId",
                table: "UsersEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersEvents_Events_EventId",
                table: "UsersEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersEvents",
                table: "UsersEvents");

            migrationBuilder.RenameTable(
                name: "UsersEvents",
                newName: "UserEvents");

            migrationBuilder.RenameIndex(
                name: "IX_UsersEvents_UserId",
                table: "UserEvents",
                newName: "IX_UserEvents_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserEvents",
                table: "UserEvents",
                columns: new[] { "EventId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserEvents_AspNetUsers_UserId",
                table: "UserEvents",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserEvents_Events_EventId",
                table: "UserEvents",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
