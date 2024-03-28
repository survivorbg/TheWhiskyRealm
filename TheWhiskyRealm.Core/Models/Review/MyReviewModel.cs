using System.ComponentModel.DataAnnotations;
using static TheWhiskyRealm.Core.Constants.MessageConstants;
using static TheWhiskyRealm.Infrastructure.Constants.ReviewDataConstants;

namespace TheWhiskyRealm.Core.Models.Review;

public class MyReviewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = RequiredMessage)]
    [StringLength(ReviewMaxTitleLength, MinimumLength = ReviewMinTitleLength, ErrorMessage = LengthMessage)]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = RequiredMessage)]
    [StringLength(ReviewMaxContentLength, MinimumLength = ReviewMinContentLength, ErrorMessage = LengthMessage)]
    public string Content { get; set; } = string.Empty;

    [Required(ErrorMessage = RecommendBoolMessage)]
    public bool Recommend { get; set; }

    [Required(ErrorMessage = RequiredMessage)]
    public int WhiskyId { get; set; }
    public string WhiskyName { get; set; } = string.Empty;
}
