using TheWhiskyRealm.Core.Models.Rating;

namespace TheWhiskyRealm.Core.Contracts;

public interface IRatingService
{
    Task<double> GetAvgRatingAsync(int whiskyId);
    Task<bool> UserAlreadyGaveRatingAsync(string userId, int whiskyId);
    Task RateAsync(string userId, RatingViewModel model);
}
