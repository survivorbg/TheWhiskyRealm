using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static TheWhiskyRealm.Infrastructure.Constants.CountryDataConstants;

namespace TheWhiskyRealm.Infrastructure.Data.Models;

/// <summary>
/// Represents a country entity.
/// </summary>
[Comment("Represents a country entity.")]
public class Country
{
    /// <summary>
    /// Gets or sets the unique identifier of the country.
    /// </summary>
    [Key]
    [Comment("The unique identifier of the country.")]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the country.
    /// </summary>
    [Required]
    [StringLength(CountryMaxNameLength)]
    [Comment("The name of the country.")]
    public string Name { get; set; } = string.Empty;

    // <summary>
    /// Gets or sets the regions associated with the country.
    /// </summary>
    [Comment("The regions associated with the country.")]
    public ICollection<Region> Regions { get; set; } = new List<Region>();

    /// <summary>
    /// Gets or sets the cities associated with the country.
    /// </summary>
    [Comment("The cities associated with the country.")]
    public ICollection<City> Cities { get; set; } = new List<City>();
}
