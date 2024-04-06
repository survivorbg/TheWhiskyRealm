using TheWhiskyRealm.Core.Models.Comment;

namespace TheWhiskyRealm.Core.Models.Article;

public class ArticleDetailsViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string FormattedContent => Content.Replace("\n", "<br>");
    public string ImageUrl { get; set; } = string.Empty;
    public string DateCreated { get; set; } = string.Empty;

    public string AuthorName { get; set; } = string.Empty;
    public string AuthorId { get; set; } = string.Empty;
    public string ArticleType { get; set; } = string.Empty;
    public CommentAddViewModel Comment { get; set; } = null!;
    public ICollection<CommentViewModel> Comments = new List<CommentViewModel>();
}
