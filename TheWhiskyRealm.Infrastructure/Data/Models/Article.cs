using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TheWhiskyRealm.Infrastructure.Data.Enums;
using static TheWhiskyRealm.Infrastructure.Constants.ArticleDataConstants;

namespace TheWhiskyRealm.Infrastructure.Data.Models;

public class Article
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
    public string ImageUrl { get; set; } = string.Empty;

    [Required]
    public DateTime DateCreated { get; set; }

    [Required]
    public string PublisherUserId { get; set; } = null!;

    [ForeignKey(nameof(PublisherUserId))]
    public IdentityUser PublisherUser { get; set; } = null!;

    [Required]
    public ArticleType Type { get; set; }
}
