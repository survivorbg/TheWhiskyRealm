using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheWhiskyRealm.Infrastructure.Data
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "The unique identifier of the article.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "The title of the article."),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false, comment: "The content of the article."),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "The URL of the image associated with the article."),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The date the article was created."),
                    Type = table.Column<int>(type: "int", nullable: false, comment: "The type of the article."),
                    PublisherUserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "The identifier of the user who published the article.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_AspNetUsers_PublisherUserId",
                        column: x => x.PublisherUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Represents an article entity.");

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "The unique identifier of the country.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(62)", maxLength: 62, nullable: false, comment: "The name of the country.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                },
                comment: "Represents a country entity.");

            migrationBuilder.CreateTable(
                name: "WhiskyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "The unique identifier of the whisky type.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false, comment: "The name of the whisky type."),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "The description of the whisky type.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WhiskyTypes", x => x.Id);
                },
                comment: "Represents a whisky type entity.");

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "The unique identifier of the comment.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false, comment: "The content of the comment."),
                    PostedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The date the comment was posted."),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "The identifier of the user who posted the comment."),
                    ArticleId = table.Column<int>(type: "int", nullable: false, comment: "The identifier of the article associated with the comment.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Represents a comment entity.");

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "The unique identifier of the city.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(187)", maxLength: 187, nullable: false, comment: "The name of the city."),
                    ZipCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, comment: "The zip code of the city."),
                    CountryId = table.Column<int>(type: "int", nullable: false, comment: "The identifier of the country that the city belongs to.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Represents a city entity.");

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "The unique identifier of the region.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(85)", maxLength: 85, nullable: false, comment: "The name of the region."),
                    CountryId = table.Column<int>(type: "int", nullable: false, comment: "The identifier of the country that the region belongs to.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Regions_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Represents a region entity.");

            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "The unique identifier of the venue.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "The name of the venue."),
                    Capacity = table.Column<int>(type: "int", nullable: false, comment: "The capacity of the venue."),
                    CityId = table.Column<int>(type: "int", nullable: false, comment: "The identifier of the city where the venue is located.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.Id);
                    table.CheckConstraint("CK_Capacity_Max", "[Capacity] <= 100");
                    table.CheckConstraint("CK_Capacity_Min", "[Capacity] >= 3");
                    table.ForeignKey(
                        name: "FK_Venues_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Represents a venue entity.");

            migrationBuilder.CreateTable(
                name: "Distilleries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "The unique identifier of the distillery.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(57)", maxLength: 57, nullable: false, comment: "The name of the distillery."),
                    YearFounded = table.Column<int>(type: "int", nullable: false, comment: "The year the distillery was founded."),
                    RegionId = table.Column<int>(type: "int", nullable: false, comment: "The identifier of the region where the distillery is located."),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "The URL address of the image of the distillery.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distilleries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Distilleries_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Represents a distillery entity.");

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "The unique identifier of the event.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false, comment: "The title of the event."),
                    Description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false, comment: "The description of the event."),
                    OrganiserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "The identifier of the user who organised the event."),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The start date of the event."),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The end date of the event."),
                    AvailableSpots = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, comment: "The price of the event."),
                    VenueId = table.Column<int>(type: "int", nullable: false, comment: "The identifier of the venue where the event will take place.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.CheckConstraint("CK_Event_EndDate", "[EndDate] > [StartDate]");
                    table.CheckConstraint("CK_Event_StartDate", "[StartDate] < [EndDate]");
                    table.ForeignKey(
                        name: "FK_Events_AspNetUsers_OrganiserId",
                        column: x => x.OrganiserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Represents an event entity.");

            migrationBuilder.CreateTable(
                name: "Whiskies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "The unique identifier of the whisky.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false, comment: "The name of the whisky."),
                    Age = table.Column<int>(type: "int", nullable: true, comment: "The age of the whisky."),
                    AlcoholPercentage = table.Column<double>(type: "float", nullable: false, comment: "The alcohol percentage of the whisky."),
                    Description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false, comment: "The description of the whisky."),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "The URL of the whisky image."),
                    DistilleryId = table.Column<int>(type: "int", nullable: false, comment: "The identifier of the distillery that produced the whisky."),
                    WhiskyTypeId = table.Column<int>(type: "int", nullable: false, comment: "The identifier of the type of the whisky.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Whiskies", x => x.Id);
                    table.CheckConstraint("CK_ABV_Max", "[AlcoholPercentage] <= 94.8");
                    table.CheckConstraint("CK_ABV_Min", "[AlcoholPercentage] >= 40");
                    table.CheckConstraint("CK_WhiskyAge_Max", "[Age] <= 99");
                    table.CheckConstraint("CK_WhiskyAge_Min", "[Age] >= 2");
                    table.ForeignKey(
                        name: "FK_Whiskies_Distilleries_DistilleryId",
                        column: x => x.DistilleryId,
                        principalTable: "Distilleries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Whiskies_WhiskyTypes_WhiskyTypeId",
                        column: x => x.WhiskyTypeId,
                        principalTable: "WhiskyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Represents a whisky entity.");

            migrationBuilder.CreateTable(
                name: "UsersEvents",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "The identifier of the user associated with the event."),
                    EventId = table.Column<int>(type: "int", nullable: false, comment: "The identifier of the event associated with the user.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersEvents", x => new { x.EventId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UsersEvents_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersEvents_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Represents a mapping entity between a user and an event.");

            migrationBuilder.CreateTable(
                name: "Awards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "The unique identifier of the award.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false, comment: "The title of the award."),
                    AwardsCeremony = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false, comment: "The award ceremony this award was won by the whisky."),
                    MedalType = table.Column<int>(type: "int", nullable: false, comment: "The type of the medal."),
                    Year = table.Column<int>(type: "int", nullable: false, comment: "The year the award was given."),
                    WhiskyId = table.Column<int>(type: "int", nullable: false, comment: "The identifier of the whisky associated with the award.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Awards", x => x.Id);
                    table.CheckConstraint("CK_Year_Max", "[Year] <= 2024");
                    table.CheckConstraint("CK_Year_Min", "[Year] >= 1690");
                    table.ForeignKey(
                        name: "FK_Awards_Whiskies_WhiskyId",
                        column: x => x.WhiskyId,
                        principalTable: "Whiskies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Represents an award entity.");

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "The unique identifier of the rating.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nose = table.Column<int>(type: "int", nullable: false, comment: "Represents the rating that is given for the whisky aroma."),
                    Taste = table.Column<int>(type: "int", nullable: false, comment: "Represents the rating that is given for the whisky taste."),
                    Finish = table.Column<int>(type: "int", nullable: false, comment: "Represents the rating that is given for the whisky finishing notes."),
                    WhiskyId = table.Column<int>(type: "int", nullable: false, comment: "The identifier of the whisky being rated."),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "The identifier of the user who gave the rating.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.CheckConstraint("CK_Finish_Max", "[Finish] <= 100");
                    table.CheckConstraint("CK_Finish_Min", "[Finish] >= 1");
                    table.CheckConstraint("CK_Nose_Max", "[Nose] <= 100");
                    table.CheckConstraint("CK_Nose_Min", "[Nose] >= 1");
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
                },
                comment: "Represents a rating entity.");

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "The unique identifier of the review.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "The title of the review."),
                    Content = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false, comment: "The content of the review."),
                    Recommend = table.Column<bool>(type: "bit", nullable: false, comment: "If the user recommends status the whisky"),
                    WhiskyId = table.Column<int>(type: "int", nullable: false, comment: "The identifier of the whisky being reviewed."),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "The identifier of the user who made the review.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Whiskies_WhiskyId",
                        column: x => x.WhiskyId,
                        principalTable: "Whiskies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Represents a review entity.");

            migrationBuilder.CreateTable(
                name: "UsersWhiskies",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WhiskyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersWhiskies", x => new { x.UserId, x.WhiskyId });
                    table.ForeignKey(
                        name: "FK_UsersWhiskies_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersWhiskies_Whiskies_WhiskyId",
                        column: x => x.WhiskyId,
                        principalTable: "Whiskies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1cf4a321-6128-459e-8e4e-e4615c85d30f", 0, "9f683785-82cc-43e5-b4e3-b87584437d05", new DateTime(2004, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "noToAlcohol@gmail.com", true, false, null, "NOTOALCOHOL@GMAIL.COM", "NOTOALCOHOL@GMAIL.COM", "AQAAAAEAACcQAAAAECmIyC+4mduVSVgLcEy6vUxOJXbahaztikT1/85ZjCXJDrN8Eo8oKyPlQU5gnm6PkA==", null, false, "d96b9420-e2d0-4b4e-8074-b5984a7dbb57", false, "noToAlcohol@gmail.com" },
                    { "7dfb241e-f8a5-4ba4-a5aa-5becf035c442", 0, "9fe5a7ed-8ed1-4f55-afb6-d416653dcb06", new DateTime(1995, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "sober@gmail.com", true, false, null, "SOBER@GMAIL.COM", "SOBER@GMAIL.COM", "AQAAAAEAACcQAAAAECq71RlyG494h2sX2Kb7tSNUgzUx+H+a0a2+CNaKkbom9l+evz05dYq/W2Pu6k30iQ==", null, false, "f55db157-4998-472b-b389-ee3b2858f9bb", false, "sober@gmail.com" },
                    { "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4", 0, "74571395-9a6a-45ed-8c48-bd92fcb89d71", new DateTime(1994, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "test@gmail.com", true, false, null, "TEST@GMAIL.COM", "TEST@GMAIL.COM", "AQAAAAEAACcQAAAAEI934Fmok8sYMqeqOp19SnQGraqTEWMiLVROhKrp7f5kBfLdfsl/dmQ2WXX99hvgrg==", null, false, "63ef3377-71d0-44f4-b5f6-850f0178d668", false, "test@gmail.com" },
                    { "f99c5e20-d91e-4a5e-9b73-fdb38b89ffc3", 0, "376bcaa1-286a-4694-9174-01eac5576c83", new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", true, false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAEAACcQAAAAEHfxC9CIKtjVe9fyceYVKudr9GCmuwrryH5keAFiR0RE3xipjPCoylayS/hggCrMbg==", null, false, "cd7b6a5c-0a1b-4c08-a5cf-618b077344f0", false, "admin@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Scotland" },
                    { 2, "Ireland" },
                    { 3, "United States" },
                    { 4, "Japan" },
                    { 5, "Taiwan" },
                    { 6, "India" },
                    { 7, "Canada" },
                    { 8, "Germany" },
                    { 9, "Finland" },
                    { 10, "Australia" },
                    { 11, "Bulgaria" }
                });

            migrationBuilder.InsertData(
                table: "WhiskyTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Single malt whisky is produced by a single distillery using a single malted grain (typically barley).", "Single Malt" },
                    { 2, "Whisky that is created by blending together multiple whiskies from different sources. These sources can include malt whisky and grain whisky from various distilleries and regions.", "Blended" },
                    { 3, "An American whiskey made primarily from corn and aged in new charred oak barrels. Opposed to Scotch, Irish and Japanese whiskies, bourbon must be matured only minimum of 2 years.", "Bourbon" },
                    { 4, "An American whiskey made primarily from rye grain(at least 51%), offering a spicier flavor profile. Must also be matured in new, charred oak casks.", "Rye" },
                    { 5, "A whiskey made in Ireland and Northern Ireland, known for its smoothness and triple distillation process. Minimum alcohol strength must be 40% ABV with a minimum three-year maturation period.", "Irish" },
                    { 6, "Japanese whisky is a meticulously crafted spirit made using malted grains and other cereals, with water sourced exclusively from Japan. Production, including mashing, fermentation, and distillation, must occur in Japanese distilleries. Maturation in wooden casks stored in Japan for a minimum of three years enhances the whisky's flavor complexity. Bottling is strictly done in Japan, ensuring a minimum strength of 40% alcohol by volume (abv).", "Japanese" },
                    { 7, "Canadian whisky is a type of whisky that is distilled and aged in Canada. Often referred to as \"rye whisky\" or simply \"rye\" in Canada, but it differs from the American Rye Whiskey in that it isn’t really defined. Technically speaking, it didn’t even need to have any rye in it at all, although that is not a common scenario. ", "Canadian" }
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Content", "DateCreated", "ImageUrl", "PublisherUserId", "Title", "Type" },
                values: new object[,]
                {
                    { 1, "Know your bourbon from your scotch (and much more!) in this beginner's guide to the most popular types of whiskey.\r\n\r\nThe sheer number of types of whiskey in the liquor store might have you stumped. What’s the difference between Irish whiskey and Scotch whisky? Is all bourbon whiskey? What whiskey is best for your favorite mixed drinks?\r\n\r\nYou’ll find everything you need to know in the guide below!\r\n\r\nBy the way, is it whiskey or whisky?\r\nThat depends where it’s made. Yes, whisk(e)y can be spelled both with an “e” and without, which does confuse even the most seasoned drinkers. But, it turns out the letter is very important to the story of the spirit. The Irish use the “e,” a tradition that carried over to American-made whiskeys. The Scots do not use the “e,” and distillers in Canada and Japan follow their lead. Hence, whisky or whiskies.\r\n\r\nSo now, without further ado, here are the types of whiskey you need to know:\r\n\r\nIrish Whiskey\r\nIrish whiskey has a smoother flavor than other types of whiskey. It’s made from a mash of malt, can only be distilled using water and caramel coloring, and must be distilled in wooden casks for at least three years. The result is a whiskey that’s easy to sip neat or on the rocks, though you can use Irish whiskey to make cocktails.\r\n\r\nScotch Whisky\r\nScotch whisky (aka just scotch) is made in Scotland with either malt or grain. The Scots take their whisky-making seriously and have laws in place that distillers must follow. The spirit must age in an oak barrel for at least three years. Plus, each bottle must have an age statement which reflects the youngest aged whisky used to make that blend. This is a whisky to sip neat—it makes an excellent after-dinner drink.\r\n\r\nJapanese Whisky\r\nA little later to the game than Irish and scotch, Japanese whisky has made its mark on the spirits world for its high standards. Japanese whisky was created to taste as close to the scotch style as possible and uses similar distilling methods. It is mostly imbibed in mixed drinks or with a splash of soda.\r\n\r\nCanadian Whisky\r\nLike scotch, Canadian whisky must be barrel-aged for at least three years. It’s lighter and smoother than other types of whiskey because it contains a high percentage of corn. You will find that most Canadian whiskies are made from corn and rye, but other may feature wheat or barley.\r\n\r\nBourbon Whiskey\r\nAn American-style whiskey, bourbon is made from corn. In fact, to be called bourbon whiskey, the spirit needs to be made from at least 51% corn, aged in a new oak barrel and produced in America. It has no minimum aging period and needs to be bottled at 80 proof or more.\r\n\r\nBourbon is the star ingredient in mint juleps—and you don’t have to wait for the Kentucky Derby to learn how to make one.\r\n\r\nTennessee Whiskey\r\nWhile Tennessee whiskey is technically classified as bourbon, some distillers in the state aren’t too keen on that. Instead, they use Tennessee whiskey to define their style. All current Tennessee whiskey producers are required by state law to produce their whiskeys in Tennessee and to use a filtering step known as the Lincoln County Process prior to aging the whiskey.\r\n\r\nRye Whiskey\r\nRye whiskey is made in America with at least 51% rye, while other ingredients include corn and barley. It follows the distilling process of bourbon. Rye that has been aged for two or more years and has not been blended is dubbed “straight rye whiskey.” Rye tends to have a spicier flavor than sweeter, smoother bourbon.\r\n\r\nBlended Whiskey\r\nBlended whiskey is exactly what the name highlights—it’s a mixture of different types of whiskey, as well as colorings, flavors and even other grains. These types of whiskeys are ideal for cocktails, as the process allows for the flavor to come through but keeps the spirit at a lower price point.\r\n\r\nSingle Malt Whisky\r\nSingle malt whisky needs to be made from one batch of scotch at a single distillery. Additionally, it must be aged for three years in oak before being bottled. The term “single malt” comes from the ingredients, as the main ingredient is malted barley. However, these rules did not make their way to U.S. distilleries. For example, in America, single malt is sometimes made from rye and not barley.", new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.tasteofhome.com/wp-content/uploads/2019/08/bottles-of-scotch-whiskey-on-shelf-shutterstock_283026071.jpg?fit=1024,640", "7dfb241e-f8a5-4ba4-a5aa-5becf035c442", "Types of Whiskey", 1 },
                    { 2, "\r\n\r\nBushmills is home to the world’s oldest licensed whiskey distillery with official records dating back to 1608, when the area was granted its license to distil. Over 400 years later, whiskey is still being made in Bushmills, thanks to experience and craft passed down from generation to generation. \r\n\r\n \r\n\r\nBushmills is more than just a whiskey, they believe it is a village, where family, friends and neighbours work side by side at the distillery. The team often say, “without the village there would be no whiskey, and without the whiskey there would be no village”. Therefore, Bushmills is named for the mills that dotted the town all along the River Bush where they get the water that flows over beds of basalt rock for their whiskys.  \r\n\r\n1850s\r\n\r\nThe Crown imposed a tax on those distilling in Ireland through a tax on barley. Even then, malted barley was known throughout the world to make the finest whiskey, known as “pure malt” whiskey. When only malted barley is used in distillation, and made at a single distillery, you have the very definition of “single malt” whiskey. That tax, however, forever changed Irish whiskey, as almost every Irish whiskey distillery began substituting corn or other inferior grains for barley. However, not Bushmills. To this day, Bushmills continues to distil single malt whiskey at the world’s oldest licensed whiskey distillery.\r\n1885\r\n\r\nA disastrous fire destroyed The Old Bushmills Distillery but they pulled together and soon rebuilt the distillery and went back in full production to meet soaring US demand. \r\n\r\n \r\n\r\nBushmills’ celebrated malt whiskey won numerous prizes in international spirits competitions, including the only gold medal for whiskey at the Paris 1889 Expo.\r\n1920s\r\n\r\nWhen Prohibition hit, it brought US shipments to a screeching halt. Hundreds of distilleries were reduced to a mere handful. They had a feeling this dry spell wouldn’t last forever, and, like the fires, famines or wars that came before, so they kept the whiskey flowing. \r\n\r\n \r\n\r\nWith the repeal of prohibition, Bushmills reportedly set sail for Chicago with the biggest shipment of whiskey ever to leave an Irish port.\r\n\r\n1950s\r\n\r\nBushmills’ fame grew, making it a fixture in popular culture, appearing in classic films and memorable advertising campaigns. This success has continued into today. \r\n", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://bayviewhotelni.com/app/uploads/2021/09/tourism_slide__0013_Layer-2.jpg", "7dfb241e-f8a5-4ba4-a5aa-5becf035c442", "Bushmills: Oldest Distillery in the World", 1 },
                    { 3, "I recently popped by Auchentoshan distillery whilst I was passing through the region. It’s a charming if curious place. Charming because it is a white-washed, manicured lawn, well-kept picture book of a distillery. Curious because such a picturesque place is wedged within the confines of a housing estate in Clydebank, on the outskirts of Glasgow. I’m sure back in 1825, when this Lowland distillery was founded by Irish refugees, there weren’t too many houses around and the location seemed like a good idea at the time.\r\n\r\nAuchentoshan Distillery\r\n\r\nWhilst I was there I had a look around – it’s got a nice distillery shop, with a decent tour to cater for the many passing tourists, and that tour finishes up in a stylish bar. As I was driving there was no sampling involved, but I did come away with a couple of dram samples to try later. One of those was a distillery-only wine cask Auchentoshan, which was far more interesting. In fact, if you ever head there then I reckon the most interesting thing you’ll find will be whatever special cask they have lined up, and which you can’t get anywhere else.\r\n\r\nI also came away with a sample of the Auchentoshan Three Wood, which was one of the core releases I hadn’t actually tried. Now, most of Auchentoshan’s core range is incredibly dull so I was prepared to fall asleep whilst drinking it. The Auchentoshan Three Wood is bottled at 43% ABV after being matured in a mixture of Pedro Ximénez sherry casks, bourbon casks and Oloroso sherry casks.\r\n\r\nAuchentoshan Three Wood\r\nAuchentoshan Three Wood Tasting Notes\r\n\r\nColour: tawny.\r\n\r\nOn the nose: very handsome aromas. Turkish delight. Sauternes or fino sherry. Maple syrup on pancakes. Raisins, figs, sultanas. Touch of cinnamon. Stem ginger. Mixed peel. Pastry – almost a mince-pie quality. Marzipan.\r\n\r\nIn the mouth: much of the same as the nose, with plenty of the dried fruits and maple syrup. There’s a bit of wood bitterness that doesn’t exactly balance the sweeter notes, but certainly contrasts with it. A bit of stewed apple, barley sugar. Cinnamon. Warming ginger and pepper. Grassy. Blood oranges, and quite tart at times. Plums. Chocolate. Nice, but not complex.\r\nConclusions\r\n\r\nThe underlying spirit doesn’t quite cut the mustard for the strong sherry influence. It’s not dense enough, in my books, to really make the most of the more robust sherry cask flavours, but it does at least make Auchentoshan Three Wood perhaps the most interesting of a dull core range. At £45 a bottle, I can imagine this being a very pleasant to have on the shelf as an everyday drinker.", new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.bourbonbanter.com/content/images/wp-content/uploads/2019/03/auchentoshan-3-wood-single-malt-scotch-whisky-review-header.jpg", "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4", "Review: Auchentoshan Three Wood", 3 }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name", "ZipCode" },
                values: new object[,]
                {
                    { 1, 11, "Sofia", "1000" },
                    { 2, 11, "Plovdiv", "4000" },
                    { 3, 11, "Varna", "9000" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Lowland" },
                    { 2, 1, "Highland" },
                    { 3, 1, "Speyside" },
                    { 4, 1, "Island" },
                    { 5, 1, "Campbeltown" },
                    { 6, 1, "Islay" },
                    { 7, 2, "County Mayo" },
                    { 8, 2, "County Kilkenny" },
                    { 9, 2, "County Donegal" },
                    { 10, 2, "County Waterford" },
                    { 11, 2, "County Meath" },
                    { 12, 2, "County Fermanagh" },
                    { 13, 2, "County Clare" },
                    { 14, 2, "County Cork" },
                    { 15, 2, "County Louth" },
                    { 16, 2, "County Down" },
                    { 17, 2, "County Kerry" },
                    { 18, 2, "County Wicklow" },
                    { 19, 2, "County Westmeath" },
                    { 20, 2, "County Sligo" },
                    { 21, 2, "County Antrim" },
                    { 22, 2, "County Carlow" },
                    { 23, 2, "County Leitrim" },
                    { 24, 2, "County Tipperary" },
                    { 25, 2, "County Offaly" },
                    { 26, 3, "Kentucky" },
                    { 27, 3, "Tennessee" },
                    { 28, 4, "Saitama" },
                    { 29, 4, "Hyogo" },
                    { 30, 4, "Shizuoka" },
                    { 31, 4, "Yamanashi" },
                    { 32, 4, "Nagano" },
                    { 33, 4, "Miyagi" },
                    { 34, 4, "Osaka" },
                    { 35, 4, "Hokkaido" },
                    { 36, 5, "Yilan County" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[] { 37, 6, "Karnataka" });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[] { 38, 7, "Ontario" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "ArticleId", "Content", "PostedDate", "UserId" },
                values: new object[,]
                {
                    { 1, 1, "Great article! I learned a lot about the whisky types.", new DateTime(2024, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "1cf4a321-6128-459e-8e4e-e4615c85d30f" },
                    { 2, 1, "I completely agree with your list! Can't wait to try these whiskies.", new DateTime(2024, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4" },
                    { 3, 3, "Auchentoshan Three Wood is one of the my favourite whiskies!", new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4" },
                    { 4, 3, "In some aspect i agree with you, but overall i don't!", new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "1cf4a321-6128-459e-8e4e-e4615c85d30f" }
                });

            migrationBuilder.InsertData(
                table: "Distilleries",
                columns: new[] { "Id", "ImageUrl", "Name", "RegionId", "YearFounded" },
                values: new object[,]
                {
                    { 1, null, "Aberargie", 1, 2017 },
                    { 2, null, "Aberfeldy", 2, 1896 },
                    { 3, null, "Aberlour", 3, 1879 },
                    { 4, null, "Abhainn Dearg", 4, 2008 },
                    { 5, null, "Ailsa Bay", 1, 2009 },
                    { 6, null, "Allt-A-Bhainne", 3, 1975 },
                    { 7, null, "Annandale", 1, 1836 },
                    { 8, null, "Arbikie", 2, 2013 },
                    { 9, null, "Ardbeg", 6, 1815 },
                    { 10, null, "Ardmore", 2, 1898 },
                    { 11, null, "Ardnahoe", 6, 2019 },
                    { 12, null, "Ardnamurchan", 2, 2014 },
                    { 13, null, "Ardross", 2, 2019 },
                    { 14, null, "Arran", 4, 1995 },
                    { 15, "https://keyassets.timeincuk.net/inspirewp/live/wp-content/uploads/sites/34/2022/06/Auchentoshan-Distillery-920x609.jpg", "Auchentoshan", 1, 1823 },
                    { 16, null, "Auchroisk", 3, 1972 },
                    { 17, null, "Aultmore", 3, 1895 },
                    { 18, null, "Balblair", 2, 1790 },
                    { 19, null, "Ballindalloch", 3, 2014 },
                    { 20, null, "Balmenach", 3, 1824 },
                    { 21, null, "Balvenie", 3, 1892 },
                    { 22, null, "Ben Nevis", 2, 1825 },
                    { 23, null, "BenRiach", 3, 1898 },
                    { 24, null, "Benrinnes", 3, 1826 },
                    { 25, null, "Benromach", 3, 1898 },
                    { 26, null, "Bladnoch", 1, 1817 },
                    { 27, null, "Blair Athol", 2, 1798 },
                    { 28, null, "Bonnington", 1, 2019 },
                    { 29, null, "Borders", 1, 2018 },
                    { 30, null, "Bowmore", 6, 1779 },
                    { 31, null, "Braeval", 3, 1973 },
                    { 32, null, "Brora", 2, 1819 },
                    { 33, null, "Bruichladdich", 6, 1881 },
                    { 34, null, "Bunnahabhain", 6, 1881 },
                    { 35, null, "Burn O Bennie", 2, 2021 },
                    { 36, null, "Cairn", 3, 2022 },
                    { 37, null, "Caol Ila", 6, 1846 },
                    { 38, null, "Cardhu", 3, 1824 }
                });

            migrationBuilder.InsertData(
                table: "Distilleries",
                columns: new[] { "Id", "ImageUrl", "Name", "RegionId", "YearFounded" },
                values: new object[,]
                {
                    { 39, null, "Clydeside", 1, 2017 },
                    { 40, null, "Clynelish", 2, 1967 },
                    { 41, null, "Crafty", 1, 2017 },
                    { 42, null, "Cragganmore", 3, 1869 },
                    { 43, null, "Craigellachie", 3, 1891 },
                    { 44, null, "Daftmill", 1, 2005 },
                    { 45, null, "Dailuaine", 3, 1852 },
                    { 46, null, "Dalmore", 2, 1839 },
                    { 47, null, "Dalmunach", 3, 2015 },
                    { 48, null, "Dalwhinnie", 2, 1898 },
                    { 49, null, "Deanston", 2, 1965 },
                    { 50, null, "Dornoch", 2, 2016 },
                    { 51, null, "Dufftown", 3, 1895 },
                    { 52, null, "Dunphail", 3, 2023 },
                    { 53, null, "Eden Mill", 1, 2012 },
                    { 54, "https://thewhiskyphiles.files.wordpress.com/2018/11/edradour-distillery.jpg", "Edradour", 2, 1825 },
                    { 55, null, "Falkirk", 1, 2020 },
                    { 56, null, "Fettercairn", 2, 1824 },
                    { 57, null, "Glasgow", 1, 2015 },
                    { 58, null, "Glenallachie", 3, 1967 },
                    { 59, null, "Glenburgie", 3, 1810 },
                    { 60, null, "Glencadam", 2, 1825 },
                    { 61, "https://www.thespiritsbusiness.com/content/uploads/2022/07/GlenDronach.jpg", "Glendronach", 2, 1826 },
                    { 62, null, "Glendullan", 3, 1897 },
                    { 63, null, "Glen Elgin", 3, 1898 },
                    { 64, null, "Glenfarclas", 3, 1836 },
                    { 65, "https://www.distillerytours.scot/uploads/store/mediaupload/1601/image/xl_limit-20210807_Glenfiddich_0391.jpg", "Glenfiddich", 3, 1886 },
                    { 66, null, "Glen Garioch", 2, 1797 },
                    { 67, null, "Glenglassaugh", 2, 1875 },
                    { 68, null, "Glengoyne", 2, 1833 },
                    { 69, null, "Glen Grant", 3, 1840 },
                    { 70, null, "Glengyle", 5, 1872 },
                    { 71, null, "Glen Keith", 3, 1957 },
                    { 72, null, "Glenkinchie", 1, 1837 },
                    { 73, null, "Glenlivet", 3, 1824 },
                    { 74, null, "Glenlossie", 3, 1876 },
                    { 75, "https://www.secret-scotland.com/datafiles/uploaded/cmsRefImage/popularPlaces/additional/main/main_110_Glenmorangie.jpg", "Glenmorangie", 2, 1843 },
                    { 76, "https://www.distillerytours.scot/uploads/store/mediaupload/1315/image/xl_limit-Still_House.jpg", "Glen Moray", 3, 1897 },
                    { 77, null, "Glen Ord", 2, 1838 },
                    { 78, null, "Glenrothes", 3, 1879 },
                    { 79, null, "Glen Scotia", 5, 1832 },
                    { 80, null, "Glen Spey", 3, 1878 }
                });

            migrationBuilder.InsertData(
                table: "Distilleries",
                columns: new[] { "Id", "ImageUrl", "Name", "RegionId", "YearFounded" },
                values: new object[,]
                {
                    { 81, null, "Glentauchers", 3, 1897 },
                    { 82, null, "Glenturret", 2, 1763 },
                    { 83, null, "GlenWyvis", 2, 2015 },
                    { 84, null, "Harris", 4, 2015 },
                    { 85, "https://www.highlandparkwhisky.com/sites/g/files/jrulke331/files/styles/text_structured_image/public/Highland%20Park%20Distillery.jpg?itok=OxqQUxKL", "Highland Park", 4, 1798 },
                    { 86, null, "Holyrood", 1, 2019 },
                    { 87, null, "Inchdairnie", 1, 2016 },
                    { 88, null, "Inchgower", 3, 1871 },
                    { 89, null, "Jackton", 1, 2020 },
                    { 90, null, "Jura", 4, 1810 },
                    { 91, null, "Kilchoman", 6, 2005 },
                    { 92, null, "Kimbland", 4, 2017 },
                    { 93, null, "Kingsbarns", 1, 2014 },
                    { 94, null, "Kininvie", 3, 1990 },
                    { 95, null, "Knockando", 3, 1898 },
                    { 96, null, "Knockdhu", 3, 1894 },
                    { 97, null, "Lagg", 4, 2019 },
                    { 98, null, "Lagavulin", 6, 1816 },
                    { 99, "https://www.laphroaig.com/sites/default/files/styles/style_20_9/public/2022-06/Laphroaig_Distillery_banner_DT.jpg.webp?itok=k-vrPwBD", "Laphroaig", 6, 1815 },
                    { 100, null, "Leven", 1, 2013 },
                    { 101, null, "Lindores Abbey", 1, 2017 },
                    { 102, null, "Linkwood", 3, 1821 },
                    { 103, null, "Lochlea", 1, 2018 },
                    { 104, null, "Loch Lomond", 2, 1964 },
                    { 105, null, "Lone Wolf", 2, 2016 },
                    { 106, null, "Longmorn", 3, 1893 },
                    { 107, "https://www.robertson.co.uk/sites/default/files/styles/news_header/public/news_images/Macallan%20press%20image%20POM2018124G0800208003.jpg?itok=wyUPa5o2", "The Macallan", 3, 1824 },
                    { 108, null, "Macduff", 2, 1960 },
                    { 109, null, "Mannochmore", 3, 1971 },
                    { 110, null, "Miltonduff", 3, 1824 },
                    { 111, null, "Mortlach", 3, 1823 },
                    { 112, null, "Nc nean", 2, 2017 },
                    { 113, null, "Oban", 2, 1794 },
                    { 114, null, "Port Ellen", 6, 1825 },
                    { 115, null, "Port of Leith", 1, 2023 },
                    { 116, null, "Pulteney", 2, 1826 },
                    { 117, null, "Raasay", 4, 2014 },
                    { 118, null, "Rosebank", 1, 1798 },
                    { 119, null, "Roseisle", 3, 2010 },
                    { 120, null, "Royal Brackla", 2, 1812 },
                    { 121, null, "Royal Lochnagar", 2, 1845 },
                    { 122, null, "Saxa Vord", 4, 2015 }
                });

            migrationBuilder.InsertData(
                table: "Distilleries",
                columns: new[] { "Id", "ImageUrl", "Name", "RegionId", "YearFounded" },
                values: new object[,]
                {
                    { 123, null, "Scapa", 4, 1885 },
                    { 124, null, "Speyburn", 3, 1897 },
                    { 125, null, "Speyside", 3, 1990 },
                    { 126, "https://www.whisky.com/fileadmin/_processed_/4/5/csm__MG_5906_732d67d3b250e58ad3dcdcddda309e7a_f491af0c94.jpg", "Springbank", 5, 1828 },
                    { 127, null, "Strathearn", 2, 2013 },
                    { 128, null, "Strathisla", 3, 1786 },
                    { 129, null, "Strathmill", 3, 1891 },
                    { 130, null, "Talisker", 4, 1830 },
                    { 131, null, "Tamdhu", 3, 1897 },
                    { 132, null, "Tamnavulin", 3, 1966 },
                    { 133, null, "Teaninich", 2, 1817 },
                    { 134, null, "Tobermory", 4, 1798 },
                    { 135, null, "Tomatin", 2, 1897 },
                    { 136, null, "Tomintoul", 3, 1964 },
                    { 137, null, "Torabhaig", 4, 2017 },
                    { 138, null, "Tormore", 3, 1958 },
                    { 139, null, "Tullibardine", 2, 1949 },
                    { 140, null, "Uile-bheist", 2, 2023 },
                    { 141, null, "Wolfburn", 2, 1822 },
                    { 142, null, "8 Doors", 2, 2022 },
                    { 143, null, "Chichibu", 28, 2008 },
                    { 144, null, "Eigashima Shuzo", 29, 1919 },
                    { 145, null, "Fuji Gotemba", 30, 1971 },
                    { 146, "https://www.suntory.co.jp/factory/ogp/hakushu_top.jpg", "Hakushu", 31, 1973 },
                    { 147, null, "Hanyu", 28, 1946 },
                    { 148, "https://sp-ao.shortpixel.ai/client/q_lossless,ret_img/https://dekanta.com/wp-content/uploads/revslider/karuizawa-distillery/Untitled-2.jpg", "Karuizawa", 32, 2022 },
                    { 149, "https://150102931.v2.pressablecdn.com/wp-content/uploads/2019/11/brick-distillery-Nikka-byJwolfson-1024x768.jpeg", "Miyagikyo", 33, 1969 },
                    { 150, null, "Shizuoka", 30, 2016 },
                    { 151, null, "The Mars Shinshu Distillery", 32, 1985 },
                    { 152, "https://cdn.osaka-info.jp/page_translation/content/16abce82-04c2-11e8-8efc-06326e701dd4.jpeg", "Yamazaki", 34, 1923 },
                    { 153, "https://www.nikka.com/eng/img/distilleries/topmenu_yoichi.jpg", "Yoichi", 35, 1934 },
                    { 154, null, "Achill Island", 7, 2015 },
                    { 155, null, "Ballykeefe", 8, 2017 },
                    { 156, null, "Baoilleach", 9, 2019 },
                    { 157, null, "Blackwater", 10, 2014 },
                    { 158, null, "Boann", 11, 2019 },
                    { 159, null, "Boatyard", 12, 2016 },
                    { 160, null, "Burren Whiskey", 13, 2019 },
                    { 161, null, "Clonakilty", 14, 2016 },
                    { 162, null, "Cooley", 15, 1987 },
                    { 163, null, "Copeland", 16, 2019 },
                    { 164, null, "Crolly", 9, 2020 }
                });

            migrationBuilder.InsertData(
                table: "Distilleries",
                columns: new[] { "Id", "ImageUrl", "Name", "RegionId", "YearFounded" },
                values: new object[,]
                {
                    { 165, null, "Dingle", 17, 2012 },
                    { 166, null, "Echlinville", 16, 2013 },
                    { 167, null, "Glendalough", 18, 2013 },
                    { 168, null, "Glendree", 13, 2019 },
                    { 169, null, "Great Northern", 15, 2015 },
                    { 170, null, "Hinch", 16, 2020 },
                    { 171, "https://dynamic-media-cdn.tripadvisor.com/media/photo-o/0f/af/82/f4/situated-on-the-brosna.jpg?w=1200&h=1200&s=1", "Kilbeggan", 19, 1757 },
                    { 172, null, "Killowen", 16, 2019 },
                    { 173, null, "Lough Gill", 20, 2019 },
                    { 174, null, "Lough Mask", 7, 2019 },
                    { 175, "https://static.wixstatic.com/media/ea720f_24a89e7c992347e68f452dbcc114dee1~mv2.jpg/v1/fill/w_598,h_390,al_c,q_80,usm_0.66_1.00_0.01,enc_auto/ea720f_24a89e7c992347e68f452dbcc114dee1~mv2.jpg", "New Midleton", 14, 1975 },
                    { 176, "https://www.discoveringireland.com/contentFiles/productImages/Medium/Bushmills_Distillery.jpg", "Old Bushmills", 21, 1784 },
                    { 177, null, "Powerscourt", 18, 2018 },
                    { 178, null, "Rademon Estate", 16, 2015 },
                    { 179, null, "Royal Oak", 22, 2016 },
                    { 180, null, "Shed", 23, 2014 },
                    { 181, null, "Slane", 11, 2018 },
                    { 182, null, "Sliabh Liag", 9, 2016 },
                    { 183, null, "Tipperary", 24, 2020 },
                    { 184, "https://www.whisky.com/fileadmin/_processed_/2/3/csm_Tullamore_distillery_e0fc2d6310.jpg", "Tullamore", 25, 2014 },
                    { 185, null, "Buffalo Trace Distillery", 26, 1775 },
                    { 186, "https://bourbontowntours.com/wp-content/uploads/2023/09/four.jpg", "Four Roses Distillery", 26, 1888 },
                    { 187, null, "Heaven Hill Distilleries, Inc.", 26, 1934 },
                    { 188, "https://americanwhiskeytrail.distilledspirits.org/sites/default/files/styles/flexslider_full/public/distillery/field_slides/Jack%20Daniels%20Visitor%27s%20Center_opt.jpg?itok=vpZAEgVu", "Jack Daniels", 27, 1866 },
                    { 189, "https://www.foodandwine.com/thmb/rVUUWewQYYdDzAQMPOQTZH06nzQ=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/James-Beam-Distillery-FT-BLOG1021-28072a663ffe4cf8ac3adeb05b843143.jpg", "Jim Beam Distillery", 26, 1795 },
                    { 190, "https://d36tnp772eyphs.cloudfront.net/blogs/1/2018/12/Castle-and-Key-distillery-interior-in-Kentucky.jpg", "Kentucky Bourbon Distillers", 26, 1935 },
                    { 191, "https://i0.wp.com/cocktailwonk.com/wp-content/uploads/2014/11/Header.jpg?fit=1200%2C800&ssl=1", "Makers Mark Distillery, Inc.", 26, 1953 },
                    { 192, null, "Michters Distillery", 26, 1847 },
                    { 193, null, "Willett Distillery", 26, 1936 },
                    { 194, "https://hips.hearstapps.com/amv-prod-gp.s3.amazonaws.com/gearpatrol/wp-content/uploads/2019/10/Sponsored-Post-Bulleit-Bourbon-gear-patrol-lead-feature.jpg", "Bulleit", 26, 2017 },
                    { 195, "https://hips.hearstapps.com/amv-prod-gp.s3.amazonaws.com/gearpatrol/wp-content/uploads/2019/10/Sponsored-Post-Bulleit-Bourbon-gear-patrol-lead-feature.jpg", "Kavalan", 36, 2005 },
                    { 196, "https://www.whisky.com/fileadmin/_processed_/1/5/csm_IMG_0402_718cec422ff28f51b8654e744519f5ec_1fb068e7c2.jpg", "Amrut", 37, 1948 },
                    { 197, "https://smartcdn.gprod.postmedia.digital/windsorstar/wp-content/uploads/2021/02/hiram-walker-sons-distillery-1.jpg", "Hiram-Walker & Sons distillery", 38, 1858 }
                });

            migrationBuilder.InsertData(
                table: "Venues",
                columns: new[] { "Id", "Capacity", "CityId", "Name" },
                values: new object[,]
                {
                    { 1, 7, 1, "Masterpiece Whisky Bar" },
                    { 2, 6, 1, "Bar Caldo" },
                    { 4, 100, 1, "Hotel Marinela" },
                    { 5, 3, 2, "Bar Sandaka" },
                    { 6, 5, 2, "The Whisky Library" },
                    { 7, 40, 2, "Hotel Imperial" },
                    { 8, 10, 3, "Tasting Room" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "AvailableSpots", "Description", "EndDate", "OrganiserId", "Price", "StartDate", "Title", "VenueId" },
                values: new object[,]
                {
                    { 1, 0, "Join us for an evening of whisky tasting and discovery.", new DateTime(2024, 3, 25, 21, 0, 0, 0, DateTimeKind.Unspecified), "7dfb241e-f8a5-4ba4-a5aa-5becf035c442", 25.99m, new DateTime(2024, 3, 25, 18, 0, 0, 0, DateTimeKind.Unspecified), "Whisky Tasting Evening", 5 },
                    { 2, 2, "Time for whiskey!\r\n\r\nThe 10th anniversary edition of Whisky Fest Sofia will feature over 35 whiskies, rums, and brandies at the stand. Over 250 different brands will be available for tasting over three days.\r\n\r\nWe'll turn back the hands of time to journey through the various years of whisky history from brands originating from Scotland, Ireland, America, Japan, Taiwan, France, Wales, Sweden, and over 10 other countries.", new DateTime(2024, 5, 27, 21, 0, 0, 0, DateTimeKind.Unspecified), "7dfb241e-f8a5-4ba4-a5aa-5becf035c442", null, new DateTime(2024, 5, 25, 18, 0, 0, 0, DateTimeKind.Unspecified), "Whisky Fest Plovdiv", 5 },
                    { 3, 7, "Whisky Show Bulgaria is the only festival event in Bulgaria and the region that is totally focused on pur favorite aged spirit – the Whisky. Once a year and in three days only the Whisky meets the local whisky community of enthusiasts, fans, aficionados, collectors, specialists, journalist, bar and restaurant owners, bloggers and whisky lovers. Whisky Show Bulgaria is an event created by and for whisky enthusiasts. A show where hundreds of exceptional whiskies that are usually described as special, independent, family-owned, exotic, limited, rare, old, single cask, small batch, produced by ghost distilleries, are to be tasted.", new DateTime(2024, 6, 3, 21, 0, 0, 0, DateTimeKind.Unspecified), "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4", 15.00m, new DateTime(2024, 6, 3, 18, 0, 0, 0, DateTimeKind.Unspecified), "Whisky Show Sofia", 1 },
                    { 4, 6, "The old & rare selection will be quite large and pleasing, thanks to the support of local collectors and partnering whisky bars like Caldo and Local.", new DateTime(2024, 5, 23, 21, 0, 0, 0, DateTimeKind.Unspecified), "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4", 35.00m, new DateTime(2024, 5, 23, 16, 0, 0, 0, DateTimeKind.Unspecified), "Irish Whiskey of things", 2 },
                    { 5, 6, "You will be able to taste Kavalan Solist Sherry, Kaval Solist Ex-Bourbon and Kavalan Concertmaster!", new DateTime(2024, 3, 23, 21, 0, 0, 0, DateTimeKind.Unspecified), "7dfb241e-f8a5-4ba4-a5aa-5becf035c442", 20.00m, new DateTime(2024, 3, 23, 16, 0, 0, 0, DateTimeKind.Unspecified), "Kavalan Masterclass", 2 }
                });

            migrationBuilder.InsertData(
                table: "Whiskies",
                columns: new[] { "Id", "Age", "AlcoholPercentage", "Description", "DistilleryId", "ImageURL", "Name", "WhiskyTypeId" },
                values: new object[,]
                {
                    { 1, 12, 40.0, "Triple distilled single malt whiskey. It has a tempting aroma of roasted almonds, caramel and the characteristic soft and delicate taste of Auchentoshan. It is aged in oak barrels, in which the Spanish \"Oloroso\" sherry and bourbon were previously aged.\r\nThe Auchentoshan Distillery is one of three in the Lowland area that continues to operate to this day. It is located north of Glasgow and was founded in 1800. The name of the excellent brand comes from Celtic and means \"The corner of the field\".", 15, "https://www.gourmetencasa-tcm.com/20487/auchentoshan-12-year-old-single-malt-scoth-whisky-70cl.jpg", "Auchentoshan 12 Year Old", 1 },
                    { 2, 10, 40.0, "Glenmorangie whiskeys are produced in the Highlands, the northernmost part of Scotland. They are distilled in the highest copper cauldrons in Scotland and aged in the highest quality oak barrels, which are used only twice to produce the purest and most elegant whiskeys. The distillery is known as an innovator in the production of whiskey, combining tradition with innovation, resulting in malt whiskey with \"unnecessarily\" high quality. A symbol of Glenmorangie whiskey, the ten-year-old Glenmorangie The Original is the most delicate single malt whiskey in the world, with the most complex taste and seductive aroma. It has an exceptional fruity elegance, exquisite finesse and a seductive complex taste. The delicate floral notes in the whiskey are combined with the softness and sweetness acquired from the premium bourbon oak barrels.", 75, "https://vida.bg/wp-content/uploads/2020/08/Glenmorangie201020YO.png", "Glenmorangie 10 Year Old", 1 },
                    { 3, 18, 43.0, "The Sherry Oak Collection is a timeless sensorial journey. Beautifully led by European Oak, sherry seasoned in Jerez de la Frontera, Spain. Photographer Erik Madigan Heck has captured his visual interpretation of The Macallan Sherry Oak 18 Years Old through the lens of his unique, signature style.", 107, "https://vintagecellarswineandspirits.com/wp-content/uploads/2023/11/The-Macallan-18-Years-Sherry-Oak-Cask-750mL.png", "The Macallan 18 Year Old Sherry Oak", 1 },
                    { 4, 10, 40.0, "The famous 10-year-old Laphroaig whisky has an extremely smoky flavour with a hint of seaweed and the sea. It is one of the most intense Islay malts. You either hate it or you love it.", 99, "https://www.canadianliquorstore.ca/cdn/shop/products/739119_1024x1024.jpg?v=1709579050", "Laphroaig 10 Year Old", 1 },
                    { 5, 15, 46.0, "A 15-year-old single malt from the Springbank distillery with plenty of sherry notes and spice, dried fruits, and nuts.", 126, "https://mldq.store/cdn/shop/files/SB21009.jpg?v=1709563871&width=1080", "Springbank 15 Year Old", 1 },
                    { 6, 18, 43.0, "Highland Park's 18 Year Old enjoyed a redesign in 2017, receiving livery inspired by the wood carvings from Urnes Stave Church and a new sub-name, \"Viking Pride\". The Orkney single malt remains the same as before - rich, complex and supremely delicious.", 85, "https://www.highlandparkwhisky.com/sites/g/files/jrulke331/files/styles/image_card/public/HP-2023-18YO-Bottle-Front%5B1%5D.png?itok=oxFBsSxT", "Highland Park 18 Year Old Viking Pride", 1 },
                    { 7, null, 40.0, "Bushmill's original whisky is a smooth, easy-drinking whiskey that has been produced in Ireland for centuries.\r\nBushmills Original is made up of grain whiskey matured for five years before blending with malt whiskeys. Bushmill's Irish whiskey is triple distilled and very supple.\r\nBushmills Original is a consistent, great value-for-money, triple-distilled blend made with a core of flavoursome malt and grain whiskies", 176, "https://drinklink.bg/media/catalog/product/cache/15aafd85a633527ad024236a2302dda5/image/262b2e/bushmills-original.jpg", "Bushmills Original", 5 },
                    { 8, null, 40.0, "Produced at the Midleton Distillery, Jameson is Ireland's quintessential Irish blend – a classic whiskey. Jim Murray even awarded it an incredible 95 points!", 175, "https://www.gourmetencasa-tcm.com/21056/jameson-1l.jpg", "Jameson", 5 },
                    { 9, null, 40.0, "Jack Daniel's Tennessee Whiskey has been made at its Lynchburg distillery since 1875. The branding and original label, sometimes referred to as No. 7 or Black Label; has made its way into pop culture, with merchandise sold the world over and a history of association with music. Frank Sinatra was even buried with a bottle. The Tennessee whiskey makers use a mash bill made up of 80% corn, 12% rye, and 8% malt to create Jack Daniels whiskey, which is then filtered through 10 feet of sugar maple charcoal to produce a mellow, slightly smoky character. A method known as the Lincoln County Process, it means this is not a bourbon, but instead meets the legal definition of a Tennessee whiskey. Jasper Newton \"Jack\" Daniel ( c. January 1849 – October 9, 1911) was an American distiller and businessman, best known as the founder of the Jack Daniel's Tennessee whiskey distillery.", 188, "https://vida.bg/wp-content/uploads/2022/03/Jack-Daniels-Old-Number-7-1L-1000x1000-1.png", "Jack Daniel's Old No. 7", 3 },
                    { 10, null, 40.0, "Jim Beam bourbon undergoes distillation at lower temperatures and is distilled to no more than 62.5%, the White label is aged for four years and has quite a high percentage of rye in the mashbill.", 189, "https://alcoprostir.com/5220-large_default/burbon-jim-beam-white-label-07l-.jpg", "Jim Beam White Label", 3 },
                    { 11, null, 45.0, "Using the two Coffey stills at the Miyagikyo distillery, which were imported from Scotland to Japan in 1963, Nikka have produced a number of single cask single grain whiskies from time to time over the years. This, however, is their larger release of wonderfully exotic grain whisky. Now in a 70cl bottle! Hooray!", 149, "https://whiskeycaviar.com/cdn/shop/products/Nikka-Coffey-Grain-Whisky_6a5f73be-e145-4e0f-b045-0f361d586941_800x.jpg?v=1697132728", "Nikka Coffey Grain", 6 },
                    { 12, null, 59.399999999999999, "A single cask, cask strength single malt from Taiwan's Kavalan, released for the Solist series. Matured in a single sherry cask (#S081217042 to be exact), this one was bottled without colouring at a generous 59.4% ABV. If you like sherry bombs and award-winning whisky, look no further!", 195, "https://drinklink.bg/media/catalog/product/cache/15aafd85a633527ad024236a2302dda5/image/5285dfbf/kavalan-solist-sherry.jpg", "Kavalan Solist Sherry", 1 },
                    { 13, null, 50.0, "Fusion is a particularly apt name for this fantastic single malt whisky from Amrut. Y'see, it's made with barley grown in India, where the Amrut Distillery can be found, as well as peated barley from Scotland! Makes sense, right? Not just a clever name, it's also a cracking whisky, offering up generous helpings of fresh fruit, honey, spice and a good whiff of smoke.", 196, "https://cdncloudcart.com/16474/products/images/2094/amrut-fusion-700ml-image_5f42d27040c9c_1280x1280.jpeg?1598214826", "Amrut Fusion", 1 },
                    { 14, null, 43.399999999999999, "A smooth amber rye whiskey with complex notes of caramel, toffee and spice with subtle undertones of green apple and pear. Enjoy the roasted rye spices that complement the soft toffee and vanilla flavors, neatly sipped on its own for a lingering green apple finish.", 197, "https://shopliquoryxe.ca/cdn/shop/products/Wiser_s-Triple-Barrel.jpg?v=1587749528", "JP Wiser's Triple Barrel Rye", 4 },
                    { 15, null, 40.0, "A pure expression of rye whisky; more complex and characterful, this pours medium gold. Aromas of sweet fruit, herb and spice, with vanilla, toffee, pepper, cedar smoke and banana on the nose. The sweet, creamy and warm palate is balanced by rye spice flavours followed by a long finish showing dried fruit, honey and ginger.", 197, "https://aem.lcbo.com/content/dam/lcbo/products/3/9/0/5/390583.jpg.thumb.1280.1280.jpg", "Canadian Club 100% Rye", 4 },
                    { 16, null, 43.0, "A Lowland single malt matured in 3 different casks, namely: Pedro Ximénez Sherry casks, bourbon casks and Oloroso Sherry casks. A distinctive triple distilled whisky from Auchentoshan.", 15, "https://nomu.asia/wp-content/uploads/2022/06/Auchentoshan-Three-Wood-700ML.png", "Auchentoshan Three Wood", 1 },
                    { 17, 12, 43.0, "A rich and fruity single malt Scotch whisky finished in sherry casks.", 75, "https://vida.bg/wp-content/uploads/2020/08/Glenmorangie20Lasanta.png", "Glenmorangie Lasanta 12 Year Old", 1 },
                    { 18, 12, 40.0, "An exciting new age statement single malt Scotch whisky from Macallan that's matured in a combination of American and European Sherry oak for a minimum of 12 years.", 107, "https://curiada.com/cdn/shop/files/Macallan12DoubleCaskScotchTransp.png?v=1701109353", "The Macallan Double Cask 12 Year Old", 1 },
                    { 19, null, 48.0, "Released in 2004, this bottling was aged for around five years before being finished in a quarter cask for several months, the size of the cask is quite small, thus does not require such a long maturation. This remains a truly great achievement from Laphroaig.", 99, "https://cdncloudcart.com/25930/products/images/6955/sotlandsko-uiski-laphroaig-quarter-cask-0-7-l-62b5c379957d7_800x800.jpeg?1656079943", "Laphroaig Quarter Cask", 1 },
                    { 20, 18, 46.0, "The 2021 release of the 18-year-old from Springbank. Matured in a combination of 50% ex-bourbon and 50% ex-sherry casks then bottled at 46% abv.", 126, "https://www.kuccagnamarket.it/7027-large_default/springbank-18-years-old-2021.jpg", "Springbank 18 Year Old 2021 Release", 1 },
                    { 21, 12, 40.0, "With a sub-name like Viking Honour, you can expect a lot from this Orcadian single malt Scotch whisky – and it delivers! A great introduction to Highland Park's famed heathery peat smoke, the 12 Year Old is matured predominantly in sherry-seasoned European and American oak casks, so it's spicy, citrusy, and full of smoky aromatics.", 85, "https://www.highlandparkwhisky.com/sites/g/files/jrulke331/files/styles/product_page_image/public/Dig_Commercial_ecomm_flat_small-HP-2017-12YO-Bottle-Shot-700ml-5000px-300dpi.jpg?itok=K8Midv04", "Highland Park 12 Year Old Viking Honour", 1 },
                    { 22, null, 40.0, "If you've been in a bar (any bar, really), you've more than likely seen a bottle of this on the shelf. One of the most well-known Irish blends, Bushmills Black Bush features a lot of sherried malt in its recipe, alongside classically caramel-y grain whiskey. Suitable for enjoying neat, but it can also be used in whiskey cocktails that call for dark fruit sweetness...", 176, "https://cdncloudcart.com/25930/products/images/6188/irlandsko-uiski-bushmills-black-bush-0-7-lit-image_615d80145ee28_800x800.jpeg?1633521502", "Bushmills Black Bush", 5 },
                    { 23, null, 40.0, "Jameson Caskmates is an intriguing release. Having sent some of their casks to the local craft stout brewers at Franciscan Well, the casks were returned to Midleton where they were subsequently used to give a stout finish to Jameson!", 175, "https://drinklink.bg/media/catalog/product/cache/15aafd85a633527ad024236a2302dda5/image/627c125/jameson-caskmates-stout-edition.png", "Jameson Caskmates Stout Edition", 5 },
                    { 24, null, 45.0, "You all know Jack Daniel’s and have no doubt seen its classic whiskey in bars and shops all over the world. But the Tennessee-based distillery is also home to lots of premium expressions, like the single barrel bottling you see before you. A premium version of Jack Daniel's, each of these comes from a cask specially selected by the master distiller. These are chosen for their suitability as standalone spirits, resulting in a whiskey full of richness and complexity.", 188, "https://cdncloudcart.com/25930/products/images/6227/uiski-jack-daniels-single-barrel-select-0-7-l-image_61825a979479f_800x800.jpeg?1635933067", "Jack Daniel's Single Barrel", 3 },
                    { 25, 8, 43.0, "There's an old axiom that claims \"Two heads are better than one\". Can that theory be transferred to whiskey barrels? Jim Beam's Double Oak might answer that question. You see, they initially mature this bourbon in freshly charred American oak barrels, and then move the whiskey over to a fresh set of freshly charred American oak barrels for the second part of its maturation!\r\n\r\nA worthy replacement for the now discontinued Jim Beam Black.", 189, "https://winepig.co.uk/cdn/shop/products/Jim-Beam-Double-Oak-31.jpg?v=1643221336", "Jim Beam Double", 3 },
                    { 26, null, 51.399999999999999, "The award-winning Nikka Whisky From The Barrel blend is absolutely full of flavour. Bottled at 51.4% ABV. The blend combines both single malt and grain whisky from the Miyagikyo and Yoichi distilleries, which are then married in a huge variety of casks, including bourbon barrels, sherry butts and refill hogsheads. A huge depth of flavour in this stunning Japanese whisky. We can't recommend this enough.", 149, "https://theworldofwhisky.com/images/detailed/8/65567.png", "Nikka From the Barrel", 6 },
                    { 27, null, 40.0, "Taiwanese whisky has been a 'thing' for a while now, since 2008 in fact, but the highly-regarded Kavalan whiskies are now finally available in Europe! This single malt whisky utilises Ruby Port, Tawny Port and Vintage Port casks from Portugal to finish whiskies that were initially matured in American oak. Kavalan Concertmaster was named Best in Class at the 2011 International Wine & Spirit Competition.", 195, "https://cdncloudcart.com/25930/products/images/7001/tajvansko-uiski-kavalan-concertmaster-0-7l-62c3f9d5232cc_1280x1280.png?1657010703", "Kavalan Concertmaster Port Cask Finish", 1 },
                    { 28, null, 46.0, "From one of India's most famous whisky producers, this smoky single malt is made from barley peated to 24ppm. It’s punchy but still fruity and malty, with the ABV increased after its initial release to 46% to add more weight and texture.", 196, "https://images.squarespace-cdn.com/content/v1/5bf41e2c70e8026f5a08ac41/1583017596415-0H17J3KA865FIKD1O41C/amrut-peated.jpg", "Amrut Peated Single Malt", 1 },
                    { 29, 18, 40.0, "A single grain whisky that is dominated by aromas of green apple in part due to the unique aging conditions in Southern Ontario. It pours a medium golden amber with additional aromas of caramel, orange peel and spice; the palate is round and medium-bodied with a silky texture and a smooth vanilla driven, finish.", 197, "https://internetwines.com/cdn/shop/products/JPWisers18yr_2021_900x.jpg?v=1622129622", "JP Wiser's 18 Year Old", 4 },
                    { 30, 12, 40.0, "The Canadian Club Classic matured for 12 years, which is a long age for a Canadian whisky. With age comes perfection. Selected whiskies mature together to create a mild and naturally smooth Blend.", 197, "https://aem.lcbo.com/content/dam/lcbo/products/3/1/1/9/311944.jpg.thumb.1280.1280.jpg", "Canadian Club Classic 12 Year Old", 2 },
                    { 31, 18, 40.0, "This 10-year-old single malt from The Macallan Fine Oak Range was matured in a mix of bourbon and sherry casks.", 107, "https://www.connosr.com/image/2/1000/1000/2/images/products/macallan-10-year-old-fine-oak-8829.jpg", "The Macallan 10 Year Old Fine Oak", 1 },
                    { 32, 21, 46.5, "Those clever clogs over at Darkness got their hands on a sought-after drop of whisky here. This 21-year-old Springbank single malt was given the Darkness treatment with a finish in one of its custom-made octave casks, that previously held oloroso sherry. The coastal malt and sherried sweetness make a perfect partnership.", 126, "https://www.whiskyshop.com/media/catalog/product/d/a/darkness_springbank_21yo_oloroso_34670_ss.jpg?width=2500&store=whiskyshop&image-type=image", "Springbank 21 Year Old Oloroso Cask Finish (Darkness)", 1 },
                    { 33, 25, 58.399999999999999, "Highland Park Bulgaria 681 is one of the three whiskies in limited edition series specially dedicated to Bulgaria, as it celebrates legendary moments from our history.\r\nThe concept includes a series of 3 collector's bottles, which will commemorate 3 key victories of the First Bulgarian Empire. Highland Park Bulgaria 681 marks the year when the Byzantine Empire recognized the existence of the Bulgarian state by signing a peace treaty after the victory of the Proto-Bulgarians, led by Khan Asparuh. ", 75, "https://adm.thewhiskylibrary.club/assets/content/products/images/681-681.png", "Highland Park Bulgaria 681", 1 }
                });

            migrationBuilder.InsertData(
                table: "Awards",
                columns: new[] { "Id", "AwardsCeremony", "MedalType", "Title", "WhiskyId", "Year" },
                values: new object[,]
                {
                    { 1, "International Spirits Challenge", 1, "Distillers' Single Malts 12 years and under", 1, 2020 },
                    { 2, "International Wine & Spirit Competition", 1, "Scotch Single Malt - Highland", 2, 2017 },
                    { 3, "International Wine & Spirit Competition", 1, "Scotch Single Malt - Highland", 2, 2014 },
                    { 9, "San Francisco World Spirits Competition", 1, "Single Malt Scotch - to 12 Yrs", 4, 2022 },
                    { 10, "Scottish Whisky Awards", 1, "Single Malt Under 12 Year Old", 4, 2019 },
                    { 11, "International Wine & Spirit Competition", 1, "Scotch Single Malt - Islay", 4, 2019 },
                    { 12, "International Wine & Spirit Competition", 2, "Scotch Single Malt - Islay", 4, 2017 },
                    { 13, "Malt Maniacs Awards", 3, "Daily Dram", 5, 2014 },
                    { 14, "San Francisco World Spirits Competition", 1, "Single Malt Scotch - 13 to 19 Yrs", 6, 2022 },
                    { 15, "International Spirits Challenge", 1, "Distillers' Single Malts between 13 and 20 years old", 6, 2020 },
                    { 16, "The Scotch Whisky Masters (The Spirits Business)", 1, "Highlands & Islands 13-18yo", 6, 2020 },
                    { 17, "The Scotch Whisky Masters (The Spirits Business)", 1, "Highlands & Islands 13-18yo", 6, 2019 },
                    { 18, "The Scotch Whisky Masters (The Spirits Business)", 1, "Highlands & Islands 13-18yo", 6, 2018 },
                    { 19, "International Spirits Challenge", 2, "Distillers' Single Malts between 13 and 20 years old", 6, 2019 },
                    { 20, "Scottish Whisky Awards", 3, "Single Malt 17-20 Year Old", 6, 2019 },
                    { 21, "The Irish Whisky Masters (The Spirits Business)", 1, "Irish Blended - Standard", 7, 2013 },
                    { 22, "San Francisco World Spirits Competition", 1, "Blended Malted Irish Whiskey", 7, 2013 },
                    { 23, "The Irish Whisky Masters (The Spirits Business)", 2, "Irish Blended - Premium", 7, 2014 },
                    { 24, "Wizards of Whisky Awards", 3, "Irish Whiskey", 7, 2014 },
                    { 25, "Jim Murray's Whisky Bible", 1, "Irish Blend of the Year", 8, 2014 },
                    { 26, "Jim Murray's Whisky Bible", 1, "Irish Blend of the Year", 8, 2013 },
                    { 27, "The Irish Whisky Masters (The Spirits Business)", 1, "Irish Blended - Premium", 8, 2020 },
                    { 28, "The Irish Whisky Masters (The Spirits Business)", 1, "Irish Blended - Premium", 8, 2018 },
                    { 29, "The Irish Whisky Masters (The Spirits Business)", 2, "Irish Blended - Standard", 8, 2019 },
                    { 30, "The Irish Whisky Masters (The Spirits Business)", 2, "Irish Blended - Standard", 8, 2017 },
                    { 31, "The Irish Whisky Masters (The Spirits Business)", 2, "Irish Blended - Premium", 8, 2013 },
                    { 32, "International Wine & Spirit Competition", 2, "Irish Whiskey - Blended", 8, 2014 },
                    { 33, "San Francisco World Spirits Competition", 3, "Blended Irish Whiskey", 8, 2013 },
                    { 34, "San Francisco World Spirits Competition", 2, "Straight Bourbon", 10, 2019 },
                    { 35, "International Spirits Challenge", 2, "Straight Bourbon 10 years old and under", 10, 2019 },
                    { 36, "Wizards of Whisky Awards", 1, "Asian Whisky", 13, 2014 },
                    { 37, "International Spirits Challenge", 1, "Single Malt No Age Statement", 16, 2021 },
                    { 38, "International Spirits Challenge", 1, "Single Malt No Age Statement", 16, 2020 },
                    { 39, "San Francisco World Spirits Competition", 1, "Single Malt Scotch - to 12 Yrs", 16, 2013 },
                    { 40, "San Francisco World Spirits Competition", 1, "Single Malt Scotch - No Age Statement", 16, 2021 },
                    { 41, "The Asian Spirits Masters (The Spirits Business)", 1, "Scotch Whisky - Single Malt - NAS", 16, 2018 },
                    { 42, "International Wine & Spirit Competition", 2, "Scotch Single Malt - Lowland", 16, 2017 },
                    { 43, "International Spirits Challenge", 2, "Distillers' Single Malts 12 years and under", 16, 2014 },
                    { 44, "International Wine & Spirit Competition", 2, "Scotch Single Malt - Lowland", 16, 2014 },
                    { 45, "International Wine & Spirit Competition", 1, "Scotch Single Malt - Highland", 17, 2019 },
                    { 46, "International Spirits Challenge", 1, "Distillers' Single Malts 12 years and under", 17, 2020 },
                    { 47, "San Francisco World Spirits Competition", 2, "Single Malt Scotch - to 12 Yrs", 17, 2022 }
                });

            migrationBuilder.InsertData(
                table: "Awards",
                columns: new[] { "Id", "AwardsCeremony", "MedalType", "Title", "WhiskyId", "Year" },
                values: new object[,]
                {
                    { 48, "International Spirits Challenge", 2, "Distillers' Single Malts 12 years and under", 17, 2022 },
                    { 49, "International Spirits Challenge", 2, "Distillers' Single Malts 12 years and under", 17, 2019 },
                    { 50, "International Wine & Spirit Competition", 2, "Scotch Single Malt - Highland", 17, 2017 },
                    { 51, "The Asian Spirits Masters (The Spirits Business)", 1, "Scotch Whisky - Single Malt - NAS", 19, 2018 },
                    { 52, "International Wine & Spirit Competition", 1, "Scotch Single Malt - Islay", 19, 2017 },
                    { 53, "International Spirits Challenge", 2, "Distillers' Single Malts 12 years and under", 19, 2022 },
                    { 54, "International Whisky Competition", 3, "Best Single Malt Scotch (Islay)", 19, 2015 },
                    { 55, "International Whisky Competition", 3, "Best Single Malt NAS", 19, 2015 },
                    { 56, "International Wine & Spirit Competition", 2, "Scotch Single Malt - Islay", 19, 2014 },
                    { 57, "International Wine & Spirit Competition", 2, "Scotch Single Malt - Islay", 19, 2013 },
                    { 58, "San Francisco World Spirits Competition", 2, "Single Malt Scotch - to 12 Yrs", 19, 2013 },
                    { 59, "International Wine & Spirit Competition", 2, "Scotch Single Malt - Islay", 19, 2019 },
                    { 60, "San Francisco World Spirits Competition", 1, "Single Malt Scotch - to 12 Yrs", 21, 2022 },
                    { 61, "International Spirits Challenge", 1, "Distillers' Single Malts 12 years and under", 21, 2020 },
                    { 62, "The Scotch Whisky Masters (The Spirits Business)", 1, "Highlands & Islands up to 12yo", 21, 2020 },
                    { 63, "The Scotch Whisky Masters (The Spirits Business)", 1, "Highlands & Islands up to 12yo", 21, 2019 },
                    { 64, "The Scotch Whisky Masters (The Spirits Business)", 1, "Highlands & Islands up to 12yo", 21, 2018 },
                    { 65, "International Spirits Challenge", 2, "Distillers' Single Malts 12 years and under", 21, 2022 },
                    { 66, "International Spirits Challenge", 2, "Distillers' Single Malts 12 years and under", 21, 2019 },
                    { 67, "The Asian Spirits Masters (The Spirits Business)", 2, "Scotch Whisky - Single Malt - Age Statement", 21, 2018 },
                    { 68, "International Wine & Spirit Competition", 2, "Scotch Single Malt - Island", 21, 2017 },
                    { 69, "Jim Murray's Whisky Bible", 1, "Irish Blend of the Year", 22, 2018 },
                    { 70, "The Irish Whisky Masters (The Spirits Business)", 1, "Irish Blended - Premium", 22, 2013 },
                    { 71, "Wizards of Whisky Awards", 2, "Irish Whiskey", 22, 2014 },
                    { 72, "The Irish Whisky Masters (The Spirits Business)", 2, "Irish Blended - Premium", 22, 2014 },
                    { 73, "San Francisco World Spirits Competition", 2, "Blended Malted Irish Whiskey", 22, 2013 },
                    { 74, "The Irish Whisky Masters (The Spirits Business)", 2, "Irish Blended - Super-Premium", 23, 2019 },
                    { 75, "The Irish Whisky Masters (The Spirits Business)", 2, "Irish Blended - Standard", 23, 2017 },
                    { 76, "San Francisco World Spirits Competition", 2, "Straight Bourbon", 25, 2019 },
                    { 77, "International Spirits Challenge", 2, "Straight Bourbon 10 years old and under", 25, 2019 },
                    { 78, "Malt Maniacs Awards", 3, "Daily Dram", 26, 2013 },
                    { 84, "International Spirits Challenge", 2, "Whiskies Worldwide", 27, 2019 },
                    { 85, "The World Whisky Masters (The Spirits Business)", 2, "Asian Single Malt", 27, 2019 },
                    { 86, "The World Whisky Masters (The Spirits Business)", 2, "Asian Single Malt", 27, 2018 }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "Finish", "Nose", "Taste", "UserId", "WhiskyId" },
                values: new object[,]
                {
                    { 1, 45, 47, 54, "7dfb241e-f8a5-4ba4-a5aa-5becf035c442", 10 },
                    { 2, 43, 39, 87, "1cf4a321-6128-459e-8e4e-e4615c85d30f", 11 },
                    { 3, 88, 78, 77, "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4", 12 },
                    { 4, 59, 59, 59, "7dfb241e-f8a5-4ba4-a5aa-5becf035c442", 13 },
                    { 5, 87, 99, 78, "1cf4a321-6128-459e-8e4e-e4615c85d30f", 14 },
                    { 6, 31, 54, 42, "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4", 15 },
                    { 7, 51, 44, 55, "7dfb241e-f8a5-4ba4-a5aa-5becf035c442", 16 },
                    { 8, 81, 77, 63, "1cf4a321-6128-459e-8e4e-e4615c85d30f", 17 }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "Finish", "Nose", "Taste", "UserId", "WhiskyId" },
                values: new object[,]
                {
                    { 9, 45, 12, 18, "7dfb241e-f8a5-4ba4-a5aa-5becf035c442", 18 },
                    { 10, 49, 49, 59, "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4", 19 },
                    { 11, 45, 47, 54, "7dfb241e-f8a5-4ba4-a5aa-5becf035c442", 1 },
                    { 12, 43, 39, 87, "1cf4a321-6128-459e-8e4e-e4615c85d30f", 1 },
                    { 13, 88, 87, 77, "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4", 1 },
                    { 14, 45, 37, 84, "7dfb241e-f8a5-4ba4-a5aa-5becf035c442", 2 },
                    { 15, 43, 45, 87, "1cf4a321-6128-459e-8e4e-e4615c85d30f", 2 },
                    { 16, 37, 87, 45, "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4", 2 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Content", "Recommend", "Title", "UserId", "WhiskyId" },
                values: new object[,]
                {
                    { 1, "This whisky has an amazing taste profile, rich and complex.", true, "Fantastic flavor!", "1cf4a321-6128-459e-8e4e-e4615c85d30f", 1 },
                    { 2, "Really enjoyed sipping on this whisky, smooth with a nice finish.", true, "Smooth and enjoyable", "7dfb241e-f8a5-4ba4-a5aa-5becf035c442", 2 },
                    { 3, "This whisky is unbeatable. Smooth and easy to drink.", true, "Great!", "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4", 3 },
                    { 4, "Expected more from this whisky. Found it lacking in flavor.", false, "Disappointing", "1cf4a321-6128-459e-8e4e-e4615c85d30f", 4 },
                    { 5, "This whisky is a real treat for the senses. Highly recommended.", true, "A real treat", "7dfb241e-f8a5-4ba4-a5aa-5becf035c442", 5 },
                    { 6, "Smooth and elegant, with a lovely finish. A delightful whisky.", true, "Smooth and elegant", "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4", 6 },
                    { 7, "Decent whisky, but nothing extraordinary. Would drink again though.", true, "Not bad", "1cf4a321-6128-459e-8e4e-e4615c85d30f", 7 },
                    { 8, "Really enjoyed the complexity of flavors in this whisky. A must-try.", true, "Complex flavors", "7dfb241e-f8a5-4ba4-a5aa-5becf035c442", 8 },
                    { 9, "Save this one for special occasions. Truly a special whisky.", true, "Perfect for special occasions", "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4", 9 },
                    { 10, "Was not impressed with this whisky. Expected more.", false, "Not impressed", "1cf4a321-6128-459e-8e4e-e4615c85d30f", 10 },
                    { 11, "The whisky of my dreams!", true, "Very good!", "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4", 1 },
                    { 12, "Honestly I dont't know what is to like about it.", false, "Nothing special", "7dfb241e-f8a5-4ba4-a5aa-5becf035c442", 1 },
                    { 13, "Like it! A lot!", true, "Yep , yep good", "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4", 2 }
                });

            migrationBuilder.InsertData(
                table: "UsersEvents",
                columns: new[] { "EventId", "UserId" },
                values: new object[,]
                {
                    { 1, "1cf4a321-6128-459e-8e4e-e4615c85d30f" },
                    { 1, "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4" },
                    { 1, "f99c5e20-d91e-4a5e-9b73-fdb38b89ffc3" },
                    { 2, "f99c5e20-d91e-4a5e-9b73-fdb38b89ffc3" }
                });

            migrationBuilder.AddCheckConstraint(
                name: "CheckDateOfBirth",
                table: "AspNetUsers",
                sql: "DateOfBirth >= '1900-01-01' AND DateOfBirth <= DATEADD(YEAR, -18, GETDATE())");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_PublisherUserId",
                table: "Articles",
                column: "PublisherUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Awards_WhiskyId",
                table: "Awards",
                column: "WhiskyId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ArticleId",
                table: "Comments",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Distilleries_RegionId",
                table: "Distilleries",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_OrganiserId",
                table: "Events",
                column: "OrganiserId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_VenueId",
                table: "Events",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_WhiskyId",
                table: "Ratings",
                column: "WhiskyId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_CountryId",
                table: "Regions",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_WhiskyId",
                table: "Reviews",
                column: "WhiskyId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersEvents_UserId",
                table: "UsersEvents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersWhiskies_WhiskyId",
                table: "UsersWhiskies",
                column: "WhiskyId");

            migrationBuilder.CreateIndex(
                name: "IX_Venues_CityId",
                table: "Venues",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Whiskies_DistilleryId",
                table: "Whiskies",
                column: "DistilleryId");

            migrationBuilder.CreateIndex(
                name: "IX_Whiskies_WhiskyTypeId",
                table: "Whiskies",
                column: "WhiskyTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Awards");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "UsersEvents");

            migrationBuilder.DropTable(
                name: "UsersWhiskies");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Whiskies");

            migrationBuilder.DropTable(
                name: "Venues");

            migrationBuilder.DropTable(
                name: "Distilleries");

            migrationBuilder.DropTable(
                name: "WhiskyTypes");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropCheckConstraint(
                name: "CheckDateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1cf4a321-6128-459e-8e4e-e4615c85d30f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f99c5e20-d91e-4a5e-9b73-fdb38b89ffc3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7dfb241e-f8a5-4ba4-a5aa-5becf035c442");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd6bbdc1-dc81-4d8d-82ad-e9cb3d393ce4");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");
        }
    }
}
