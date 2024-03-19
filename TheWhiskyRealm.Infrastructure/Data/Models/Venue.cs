using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TheWhiskyRealm.Infrastructure.Constants.VenueDataConstants;

namespace TheWhiskyRealm.Infrastructure.Data.Models;

/// <summary>
/// Represents a venue entity.
/// </summary>
[Comment("Represents a venue entity.")]
public class Venue
{
    /// <summary>
    /// Gets or sets the unique identifier of the venue.
    /// </summary>
    [Key]
    [Comment("The unique identifier of the venue.")]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the venue.
    /// </summary>
    [Required]
    [StringLength(MaxNameLength)]
    [Comment("The name of the venue.")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the capacity of the venue.
    /// </summary>
    [Required]
    [Range(MinCapacity, MaxCapacity)]
    [Comment("The capacity of the venue.")]
    public int Capacity { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the city where the venue is located.
    /// </summary>
    [Required]
    [Comment("The identifier of the city where the venue is located.")]
    public int CityId { get; set; }

    /// <summary>
    /// Gets or sets the city where the venue is located.
    /// </summary>
    [ForeignKey(nameof(CityId))]
    [Comment("The city where the venue is located.")]
    public City City { get; set; } = null!;
}
