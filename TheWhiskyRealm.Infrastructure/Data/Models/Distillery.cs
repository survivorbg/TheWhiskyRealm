using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TheWhiskyRealm.Infrastructure.Constants.DistilleryDataConstants;

namespace TheWhiskyRealm.Infrastructure.Data.Models;

/// <summary>
/// Represents a distillery entity.
/// </summary>
[Comment("Represents a distillery entity.")]
public class Distillery
{
    /// <summary>
    /// Gets or sets the unique identifier of the distillery.
    /// </summary>
    [Key]
    [Comment("The unique identifier of the distillery.")]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the distillery.
    /// </summary>
    [Required]
    [StringLength(DistilleryMaxNameLength)]
    [Comment("The name of the distillery.")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the year the distillery was founded.
    /// </summary>
    [Required]
    [Range(DistilleryMinYearFounded, DistilleryMaxYearFounded)]
    [Comment("The year the distillery was founded.")]
    public int YearFounded { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the region where the distillery is located.
    /// </summary>
    [Required]
    [Comment("The identifier of the region where the distillery is located.")]
    public int RegionId { get; set; }

    /// <summary>
    /// Gets or sets the URL address of the image of the distillery.
    /// </summary>
    [Comment("The URL address of the image of the distillery.")]
    public string? ImageUrl { get; set; }

    /// <summary>
    /// Gets or sets the region where the distillery is located.
    /// </summary>
    [ForeignKey(nameof(RegionId))]
    [Comment("The region where the distillery is located.")]
    public Region Region { get; set; } = null!;

    /// <summary>
    /// Gets or sets the whiskies produced by the distillery.
    /// </summary>
    [Comment("The whiskies produced by the distillery.")]
    public ICollection<Whisky> Whiskies { get; set; } = new List<Whisky>();
}
