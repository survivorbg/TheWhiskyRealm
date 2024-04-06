namespace TheWhiskyRealm.Core.Models.Comment;

public class CommentViewModel
{

    public int Id { get; set; }

    public string Content { get; set; } = string.Empty;

    public string DatePosted { get; set; } = string.Empty;

    public string AuthorName { get; set; } = string.Empty;

    public int ArticleId { get; set; }

}
