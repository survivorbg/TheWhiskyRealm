using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TheWhiskyRealm.Infrastructure.Constants.RatingDataConstants;

namespace TheWhiskyRealm.Infrastructure.Data.Models;

public class Rating
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Range(MinNoseValue,MaxNoseValue)]
    public int Nose { get; set; }

    [Required]
    [Range(MinTasteValue,MaxTasteValue)]
    public int Taste { get; set; }

    [Required]
    [Range(MinFinishValue,MaxFinishValue)]
    public int Finish { get; set; }

    [Required]
    public int WhiskyId { get; set; }

    [ForeignKey(nameof(WhiskyId))]
    public Whisky Whisky { get; set; } = null!;

    [Required]
    public string UserId { get; set; } = null!;

    [ForeignKey(nameof(UserId))]
    public IdentityUser User { get; set; } = null!;
}