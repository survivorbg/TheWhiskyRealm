using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Infrastructure.Data.Configurations;

public class DistilleryConfiguration : IEntityTypeConfiguration<Distillery>
{
    public void Configure(EntityTypeBuilder<Distillery> builder)
    {
        builder.HasData(GenerateDistilleries());
    }

    private Distillery[] GenerateDistilleries()
    {
        ICollection<Distillery> distilleries = new HashSet<Distillery>();

        
        distilleries.Add(new Distillery
        {
            Id = 1,
            Name = "Auchentoshan",
            YearFounded = 1823,
            ImageUrl = "https://keyassets.timeincuk.net/inspirewp/live/wp-content/uploads/sites/34/2022/06/Auchentoshan-Distillery-920x609.jpg",
            RegionId = 1
        });

        distilleries.Add(new Distillery
        {
            Id = 2,
            Name = "Glenmorangie",
            YearFounded = 1843,
            ImageUrl = "https://www.secret-scotland.com/datafiles/uploaded/cmsRefImage/popularPlaces/additional/main/main_110_Glenmorangie.jpg",
            RegionId = 2
        });

        distilleries.Add(new Distillery
        {
            Id = 3,
            Name = "The Macallan",
            YearFounded = 2017,
            ImageUrl = "https://www.robertson.co.uk/sites/default/files/styles/news_header/public/news_images/Macallan%20press%20image%20POM2018124G0800208003.jpg?itok=wyUPa5o2",
            RegionId = 3
        });

        distilleries.Add(new Distillery
        {
            Id = 4,
            Name = "Laphroaig",
            YearFounded = 1815,
            ImageUrl = "https://www.laphroaig.com/sites/default/files/styles/style_20_9/public/2022-06/Laphroaig_Distillery_banner_DT.jpg.webp?itok=k-vrPwBD",
            RegionId = 6
        });

        distilleries.Add(new Distillery
        {
            Id = 5,
            Name = "Springbank",
            YearFounded = 1828,
            ImageUrl = "https://www.whisky.com/fileadmin/_processed_/4/5/csm__MG_5906_732d67d3b250e58ad3dcdcddda309e7a_f491af0c94.jpg",
            RegionId = 5
        });

        distilleries.Add(new Distillery
        {
            Id = 6,
            Name = "Highland Park",
            YearFounded = 1798,
            ImageUrl = "https://www.highlandparkwhisky.com/sites/g/files/jrulke331/files/styles/text_structured_image/public/Highland%20Park%20Distillery.jpg?itok=OxqQUxKL",
            RegionId = 4
        });

        distilleries.Add(new Distillery
        {
            Id = 7,
            Name = "Bushmills",
            YearFounded = 1784,
            ImageUrl = "https://www.discoveringireland.com/contentFiles/productImages/Medium/Bushmills_Distillery.jpg",
            RegionId = 7 
        });

        distilleries.Add(new Distillery
        {
            Id = 8,
            Name = "Jameson",
            YearFounded = 1780,
            ImageUrl = "https://static.wixstatic.com/media/ea720f_24a89e7c992347e68f452dbcc114dee1~mv2.jpg/v1/fill/w_598,h_390,al_c,q_80,usm_0.66_1.00_0.01,enc_auto/ea720f_24a89e7c992347e68f452dbcc114dee1~mv2.jpg",
            RegionId = 9 
        });

        distilleries.Add(new Distillery
        {
            Id = 9,
            Name = "Jack Daniel's",
            YearFounded = 1866,
            ImageUrl = "https://americanwhiskeytrail.distilledspirits.org/sites/default/files/styles/flexslider_full/public/distillery/field_slides/Jack%20Daniels%20Visitor%27s%20Center_opt.jpg?itok=vpZAEgVu",
            RegionId = 12 
        });

        distilleries.Add(new Distillery
        {
            Id = 10,
            Name = "Jim Beam",
            YearFounded = 1795,
            ImageUrl = "https://www.foodandwine.com/thmb/rVUUWewQYYdDzAQMPOQTZH06nzQ=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/James-Beam-Distillery-FT-BLOG1021-28072a663ffe4cf8ac3adeb05b843143.jpg",
            RegionId = 11 
        });

        distilleries.Add(new Distillery
        {
            Id = 12,
            Name = "Nikka",
            ImageUrl = "https://www.nikka.com/eng/img/distilleries/topmenu_miyagikyo.jpg",
            YearFounded = 1934,
            RegionId = 13 
        });

        distilleries.Add(new Distillery
        {
            Id = 13,
            Name = "Kavalan",
            ImageUrl = "https://whiskyetc.files.wordpress.com/2019/05/kavalan-distillery-03-1.jpg",
            YearFounded = 2005,
            RegionId = 14 
        });

        distilleries.Add(new Distillery
        {
            Id = 14,
            Name = "Amrut",
            ImageUrl = "https://www.whisky.com/fileadmin/_processed_/1/5/csm_IMG_0402_718cec422ff28f51b8654e744519f5ec_1fb068e7c2.jpg",
            YearFounded = 1948,
            RegionId = 15 
        });

        distilleries.Add(new Distillery
        {
            Id = 15,
            Name = "Hiram-Walker & Sons distillery",
            ImageUrl = "https://smartcdn.gprod.postmedia.digital/windsorstar/wp-content/uploads/2021/02/hiram-walker-sons-distillery-1.jpg",
            YearFounded = 1858,
            RegionId = 16 
        });

        return distilleries.ToArray();
    }
}
