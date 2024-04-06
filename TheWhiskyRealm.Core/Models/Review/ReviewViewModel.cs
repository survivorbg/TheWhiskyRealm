namespace TheWhiskyRealm.Core.Models.Review;

public class ReviewViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public bool Recommend { get; set; }
    public string UserName { get; set; } = string.Empty;
}