using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static TheWhiskyRealm.Infrastructure.Constants.AwardDataConstants;
using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Infrastructure.Data.Enums;


namespace TheWhiskyRealm.Infrastructure.Data.Models;

/// <summary>
/// Represents an award entity.
/// </summary>
[Comment("Represents an award entity.")]
public class Award
{
    /// <summary>
    /// Gets or sets the unique identifier of the award.
    /// </summary>
    [Key]
    [Comment("The unique identifier of the award.")]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the award.
    /// </summary>
    [Required]
    [StringLength(AwardMaxTitleLength)]
    [Comment("The title of the award.")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the award ceremony this award was won by the whisky.
    /// </summary>
    [StringLength(AwardMaxDescLength)]
    [Comment("The award ceremony this award was won by the whisky.")]
    public string AwardsCeremony { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the type of the medal.(Gold / Silver / Bronze)
    /// </summary>
    [Comment("The type of the medal.")]
    [Required]
    public MedalType MedalType { get; set; }

    /// <summary>
    /// Gets or sets the year the award was given.
    /// </summary>
    [Required]
    [Range(AwardMinYearValue, AwardMaxYearValue)]
    [Comment("The year the award was given.")]
    public int Year { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the whisky associated with the award.
    /// </summary>
    [Required]
    [Comment("The identifier of the whisky associated with the award.")]
    public int WhiskyId { get; set; }

    /// <summary>
    /// Gets or sets the whisky associated with the award.
    /// </summary>
    [ForeignKey(nameof(WhiskyId))]
    [Comment("The whisky associated with the award.")]
    public Whisky Whisky { get; set; } = null!;
}
