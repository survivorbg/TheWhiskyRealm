using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TheWhiskyRealm.Infrastructure.Constants.DistilleryDataConstants;

namespace TheWhiskyRealm.Infrastructure.Data.Models;

public class Distillery
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(MaxNameLength)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Range(MinYearFounded, MaxYearFounded)]
    public int YearFounded { get; set; }

    [Required]
    public int RegionId { get; set; }

    [ForeignKey(nameof(RegionId))]
    public Region Region { get; set; } = null!;

    public ICollection<Whisky> Whiskies { get; set; } = new List<Whisky>();
}
