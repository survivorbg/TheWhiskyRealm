using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TheWhiskyRealm.Infrastructure.Constants.RatingDataConstants;

namespace TheWhiskyRealm.Infrastructure.Data.Models;

/// <summary>
/// Represents a rating entity.
/// </summary>
[Comment("Represents a rating entity.")]
public class Rating
{
    /// <summary>
    /// Gets or sets the unique identifier of the rating.
    /// </summary>
    [Key]
    [Comment("The unique identifier of the rating.")]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the rating that is given for the whisky aroma.
    /// </summary>
    [Required]
    [Range(RatingMinNoseValue, RatingMaxNoseValue)]
    [Comment("Represents the rating that is given for the whisky aroma.")]
    public int Nose { get; set; }

    /// <summary>
    /// Gets or sets the rating that is given for the whisky taste.
    /// </summary>
    [Required]
    [Range(RatingMinTasteValue, RatingMaxTasteValue)]
    [Comment("Represents the rating that is given for the whisky taste.")]
    public int Taste { get; set; }

    /// <summary>
    /// Gets or sets the rating that is given for the whisky finishing notes.
    /// </summary>
    [Required]
    [Range(RatingMinFinishValue, RatingMaxFinishValue)]
    [Comment("Represents the rating that is given for the whisky finishing notes.")]
    public int Finish { get; set; }

    /// <summary>
    /// Average points(Nose,Taste,Finish)
    /// </summary>
    [NotMapped]
    public int AveragePoints
    {
        get
        {
            return (Nose + Taste + Finish) / 3;
        }
    }

    /// <summary>
    /// Gets or sets the identifier of the whisky being rated.
    /// </summary>
    [Required]
    [Comment("The identifier of the whisky being rated.")]
    public int WhiskyId { get; set; }

    /// <summary>
    /// Gets or sets the whisky being rated.
    /// </summary>
    [ForeignKey(nameof(WhiskyId))]
    [Comment("The whisky being rated.")]
    public Whisky Whisky { get; set; } = null!;

    /// <summary>
    /// Gets or sets the identifier of the user who gave the rating.
    /// </summary>
    [Required]
    [Comment("The identifier of the user who gave the rating.")]
    public string UserId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user who gave the rating.
    /// </summary>
    [ForeignKey(nameof(UserId))]
    [Comment("The user who gave the rating.")]
    public ApplicationUser User { get; set; } = null!;
}