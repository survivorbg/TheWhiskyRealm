using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TheWhiskyRealm.Infrastructure.Constants.DistilleryDataConstants;

namespace TheWhiskyRealm.Infrastructure.Data.Models;

/// <summary>
/// Represents a distillery entity.
/// </summary>
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
    [StringLength(MaxNameLength)]
    [Comment("The name of the distillery.")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the year the distillery was founded.
    /// </summary>
    [Required]
    [Range(MinYearFounded, MaxYearFounded)]
    [Comment("The year the distillery was founded.")]
    public int YearFounded { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the country where the distillery is located.
    /// </summary>
    [Required]
    [Comment("The identifier of the country where the distillery is located.")]
    public int CountryId { get; set; }

    /// <summary>
    /// The country where the distillery is located.
    /// </summary>
    [ForeignKey(nameof(CountryId))]
    [Comment("Foreign key relationship to Country table")]
    public Country Country { get; set; } = null!;

    /// <summary>
    /// Gets or sets the identifier of the region where the distillery is located.
    /// </summary>
    [Required]
    [Comment("The identifier of the region where the distillery is located.")]
    public int RegionId { get; set; }

    /// <summary>
    /// The region where the distillery is located.
    /// </summary>
    [ForeignKey(nameof(RegionId))]
    [Comment("Foreign key relationship to Region table")]
    public Region Region { get; set; } = null!;

    /// <summary>
    /// Gets or sets the identifier of the user representing the distillery (optional).
    /// </summary>
    [Comment("Optional foreign key relationship to the IdentityUser")]
    public string? RepresentativeId { get; set; }

    /// <summary>
    /// The user representing the distillery (optional).
    /// </summary>
    [ForeignKey(nameof(RepresentativeId))]
    [Comment("Navigation property for the user representing the distillery.")]
    public IdentityUser? RepresentativeUser { get; set; }

    /// <summary>
    /// A collection of whiskies produced by this distillery.
    /// </summary>
    [Comment("One-to-Many relationship with the Whisky entity")]
    public ICollection<Whisky> Whiskies { get; set; } = new List<Whisky>();

}