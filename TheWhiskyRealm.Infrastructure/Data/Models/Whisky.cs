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

    [Required]
    public int WhiskyTypeId { get; set; }

    [ForeignKey(nameof(WhiskyTypeId))]
    public WhiskyType WhiskyType { get; set; } = null!;

    public ICollection<Award> Awards { get; set; } = new List<Award>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}
