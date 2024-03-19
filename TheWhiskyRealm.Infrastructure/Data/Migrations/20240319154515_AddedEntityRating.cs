using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheWhiskyRealm.Infrastructure.Data
{
    public partial class AddedEntityRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nose = table.Column<int>(type: "int", nullable: false),
                    Taste = table.Column<int>(type: "int", nullable: false),
                    Finish = table.Column<int>(type: "int", nullable: false),
                    WhiskyId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.CheckConstraint("CK_Nose_Max", "[Finish] <= 100");
                    table.CheckConstraint("CK_Nose_Min", "[Finish] >= 1");
                    table.CheckConstraint("CK_Taste_Max", "[Taste] <= 100");
                    table.CheckConstraint("CK_Taste_Min", "[Taste] >= 1");
                    table.ForeignKey(
                        name: "FK_Ratings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_Whiskies_WhiskyId",
                        column: x => x.WhiskyId,
                        principalTable: "Whiskies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_WhiskyId",
                table: "Ratings",
                column: "WhiskyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ratings");
        }
    }
}
