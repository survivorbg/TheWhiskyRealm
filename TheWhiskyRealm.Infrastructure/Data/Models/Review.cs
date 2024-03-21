using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TheWhiskyRealm.Infrastructure.Constants.ReviewDataConstants;

namespace TheWhiskyRealm.Infrastructure.Data.Models;

/// <summary>
/// Represents a review entity.
/// </summary>
[Comment("Represents a review entity.")]
public class Review
{
    /// <summary>
    /// Gets or sets the unique identifier of the review.
    /// </summary>
    [Key]
    [Comment("The unique identifier of the review.")]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the review.
    /// </summary>
    [Required]
    [StringLength(MaxTitleLength)]
    [Comment("The title of the review.")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the content of the review.
    /// </summary>
    [Required]
    [StringLength(MaxContentLength)]
    [Comment("The content of the review.")]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets if the user recommends status the whisky.
    /// </summary>
    [Required]
    [Comment("If the user recommends status the whisky")]
    public bool Recommend { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the whisky being reviewed.
    /// </summary>
    [Required]
    [Comment("The identifier of the whisky being reviewed.")]
    public int WhiskyId { get; set; }

    /// <summary>
    /// Gets or sets the whisky being reviewed.
    /// </summary>
    [ForeignKey(nameof(WhiskyId))]
    [Comment("The whisky being reviewed.")]
    public Whisky Whisky { get; set; } = null!;

    /// <summary>
    /// Gets or sets the identifier of the user who made the review.
    /// </summary>
    [Required]
    [Comment("The identifier of the user who made the review.")]
    public string UserId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user who made the review.
    /// </summary>
    [ForeignKey(nameof(UserId))]
    [Comment("The user who made the review.")]
    public ApplicationUser User { get; set; } = null!;
}
