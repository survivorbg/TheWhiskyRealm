using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheWhiskyRealm.Infrastructure.Data
{
    public partial class ChangedUserEventAndEventSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UsersEvents",
                keyColumns: new[] { "EventId", "UserId" },
                keyValues: new object[] { 1, "f99c5e20-d91e-4a5e-9b73-fdb38b89ffc3" });

            migrationBuilder.DeleteData(
                table: "UsersEvents",
                keyColumns: new[] { "EventId", "UserId" },
                keyValues: new object[] { 2, "f99c5e20-d91e-4a5e-9b73-fdb38b89ffc3" });

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

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "AvailableSpots",
                value: 1);

            migrationBuilder.InsertData(
                table: "UsersEvents",
                columns: new[] { "EventId", "UserId" },
                values: new object[] { 2, "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UsersEvents",
                keyColumns: new[] { "EventId", "UserId" },
                keyValues: new object[] { 2, "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1cf4a321-6128-459e-8e4e-e4615c85d30f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "183397d3-3921-4a64-9c04-38d5de3ac927", "AQAAAAEAACcQAAAAEJnQApeKdrcMewe9gMJRox0bgvYH+50mksTTO4gEzxuK1GAQ6CBwwa3xHy23pnLAxw==", "9c7f129b-0246-486b-8208-536c086951b4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7dfb241e-f8a5-4ba4-a5aa-5becf035c442",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "079198b8-a224-45a0-81f7-acae60ac9006", "AQAAAAEAACcQAAAAEBpJHgap3DXKLDZYV74rwnkjnPyQZ3ZwNfFhOPYiY0qu0/RoWYpbaJqV5aOgugHEGg==", "5b5fea60-5978-44bd-acf6-bb5a2b30a5a3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d412a35d-8d70-4526-b808-3df905f84934", "AQAAAAEAACcQAAAAEC3dGwG5pQUDAyCXIjeWb9L5+taQZFq4Loek97RGZZ44bcAjXieY6MhywYMgZobnqw==", "a8b49056-4a0f-4460-b673-7a925e5e0ffd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f99c5e20-d91e-4a5e-9b73-fdb38b89ffc3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dcc71517-4644-45b8-b1db-a539dbbbd98d", "AQAAAAEAACcQAAAAEBsWZa2UAO9zPfnaIcXDixxS46FJbEO87H0mvwht/d+j3pGVdj5x7ODSrAzzmYj28g==", "e9480d95-1164-4a9e-af9d-1d601a58dcdf" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "AvailableSpots",
                value: 0);

            migrationBuilder.InsertData(
                table: "UsersEvents",
                columns: new[] { "EventId", "UserId" },
                values: new object[,]
                {
                    { 1, "f99c5e20-d91e-4a5e-9b73-fdb38b89ffc3" },
                    { 2, "f99c5e20-d91e-4a5e-9b73-fdb38b89ffc3" }
                });
        }
    }
}
