using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TheWhiskyRealm.Infrastructure.Constants.RegionDataConstants;

namespace TheWhiskyRealm.Infrastructure.Data.Models;

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
    [StringLength(RegionMaxNameLength)]
    [Comment("The name of the region.")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the identifier of the country that the region belongs to.
    /// </summary>
    [Required]
    [Comment("The identifier of the country that the region belongs to.")]
    public int CountryId { get; set; }

    /// <summary>
    /// Gets or sets the country that the region belongs to.
    /// </summary>
    [ForeignKey(nameof(CountryId))]
    [Comment("The country that the region belongs to.")]
    public Country Country { get; set; } = null!;

    /// <summary>
    /// Gets or sets the distilleries located in the region.
    /// </summary>
    [Comment("The distilleries located in the region.")]
    public ICollection<Distillery> Distilleries { get; set; } = new List<Distillery>();
}
