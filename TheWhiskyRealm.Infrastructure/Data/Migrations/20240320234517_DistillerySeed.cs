using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheWhiskyRealm.Infrastructure.Data
{
    public partial class DistillerySeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Distilleries",
                columns: new[] { "Id", "ImageUrl", "Name", "RegionId", "YearFounded" },
                values: new object[,]
                {
                    { 1, "https://keyassets.timeincuk.net/inspirewp/live/wp-content/uploads/sites/34/2022/06/Auchentoshan-Distillery-920x609.jpg", "Auchentoshan", 1, 1823 },
                    { 2, "https://www.secret-scotland.com/datafiles/uploaded/cmsRefImage/popularPlaces/additional/main/main_110_Glenmorangie.jpg", "Glenmorangie", 2, 1843 },
                    { 3, "https://www.robertson.co.uk/sites/default/files/styles/news_header/public/news_images/Macallan%20press%20image%20POM2018124G0800208003.jpg?itok=wyUPa5o2", "The Macallan", 3, 2017 },
                    { 4, "https://www.laphroaig.com/sites/default/files/styles/style_20_9/public/2022-06/Laphroaig_Distillery_banner_DT.jpg.webp?itok=k-vrPwBD", "Laphroaig", 6, 1815 },
                    { 5, "https://www.whisky.com/fileadmin/_processed_/4/5/csm__MG_5906_732d67d3b250e58ad3dcdcddda309e7a_f491af0c94.jpg", "Springbank", 5, 1828 },
                    { 6, "https://www.highlandparkwhisky.com/sites/g/files/jrulke331/files/styles/text_structured_image/public/Highland%20Park%20Distillery.jpg?itok=OxqQUxKL", "Highland Park", 4, 1798 },
                    { 7, "https://www.discoveringireland.com/contentFiles/productImages/Medium/Bushmills_Distillery.jpg", "Bushmills", 7, 1784 },
                    { 8, "https://static.wixstatic.com/media/ea720f_24a89e7c992347e68f452dbcc114dee1~mv2.jpg/v1/fill/w_598,h_390,al_c,q_80,usm_0.66_1.00_0.01,enc_auto/ea720f_24a89e7c992347e68f452dbcc114dee1~mv2.jpg", "Jameson", 9, 1780 },
                    { 9, "https://americanwhiskeytrail.distilledspirits.org/sites/default/files/styles/flexslider_full/public/distillery/field_slides/Jack%20Daniels%20Visitor%27s%20Center_opt.jpg?itok=vpZAEgVu", "Jack Daniel's", 12, 1866 },
                    { 10, "https://www.foodandwine.com/thmb/rVUUWewQYYdDzAQMPOQTZH06nzQ=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/James-Beam-Distillery-FT-BLOG1021-28072a663ffe4cf8ac3adeb05b843143.jpg", "Jim Beam", 11, 1795 },
                    { 12, "https://www.nikka.com/eng/img/distilleries/topmenu_miyagikyo.jpg", "Nikka", 13, 1934 },
                    { 13, "https://whiskyetc.files.wordpress.com/2019/05/kavalan-distillery-03-1.jpg", "Kavalan", 14, 2005 },
                    { 14, "https://www.whisky.com/fileadmin/_processed_/1/5/csm_IMG_0402_718cec422ff28f51b8654e744519f5ec_1fb068e7c2.jpg", "Amrut", 15, 1948 },
                    { 15, "https://smartcdn.gprod.postmedia.digital/windsorstar/wp-content/uploads/2021/02/hiram-walker-sons-distillery-1.jpg", "Hiram-Walker & Sons distillery", 16, 1858 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 15);
        }
    }
}
