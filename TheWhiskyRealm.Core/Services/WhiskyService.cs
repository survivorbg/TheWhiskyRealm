using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Whisky;
using TheWhiskyRealm.Core.Models.Whisky.Add;
using TheWhiskyRealm.Infrastructure.Data.Common;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Core.Services;

public class WhiskyService : IWhiskyService
{
    private readonly IRepository repo;
    private readonly IRatingService ratingService;

    public WhiskyService(IRepository repo, IRatingService ratingService)
    {
        this.repo = repo;
        this.ratingService = ratingService;
    }

    public async Task<IEnumerable<AllWhiskyModel>> GetPagedWhiskiesAsync(int skip, int take)
    {
        return await repo.AllReadOnly<Whisky>()
            .OrderByDescending(w => w.Id)
            .Skip(skip)
            .Take(take)
            .Select(x => new AllWhiskyModel()
            {
                Id = x.Id,
                Name = x.Name,
                Age = x.Age,
                AlcoholPercentage = x.AlcoholPercentage,
                WhiskyType = x.WhiskyType.Name,
                Reviews = x.Reviews.Count(),
                ImageURL = x.ImageURL
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<AllWhiskyModel>> GetMoreWhiskiesAsync(int skip, int take)
    {

        return await repo.AllReadOnly<Whisky>()
            .OrderByDescending(w => w.Id)
            .Skip(skip)
            .Take(take)
            .Select(x => new AllWhiskyModel()
            {
                Id = x.Id,
                Name = x.Name,
                Age = x.Age,
                AlcoholPercentage = x.AlcoholPercentage,
                WhiskyType = x.WhiskyType.Name,
                Reviews = x.Reviews.Count(),
                ImageURL = x.ImageURL
            })
            .ToListAsync();
    }

    public async Task<bool> WhiskyExistAsync(int id)
    {
        return await repo
            .AllReadOnly<Whisky>()
            .AnyAsync(w => w.Id == id);
    }

    public async Task<DetailsWhiskyViewModel> GetWhiskyByIdAsync(int id)
    {
        var avgRating = await ratingService.GetAvgRatingAsync(id);

        return await repo
            .AllReadOnly<Whisky>()
            .Where(w => w.Id == id)
            .Select(w => new DetailsWhiskyViewModel
            {
                Id = w.Id,
                Name = w.Name,
                Age = w.Age,
                AlcoholPercentage = w.AlcoholPercentage.ToString("F1"),
                Description = w.Description,
                WhiskyType = w.WhiskyType.Name,
                DistilleryName = w.Distillery.Name,
                CountryName = w.Distillery.Region.Country.Name,
                RegionName = w.Distillery.Region.Name,
                AverageRating = avgRating != -1 ? avgRating.ToString("F2") : "No ratings yet",
                ImageURL = w.ImageURL,
            })
            .FirstAsync();
    }

    public async Task AddWhiskyAsync(WhiskyFormModel model)
    {
        if (model != null)
        {
            Whisky whisky = new()
            {
                Age = model.Age,
                AlcoholPercentage = model.AlcoholPercentage,
                Description = model.Description,
                DistilleryId = model.DistilleryId,
                WhiskyTypeId = model.WhiskyTypeId,
                Name = model.Name
            };
            await repo.AddAsync(whisky);
            await repo.SaveChangesAsync();
        }
    }

    public async Task<WhiskyFormModel> GetWhiskyByIdForEditAsync(int id)
    {
        return await repo
            .AllReadOnly<Whisky>()
            .Where(w => w.Id == id)
            .Select(w => new WhiskyFormModel
            {
                Name = w.Name,
                Age = w.Age,
                AlcoholPercentage = w.AlcoholPercentage,
                Description = w.Description,
                WhiskyTypeId = w.WhiskyType.Id,
                DistilleryId = w.Distillery.Id,
            })
            .FirstAsync();
    }

    public async Task EditWhiskyAsync(int whiskyId, WhiskyFormModel model)
    {
        var whisky = await repo.GetByIdAsync<Whisky>(whiskyId);

        if(whisky != null)
        {
            whisky.Name = model.Name;
            whisky.Age = model.Age;
            whisky.WhiskyTypeId = model.WhiskyTypeId;
            whisky.AlcoholPercentage = model.AlcoholPercentage;
            whisky.Description = model.Description;
            whisky.DistilleryId = model.DistilleryId;
        }

        await repo.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        if (await WhiskyExistAsync(id))
        {
            await repo.DeleteById<Whisky>(id);
        }
        await repo.SaveChangesAsync();
    }

    public async Task<bool> WhiskyInFavouritesAsync(string userId, int whiskyId)
    {
        return await repo
            .AllReadOnly<UserWhisky>()
            .AnyAsync(uw=> uw.User.Id == userId && uw.Whisky.Id == whiskyId);
    }

    public async Task AddToFavouritesAsync(string userId, int whiskyId)
    {
        var userWhisky = new UserWhisky()
        {
            UserId = userId,
            WhiskyId = whiskyId
        };
        await repo.AddAsync(userWhisky);
        await repo.SaveChangesAsync();
    }

    public async Task RemoveFromFavouritesAsync(string userId, int whiskyId)
    {
        var entityToRemove = await repo.
            All<UserWhisky>()
            .Where(uw => uw.UserId == userId && uw.WhiskyId == whiskyId)
            .FirstOrDefaultAsync();

        if(entityToRemove != null)
        {
            repo.Delete(entityToRemove);
        }
        await repo.SaveChangesAsync();
    }

    public async Task<IEnumerable<AllWhiskyModel>> GetPagedWhiskiesAsync(int skip, int take, string sortOrder)
    {
        var whiskiesQuery = repo
            .AllReadOnly<Whisky>()
            .Select(x => new AllWhiskyModel()
        {
            Id = x.Id,
            Name = x.Name,
            Age = x.Age,
            AlcoholPercentage = x.AlcoholPercentage,
            WhiskyType = x.WhiskyType.Name,
            Reviews = x.Reviews.Count(),
            ImageURL = x.ImageURL,
        });

        

        switch (sortOrder)
        {
            case "recent":
                whiskiesQuery = whiskiesQuery.OrderByDescending(w => w.Id);
                break;
            case "oldest":
                whiskiesQuery = whiskiesQuery.OrderByDescending(w => w.Age);
                break;
            case "youngest":
                whiskiesQuery = whiskiesQuery.OrderBy(w => w.Age);
                break;
            case "mostReviewed":
                whiskiesQuery = whiskiesQuery.OrderByDescending(w => w.Reviews);
                break;
            case "abv":
                whiskiesQuery = whiskiesQuery.OrderByDescending(w => w.AlcoholPercentage);
                break;
            //case "highestRating":
            //    whiskiesQuery = whiskiesQuery.OrderByDescending(w => w.AverageRating);
            //    break;
            //case "lowestRating":
            //    whiskiesQuery = whiskiesQuery.OrderBy(w => w.AverageRating);
            //    break;
            default:
                whiskiesQuery = whiskiesQuery.OrderByDescending(w => w.Id);
                break;
        }

        return await whiskiesQuery.Skip(skip).Take(take).ToListAsync();
    }

    public async Task<ICollection<MyCollectionWhiskyModel>> MyFavouriteWhiskiesAsync(string userId)
    {
        return await repo
            .AllReadOnly<UserWhisky>()
            .Where(uw => uw.UserId == userId)
            .Select(uw => new MyCollectionWhiskyModel()
            {
                Id = uw.Whisky.Id,
                WhiskyType = uw.Whisky.WhiskyType.Name,
                Name = uw.Whisky.Name,
                DistilleryName = uw.Whisky.Distillery.Name,
                ABV = uw.Whisky.AlcoholPercentage.ToString("F1"),
                Age = uw.Whisky.Age,
                Description = uw.Whisky.Description,
            })
            .ToListAsync();
    }
}
