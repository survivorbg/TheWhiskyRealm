using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static TheWhiskyRealm.Infrastructure.Constants.AwardDataConstants;


namespace TheWhiskyRealm.Infrastructure.Data.Models;

public class Award
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(MaxTitleLength)]
    public string Title { get; set; } = string.Empty;

    public string? ImageUrl { get; set; }

    [Required]
    [Range(MinYearValue,MaxYearValue)]
    public int Year { get; set; }

    [Required]
    public int WhiskyId { get; set; }

    [ForeignKey(nameof(WhiskyId))]
    public Whisky Whisky { get; set; } = null!;
}
