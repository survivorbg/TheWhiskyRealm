using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Review;
using TheWhiskyRealm.Core.Models.Whisky;
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

    public async Task<ICollection<ReviewViewModel>> AllReviewsForWhiskyAsync(int whiskyId)
    {
        return await repo
            .All<Review>()
            .Where(r => r.WhiskyId == whiskyId)
            .OrderByDescending(r=>r.Id)
            .Select(r => new ReviewViewModel()
            {
                Content= r.Content,
                Id = r.Id,
                Recommend = r.Recommend,
                Title = r.Title,
                UserName = r.User.UserName
            })
            .ToListAsync();
    }

    public async Task<ICollection<MyReviewModel>> AllUserReviewsAsync(string userId)
    {
        return await repo
            .AllReadOnly<Review>()
            .Where(r=>r.UserId ==  userId)
            .OrderByDescending (r=>r.Id)
            .Select(r=> new MyReviewModel()
            {
                Content = r.Content,
                Id = r.Id,
                Recommend= r.Recommend,
                Title = r.Title,
                WhiskyId = r.WhiskyId,
                WhiskyName = r.Whisky.Name
            })
            .ToListAsync();
    }

    public async Task DeleteReviewAsync(int id)
    {
        var review = await repo
            .GetByIdAsync<Review>(id);

        if(review != null)
        {
            repo.Delete(review);    
        }

        await repo.SaveChangesAsync();
    }

    public async Task EditReviewAsync(int id, ReviewFormModel model)
    {
        var review = await repo
            .GetByIdAsync<Review>(id);

        if(review != null)
        {
            review.Title = model.Title; 
            review.Recommend = model.Recommend;
            review.Content = model.Content;
        }

        await repo.SaveChangesAsync();
    }

    public async Task<EditReviewFormModel?> GetReviewAsync(int id)
    {
        return await repo
            .AllReadOnly<Review>()
            .Where(r => r.Id == id)
            .Select(r => new EditReviewFormModel()
            {
                Content=r.Content,
                Recommend=r.Recommend,
                Title = r.Title,
                WhiskyId = r.WhiskyId,
                UserId = r.UserId,
                Id = id
            })
            .FirstOrDefaultAsync();
    }

    public async Task<int> GetReviewIdAsync(string userId, int whiskyId)
    {
        var review = await repo
             .AllReadOnly<Review>()
             .FirstAsync(r => r.WhiskyId == whiskyId && r.UserId == userId);
        return review.Id;
    }

    public async Task<bool> ReviewExistAsync(int id)
    {
        return await repo
            .AllReadOnly<Review>()
            .AnyAsync(r => r.Id == id);
    }

    public async Task<bool> UserAlreadyReviewedWhiskyAsync(string userId, int whiskyId)
    {
        return await repo
            .AllReadOnly<Review>()
            .AnyAsync(r=>r.WhiskyId == whiskyId && r.UserId == userId);
    }
}
