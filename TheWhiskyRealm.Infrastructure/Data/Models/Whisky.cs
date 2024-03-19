using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TheWhiskyRealm.Infrastructure.Constants.WhiskyDataConstants;

namespace TheWhiskyRealm.Infrastructure.Data.Models;

public class Whisky
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(MaxNameLength)]
    public string Name { get; set; } = string.Empty;

    [Range(MinAge,MaxAge)]
    public int? Age { get; set; }

    [Required]
    [Range(MinABV,MaxABV)]
    public double AlcoholPercentage { get; set; }

    [Required]
    [StringLength(MaxDescLength)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public int DistilleryId { get; set; }

    [ForeignKey(nameof(DistilleryId))]
    public Distillery Distillery { get; set; } = null!;
}
