using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheWhiskyRealm.Infrastructure.Data.Models;
using static TheWhiskyRealm.Infrastructure.Constants.RegionDataConstants;

/// <summary>
/// Represents a region entity.
/// </summary>
[Comment("Represents a region entity.")]
public class Region
{
    /// <summary>
    /// Gets or sets the unique identifier of the region.
    /// </summary>
    [Key]
    [Comment("The unique identifier of the region.")]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the region.
    /// </summary>
    [Required]
    [StringLength(MaxNameLength)]
    [Comment("The name of the region.")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the identifier of the country to which the region belongs.
    /// </summary>
    [Required]
    [Comment("The identifier of the country to which the region belongs.")]
    public int CountryId { get; set; }

    /// <summary>
    /// The country to which this region belongs.
    /// </summary>
    [Comment("Foreign key relationship to Country table")]
    [ForeignKey(nameof(CountryId))]
    public Country Country { get; set; } = null!;

    /// <summary>
    /// Distilleries located within this region. 
    /// </summary>
    public ICollection<Distillery> Distilleries { get; set; } = new List<Distillery>();
}
