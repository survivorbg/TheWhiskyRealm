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
    [StringLength(MaxNameLength)]
    [Comment("The name of the country.")]
    public string Name { get; set; } = string.Empty;

    public ICollection<Region> Regions { get; set; } = new List<Region>();
    public ICollection<City> Cities { get; set; } = new List<City>();
}
