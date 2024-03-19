using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TheWhiskyRealm.Infrastructure.Constants.CommentDataConstants;

namespace TheWhiskyRealm.Infrastructure.Data.Models;

/// <summary>
/// Represents a comment entity.
/// </summary>
[Comment("Represents a comment entity.")]
public class Comment
{
    /// <summary>
    /// Gets or sets the unique identifier of the comment.
    /// </summary>
    [Key]
    [Comment("The unique identifier of the comment.")]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the content of the comment.
    /// </summary>
    [Required]
    [StringLength(MaxContentLength)]
    [Comment("The content of the comment.")]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date the comment was posted.
    /// </summary>
    [Required]
    [Comment("The date the comment was posted.")]
    public DateTime PostedDate { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who posted the comment.
    /// </summary>
    [Required]
    [Comment("The identifier of the user who posted the comment.")]
    public string UserId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user who posted the comment.
    /// </summary>
    [ForeignKey(nameof(UserId))]
    [Comment("The user who posted the comment.")]       
    public ApplicationUser User { get; set; } = null!;

    /// <summary>
    /// Gets or sets the identifier of the article associated with the comment.
    /// </summary>
    [Required]
    [Comment("The identifier of the article associated with the comment.")]
    public int ArticleId { get; set; }

    /// <summary>
    /// Gets or sets the article associated with the comment.
    /// </summary>
    [ForeignKey(nameof(ArticleId))]
    [Comment("The article associated with the comment.")]
    public Article Article { get; set; } = null!;
}
