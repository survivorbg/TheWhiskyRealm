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
                columns: new[] { "Id", "Name", "YearFounded", "RegionId", "ImageUrl" },
                values: new object[,]
                {
                    {
            1, "Aberargie", 2017, 1, null
        },
        {
            2, "Aberfeldy", 1896, 2, null
        },
        {
            3, "Aberlour", 1879, 3, null
        },
        {
            4, "Abhainn Dearg", 2008, 4, null
        },
        {
            5, "Ailsa Bay", 2009, 1, null
        },
        {
            6, "Allt-A-Bhainne", 1975, 3, null
        },
        {
            7, "Annandale", 1836, 1, null
        },
        {
            8, "Arbikie", 2013, 2, null
        },
        {
            9, "Ardbeg", 1815, 6, null
        },
        {
            10, "Ardmore", 1898, 2, null
        },
        {
            11, "Ardnahoe", 2019, 6, null
        },
        {
            12, "Ardnamurchan", 2014, 2, null
        },
        {
            13, "Ardross", 2019, 2, null
        },
        {
            14, "Arran", 1995, 4, null
        },
        {
            15, "Auchentoshan", 1823, 1, "https://keyassets.timeincuk.net/inspirewp/live/wp-content/uploads/sites/34/2022/06/Auchentoshan-Distillery-920x609.jpg"
        },
        {
            16, "Auchroisk", 1972, 3, null
        },
        {
            17, "Aultmore", 1895, 3, null
        },
        {
            18, "Balblair", 1790, 2, null
        },
        {
            19, "Ballindalloch", 2014, 3, null
        },
        {
            20, "Balmenach", 1824, 3, null
        },
        {
            21, "Balvenie", 1892, 3, null
        },
        {
            22, "Ben Nevis", 1825, 2, null
        },
        {
            23, "BenRiach", 1898, 3, null
        },
        {
            24, "Benrinnes", 1826, 3, null
        },
        {
            25, "Benromach", 1898, 3, null
        },
        {
            26, "Bladnoch", 1817, 1, null
        },
        {
            27, "Blair Athol", 1798, 2, null
        },
        {
            28, "Bonnington", 2019, 1, null
        },
        {
            29, "Borders", 2018, 1, null
        },
        {
            30, "Bowmore", 1779, 6, null
        },
        {
            31, "Braeval", 1973, 3, null
        },
        {
            32, "Brora", 1819, 2, null
        },
        {
            33, "Bruichladdich", 1881, 6, null
        },
        {
            34, "Bunnahabhain", 1881, 6, null
        },
        {
            35, "Burn O Bennie", 2021, 2, null
        },
        {
            36, "Cairn", 2022, 3, null
        },
        {
            37, "Caol Ila", 1846, 6, null
        },
        {
            38, "Cardhu", 1824, 3, null
        },
        {
            39, "Clydeside", 2017, 1, null
        },
        {
            40, "Clynelish", 1967, 2, null
        },
        {
            41, "Crafty", 2017, 1, null
        },
        {
            42, "Cragganmore", 1869, 3, null
        },
        {
            43, "Craigellachie", 1891, 3, null
        },
        {
            44, "Daftmill", 2005, 1, null
        },
        {
            45, "Dailuaine", 1852, 3, null
        },
        {
            46, "Dalmore", 1839, 2, null
        },
        {
            47, "Dalmunach", 2015, 3, null
        },
        {
            48, "Dalwhinnie", 1898, 2, null
        },
        {
            49, "Deanston", 1965, 2, null
        },
        {
            50, "Dornoch", 2016, 2, null
        },
        {
            51, "Dufftown", 1895, 3, null
        },
        {
            52, "Dunphail", 2023, 3, null
        },
        {
            53, "Eden Mill", 2012, 1, null
        },
        {
            54, "Edradour", 1825, 2, "https://thewhiskyphiles.files.wordpress.com/2018/11/edradour-distillery.jpg"
        },
        {
            55, "Falkirk", 2020, 1, null
        },
        {
            56, "Fettercairn", 1824, 2, null
        },
        {
            57, "Glasgow", 2015, 1, null
        },
        {
            58, "Glenallachie", 1967, 3, null
        },
        {
            59, "Glenburgie", 1810, 3, null
        },
        {
            60, "Glencadam", 1825, 2, null
        },
        {
            61, "Glendronach", 1826, 2, "https://www.thespiritsbusiness.com/content/uploads/2022/07/GlenDronach.jpg"
        },
        {
            62, "Glendullan", 1897, 3, null
        },
        {
            63, "Glen Elgin", 1898, 3, null
        },
        {
            64, "Glenfarclas", 1836, 3, null
        },
        {
            65, "Glenfiddich", 1886, 3, "https://www.distillerytours.scot/uploads/store/mediaupload/1601/image/xl_limit-20210807_Glenfiddich_0391.jpg"
        },
        {
            66, "Glen Garioch", 1797, 2, null
        },
        {
            67, "Glenglassaugh", 1875, 2, null
        },
        {
            68, "Glengoyne", 1833, 2, null
        },
        {
            69, "Glen Grant", 1840, 3, null
        },
        {
            70, "Glengyle", 1872, 5, null
        },
        {
            71, "Glen Keith", 1957, 3, null
        },
        {
            72, "Glenkinchie", 1837, 1, null
        },
        {
            73, "Glenlivet", 1824, 3, null
        },
        {
            74, "Glenlossie", 1876, 3, null
        },
        {
            75, "Glenmorangie", 1843, 2, "https://www.secret-scotland.com/datafiles/uploaded/cmsRefImage/popularPlaces/additional/main/main_110_Glenmorangie.jpg"
        },
        {
            76, "Glen Moray", 1897, 3, "https://www.distillerytours.scot/uploads/store/mediaupload/1315/image/xl_limit-Still_House.jpg"
        },
        {
            77, "Glen Ord", 1838, 2, null
        },
        {
            78, "Glenrothes", 1879, 3, null
        },
        {
            79, "Glen Scotia", 1832, 5, null
        },
        {
            80, "Glen Spey", 1878, 3, null
        },
        {
            81, "Glentauchers", 1897, 3, null
        },
        {
            82, "Glenturret", 1763, 2, null
        },
        {
            83, "GlenWyvis", 2015, 2, null
        },
        {
            84, "Harris", 2015, 4, null
        },
        {
            85, "Highland Park", 1798, 4, "https://www.highlandparkwhisky.com/sites/g/files/jrulke331/files/styles/text_structured_image/public/Highland%20Park%20Distillery.jpg?itok=OxqQUxKL"
        },
        {
            86, "Holyrood", 2019, 1, null
        },
        {
            87, "Inchdairnie", 2016, 1, null
        },
        {
            88, "Inchgower", 1871, 3, null
        },
        {
            89, "Jackton", 2020, 1, null
        },
        {
            90, "Jura", 1810, 4, null
        },
        {
            91, "Kilchoman", 2005, 6, null
        },
        {
            92, "Kimbland", 2017, 4, null
        },
        {
            93, "Kingsbarns", 2014, 1, null
        },
        {
            94, "Kininvie", 1990, 3, null
        },
        {
            95, "Knockando", 1898, 3, null
        },
        {
            96, "Knockdhu", 1894, 3, null
        },
        {
            97, "Lagg", 2019, 4, null
        },
        {
            98, "Lagavulin", 1816, 6, null
        },
        {
            99, "Laphroaig", 1815, 6, "https://www.laphroaig.com/sites/default/files/styles/style_20_9/public/2022-06/Laphroaig_Distillery_banner_DT.jpg.webp?itok=k-vrPwBD"
        },
        {
            100, "Leven", 2013, 1, null
        },
        {
            101, "Lindores Abbey", 2017, 1, null
        },
        {
            102, "Linkwood", 1821, 3, null
        },
        {
            103, "Lochlea", 2018, 1, null
        },
        {
            104, "Loch Lomond", 1964, 2, null
        },
        {
            105, "Lone Wolf", 2016, 2, null
        },
        {
            106, "Longmorn", 1893, 3, null
        },
        {
            107, "The Macallan", 1824, 3, "https://www.robertson.co.uk/sites/default/files/styles/news_header/public/news_images/Macallan%20press%20image%20POM2018124G0800208003.jpg?itok=wyUPa5o2"
        },
        {
            108, "Macduff", 1960, 2, null
        },
        {
            109, "Mannochmore", 1971, 3, null
        },
        {
            110, "Miltonduff", 1824, 3, null
        },
        {
            111, "Mortlach", 1823, 3, null
        },
        {
            112, "Nc nean", 2017, 2, null
        },
        {
            113, "Oban", 1794, 2, null
        },
        {
            114, "Port Ellen", 1825, 6, null
        },
        {
            115, "Port of Leith", 2023, 1, null
        },
        {
            116, "Pulteney", 1826, 2, null
        },
        {
            117, "Raasay", 2014, 4, null
        },
        {
            118, "Rosebank", 1798, 1, null
        },
        {
            119, "Roseisle", 2010, 3, null
        },
        {
            120, "Royal Brackla", 1812, 2, null
        },
        {
            121, "Royal Lochnagar", 1845, 2, null
        },
        {
            122, "Saxa Vord", 2015, 4, null
        },
        {
            123, "Scapa", 1885, 4, null
        },
        {
            124, "Speyburn", 1897, 3, null
        },
        {
            125, "Speyside", 1990, 3, null
        },
        {
            126, "Springbank", 1828, 5, "https://www.whisky.com/fileadmin/_processed_/4/5/csm__MG_5906_732d67d3b250e58ad3dcdcddda309e7a_f491af0c94.jpg"
        },
        {
            127, "Strathearn", 2013, 2, null
        },
        {
            128, "Strathisla", 1786, 3, null
        },
        {
            129, "Strathmill", 1891, 3, null
        },
        {
            130, "Talisker", 1830, 4, null
        },
        {
            131, "Tamdhu", 1897, 3, null
        },
        {
            132, "Tamnavulin", 1966, 3, null
        },
        {
            133, "Teaninich", 1817, 2, null
        },
        {
            134, "Tobermory", 1798, 4, null
        },
        {
            135, "Tomatin", 1897, 2, null
        },
        {
            136, "Tomintoul", 1964, 3, null
        },
        {
            137, "Torabhaig", 2017, 4, null
        },
        {
            138, "Tormore", 1958, 3, null
        },
        {
            139, "Tullibardine", 1949, 2, null
        },
        {
            140, "Uile-bheist", 2023, 2, null
        },
        {
            141, "Wolfburn", 1822, 2, null
        },
        {
            142, "8 Doors", 2022, 2, null
        },
        {
            143, "Chichibu", 2008, 28, null
        },
        {
            144, "Eigashima Shuzo", 1919, 29, null
        },
        {
            145, "Fuji Gotemba", 1971, 30, null
        },
        {
            146, "Hakushu", 1973, 31, "https://www.suntory.co.jp/factory/ogp/hakushu_top.jpg"
        },
        {
            147, "Hanyu", 1946, 28, null
        },
        {
            148, "Karuizawa", 2022, 32, "https://sp-ao.shortpixel.ai/client/q_lossless,ret_img/https://dekanta.com/wp-content/uploads/revslider/karuizawa-distillery/Untitled-2.jpg"
        },
        {
            149, "Miyagikyo", 1969, 33, "https://150102931.v2.pressablecdn.com/wp-content/uploads/2019/11/brick-distillery-Nikka-byJwolfson-1024x768.jpeg"
        },
        {
            150, "Shizuoka", 2016, 30, null
        },
        {
            151, "The Mars Shinshu Distillery", 1985, 32, null
        },
        {
            152, "Yamazaki", 1923, 34, "https://cdn.osaka-info.jp/page_translation/content/16abce82-04c2-11e8-8efc-06326e701dd4.jpeg"
        },
        {
            153, "Yoichi", 1934, 35, "https://www.nikka.com/eng/img/distilleries/topmenu_yoichi.jpg"
        },
        {
            154, "Achill Island", 2015, 7, null
        },
        {
            155, "Ballykeefe", 2017, 8, null
        },
        {
            156, "Baoilleach", 2019, 9, null
        },
        {
            157, "Blackwater", 2014, 10, null
        },
        {
            158, "Boann", 2019, 11, null
        },
        {
            159, "Boatyard", 2016, 12, null
        },
        {
            160, "Burren Whiskey", 2019, 13, null
        },
        {
            161, "Clonakilty", 2016, 14, null
        },
        {
            162, "Cooley", 1987, 15, null
        },
        {
            163, "Copeland", 2019, 16, null
        },
        {
            164, "Crolly", 2020, 9, null
        },
        {
            165, "Dingle", 2012, 17, null
        },
        {
            166, "Echlinville", 2013, 16, null
        },
        {
            167, "Glendalough", 2013, 18, null
        },
        {
            168, "Glendree", 2019, 13, null
        },
        {
            169, "Great Northern", 2015, 15, null
        },
        {
            170, "Hinch", 2020, 16, null
        },
        {
            171, "Kilbeggan", 1757, 19, "https://dynamic-media-cdn.tripadvisor.com/media/photo-o/0f/af/82/f4/situated-on-the-brosna.jpg?w=1200&h=1200&s=1"
        },
        {
            172, "Killowen", 2019, 16, null
        },
        {
            173, "Lough Gill", 2019, 20, null
        },
        {
            174, "Lough Mask", 2019, 7, null
        },
        {
            175, "New Midleton", 1975, 14, "https://static.wixstatic.com/media/ea720f_24a89e7c992347e68f452dbcc114dee1~mv2.jpg/v1/fill/w_598,h_390,al_c,q_80,usm_0.66_1.00_0.01,enc_auto/ea720f_24a89e7c992347e68f452dbcc114dee1~mv2.jpg"
        },
        {
            176, "Old Bushmills", 1784, 21, "https://www.discoveringireland.com/contentFiles/productImages/Medium/Bushmills_Distillery.jpg"
        },
        {
            177, "Powerscourt", 2018, 18, null
        },
        {
            178, "Rademon Estate", 2015, 16, null
        },
        {
            179, "Royal Oak", 2016, 22, null
        },
        {
            180, "Shed", 2014, 23, null
        },
        {
            181, "Slane", 2018, 11, null
        },
        {
            182, "Sliabh Liag", 2016, 9, null
        },
        {
            183, "Tipperary", 2020, 24, null
        },
        {
            184, "Tullamore", 2014, 25, "https://www.whisky.com/fileadmin/_processed_/2/3/csm_Tullamore_distillery_e0fc2d6310.jpg"
        },
        {
            185, "Buffalo Trace Distillery", 1775, 26, null
        },
        {
            186, "Four Roses Distillery", 1888, 26, "https://bourbontowntours.com/wp-content/uploads/2023/09/four.jpg"
        },
        {
            187, "Heaven Hill Distilleries, Inc.", 1934, 26, null
        },
        {
            188, "Jack Daniels", 1866, 27, "https://americanwhiskeytrail.distilledspirits.org/sites/default/files/styles/flexslider_full/public/distillery/field_slides/Jack%20Daniels%20Visitor%27s%20Center_opt.jpg?itok=vpZAEgVu"
        },
        {
            189, "Jim Beam Distillery", 1795, 26, "https://www.foodandwine.com/thmb/rVUUWewQYYdDzAQMPOQTZH06nzQ=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/James-Beam-Distillery-FT-BLOG1021-28072a663ffe4cf8ac3adeb05b843143.jpg"
        },
        {
            190, "Kentucky Bourbon Distillers", 1935, 26, "https://d36tnp772eyphs.cloudfront.net/blogs/1/2018/12/Castle-and-Key-distillery-interior-in-Kentucky.jpg"
        },
        {
            191, "Makers Mark Distillery, Inc.", 1953, 26, "https://i0.wp.com/cocktailwonk.com/wp-content/uploads/2014/11/Header.jpg?fit=1200%2C800&ssl=1"
        },
        {
            192, "Michters Distillery", 1847, 26, null
        },
        {
            193, "Willett Distillery", 1936, 26, null
        },
        {
            194, "Bulleit", 2017, 26, "https://hips.hearstapps.com/amv-prod-gp.s3.amazonaws.com/gearpatrol/wp-content/uploads/2019/10/Sponsored-Post-Bulleit-Bourbon-gear-patrol-lead-feature.jpg"
        },
        {
            195, "Kavalan", 2005, 26, "https://hips.hearstapps.com/amv-prod-gp.s3.amazonaws.com/gearpatrol/wp-content/uploads/2019/10/Sponsored-Post-Bulleit-Bourbon-gear-patrol-lead-feature.jpg"
        },
        {
            196, "Amrut", 1948, 37, "https://www.whisky.com/fileadmin/_processed_/1/5/csm_IMG_0402_718cec422ff28f51b8654e744519f5ec_1fb068e7c2.jpg"
        },
        {
            197, "Hiram-Walker & Sons distillery", 1858, 38, "https://smartcdn.gprod.postmedia.digital/windsorstar/wp-content/uploads/2021/02/hiram-walker-sons-distillery-1.jpg"
        }
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
                keyValue: 2
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 3
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 4
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 5
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 6
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 7
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 8
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 9
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 10
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 11
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 12
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 13
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 14
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 15
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 16
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 17
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 18
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 19
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 20
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 21
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 22
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 23
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 24
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 25
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 26
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 27
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 28
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 29
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 30
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 31
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 32
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 33
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 34
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 35
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 36
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 37
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 38
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 39
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 40
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 41
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 42
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 43
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 44
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 45
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 46
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 47
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 48
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 49
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 50
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 51
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 52
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 53
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 54
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 55
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 56
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 57
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 58
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 59
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 60
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 61
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 62
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 63
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 64
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 65
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 66
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 67
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 68
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 69
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 70
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 71
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 72
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 73
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 74
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 75
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 76
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 77
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 78
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 79
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 80
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 81
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 82
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 83
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 84
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 85
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 86
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 87
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 88
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 89
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 90
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 91
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 92
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 93
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 94
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 95
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 96
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 97
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 98
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 99
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 100
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 101
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 102
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 103
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 104
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 105
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 106
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 107
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 108
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 109
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 110
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 111
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 112
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 113
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 114
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 115
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 116
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 117
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 118
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 119
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 120
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 121
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 122
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 123
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 124
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 125
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 126
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 127
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 128
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 129
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 130
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 131
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 132
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 133
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 134
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 135
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 136
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 137
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 138
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 139
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 140
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 141
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 142
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 143
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 144
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 145
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 146
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 147
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 148
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 149
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 150
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 151
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 152
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 153
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 154
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 155
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 156
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 157
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 158
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 159
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 160
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 161
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 162
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 163
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 164
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 165
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 166
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 167
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 168
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 169
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 170
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 171
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 172
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 173
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 174
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 175
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 176
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 177
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 178
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 179
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 180
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 181
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 182
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 183
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 184
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 185
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 186
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 187
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 188
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 189
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 190
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 191
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 192
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 193
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 194
            );

            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 195
            );
            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 196
            );
            migrationBuilder.DeleteData(
                table: "Distilleries",
                keyColumn: "Id",
                keyValue: 197
            );
        }
    }
}
