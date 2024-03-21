using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheWhiskyRealm.Infrastructure.Data
{
    public partial class AwardsSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 86);
        }
    }
}
