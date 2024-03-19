using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TheWhiskyRealm.Infrastructure.Constants.RegionDataConstants;

namespace TheWhiskyRealm.Infrastructure.Data.Models;

public class Region
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(MaxNameLength)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int CountryId { get; set; }

    [ForeignKey(nameof(CountryId))]
    public Country Country { get; set; } = null!;
}
