using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Rating;
using TheWhiskyRealm.Infrastructure.Data.Common;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Core.Services;

public class RatingService : IRatingService
{
    private readonly IRepository repo;

    public RatingService(IRepository repo)
    {
        this.repo = repo;
    }

    public async Task<double> GetAvgRatingAsync(int whiskyId)
    {
        var ratings = await repo
            .AllReadOnly<Rating>()
            .Where(r => r.WhiskyId == whiskyId)
            .ToListAsync();

        if (ratings.Any())
        {
            return ratings.Average(r => r.AveragePoints);
        }

        return -1;
    }

    public async Task<RatingViewModel?> GetRatingAsync(string userId, int whiskyId)
    {
        return await repo
            .All<Rating>()
            .Where(r => r.UserId == userId && r.WhiskyId == whiskyId)
            .Select(r => new RatingViewModel
            {
                Finish = r.Finish,
                Nose = r.Nose,
                Taste = r.Taste,
                WhiskyId = r.WhiskyId
            })
            .FirstOrDefaultAsync();
    }

    public async Task RateAsync(string userId, RatingViewModel model)
    {
        var rating = new Rating()
        {
            Finish = model.Finish,
            Nose = model.Nose,
            Taste = model.Taste,
            UserId = userId,
            WhiskyId = model.WhiskyId
        };

        await repo.AddAsync(rating);
        await repo.SaveChangesAsync();
    }

    public async Task<bool> UserAlreadyGaveRatingAsync(string userId, int whiskyId)
    {
        return await repo
            .AllReadOnly<Rating>()
            .AnyAsync(r => r.WhiskyId == whiskyId && r.UserId == userId);
    }
}
