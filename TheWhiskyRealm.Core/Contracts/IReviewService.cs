using TheWhiskyRealm.Core.Models.Review;

namespace TheWhiskyRealm.Core.Contracts;

public interface IReviewService
{
    Task AddReviewAsync(ReviewFormModel model,string userId);
    Task<bool> UserAlreadyReviewedWhiskyAsync(string userId,int whiskyId);
    Task<int> GetReviewIdAsync(string userId, int whiskyId);
    Task<EditReviewFormModel?> GetReviewAsync(int id);
    Task<bool> ReviewExistAsync(int id);
    Task EditReviewAsync(int id, ReviewFormModel model);
    Task<ICollection<ReviewViewModel>> AllReviewsForWhiskyAsync(int whiskyId);
    Task DeleteReviewAsync(int id);
    Task<ICollection<MyReviewModel>> AllUserReviewsAsync(string userId);
}
