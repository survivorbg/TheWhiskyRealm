using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheWhiskyRealm.Infrastructure.Data
{
    public partial class AddedEntityWhiskyType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WhiskyTypeId",
                table: "Whiskies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "WhiskyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WhiskyTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Whiskies_WhiskyTypeId",
                table: "Whiskies",
                column: "WhiskyTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Whiskies_WhiskyTypes_WhiskyTypeId",
                table: "Whiskies",
                column: "WhiskyTypeId",
                principalTable: "WhiskyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Whiskies_WhiskyTypes_WhiskyTypeId",
                table: "Whiskies");

            migrationBuilder.DropTable(
                name: "WhiskyTypes");

            migrationBuilder.DropIndex(
                name: "IX_Whiskies_WhiskyTypeId",
                table: "Whiskies");

            migrationBuilder.DropColumn(
                name: "WhiskyTypeId",
                table: "Whiskies");
        }
    }
}
