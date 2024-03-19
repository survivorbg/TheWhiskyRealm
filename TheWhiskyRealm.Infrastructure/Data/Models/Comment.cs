using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TheWhiskyRealm.Infrastructure.Constants.CommentDataConstants;

namespace TheWhiskyRealm.Infrastructure.Data.Models;

public class Comment
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(MaxContentLength)]
    public string Content { get; set; } = string.Empty;

    [Required]
    public DateTime PostedDate { get; set; }

    [Required]
    public string UserId { get; set; } = null!;

    [ForeignKey(nameof(UserId))]
    public IdentityUser User { get; set; } = null!;

    [Required]
    public int ArticleId { get; set; } 

    [ForeignKey(nameof(ArticleId))]
    public Article Article { get; set; } = null!;
}
