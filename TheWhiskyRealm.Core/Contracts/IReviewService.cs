using TheWhiskyRealm.Core.Models.Review;
using TheWhiskyRealm.Core.Models.Whisky;
using TheWhiskyRealm.Infrastructure.Data.Models;

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
}
