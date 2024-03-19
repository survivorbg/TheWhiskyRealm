using System.ComponentModel.DataAnnotations;
using static TheWhiskyRealm.Infrastructure.Constants.WhiskyTypeDataConstants;

namespace TheWhiskyRealm.Infrastructure.Data.Models;

public class WhiskyType
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(MaxNameLength)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(MaxDescLength)]
    public string Description { get; set; } = string.Empty;

    public ICollection<Whisky> Whiskies { get; set; } = new List<Whisky>(); 
}
