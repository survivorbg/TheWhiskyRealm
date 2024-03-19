using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TheWhiskyRealm.Infrastructure.Constants.VenueDataConstants;

namespace TheWhiskyRealm.Infrastructure.Data.Models;

public class Venue
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(MaxNameLength)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Range(MinCapacity,MaxCapacity)]
    public int Capacity { get; set; }

    [Required]
    public int CityId { get; set; }

    [ForeignKey(nameof(CityId))]
    public City City { get; set; } = null!;
}
