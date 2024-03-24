using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheWhiskyRealm.Infrastructure.Data
{
    public partial class DistilleryImageUrlMadeNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Distilleries",
                type: "nvarchar(max)",
                nullable: true,
                comment: "The URL address of the image of the distillery.",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "The URL address of the image of the distillery.");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Distilleries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "The URL address of the image of the distillery.",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "The URL address of the image of the distillery.");
        }
    }
}
