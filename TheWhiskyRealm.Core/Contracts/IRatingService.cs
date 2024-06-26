﻿using TheWhiskyRealm.Core.Models.Rating;

namespace TheWhiskyRealm.Core.Contracts;

public interface IRatingService
{
    Task<double> GetAvgRatingAsync(int whiskyId);
    Task<bool> UserAlreadyGaveRatingAsync(string userId, int whiskyId);
    Task RateAsync(string userId, RatingViewModel model);
    Task<RatingEditViewModel?> GetRatingAsync(string userId, int whiskyId);
    Task EditRatingAsync(RatingViewModel model, int ratingId);
    Task<ICollection<MyRatingViewModel>> GetRatingsByUserAsync(string userId);
}
