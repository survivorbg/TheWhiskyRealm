using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheWhiskyRealm.Infrastructure.Data
{
    public partial class WhiskyTypeSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WhiskyTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WhiskyTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WhiskyTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "WhiskyTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "WhiskyTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "WhiskyTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "WhiskyTypes",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
