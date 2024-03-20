using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Infrastructure.Data.Configurations;

public class WhiskyTypeConfiguration : IEntityTypeConfiguration<WhiskyType>
{
    public void Configure(EntityTypeBuilder<WhiskyType> builder)
    {
        builder.HasData(GenerateWhiskyTypes());
    }

    private WhiskyType[] GenerateWhiskyTypes()
    {
        ICollection<WhiskyType> whiskyTypes = new HashSet<WhiskyType>();

        whiskyTypes.Add(new WhiskyType
        {
            Id = 1,
            Name = "Single Malt",
            Description = "Single malt whisky is produced by a single distillery using a single malted grain (typically barley)."
        });

        whiskyTypes.Add(new WhiskyType
        {
            Id = 2,
            Name = "Blended",
            Description = "Whisky that is created by blending together multiple whiskies from different sources. These sources can include malt whisky and grain whisky from various distilleries and regions."
        });

        whiskyTypes.Add(new WhiskyType
        {
            Id = 3,
            Name = "Bourbon",
            Description = "An American whiskey made primarily from corn and aged in new charred oak barrels. Opposed to Scotch, Irish and Japanese whiskies, bourbon must be matured only minimum of 2 years."
        });

        whiskyTypes.Add(new WhiskyType
        {
            Id = 4,
            Name = "Rye",
            Description = "An American whiskey made primarily from rye grain(at least 51%), offering a spicier flavor profile. Must also be matured in new, charred oak casks."
        });

        whiskyTypes.Add(new WhiskyType
        {
            Id = 5,
            Name = "Irish",
            Description = "A whiskey made in Ireland and Northern Ireland, known for its smoothness and triple distillation process. Minimum alcohol strength must be 40% ABV with a minimum three-year maturation period."
        });

        whiskyTypes.Add(new WhiskyType
        {
            Id = 6,
            Name = "Japanese",
            Description = "Japanese whisky is a meticulously crafted spirit made using malted grains and other cereals, with water sourced exclusively from Japan. Production, including mashing, fermentation, and distillation, must occur in Japanese distilleries. Maturation in wooden casks stored in Japan for a minimum of three years enhances the whisky's flavor complexity. Bottling is strictly done in Japan, ensuring a minimum strength of 40% alcohol by volume (abv)."
        });

        whiskyTypes.Add(new WhiskyType
        {
            Id = 7,
            Name = "Canadian",
            Description = "Canadian whisky is a type of whisky that is distilled and aged in Canada. Often referred to as \"rye whisky\" or simply \"rye\" in Canada, but it differs from the American Rye Whiskey in that it isn’t really defined. Technically speaking, it didn’t even need to have any rye in it at all, although that is not a common scenario. "
        });

        return whiskyTypes.ToArray();
    }
}
