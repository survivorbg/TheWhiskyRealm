namespace TheWhiskyRealm.Core.Models.Review;

public class EditReviewFormModel : ReviewFormModel
{
    public string UserId { get; set; } = string.Empty;
    public int Id { get; set; }
}
