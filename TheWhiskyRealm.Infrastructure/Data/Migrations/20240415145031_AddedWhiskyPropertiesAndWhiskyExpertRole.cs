using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheWhiskyRealm.Infrastructure.Data
{
    public partial class AddedWhiskyPropertiesAndWhiskyExpertRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dc3cf4ec-f90c-4915-b749-4ab01863fdf6", "7dfb241e-f8a5-4ba4-a5aa-5becf035c442" });

            migrationBuilder.AddColumn<string>(
                name: "PublishedBy",
                table: "Whiskies",
                type: "nvarchar(max)",
                nullable: true,
                comment: "The Id of the user who published the whisky, if not added by Administrator.");

            migrationBuilder.AddColumn<bool>(
                name: "isApproved",
                table: "Whiskies",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "The state of the whisky.");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "77af610e-3202-4bea-8d5c-c20c07f7effe", "3882b86e-4ce3-49e6-83a1-a0294c57a8ff", "WhiskyExpert", "WHISKYEXPERT" });

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

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                column: "OrganiserId",
                value: "7dfb241e-f8a5-4ba4-a5aa-5becf035c442");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                column: "OrganiserId",
                value: "7dfb241e-f8a5-4ba4-a5aa-5becf035c442");

            migrationBuilder.UpdateData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 1,
                column: "isApproved",
                value: true);

            migrationBuilder.UpdateData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 2,
                column: "isApproved",
                value: true);

            migrationBuilder.UpdateData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 3,
                column: "isApproved",
                value: true);

            migrationBuilder.UpdateData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 4,
                column: "isApproved",
                value: true);

            migrationBuilder.UpdateData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 5,
                column: "isApproved",
                value: true);

            migrationBuilder.UpdateData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 6,
                column: "isApproved",
                value: true);

            migrationBuilder.UpdateData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 7,
                column: "isApproved",
                value: true);

            migrationBuilder.UpdateData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 8,
                column: "isApproved",
                value: true);

            migrationBuilder.UpdateData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 9,
                column: "isApproved",
                value: true);

            migrationBuilder.UpdateData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 10,
                column: "isApproved",
                value: true);

            migrationBuilder.UpdateData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 11,
                column: "isApproved",
                value: true);

            migrationBuilder.UpdateData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 12,
                column: "isApproved",
                value: true);

            migrationBuilder.UpdateData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 13,
                column: "isApproved",
                value: true);

            migrationBuilder.UpdateData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 14,
                column: "isApproved",
                value: true);

            migrationBuilder.UpdateData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 15,
                column: "isApproved",
                value: true);

            migrationBuilder.UpdateData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 16,
                column: "isApproved",
                value: true);

            migrationBuilder.UpdateData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 17,
                column: "isApproved",
                value: true);

            migrationBuilder.UpdateData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 18,
                column: "isApproved",
                value: true);

            migrationBuilder.UpdateData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 19,
                column: "isApproved",
                value: true);

            migrationBuilder.UpdateData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 20,
                column: "isApproved",
                value: true);

            migrationBuilder.UpdateData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 21,
                column: "isApproved",
                value: true);

            migrationBuilder.UpdateData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 22,
                column: "isApproved",
                value: true);

            migrationBuilder.UpdateData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 23,
                column: "isApproved",
                value: true);

            migrationBuilder.UpdateData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 24,
                column: "isApproved",
                value: true);

            migrationBuilder.UpdateData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 25,
                column: "isApproved",
                value: true);

            migrationBuilder.UpdateData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 26,
                column: "isApproved",
                value: true);

            migrationBuilder.UpdateData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 27,
                column: "isApproved",
                value: true);

            migrationBuilder.UpdateData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 28,
                column: "isApproved",
                value: true);

            migrationBuilder.UpdateData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 29,
                column: "isApproved",
                value: true);

            migrationBuilder.UpdateData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 30,
                column: "isApproved",
                value: true);

            migrationBuilder.UpdateData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 31,
                column: "isApproved",
                value: true);

            migrationBuilder.UpdateData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 32,
                column: "isApproved",
                value: true);

            migrationBuilder.UpdateData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 33,
                column: "isApproved",
                value: true);

            migrationBuilder.InsertData(
                table: "Whiskies",
                columns: new[] { "Id", "Age", "AlcoholPercentage", "Description", "DistilleryId", "ImageURL", "Name", "PublishedBy", "WhiskyTypeId", "isApproved" },
                values: new object[] { 34, 15, 46.0, "One of the most decorated Irish whiskeys, Redbreast is the largest selling Single Pot Still Irish Whiskey in the world. Regarded as the definitive expression of traditional Pot Still Irish whiskey, Redbreast dates back to 1903 when Jameson entered into an agreement with the Gilbeys Wines & Spirits Import Company to supply them with new make spirit from their Bow St. Distillery. The custom of that era was that distilleries sold bulk whiskey to ‘bonders’ who, being in the business of importing fortified wines such as sherry and port, had ample supplies of empty casks in which to mature new make whiskeys under bond. Redbreast 15 Year Old was first created in 2005 for La Maison du Whisky in Paris, France. Due to the positive attention it received, it is now in regular production at the Midleton Distillery, Co. Cork. It is a 15 year old whiskey and is bottled at 46% ABV.", 175, "https://m.ebag.bg/products/images/125004/800", "Redbreast 15 Year Old", "7dfb241e-f8a5-4ba4-a5aa-5becf035c442", 5, false });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "77af610e-3202-4bea-8d5c-c20c07f7effe", "7dfb241e-f8a5-4ba4-a5aa-5becf035c442" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "77af610e-3202-4bea-8d5c-c20c07f7effe", "7dfb241e-f8a5-4ba4-a5aa-5becf035c442" });

            migrationBuilder.DeleteData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "77af610e-3202-4bea-8d5c-c20c07f7effe");

            migrationBuilder.DropColumn(
                name: "PublishedBy",
                table: "Whiskies");

            migrationBuilder.DropColumn(
                name: "isApproved",
                table: "Whiskies");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "dc3cf4ec-f90c-4915-b749-4ab01863fdf6", "7dfb241e-f8a5-4ba4-a5aa-5becf035c442" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1cf4a321-6128-459e-8e4e-e4615c85d30f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8a8c50c8-2b0d-4e48-bc7b-7bb80bfe5beb", "AQAAAAEAACcQAAAAELEOsQxCJad9eCUlOPh0MEYnOYlWdu6SKhGPSEk0ApBl7fsZ/OXCbM7wINX/gN+y+g==", "0cd08015-2e94-4d55-8842-0ca9bd737963" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7dfb241e-f8a5-4ba4-a5aa-5becf035c442",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c8c39a5e-2bff-41e8-8677-91ee847b2277", "AQAAAAEAACcQAAAAEMWwiPCblJPUP4zZ0iXYJ+WItKj6UHpW4n9HK4R8/L7OUL6ZBHdlHfr9LBpW5FnrsA==", "9e0bdb17-422c-41bd-8c9b-ffa0da906a7f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "28d7a589-90a0-4b19-ae77-a5bbf4cf03a3", "AQAAAAEAACcQAAAAELRoJHXj1Nq11Wu/+qxY3MbH7jjlfjKjhyPEMZqXU6ZOdTj7TGXmPFf5CyMsw3AgRQ==", "c620b951-ebf4-4f61-bdd9-e16c9111ec31" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f99c5e20-d91e-4a5e-9b73-fdb38b89ffc3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "52fa13cb-1c80-4ce5-90e4-e9a87c3f9188", "AQAAAAEAACcQAAAAEBqDsXqDrtpbEjg0cvAcxI/Ix8F85kh0j88sJJWT6oQiUuUqbUC8/UwMvC1EkMtypQ==", "50ad8254-31f2-4c70-a87a-c13a7ae3667f" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                column: "OrganiserId",
                value: "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                column: "OrganiserId",
                value: "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4");
        }
    }
}
