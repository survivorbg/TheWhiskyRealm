using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheWhiskyRealm.Infrastructure.Data.Models;
using static TheWhiskyRealm.Infrastructure.Constants.WhiskyDataConstants;

namespace TheWhiskyRealm.Infrastructure.Data.Configurations;

public class WhiskyConfiguration : IEntityTypeConfiguration<Whisky>
{
    public void Configure(EntityTypeBuilder<Whisky> builder)
    {
        builder
            .HasCheckConstraint("CK_WhiskyAge_Min", $"[Age] >= {MinAge}")
            .HasCheckConstraint("CK_WhiskyAge_Max", $"[Age] <= {MaxAge}")
            .HasCheckConstraint("CK_ABV_Min", $"[AlcoholPercentage] >= {MinABV}")
            .HasCheckConstraint("CK_ABV_Max", $"[AlcoholPercentage] <= {MaxABV}");

        builder.HasData(GenerateWhiskies());
    }

    private Whisky[] GenerateWhiskies()
    {
        ICollection<Whisky> whiskies = new HashSet<Whisky>();

        // Уискита
        whiskies.Add(new Whisky
        {
            Id = 1,
            Name = "Auchentoshan 12 Year Old",
            Age = 12,
            AlcoholPercentage = 40.0,
            Description = "Triple distilled single malt whiskey. It has a tempting aroma of roasted almonds, caramel and the characteristic soft and delicate taste of Auchentoshan. It is aged in oak barrels, in which the Spanish \"Oloroso\" sherry and bourbon were previously aged.\r\nThe Auchentoshan Distillery is one of three in the Lowland area that continues to operate to this day. It is located north of Glasgow and was founded in 1800. The name of the excellent brand comes from Celtic and means \"The corner of the field\".",
            DistilleryId = 1, // Auchentoshan
            WhiskyTypeId = 1 // Single Malt
        });

        whiskies.Add(new Whisky
        {
            Id = 2,
            Name = "Glenmorangie 10 Year Old",
            Age = 10,
            AlcoholPercentage = 40.0,
            Description = "Glenmorangie whiskeys are produced in the Highlands, the northernmost part of Scotland. They are distilled in the highest copper cauldrons in Scotland and aged in the highest quality oak barrels, which are used only twice to produce the purest and most elegant whiskeys. The distillery is known as an innovator in the production of whiskey, combining tradition with innovation, resulting in malt whiskey with \"unnecessarily\" high quality. A symbol of Glenmorangie whiskey, the ten-year-old Glenmorangie The Original is the most delicate single malt whiskey in the world, with the most complex taste and seductive aroma. It has an exceptional fruity elegance, exquisite finesse and a seductive complex taste. The delicate floral notes in the whiskey are combined with the softness and sweetness acquired from the premium bourbon oak barrels.",
            DistilleryId = 2, // Glenmorangie
            WhiskyTypeId = 1 // Single Malt
        });

        whiskies.Add(new Whisky
        {
            Id = 3,
            Name = "The Macallan 18 Year Old Sherry Oak",
            Age = 18,
            AlcoholPercentage = 43.0,
            Description = "The Sherry Oak Collection is a timeless sensorial journey. Beautifully led by European Oak, sherry seasoned in Jerez de la Frontera, Spain. Photographer Erik Madigan Heck has captured his visual interpretation of The Macallan Sherry Oak 18 Years Old through the lens of his unique, signature style.",
            DistilleryId = 3, // The Macallan
            WhiskyTypeId = 1 // Single Malt
        });

        whiskies.Add(new Whisky
        {
            Id = 4,
            Name = "Laphroaig 10 Year Old",
            Age = 10,
            AlcoholPercentage = 40.0,
            Description = "The famous 10-year-old Laphroaig whisky has an extremely smoky flavour with a hint of seaweed and the sea. It is one of the most intense Islay malts. You either hate it or you love it.",
            DistilleryId = 4, // Laphroaig
            WhiskyTypeId = 1 // Single Malt
        });

        whiskies.Add(new Whisky
        {
            Id = 5,
            Name = "Springbank 15 Year Old",
            Age = 15,
            AlcoholPercentage = 46.0,
            Description = "A 15-year-old single malt from the Springbank distillery with plenty of sherry notes and spice, dried fruits, and nuts.",
            DistilleryId = 5, // Springbank
            WhiskyTypeId = 1 // Single Malt
        });

        whiskies.Add(new Whisky
        {
            Id = 6,
            Name = "Highland Park 18 Year Old Viking Pride",
            Age = 18,
            AlcoholPercentage = 43.0,
            Description = "Highland Park's 18 Year Old enjoyed a redesign in 2017, receiving livery inspired by the wood carvings from Urnes Stave Church and a new sub-name, \"Viking Pride\". The Orkney single malt remains the same as before - rich, complex and supremely delicious.",
            DistilleryId = 6, // Highland Park
            WhiskyTypeId = 1 // Single Malt
        });

        whiskies.Add(new Whisky
        {
            Id = 7,
            Name = "Bushmills Original",
            Age = null,
            AlcoholPercentage = 40.0,
            Description = "Bushmill's original whisky is a smooth, easy-drinking whiskey that has been produced in Ireland for centuries.\r\nBushmills Original is made up of grain whiskey matured for five years before blending with malt whiskeys. Bushmill's Irish whiskey is triple distilled and very supple.\r\nBushmills Original is a consistent, great value-for-money, triple-distilled blend made with a core of flavoursome malt and grain whiskies",
            DistilleryId = 7, // Bushmills
            WhiskyTypeId = 5 // Irish
        });

        whiskies.Add(new Whisky
        {
            Id = 8,
            Name = "Jameson",
            Age = null,
            AlcoholPercentage = 40.0,
            Description = "Produced at the Midleton Distillery, Jameson is Ireland's quintessential Irish blend – a classic whiskey. Jim Murray even awarded it an incredible 95 points!",
            DistilleryId = 8, // Jameson
            WhiskyTypeId = 5 // Irish
        });

        whiskies.Add(new Whisky
        {
            Id = 9,
            Name = "Jack Daniel's Old No. 7",
            Age = null,
            AlcoholPercentage = 40.0,
            Description = "Jack Daniel's Tennessee Whiskey has been made at its Lynchburg distillery since 1875. The branding and original label, sometimes referred to as No. 7 or Black Label; has made its way into pop culture, with merchandise sold the world over and a history of association with music. Frank Sinatra was even buried with a bottle. The Tennessee whiskey makers use a mash bill made up of 80% corn, 12% rye, and 8% malt to create Jack Daniels whiskey, which is then filtered through 10 feet of sugar maple charcoal to produce a mellow, slightly smoky character. A method known as the Lincoln County Process, it means this is not a bourbon, but instead meets the legal definition of a Tennessee whiskey. Jasper Newton \"Jack\" Daniel ( c. January 1849 – October 9, 1911) was an American distiller and businessman, best known as the founder of the Jack Daniel's Tennessee whiskey distillery.",
            DistilleryId = 9, // Jack Daniel's
            WhiskyTypeId = 3 // Bourbon
        });

        whiskies.Add(new Whisky
        {
            Id = 10,
            Name = "Jim Beam White Label",
            Age = null,
            AlcoholPercentage = 40.0,
            Description = "Jim Beam bourbon undergoes distillation at lower temperatures and is distilled to no more than 62.5%, the White label is aged for four years and has quite a high percentage of rye in the mashbill.",
            DistilleryId = 10, // Jim Beam
            WhiskyTypeId = 3 // Bourbon
        });

        whiskies.Add(new Whisky
        {
            Id = 11,
            Name = "Nikka Coffey Grain",
            Age = null,
            AlcoholPercentage = 45.0,
            Description = "Using the two Coffey stills at the Miyagikyo distillery, which were imported from Scotland to Japan in 1963, Nikka have produced a number of single cask single grain whiskies from time to time over the years. This, however, is their larger release of wonderfully exotic grain whisky. Now in a 70cl bottle! Hooray!",
            DistilleryId = 12, // Nikka
            WhiskyTypeId = 6 // Japanese
        });

        whiskies.Add(new Whisky
        {
            Id = 12,
            Name = "Kavalan Solist Sherry",
            Age = null,
            AlcoholPercentage = 59.4,
            Description = "A single cask, cask strength single malt from Taiwan's Kavalan, released for the Solist series. Matured in a single sherry cask (#S081217042 to be exact), this one was bottled without colouring at a generous 59.4% ABV. If you like sherry bombs and award-winning whisky, look no further!",
            DistilleryId = 13, // Kavalan
            WhiskyTypeId = 1 // Single Malt
        });

        whiskies.Add(new Whisky
        {
            Id = 13,
            Name = "Amrut Fusion",
            Age = null,
            AlcoholPercentage = 50.0,
            Description = "Fusion is a particularly apt name for this fantastic single malt whisky from Amrut. Y'see, it's made with barley grown in India, where the Amrut Distillery can be found, as well as peated barley from Scotland! Makes sense, right? Not just a clever name, it's also a cracking whisky, offering up generous helpings of fresh fruit, honey, spice and a good whiff of smoke.",
            DistilleryId = 14, // Amrut
            WhiskyTypeId = 1 // Single Malt
        });

        whiskies.Add(new Whisky
        {
            Id = 14,
            Name = "JP Wiser's Triple Barrel Rye",
            Age = null,
            AlcoholPercentage = 43.4,
            Description = "A smooth amber rye whiskey with complex notes of caramel, toffee and spice with subtle undertones of green apple and pear. Enjoy the roasted rye spices that complement the soft toffee and vanilla flavors, neatly sipped on its own for a lingering green apple finish.",
            DistilleryId = 15, // JP Wiser's
            WhiskyTypeId = 4 // Rye
        });

        whiskies.Add(new Whisky
        {
            Id = 15,
            Name = "Canadian Club 100% Rye",
            Age = null,
            AlcoholPercentage = 40.0,
            Description = "A pure expression of rye whisky; more complex and characterful, this pours medium gold. Aromas of sweet fruit, herb and spice, with vanilla, toffee, pepper, cedar smoke and banana on the nose. The sweet, creamy and warm palate is balanced by rye spice flavours followed by a long finish showing dried fruit, honey and ginger.",
            DistilleryId = 15, // JP Wiser's (Hiram-Walker & Sons distillery)
            WhiskyTypeId = 4 // Rye
        });

        whiskies.Add(new Whisky
        {
            Id = 16,
            Name = "Auchentoshan Three Wood",
            Age = null,
            AlcoholPercentage = 43.0,
            Description = "A Lowland single malt matured in 3 different casks, namely: Pedro Ximénez Sherry casks, bourbon casks and Oloroso Sherry casks. A distinctive triple distilled whisky from Auchentoshan.",
            DistilleryId = 1, // Auchentoshan
            WhiskyTypeId = 1 // Single Malt
        });

        whiskies.Add(new Whisky
        {
            Id = 17,
            Name = "Glenmorangie Lasanta 12 Year Old",
            Age = 12,
            AlcoholPercentage = 43.0,
            Description = "A rich and fruity single malt Scotch whisky finished in sherry casks.",
            DistilleryId = 2, // Glenmorangie
            WhiskyTypeId = 1 // Single Malt
        });

        whiskies.Add(new Whisky
        {
            Id = 18,
            Name = "The Macallan Double Cask 12 Year Old",
            Age = 12,
            AlcoholPercentage = 40.0,
            Description = "An exciting new age statement single malt Scotch whisky from Macallan that's matured in a combination of American and European Sherry oak for a minimum of 12 years.",
            DistilleryId = 3, // The Macallan
            WhiskyTypeId = 1 // Single Malt
        });

        whiskies.Add(new Whisky
        {
            Id = 19,
            Name = "Laphroaig Quarter Cask",
            Age = null,
            AlcoholPercentage = 48.0,
            Description = "Released in 2004, this bottling was aged for around five years before being finished in a quarter cask for several months, the size of the cask is quite small, thus does not require such a long maturation. This remains a truly great achievement from Laphroaig.",
            DistilleryId = 4, // Laphroaig
            WhiskyTypeId = 1 // Single Malt
        });

        whiskies.Add(new Whisky
        {
            Id = 20,
            Name = "Springbank 18 Year Old 2021 Release",
            Age = 18,
            AlcoholPercentage = 46.0,
            Description = "The 2021 release of the 18-year-old from Springbank. Matured in a combination of 50% ex-bourbon and 50% ex-sherry casks then bottled at 46% abv.",
            DistilleryId = 5, // Springbank
            WhiskyTypeId = 1 // Single Malt
        });

        whiskies.Add(new Whisky
        {
            Id = 21,
            Name = "Highland Park 12 Year Old Viking Honour",
            Age = 12,
            AlcoholPercentage = 40.0,
            Description = "With a sub-name like Viking Honour, you can expect a lot from this Orcadian single malt Scotch whisky – and it delivers! A great introduction to Highland Park's famed heathery peat smoke, the 12 Year Old is matured predominantly in sherry-seasoned European and American oak casks, so it's spicy, citrusy, and full of smoky aromatics.",
            DistilleryId = 6, // Highland Park
            WhiskyTypeId = 1 // Single Malt
        });

        whiskies.Add(new Whisky
        {
            Id = 22,
            Name = "Bushmills Black Bush",
            Age = null,
            AlcoholPercentage = 40.0,
            Description = "If you've been in a bar (any bar, really), you've more than likely seen a bottle of this on the shelf. One of the most well-known Irish blends, Bushmills Black Bush features a lot of sherried malt in its recipe, alongside classically caramel-y grain whiskey. Suitable for enjoying neat, but it can also be used in whiskey cocktails that call for dark fruit sweetness...",
            DistilleryId = 7, // Bushmills
            WhiskyTypeId = 5 // Irish
        });

        whiskies.Add(new Whisky
        {
            Id = 23,
            Name = "Jameson Caskmates Stout Edition",
            Age = null,
            AlcoholPercentage = 40.0,
            Description = "Jameson Caskmates is an intriguing release. Having sent some of their casks to the local craft stout brewers at Franciscan Well, the casks were returned to Midleton where they were subsequently used to give a stout finish to Jameson!",
            DistilleryId = 8, // Jameson
            WhiskyTypeId = 5 // Irish
        });

        whiskies.Add(new Whisky
        {
            Id = 24,
            Name = "Jack Daniel's Single Barrel",
            Age = null,
            AlcoholPercentage = 45.0,
            Description = "You all know Jack Daniel’s and have no doubt seen its classic whiskey in bars and shops all over the world. But the Tennessee-based distillery is also home to lots of premium expressions, like the single barrel bottling you see before you. A premium version of Jack Daniel's, each of these comes from a cask specially selected by the master distiller. These are chosen for their suitability as standalone spirits, resulting in a whiskey full of richness and complexity.",
            DistilleryId = 9, // Jack Daniel's
            WhiskyTypeId = 3 // Bourbon
        });

        whiskies.Add(new Whisky
        {
            Id = 25,
            Name = "Jim Beam Double",
            Age = 8,
            AlcoholPercentage = 43.0,
            Description = "There's an old axiom that claims \"Two heads are better than one\". Can that theory be transferred to whiskey barrels? Jim Beam's Double Oak might answer that question. You see, they initially mature this bourbon in freshly charred American oak barrels, and then move the whiskey over to a fresh set of freshly charred American oak barrels for the second part of its maturation!\r\n\r\nA worthy replacement for the now discontinued Jim Beam Black.",
            DistilleryId = 10, // Jim Beam
            WhiskyTypeId = 3 // Bourbon
        });

        whiskies.Add(new Whisky
        {
            Id = 26,
            Name = "Nikka From the Barrel",
            Age = null,
            AlcoholPercentage = 51.4,
            Description = "The award-winning Nikka Whisky From The Barrel blend is absolutely full of flavour. Bottled at 51.4% ABV. The blend combines both single malt and grain whisky from the Miyagikyo and Yoichi distilleries, which are then married in a huge variety of casks, including bourbon barrels, sherry butts and refill hogsheads. A huge depth of flavour in this stunning Japanese whisky. We can't recommend this enough.",
            DistilleryId = 12, // Nikka
            WhiskyTypeId = 6 // Japanese
        });

        whiskies.Add(new Whisky
        {
            Id = 27,
            Name = "Kavalan Concertmaster Port Cask Finish",
            Age = null,
            AlcoholPercentage = 40.0,
            Description = "Taiwanese whisky has been a 'thing' for a while now, since 2008 in fact, but the highly-regarded Kavalan whiskies are now finally available in Europe! This single malt whisky utilises Ruby Port, Tawny Port and Vintage Port casks from Portugal to finish whiskies that were initially matured in American oak. Kavalan Concertmaster was named Best in Class at the 2011 International Wine & Spirit Competition.",
            DistilleryId = 13, // Kavalan
            WhiskyTypeId = 1 // Single Malt
        });

        whiskies.Add(new Whisky
        {
            Id = 28,
            Name = "Amrut Peated Single Malt",
            Age = null,
            AlcoholPercentage = 46.0,
            Description = "From one of India's most famous whisky producers, this smoky single malt is made from barley peated to 24ppm. It’s punchy but still fruity and malty, with the ABV increased after its initial release to 46% to add more weight and texture.",
            DistilleryId = 14, // Amrut
            WhiskyTypeId = 1 // Single Malt
        });

        whiskies.Add(new Whisky
        {
            Id = 29,
            Name = "JP Wiser's 18 Year Old",
            Age = 18,
            AlcoholPercentage = 40.0,
            Description = "A single grain whisky that is dominated by aromas of green apple in part due to the unique aging conditions in Southern Ontario. It pours a medium golden amber with additional aromas of caramel, orange peel and spice; the palate is round and medium-bodied with a silky texture and a smooth vanilla driven, finish.",
            DistilleryId = 15, // JP Wiser's
            WhiskyTypeId = 4 // Rye
        });

        whiskies.Add(new Whisky
        {
            Id = 30,
            Name = "Canadian Club Classic 12 Year Old",
            Age = 12,
            AlcoholPercentage = 40.0,
            Description = "The Canadian Club Classic matured for 12 years, which is a long age for a Canadian whisky. With age comes perfection. Selected whiskies mature together to create a mild and naturally smooth Blend.",
            DistilleryId = 15, // Hiram-Walker & Sons distillery (Canadian Club)
            WhiskyTypeId = 2 // Blended
        });

        whiskies.Add(new Whisky
        {
            Id = 31,
            Name = "The Macallan 10 Year Old Fine Oak",
            Age = 18,
            AlcoholPercentage = 40.0,
            Description = "This 10-year-old single malt from The Macallan Fine Oak Range was matured in a mix of bourbon and sherry casks.",
            DistilleryId = 3, // The Macallan
            WhiskyTypeId = 1 // Single Malt
        });

        whiskies.Add(new Whisky
        {
            Id = 32,
            Name = "Springbank 21 Year Old Oloroso Cask Finish (Darkness)",
            Age = 21,
            AlcoholPercentage = 46.5,
            Description = "Those clever clogs over at Darkness got their hands on a sought-after drop of whisky here. This 21-year-old Springbank single malt was given the Darkness treatment with a finish in one of its custom-made octave casks, that previously held oloroso sherry. The coastal malt and sherried sweetness make a perfect partnership.",
            DistilleryId = 5, // Springbank
            WhiskyTypeId = 1 // Single Malt
        });

        whiskies.Add(new Whisky
        {
            Id = 33,
            Name = "Highland Park Bulgaria 681",
            Age = 25,
            AlcoholPercentage = 58.4,
            Description = "Highland Park Bulgaria 681 is one of the three whiskies in limited edition series specially dedicated to Bulgaria, as it celebrates legendary moments from our history.\r\nThe concept includes a series of 3 collector's bottles, which will commemorate 3 key victories of the First Bulgarian Empire. Highland Park Bulgaria 681 marks the year when the Byzantine Empire recognized the existence of the Bulgarian state by signing a peace treaty after the victory of the Proto-Bulgarians, led by Khan Asparuh. ",
            DistilleryId = 6, // Highland Park
            WhiskyTypeId = 1 // Single Malt
        });



        return whiskies.ToArray();
    }
}
