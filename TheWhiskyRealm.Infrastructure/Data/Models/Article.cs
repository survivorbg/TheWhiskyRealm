using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TheWhiskyRealm.Infrastructure.Data.Enums;
using static TheWhiskyRealm.Infrastructure.Constants.ArticleDataConstants;

namespace TheWhiskyRealm.Infrastructure.Data.Models;

/// <summary>
/// Represents an article entity.
/// </summary>
[Comment("Represents an article entity.")]
public class Article
{
    /// <summary>
    /// Gets or sets the unique identifier of the article.
    /// </summary>
    [Key]
    [Comment("The unique identifier of the article.")]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the article.
    /// </summary>
    [Required]
    [StringLength(MaxTitleLength)]
    [Comment("The title of the article.")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the content of the article.
    /// </summary>
    [Required]
    [StringLength(MaxContentLength)]
    [Comment("The content of the article.")]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the URL of the image associated with the article.
    /// </summary>
    [Required]
    [Comment("The URL of the image associated with the article.")]
    public string ImageUrl { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date the article was created.
    /// </summary>
    [Required]
    [Comment("The date the article was created.")]
    public DateTime DateCreated { get; set; }

    /// <summary>
    /// Gets or sets the type of the article.
    /// </summary>
    [Required]
    [Comment("The type of the article.")]
    public ArticleType Type { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who published the article.
    /// </summary>
    [Required]
    [Comment("The identifier of the user who published the article.")]
    public string PublisherUserId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user who published the article.
    /// </summary>
    [ForeignKey(nameof(PublisherUserId))]
    [Comment("The user who published the article.")]
    public IdentityUser PublisherUser { get; set; } = null!;

    
}
