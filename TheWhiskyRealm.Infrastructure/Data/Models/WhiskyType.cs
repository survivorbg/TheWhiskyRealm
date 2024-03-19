using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static TheWhiskyRealm.Infrastructure.Constants.WhiskyTypeDataConstants;

namespace TheWhiskyRealm.Infrastructure.Data.Models;

/// <summary>
/// Represents a whisky type entity.
/// </summary>
[Comment("Represents a whisky type entity.")]
public class WhiskyType
{
    /// <summary>
    /// Gets or sets the unique identifier of the whisky type.
    /// </summary>
    [Key]
    [Comment("The unique identifier of the whisky type.")]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the whisky type.
    /// </summary>
    [Required]
    [StringLength(MaxNameLength)]
    [Comment("The name of the whisky type.")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the whisky type.
    /// </summary>
    [Required]
    [StringLength(MaxDescLength)]
    [Comment("The description of the whisky type.")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the whiskies of this type.
    /// </summary>
    [Comment("The whiskies of this type.")]
    public ICollection<Whisky> Whiskies { get; set; } = new List<Whisky>(); 
}
