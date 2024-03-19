using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheWhiskyRealm.Infrastructure.Data
{
    public partial class DocumentatingDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "WhiskyTypes",
                comment: "Represents a whisky type entity.");

            migrationBuilder.AlterTable(
                name: "Whiskies",
                comment: "Represents a whisky entity.");

            migrationBuilder.AlterTable(
                name: "Venues",
                comment: "Represents a venue entity.");

            migrationBuilder.AlterTable(
                name: "UsersEvents",
                comment: "Represents a mapping entity between a user and an event.");

            migrationBuilder.AlterTable(
                name: "Reviews",
                comment: "Represents a review entity.");

            migrationBuilder.AlterTable(
                name: "Regions",
                comment: "Represents a region entity.");

            migrationBuilder.AlterTable(
                name: "Ratings",
                comment: "Represents a rating entity.");

            migrationBuilder.AlterTable(
                name: "Events",
                comment: "Represents an event entity.");

            migrationBuilder.AlterTable(
                name: "Distilleries",
                comment: "Represents a distillery entity.");

            migrationBuilder.AlterTable(
                name: "Comments",
                comment: "Represents a comment entity.");

            migrationBuilder.AlterTable(
                name: "Cities",
                comment: "Represents a city entity.");

            migrationBuilder.AlterTable(
                name: "Awards",
                comment: "Represents an award entity.");

            migrationBuilder.AlterTable(
                name: "Articles",
                comment: "Represents an article entity.");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "WhiskyTypes",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                comment: "The name of the whisky type.",
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "WhiskyTypes",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                comment: "The description of the whisky type.",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "WhiskyTypes",
                type: "int",
                nullable: false,
                comment: "The unique identifier of the whisky type.",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "WhiskyTypeId",
                table: "Whiskies",
                type: "int",
                nullable: false,
                comment: "The identifier of the type of the whisky.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Whiskies",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: false,
                comment: "The name of the whisky.",
                oldClrType: typeof(string),
                oldType: "nvarchar(70)",
                oldMaxLength: 70);

            migrationBuilder.AlterColumn<int>(
                name: "DistilleryId",
                table: "Whiskies",
                type: "int",
                nullable: false,
                comment: "The identifier of the distillery that produced the whisky.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Whiskies",
                type: "nvarchar(1500)",
                maxLength: 1500,
                nullable: false,
                comment: "The description of the whisky.",
                oldClrType: typeof(string),
                oldType: "nvarchar(1500)",
                oldMaxLength: 1500);

            migrationBuilder.AlterColumn<double>(
                name: "AlcoholPercentage",
                table: "Whiskies",
                type: "float",
                nullable: false,
                comment: "The alcohol percentage of the whisky.",
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "Whiskies",
                type: "int",
                nullable: true,
                comment: "The age of the whisky.",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Whiskies",
                type: "int",
                nullable: false,
                comment: "The unique identifier of the whisky.",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Venues",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "The name of the venue.",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Venues",
                type: "int",
                nullable: false,
                comment: "The identifier of the city where the venue is located.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Capacity",
                table: "Venues",
                type: "int",
                nullable: false,
                comment: "The capacity of the venue.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Venues",
                type: "int",
                nullable: false,
                comment: "The unique identifier of the venue.",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UsersEvents",
                type: "nvarchar(450)",
                nullable: false,
                comment: "The identifier of the user associated with the event.",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "UsersEvents",
                type: "int",
                nullable: false,
                comment: "The identifier of the event associated with the user.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "WhiskyId",
                table: "Reviews",
                type: "int",
                nullable: false,
                comment: "The identifier of the whisky being reviewed.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: false,
                comment: "The identifier of the user who made the review.",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Reviews",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "The title of the review.",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<bool>(
                name: "Recommend",
                table: "Reviews",
                type: "bit",
                nullable: false,
                comment: "If the user recommends status the whisky",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Reviews",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                comment: "The content of the review.",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Reviews",
                type: "int",
                nullable: false,
                comment: "The unique identifier of the review.",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Regions",
                type: "nvarchar(85)",
                maxLength: 85,
                nullable: false,
                comment: "The name of the region.",
                oldClrType: typeof(string),
                oldType: "nvarchar(85)",
                oldMaxLength: 85);

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Regions",
                type: "int",
                nullable: false,
                comment: "The identifier of the country that the region belongs to.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Regions",
                type: "int",
                nullable: false,
                comment: "The unique identifier of the region.",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "WhiskyId",
                table: "Ratings",
                type: "int",
                nullable: false,
                comment: "The identifier of the whisky being rated.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Ratings",
                type: "nvarchar(450)",
                nullable: false,
                comment: "The identifier of the user who gave the rating.",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "Taste",
                table: "Ratings",
                type: "int",
                nullable: false,
                comment: "Represents the rating that is given for the whisky taste.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Nose",
                table: "Ratings",
                type: "int",
                nullable: false,
                comment: "Represents the rating that is given for the whisky aroma.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Finish",
                table: "Ratings",
                type: "int",
                nullable: false,
                comment: "Represents the rating that is given for the whisky finishing notes.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Ratings",
                type: "int",
                nullable: false,
                comment: "The unique identifier of the rating.",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "VenueId",
                table: "Events",
                type: "int",
                nullable: false,
                comment: "The identifier of the venue where the event will take place.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Events",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                comment: "The title of the event.",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Events",
                type: "datetime2",
                nullable: false,
                comment: "The start date of the event.",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Events",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                comment: "The price of the event.",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OrganiserId",
                table: "Events",
                type: "nvarchar(450)",
                nullable: false,
                comment: "The identifier of the user who organised the event.",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Events",
                type: "datetime2",
                nullable: false,
                comment: "The end date of the event.",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "DurationInHours",
                table: "Events",
                type: "int",
                nullable: false,
                comment: "The duration of the event in hours.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Events",
                type: "nvarchar(1500)",
                maxLength: 1500,
                nullable: false,
                comment: "The description of the event.",
                oldClrType: typeof(string),
                oldType: "nvarchar(1500)",
                oldMaxLength: 1500);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Events",
                type: "int",
                nullable: false,
                comment: "The unique identifier of the event.",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "YearFounded",
                table: "Distilleries",
                type: "int",
                nullable: false,
                comment: "The year the distillery was founded.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RegionId",
                table: "Distilleries",
                type: "int",
                nullable: false,
                comment: "The identifier of the region where the distillery is located.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Distilleries",
                type: "nvarchar(57)",
                maxLength: 57,
                nullable: false,
                comment: "The name of the distillery.",
                oldClrType: typeof(string),
                oldType: "nvarchar(57)",
                oldMaxLength: 57);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Distilleries",
                type: "int",
                nullable: false,
                comment: "The unique identifier of the distillery.",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: false,
                comment: "The identifier of the user who posted the comment.",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostedDate",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                comment: "The date the comment was posted.",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Comments",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                comment: "The content of the comment.",
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<int>(
                name: "ArticleId",
                table: "Comments",
                type: "int",
                nullable: false,
                comment: "The identifier of the article associated with the comment.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Comments",
                type: "int",
                nullable: false,
                comment: "The unique identifier of the comment.",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                table: "Cities",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                comment: "The zip code of the city.",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cities",
                type: "nvarchar(187)",
                maxLength: 187,
                nullable: false,
                comment: "The name of the city.",
                oldClrType: typeof(string),
                oldType: "nvarchar(187)",
                oldMaxLength: 187);

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Cities",
                type: "int",
                nullable: false,
                comment: "The identifier of the country that the city belongs to.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Cities",
                type: "int",
                nullable: false,
                comment: "The unique identifier of the city.",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "Awards",
                type: "int",
                nullable: false,
                comment: "The year the award was given.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "WhiskyId",
                table: "Awards",
                type: "int",
                nullable: false,
                comment: "The identifier of the whisky associated with the award.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Awards",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                comment: "The title of the award.",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Awards",
                type: "nvarchar(max)",
                nullable: true,
                comment: "The URL of the image associated with the award.",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Awards",
                type: "int",
                nullable: false,
                comment: "The unique identifier of the award.",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Articles",
                type: "int",
                nullable: false,
                comment: "The type of the article.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Articles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "The title of the article.",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "PublisherUserId",
                table: "Articles",
                type: "nvarchar(450)",
                nullable: false,
                comment: "The identifier of the user who published the article.",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: false,
                comment: "The URL of the image associated with the article.",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Articles",
                type: "datetime2",
                nullable: false,
                comment: "The date the article was created.",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Articles",
                type: "nvarchar(2500)",
                maxLength: 2500,
                nullable: false,
                comment: "The content of the article.",
                oldClrType: typeof(string),
                oldType: "nvarchar(2500)",
                oldMaxLength: 2500);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Articles",
                type: "int",
                nullable: false,
                comment: "The unique identifier of the article.",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "WhiskyTypes",
                oldComment: "Represents a whisky type entity.");

            migrationBuilder.AlterTable(
                name: "Whiskies",
                oldComment: "Represents a whisky entity.");

            migrationBuilder.AlterTable(
                name: "Venues",
                oldComment: "Represents a venue entity.");

            migrationBuilder.AlterTable(
                name: "UsersEvents",
                oldComment: "Represents a mapping entity between a user and an event.");

            migrationBuilder.AlterTable(
                name: "Reviews",
                oldComment: "Represents a review entity.");

            migrationBuilder.AlterTable(
                name: "Regions",
                oldComment: "Represents a region entity.");

            migrationBuilder.AlterTable(
                name: "Ratings",
                oldComment: "Represents a rating entity.");

            migrationBuilder.AlterTable(
                name: "Events",
                oldComment: "Represents an event entity.");

            migrationBuilder.AlterTable(
                name: "Distilleries",
                oldComment: "Represents a distillery entity.");

            migrationBuilder.AlterTable(
                name: "Comments",
                oldComment: "Represents a comment entity.");

            migrationBuilder.AlterTable(
                name: "Cities",
                oldComment: "Represents a city entity.");

            migrationBuilder.AlterTable(
                name: "Awards",
                oldComment: "Represents an award entity.");

            migrationBuilder.AlterTable(
                name: "Articles",
                oldComment: "Represents an article entity.");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "WhiskyTypes",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25,
                oldComment: "The name of the whisky type.");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "WhiskyTypes",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldComment: "The description of the whisky type.");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "WhiskyTypes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The unique identifier of the whisky type.")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "WhiskyTypeId",
                table: "Whiskies",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The identifier of the type of the whisky.");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Whiskies",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(70)",
                oldMaxLength: 70,
                oldComment: "The name of the whisky.");

            migrationBuilder.AlterColumn<int>(
                name: "DistilleryId",
                table: "Whiskies",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The identifier of the distillery that produced the whisky.");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Whiskies",
                type: "nvarchar(1500)",
                maxLength: 1500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1500)",
                oldMaxLength: 1500,
                oldComment: "The description of the whisky.");

            migrationBuilder.AlterColumn<double>(
                name: "AlcoholPercentage",
                table: "Whiskies",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float",
                oldComment: "The alcohol percentage of the whisky.");

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "Whiskies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "The age of the whisky.");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Whiskies",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The unique identifier of the whisky.")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Venues",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "The name of the venue.");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Venues",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The identifier of the city where the venue is located.");

            migrationBuilder.AlterColumn<int>(
                name: "Capacity",
                table: "Venues",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The capacity of the venue.");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Venues",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The unique identifier of the venue.")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UsersEvents",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldComment: "The identifier of the user associated with the event.");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "UsersEvents",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The identifier of the event associated with the user.");

            migrationBuilder.AlterColumn<int>(
                name: "WhiskyId",
                table: "Reviews",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The identifier of the whisky being reviewed.");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldComment: "The identifier of the user who made the review.");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Reviews",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "The title of the review.");

            migrationBuilder.AlterColumn<bool>(
                name: "Recommend",
                table: "Reviews",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "If the user recommends status the whisky");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Reviews",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldComment: "The content of the review.");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Reviews",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The unique identifier of the review.")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Regions",
                type: "nvarchar(85)",
                maxLength: 85,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(85)",
                oldMaxLength: 85,
                oldComment: "The name of the region.");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Regions",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The identifier of the country that the region belongs to.");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Regions",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The unique identifier of the region.")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "WhiskyId",
                table: "Ratings",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The identifier of the whisky being rated.");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Ratings",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldComment: "The identifier of the user who gave the rating.");

            migrationBuilder.AlterColumn<int>(
                name: "Taste",
                table: "Ratings",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Represents the rating that is given for the whisky taste.");

            migrationBuilder.AlterColumn<int>(
                name: "Nose",
                table: "Ratings",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Represents the rating that is given for the whisky aroma.");

            migrationBuilder.AlterColumn<int>(
                name: "Finish",
                table: "Ratings",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Represents the rating that is given for the whisky finishing notes.");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Ratings",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The unique identifier of the rating.")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "VenueId",
                table: "Events",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The identifier of the venue where the event will take place.");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Events",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldComment: "The title of the event.");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Events",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "The start date of the event.");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Events",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true,
                oldComment: "The price of the event.");

            migrationBuilder.AlterColumn<string>(
                name: "OrganiserId",
                table: "Events",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldComment: "The identifier of the user who organised the event.");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Events",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "The end date of the event.");

            migrationBuilder.AlterColumn<int>(
                name: "DurationInHours",
                table: "Events",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The duration of the event in hours.");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Events",
                type: "nvarchar(1500)",
                maxLength: 1500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1500)",
                oldMaxLength: 1500,
                oldComment: "The description of the event.");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Events",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The unique identifier of the event.")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "YearFounded",
                table: "Distilleries",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The year the distillery was founded.");

            migrationBuilder.AlterColumn<int>(
                name: "RegionId",
                table: "Distilleries",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The identifier of the region where the distillery is located.");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Distilleries",
                type: "nvarchar(57)",
                maxLength: 57,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(57)",
                oldMaxLength: 57,
                oldComment: "The name of the distillery.");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Distilleries",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The unique identifier of the distillery.")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldComment: "The identifier of the user who posted the comment.");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostedDate",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "The date the comment was posted.");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Comments",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldComment: "The content of the comment.");

            migrationBuilder.AlterColumn<int>(
                name: "ArticleId",
                table: "Comments",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The identifier of the article associated with the comment.");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Comments",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The unique identifier of the comment.")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                table: "Cities",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldComment: "The zip code of the city.");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cities",
                type: "nvarchar(187)",
                maxLength: 187,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(187)",
                oldMaxLength: 187,
                oldComment: "The name of the city.");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Cities",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The identifier of the country that the city belongs to.");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Cities",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The unique identifier of the city.")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "Awards",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The year the award was given.");

            migrationBuilder.AlterColumn<int>(
                name: "WhiskyId",
                table: "Awards",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The identifier of the whisky associated with the award.");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Awards",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldComment: "The title of the award.");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Awards",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "The URL of the image associated with the award.");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Awards",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The unique identifier of the award.")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Articles",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The type of the article.");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Articles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "The title of the article.");

            migrationBuilder.AlterColumn<string>(
                name: "PublisherUserId",
                table: "Articles",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldComment: "The identifier of the user who published the article.");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "The URL of the image associated with the article.");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Articles",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "The date the article was created.");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Articles",
                type: "nvarchar(2500)",
                maxLength: 2500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2500)",
                oldMaxLength: 2500,
                oldComment: "The content of the article.");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Articles",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The unique identifier of the article.")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
