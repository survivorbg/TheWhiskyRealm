using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheWhiskyRealm.Infrastructure.Data
{
    public partial class WhiskySeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Whiskies",
                columns: new[] { "Id", "Age", "AlcoholPercentage", "Description", "DistilleryId", "Name", "WhiskyTypeId" },
                values: new object[,]
                {
                    { 1, 12, 40.0, "Triple distilled single malt whiskey. It has a tempting aroma of roasted almonds, caramel and the characteristic soft and delicate taste of Auchentoshan. It is aged in oak barrels, in which the Spanish \"Oloroso\" sherry and bourbon were previously aged.\r\nThe Auchentoshan Distillery is one of three in the Lowland area that continues to operate to this day. It is located north of Glasgow and was founded in 1800. The name of the excellent brand comes from Celtic and means \"The corner of the field\".", 1, "Auchentoshan 12 Year Old", 1 },
                    { 2, 10, 40.0, "Glenmorangie whiskeys are produced in the Highlands, the northernmost part of Scotland. They are distilled in the highest copper cauldrons in Scotland and aged in the highest quality oak barrels, which are used only twice to produce the purest and most elegant whiskeys. The distillery is known as an innovator in the production of whiskey, combining tradition with innovation, resulting in malt whiskey with \"unnecessarily\" high quality. A symbol of Glenmorangie whiskey, the ten-year-old Glenmorangie The Original is the most delicate single malt whiskey in the world, with the most complex taste and seductive aroma. It has an exceptional fruity elegance, exquisite finesse and a seductive complex taste. The delicate floral notes in the whiskey are combined with the softness and sweetness acquired from the premium bourbon oak barrels.", 2, "Glenmorangie 10 Year Old", 1 },
                    { 3, 18, 43.0, "The Sherry Oak Collection is a timeless sensorial journey. Beautifully led by European Oak, sherry seasoned in Jerez de la Frontera, Spain. Photographer Erik Madigan Heck has captured his visual interpretation of The Macallan Sherry Oak 18 Years Old through the lens of his unique, signature style.", 3, "The Macallan 18 Year Old Sherry Oak", 1 },
                    { 4, 10, 40.0, "The famous 10-year-old Laphroaig whisky has an extremely smoky flavour with a hint of seaweed and the sea. It is one of the most intense Islay malts. You either hate it or you love it.", 4, "Laphroaig 10 Year Old", 1 },
                    { 5, 15, 46.0, "A 15-year-old single malt from the Springbank distillery with plenty of sherry notes and spice, dried fruits, and nuts.", 5, "Springbank 15 Year Old", 1 },
                    { 6, 18, 43.0, "Highland Park's 18 Year Old enjoyed a redesign in 2017, receiving livery inspired by the wood carvings from Urnes Stave Church and a new sub-name, \"Viking Pride\". The Orkney single malt remains the same as before - rich, complex and supremely delicious.", 6, "Highland Park 18 Year Old Viking Pride", 1 },
                    { 7, null, 40.0, "Bushmill's original whisky is a smooth, easy-drinking whiskey that has been produced in Ireland for centuries.\r\nBushmills Original is made up of grain whiskey matured for five years before blending with malt whiskeys. Bushmill's Irish whiskey is triple distilled and very supple.\r\nBushmills Original is a consistent, great value-for-money, triple-distilled blend made with a core of flavoursome malt and grain whiskies", 7, "Bushmills Original", 5 },
                    { 8, null, 40.0, "Produced at the Midleton Distillery, Jameson is Ireland's quintessential Irish blend – a classic whiskey. Jim Murray even awarded it an incredible 95 points!", 8, "Jameson", 5 },
                    { 9, null, 40.0, "Jack Daniel's Tennessee Whiskey has been made at its Lynchburg distillery since 1875. The branding and original label, sometimes referred to as No. 7 or Black Label; has made its way into pop culture, with merchandise sold the world over and a history of association with music. Frank Sinatra was even buried with a bottle. The Tennessee whiskey makers use a mash bill made up of 80% corn, 12% rye, and 8% malt to create Jack Daniels whiskey, which is then filtered through 10 feet of sugar maple charcoal to produce a mellow, slightly smoky character. A method known as the Lincoln County Process, it means this is not a bourbon, but instead meets the legal definition of a Tennessee whiskey. Jasper Newton \"Jack\" Daniel ( c. January 1849 – October 9, 1911) was an American distiller and businessman, best known as the founder of the Jack Daniel's Tennessee whiskey distillery.", 9, "Jack Daniel's Old No. 7", 3 },
                    { 10, null, 40.0, "Jim Beam bourbon undergoes distillation at lower temperatures and is distilled to no more than 62.5%, the White label is aged for four years and has quite a high percentage of rye in the mashbill.", 10, "Jim Beam White Label", 3 },
                    { 11, null, 45.0, "Using the two Coffey stills at the Miyagikyo distillery, which were imported from Scotland to Japan in 1963, Nikka have produced a number of single cask single grain whiskies from time to time over the years. This, however, is their larger release of wonderfully exotic grain whisky. Now in a 70cl bottle! Hooray!", 12, "Nikka Coffey Grain", 6 },
                    { 12, null, 59.399999999999999, "A single cask, cask strength single malt from Taiwan's Kavalan, released for the Solist series. Matured in a single sherry cask (#S081217042 to be exact), this one was bottled without colouring at a generous 59.4% ABV. If you like sherry bombs and award-winning whisky, look no further!", 13, "Kavalan Solist Sherry", 1 },
                    { 13, null, 50.0, "Fusion is a particularly apt name for this fantastic single malt whisky from Amrut. Y'see, it's made with barley grown in India, where the Amrut Distillery can be found, as well as peated barley from Scotland! Makes sense, right? Not just a clever name, it's also a cracking whisky, offering up generous helpings of fresh fruit, honey, spice and a good whiff of smoke.", 14, "Amrut Fusion", 1 },
                    { 14, null, 43.399999999999999, "A smooth amber rye whiskey with complex notes of caramel, toffee and spice with subtle undertones of green apple and pear. Enjoy the roasted rye spices that complement the soft toffee and vanilla flavors, neatly sipped on its own for a lingering green apple finish.", 15, "JP Wiser's Triple Barrel Rye", 4 },
                    { 15, null, 40.0, "A pure expression of rye whisky; more complex and characterful, this pours medium gold. Aromas of sweet fruit, herb and spice, with vanilla, toffee, pepper, cedar smoke and banana on the nose. The sweet, creamy and warm palate is balanced by rye spice flavours followed by a long finish showing dried fruit, honey and ginger.", 15, "Canadian Club 100% Rye", 4 },
                    { 16, null, 43.0, "A Lowland single malt matured in 3 different casks, namely: Pedro Ximénez Sherry casks, bourbon casks and Oloroso Sherry casks. A distinctive triple distilled whisky from Auchentoshan.", 1, "Auchentoshan Three Wood", 1 },
                    { 17, 12, 43.0, "A rich and fruity single malt Scotch whisky finished in sherry casks.", 2, "Glenmorangie Lasanta 12 Year Old", 1 },
                    { 18, 12, 40.0, "An exciting new age statement single malt Scotch whisky from Macallan that's matured in a combination of American and European Sherry oak for a minimum of 12 years.", 3, "The Macallan Double Cask 12 Year Old", 1 },
                    { 19, null, 48.0, "Released in 2004, this bottling was aged for around five years before being finished in a quarter cask for several months, the size of the cask is quite small, thus does not require such a long maturation. This remains a truly great achievement from Laphroaig.", 4, "Laphroaig Quarter Cask", 1 },
                    { 20, 18, 46.0, "The 2021 release of the 18-year-old from Springbank. Matured in a combination of 50% ex-bourbon and 50% ex-sherry casks then bottled at 46% abv.", 5, "Springbank 18 Year Old 2021 Release", 1 },
                    { 21, 12, 40.0, "With a sub-name like Viking Honour, you can expect a lot from this Orcadian single malt Scotch whisky – and it delivers! A great introduction to Highland Park's famed heathery peat smoke, the 12 Year Old is matured predominantly in sherry-seasoned European and American oak casks, so it's spicy, citrusy, and full of smoky aromatics.", 6, "Highland Park 12 Year Old Viking Honour", 1 },
                    { 22, null, 40.0, "If you've been in a bar (any bar, really), you've more than likely seen a bottle of this on the shelf. One of the most well-known Irish blends, Bushmills Black Bush features a lot of sherried malt in its recipe, alongside classically caramel-y grain whiskey. Suitable for enjoying neat, but it can also be used in whiskey cocktails that call for dark fruit sweetness...", 7, "Bushmills Black Bush", 5 },
                    { 23, null, 40.0, "Jameson Caskmates is an intriguing release. Having sent some of their casks to the local craft stout brewers at Franciscan Well, the casks were returned to Midleton where they were subsequently used to give a stout finish to Jameson!", 8, "Jameson Caskmates Stout Edition", 5 },
                    { 24, null, 45.0, "You all know Jack Daniel’s and have no doubt seen its classic whiskey in bars and shops all over the world. But the Tennessee-based distillery is also home to lots of premium expressions, like the single barrel bottling you see before you. A premium version of Jack Daniel's, each of these comes from a cask specially selected by the master distiller. These are chosen for their suitability as standalone spirits, resulting in a whiskey full of richness and complexity.", 9, "Jack Daniel's Single Barrel", 3 },
                    { 25, 8, 43.0, "There's an old axiom that claims \"Two heads are better than one\". Can that theory be transferred to whiskey barrels? Jim Beam's Double Oak might answer that question. You see, they initially mature this bourbon in freshly charred American oak barrels, and then move the whiskey over to a fresh set of freshly charred American oak barrels for the second part of its maturation!\r\n\r\nA worthy replacement for the now discontinued Jim Beam Black.", 10, "Jim Beam Double", 3 },
                    { 26, null, 51.399999999999999, "The award-winning Nikka Whisky From The Barrel blend is absolutely full of flavour. Bottled at 51.4% ABV. The blend combines both single malt and grain whisky from the Miyagikyo and Yoichi distilleries, which are then married in a huge variety of casks, including bourbon barrels, sherry butts and refill hogsheads. A huge depth of flavour in this stunning Japanese whisky. We can't recommend this enough.", 12, "Nikka From the Barrel", 6 },
                    { 27, null, 40.0, "Taiwanese whisky has been a 'thing' for a while now, since 2008 in fact, but the highly-regarded Kavalan whiskies are now finally available in Europe! This single malt whisky utilises Ruby Port, Tawny Port and Vintage Port casks from Portugal to finish whiskies that were initially matured in American oak. Kavalan Concertmaster was named Best in Class at the 2011 International Wine & Spirit Competition.", 13, "Kavalan Concertmaster Port Cask Finish", 1 },
                    { 28, null, 46.0, "From one of India's most famous whisky producers, this smoky single malt is made from barley peated to 24ppm. It’s punchy but still fruity and malty, with the ABV increased after its initial release to 46% to add more weight and texture.", 14, "Amrut Peated Single Malt", 1 },
                    { 29, 18, 40.0, "A single grain whisky that is dominated by aromas of green apple in part due to the unique aging conditions in Southern Ontario. It pours a medium golden amber with additional aromas of caramel, orange peel and spice; the palate is round and medium-bodied with a silky texture and a smooth vanilla driven, finish.", 15, "JP Wiser's 18 Year Old", 4 },
                    { 30, 12, 40.0, "The Canadian Club Classic matured for 12 years, which is a long age for a Canadian whisky. With age comes perfection. Selected whiskies mature together to create a mild and naturally smooth Blend.", 15, "Canadian Club Classic 12 Year Old", 2 },
                    { 31, 18, 40.0, "This 10-year-old single malt from The Macallan Fine Oak Range was matured in a mix of bourbon and sherry casks.", 3, "The Macallan 10 Year Old Fine Oak", 1 },
                    { 32, 21, 46.5, "Those clever clogs over at Darkness got their hands on a sought-after drop of whisky here. This 21-year-old Springbank single malt was given the Darkness treatment with a finish in one of its custom-made octave casks, that previously held oloroso sherry. The coastal malt and sherried sweetness make a perfect partnership.", 5, "Springbank 21 Year Old Oloroso Cask Finish (Darkness)", 1 },
                    { 33, 25, 58.399999999999999, "Highland Park Bulgaria 681 is one of the three whiskies in limited edition series specially dedicated to Bulgaria, as it celebrates legendary moments from our history.\r\nThe concept includes a series of 3 collector's bottles, which will commemorate 3 key victories of the First Bulgarian Empire. Highland Park Bulgaria 681 marks the year when the Byzantine Empire recognized the existence of the Bulgarian state by signing a peace treaty after the victory of the Proto-Bulgarians, led by Khan Asparuh. ", 6, "Highland Park Bulgaria 681", 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Whiskies",
                keyColumn: "Id",
                keyValue: 33);
        }
    }
}
