using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Review;
using TheWhiskyRealm.Infrastructure.Data.Common;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Core.Services;

public class ReviewService : IReviewService
{
    private readonly IRepository repo;

    public ReviewService(IRepository repo)
    {
        this.repo = repo;
    }

    public async Task AddReviewAsync(ReviewFormModel model,string userId)
    {

        Review review = new Review()
        {
            Content = model.Content,
            Recommend = model.Recommend,
            Title = model.Title,
            WhiskyId = model.WhiskyId,
            UserId = userId
        };

        await repo.AddAsync(review);
        await repo.SaveChangesAsync();
    }

    public async Task<int> GetReviewIdAsync(string userId, int whiskyId)
    {
        var review = await repo
             .AllReadOnly<Review>()
             .FirstAsync(r => r.WhiskyId == whiskyId && r.UserId == userId);
        return review.Id;
    }

    public async Task<bool> UserAlreadyReviewedWhiskyAsync(string userId, int whiskyId)
    {
        return await repo
            .AllReadOnly<Review>()
            .AnyAsync(r=>r.WhiskyId == whiskyId && r.UserId == userId);
    }
}
