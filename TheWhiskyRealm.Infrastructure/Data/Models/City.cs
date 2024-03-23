using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TheWhiskyRealm.Infrastructure.Constants.CityDataConstants;

namespace TheWhiskyRealm.Infrastructure.Data.Models;

/// <summary>
/// Represents a city entity.
/// </summary>
[Comment("Represents a city entity.")]
public class City
{
    /// <summary>
    /// Gets or sets the unique identifier of the city.
    /// </summary>
    [Key]
    [Comment("The unique identifier of the city.")]
    public int Id { get; set; }


    /// <summary>
    /// Gets or sets the name of the city.
    /// </summary>
    [Required]
    [StringLength(CityMaxNameLength)]
    [Comment("The name of the city.")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the zip code of the city.
    /// </summary>
    [Required]
    [StringLength(CityMaxZipCodeLength)]
    [Comment("The zip code of the city.")]
    public string ZipCode { get; set; } = string.Empty;


    /// <summary>
    /// Gets or sets the identifier of the country that the city belongs to.
    /// </summary>
    [Required]
    [Comment("The identifier of the country that the city belongs to.")]
    public int CountryId { get; set; }


    /// <summary>
    /// Gets or sets the country that the city belongs to.
    /// </summary>
    [ForeignKey(nameof(CountryId))]
    [Comment("The country that the city belongs to.")]
    public Country Country { get; set; } = null!;

    /// <summary>
    /// Gets or sets the venues located in the city.
    /// </summary>
    [Comment("The venues located in the city.")]
    public ICollection<Venue> Venues { get; set; } = new List<Venue>();
}
