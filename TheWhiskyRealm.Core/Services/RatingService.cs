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

    public async Task EditRatingAsync(RatingViewModel model, int ratingId)
    {
        var rating = await repo.GetByIdAsync<Rating>(ratingId);
        if(rating != null)
        {
            rating.Nose = model.Nose;
            rating.Finish = model.Finish;
            rating.Taste = model.Taste;
        }
        await repo.SaveChangesAsync();
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

    public async Task<RatingEditViewModel?> GetRatingAsync(string userId, int whiskyId)
    {
        return await repo
            .All<Rating>()
            .Where(r => r.UserId == userId && r.WhiskyId == whiskyId)
            .Select(r => new RatingEditViewModel
            {
                Id = r.Id,
                Finish = r.Finish,
                Nose = r.Nose,
                Taste = r.Taste,
                WhiskyId = r.WhiskyId,
                UserId = r.UserId,
            })
            .FirstOrDefaultAsync();
    }

    public async Task<ICollection<MyRatingViewModel>> GetRatingsByUserAsync(string userId)
    {
        return await repo
            .AllReadOnly<Rating>()
            .Where(r => r.UserId == userId && r.Whisky.isApproved == true)
            .OrderByDescending(r=>r.Id)
            .Select(r => new MyRatingViewModel
            {
                Finish = r.Finish,
                Nose = r.Nose,
                Taste = r.Taste,
                WhiskyId= r.WhiskyId,
                WhiskyName = r.Whisky.Name
            })
            .ToListAsync();
    }

    public async Task RateAsync(string userId, RatingViewModel model)
    {
        if (userId != null && model != null)
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
        }
        await repo.SaveChangesAsync();
    }

    public async Task<bool> UserAlreadyGaveRatingAsync(string userId, int whiskyId)
    {
        return await repo
            .AllReadOnly<Rating>()
            .AnyAsync(r => r.WhiskyId == whiskyId && r.UserId == userId);
    }
}
