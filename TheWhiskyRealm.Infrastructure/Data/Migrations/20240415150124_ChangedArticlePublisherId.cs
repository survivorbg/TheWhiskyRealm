using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheWhiskyRealm.Infrastructure.Data
{
    public partial class ChangedArticlePublisherId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublisherUserId",
                value: "7dfb241e-f8a5-4ba4-a5aa-5becf035c442");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublisherUserId",
                value: "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1cf4a321-6128-459e-8e4e-e4615c85d30f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bacdc739-c7ef-484e-87dc-156d2163ef22", "AQAAAAEAACcQAAAAEI9jK9s2bJyHs3S/hXZ8uQ5FryEDl+ECYpZrDPvw+gZdYJUKIvEigHqieaeG7DAixw==", "c9bae462-6b44-4195-8f7a-dc052aeab86a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7dfb241e-f8a5-4ba4-a5aa-5becf035c442",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ec1260a5-ccc1-4b17-8cef-809545d7a783", "AQAAAAEAACcQAAAAEG+oPnAKKBhHluLOPZs+lS9PGDgdHhgnX6qaG5pc8hJvLNBKJdxFaxuSS8MzIZ8oAA==", "8f543c9c-f183-49d7-ad79-46e6be8c681c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cb99604f-da86-4cd9-8ab8-f91e171ce8ef", "AQAAAAEAACcQAAAAEDw1NU7rkE7+JmGnTqqJmTT4yr/oI3QkgpdsyCpMARS1cmZmKbKympEWn40qzpKLHw==", "9e54ed64-92c5-40ae-9537-05db8f129dd5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f99c5e20-d91e-4a5e-9b73-fdb38b89ffc3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c9d86cc2-b62c-4b3e-b66d-0ecf1530ad72", "AQAAAAEAACcQAAAAEFUznCP05bwomPJq6fcBGnenFvGBkNDikl2pJQ3eKOSG6+4rPRdYS92rnTP+o/eymA==", "2fedde63-3150-45fb-96e5-8f74357bb0cd" });
        }
    }
}
