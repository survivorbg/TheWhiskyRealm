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
    [StringLength(WhiskyMaxNameLength)]
    [Comment("The name of the whisky.")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the age of the whisky.
    /// </summary>
    [Range(WhiskyMinAge, WhiskyMaxAge)]
    [Comment("The age of the whisky.")]
    public int? Age { get; set; }

    /// <summary>
    /// Gets or sets the alcohol percentage of the whisky.
    /// </summary>
    [Required]
    [Range(WhiskyMinABV, WhiskyMaxABV)]
    [Comment("The alcohol percentage of the whisky.")]
    public double AlcoholPercentage { get; set; }

    /// <summary>
    /// Gets or sets the description of the whisky.
    /// </summary>
    [Required]
    [StringLength(WhiskyMaxDescLength)]
    [Comment("The description of the whisky.")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the The URL of the whisky image.
    /// </summary>
    [Required]
    [Comment("The URL of the whisky image.")]
    public string ImageURL { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the identifier of the distillery that produced the whisky.
    /// </summary>
    [Required]
    [Comment("The identifier of the distillery that produced the whisky.")]
    public int DistilleryId { get; set; }

    /// <summary>
    /// Gets or sets the distillery that produced the whisky.
    /// </summary>
    [ForeignKey(nameof(DistilleryId))]
    [Comment("The distillery that produced the whisky.")]
    public Distillery Distillery { get; set; } = null!;

    /// <summary>
    /// Gets or sets the identifier of the type of the whisky.
    /// </summary>
    [Required]
    [Comment("The identifier of the type of the whisky.")]
    public int WhiskyTypeId { get; set; }

    /// <summary>
    /// Gets or sets the type of the whisky.
    /// </summary>
    [ForeignKey(nameof(WhiskyTypeId))]
    [Comment("The type of the whisky.")]
    public WhiskyType WhiskyType { get; set; } = null!;

    /// <summary>
    /// Gets or sets the awards the whisky has won.
    /// </summary>
    [Comment("The awards the whisky has won.")]
    public ICollection<Award> Awards { get; set; } = new List<Award>();

    /// <summary>
    /// Gets or sets the reviews associated with the whisky.
    /// </summary>
    [Comment("The reviews associated with the whisky.")]
    public ICollection<Review> Reviews { get; set; } = new List<Review>();

    /// <summary>
    /// Gets or sets the ratings given for the whisky.
    /// </summary>
    [Comment("The ratings given for the whisky.")]
    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    /// <summary>
    /// Mapping table between Whiskies and Users
    /// </summary>
    public ICollection<UserWhisky> UsersWhiskies { get; set; } = new List<UserWhisky>();
}
