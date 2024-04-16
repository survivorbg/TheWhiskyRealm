using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheWhiskyRealm.Infrastructure.Data
{
    public partial class AddedCollectionsToApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1cf4a321-6128-459e-8e4e-e4615c85d30f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "af2dd5f8-fc9f-46d6-a64e-00bc8e2cd1e4", "AQAAAAEAACcQAAAAEHito8VCZNK0UCLGrIPdiSbeQ0eQSKhbUCC80zkH8BvCZo6gESnc/+Utqahp+ncJGA==", "00caeebe-440b-49c5-94eb-e65a7cbeea8b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7dfb241e-f8a5-4ba4-a5aa-5becf035c442",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "430dbc9d-387e-4c7e-8b8d-07f56755a0e3", "AQAAAAEAACcQAAAAELYPsf6z8xWaFjmE5VXn9L0Veko3xmVAFKk6LPzsx048gvxom2saWs0AtnyzO/P6tg==", "2d03447e-2ce9-4eb5-8100-a90d4f9434cc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8f5881ff-52c0-4732-bb01-1f70b5a9da5a", "AQAAAAEAACcQAAAAEPfg1iLTWvP67hhLJhY48RtWZuXJRPzs6Fwk87O1sfKHkYvqpi7nYZvBqEzOEJuCFQ==", "fce34782-a66a-4f6a-bfe1-5b47ea2cca00" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f99c5e20-d91e-4a5e-9b73-fdb38b89ffc3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4fe4b222-eb0e-4ee4-84b0-08be1e1d5f81", "AQAAAAEAACcQAAAAEIqIRWNrMkbTpkWgdq9aKMp49JLDTk4kguaq1Vr+MADK2yh1SxSYCxpIZF9DDVfzpw==", "87f1151c-8e94-448e-bafb-68384cacc1cc" });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ApplicationUserId",
                table: "Comments",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_ApplicationUserId",
                table: "Comments",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_ApplicationUserId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ApplicationUserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Comments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1cf4a321-6128-459e-8e4e-e4615c85d30f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5f7586ba-932b-41d3-b0b1-3f69205fefc7", "AQAAAAEAACcQAAAAEGx6Ed8990Qg3z/WYMhwbVsoMgwlWlx9CRBarxYpulpYng8ABsWyFJVv9jqqfVPwwA==", "9bd46c16-661f-4a30-ba78-8b642bf5be7e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7dfb241e-f8a5-4ba4-a5aa-5becf035c442",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "75034623-7ce5-4203-8bbc-646af754ad3f", "AQAAAAEAACcQAAAAEHbD0oY+JACTywdWbhixjJq+lRfSX9wQ74gd9uqi6lZkRsjNFcEYsrhmqH9jB7zbCg==", "6d7a0003-9222-4d93-8477-1c98f556d467" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f2b7dee2-28b5-4fc2-80b5-98cda3035d83", "AQAAAAEAACcQAAAAEDtbbdy4H2zCOWF2yJ3gcKpqKgjxxSj1VzR1uvSiuFF0gwbMv9zBvtnYZAwz6LtzCw==", "8918fa73-4d57-4739-9a35-30e602db07ee" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f99c5e20-d91e-4a5e-9b73-fdb38b89ffc3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fc679fad-de11-4261-a0b7-e1d11b75e380", "AQAAAAEAACcQAAAAEB8NEGxrk3B3Rp/pu/AiEel1l1FGrthLOMDbAwHTykdSl0D35H2fEKB5KnN9nyRWog==", "3e172cbe-0e2f-4f5c-9758-ce261bb753ac" });
        }
    }
}
