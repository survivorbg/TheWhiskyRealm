using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TheWhiskyRealm.Infrastructure.Constants.ReviewDataConstants;

namespace TheWhiskyRealm.Infrastructure.Data.Models;

public class Review
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(MaxTitleLength)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(MaxContentLength)]
    public string Content { get; set; } = string.Empty;

    [Required]
    public bool Recommend { get; set; }

    [Required]
    public int WhiskyId { get; set; }

    [ForeignKey(nameof(WhiskyId))]
    public Whisky Whisky { get; set; } = null!;

    [Required]
    public string UserId { get; set; } = null!;

    [ForeignKey(nameof(UserId))]
    public IdentityUser User { get; set; } = null!;
}
