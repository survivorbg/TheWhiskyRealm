using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TheWhiskyRealm.Infrastructure.Constants.WhiskyDataConstants;

namespace TheWhiskyRealm.Infrastructure.Data.Models;

/// <summary>
/// Represents a whisky entity.
/// </summary>
[Comment("Represents a whisky entity.")]
public class Whisky
{
    /// <summary>
    /// Gets or sets the unique identifier of the whisky.
    /// </summary>
    [Key]
    [Comment("The unique identifier of the whisky.")]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the whisky.
    /// </summary>
    [Required]
    [StringLength(MaxNameLength)]
    [Comment("The name of the whisky.")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a detailed description of the whisky.
    /// </summary>
    [Required]
    [StringLength(MaxDescLength)]
    [Comment("A detailed description of the whisky.")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the age of the whisky (in years), if stated.
    /// </summary>
    [Range(MinAge, MaxAge)]
    [Comment("The age of the whisky (in years), if stated.")]
    public int? Age { get; set; }

    /// <summary>
    /// Gets or sets the alcohol by volume (ABV) of the whisky.
    /// </summary>
    [Required]
    [Range(MinABV, MaxABV)]
    [Comment("The alcohol by volume (ABV) of the whisky.")]
    public double ABV { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the distillery that produced the whisky.
    /// </summary>
    [Required]
    [Comment("The identifier of the distillery that produced the whisky.")]
    public int DistilleryId { get; set; }

    /// <summary>
    /// The distillery that produced this whisky.
    /// </summary>
    [ForeignKey(nameof(DistilleryId))]
    [Comment("Foreign key relationship to the Distillery entity.")]
    public Distillery Distillery { get; set; } = null!;

    /// <summary>
    /// Gets or sets the identifier of the region where this whisky was produced.
    /// </summary>
    [Required]
    [Comment("The identifier of the region where this whisky was produced.")]
    public int RegionId { get; set; }

    /// <summary>
    /// The region where this whisky was produced.
    /// </summary>
    [ForeignKey(nameof(RegionId))]
    [Comment("Foreign key relationship to the Region entity.")]
    public Region Region { get; set; } = null!;

    /// <summary>
    /// Gets or sets the identifier of the country where this whisky was produced.
    /// </summary>
    [Required]
    [Comment("The identifier of the country where this whisky was produced")]
    public int CountryId { get; set; }

    /// <summary>
    /// The country where this whisky was produced.
    /// </summary>
    [ForeignKey(nameof(CountryId))]
    [Comment("Foreign key relationship to the Country entity.")]
    public Country Country { get; set; } = null!;

    /// <summary>
    /// Gets or sets the identifier of the whisky category.
    /// </summary>
    [Required]
    [Comment("The identifier of the whisky category.")]
    public int WhiskyCategoryId { get; set; }

    /// <summary>
    /// The category this whisky belongs to (e.g., Single Malt, Blended).
    /// </summary>
    [ForeignKey(nameof(WhiskyCategoryId))]
    [Comment("Foreign key relationship to the WhiskyCategory entity.")]
    public WhiskyCategory WhiskyCategory { get; set; } = null!;

    /// <summary>
    /// A collection of reviews associated with this whisky.
    /// </summary>
    [Comment("A collection of reviews associated with this whisky.")]
    public ICollection<Review> Reviews { get; set; } = new List<Review>();

    /// <summary>
    /// A collection of ratings associated with this whisky.
    /// </summary>
    [Comment("A collection of ratings associated with this whisky.")]
    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}