using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea.Whisky;
using TheWhiskyRealm.Core.Models.Whisky;
using TheWhiskyRealm.Core.Models.Whisky.Add;
using TheWhiskyRealm.Core.Models.Whisky.WhiskyApi;
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
            .Where(w => w.isApproved == true)
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
                DistilleryId = w.Distillery.Id,
                CountryName = w.Distillery.Region.Country.Name,
                RegionName = w.Distillery.Region.Name,
                AverageRating = avgRating != -1 ? avgRating.ToString("F2") : "No ratings yet",
                ImageURL = w.ImageURL,
                PublishedBy = w.PublishedBy,
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
                Name = model.Name,
                ImageURL = model.ImageURL,
                isApproved = model.IsApproved,
                PublishedBy = model.PublishedBy
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
                ImageURL = w.ImageURL,
                PublishedBy = w.PublishedBy,
                IsApproved = w.isApproved
            })
            .FirstAsync();
    }

    public async Task EditWhiskyAsync(int whiskyId, WhiskyFormModel model)
    {
        var whisky = await repo.GetByIdAsync<Whisky>(whiskyId);


        if (whisky != null)
        {
            whisky.Name = model.Name;
            whisky.Age = model.Age;
            whisky.WhiskyTypeId = model.WhiskyTypeId;
            whisky.AlcoholPercentage = model.AlcoholPercentage;
            whisky.Description = model.Description;
            whisky.DistilleryId = model.DistilleryId;
            whisky.ImageURL = model.ImageURL;
            whisky.isApproved = model.IsApproved;
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
            .AnyAsync(uw => uw.UserId == userId && uw.WhiskyId == whiskyId);
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

        if (entityToRemove != null)
        {
            repo.Delete(entityToRemove);
        }
        await repo.SaveChangesAsync();
    }

    public async Task<IEnumerable<AllWhiskyModel>> GetPagedWhiskiesAsync(int skip, int take, string sortOrder)
    {
        var whiskiesQuery = repo
            .AllReadOnly<Whisky>()
            .Where(w => w.isApproved == true)
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
            case "mostReviewed":
                whiskiesQuery = whiskiesQuery.OrderByDescending(w => w.Reviews);
                break;
            case "abv":
                whiskiesQuery = whiskiesQuery.OrderByDescending(w => w.AlcoholPercentage);
                break;
            case "type":
                whiskiesQuery = whiskiesQuery.OrderBy(w => w.WhiskyType);
                break;
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
            .Where(uw => uw.UserId == userId && uw.Whisky.isApproved == true)
            .Select(uw => new MyCollectionWhiskyModel()
            {
                Id = uw.Whisky.Id,
                WhiskyType = uw.Whisky.WhiskyType.Name,
                Name = uw.Whisky.Name,
                DistilleryName = uw.Whisky.Distillery.Name,
                ABV = uw.Whisky.AlcoholPercentage.ToString("F1"),
                Age = uw.Whisky.Age,
                Description = uw.Whisky.Description,
                ImageURL = uw.Whisky.ImageURL
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<WhiskyDistilleryViewModel>> GetWhiskiesByDistilleryIdAsync(int distilleryId)
    {
        return await repo
            .AllReadOnly<Whisky>()
            .Where(w => w.DistilleryId == distilleryId)
            .Select(w => new WhiskyDistilleryViewModel
            {
                Id = w.Id,
                Name = w.Name,
                IsApproved = w.isApproved ? "Yes" : "No"
            })
            .ToListAsync();

    }

    public async Task<bool> WhiskyIsApprovedAsync(int id)
    {
        return await repo
           .AllReadOnly<Whisky>()
           .AnyAsync(w => w.Id == id && w.isApproved == true);
    }

    public async Task<string?> GetWhiskyPublisherAsync(int id)
    {
        string? publisherId = null;
        var whisky = await repo.GetByIdAsync<Whisky>(id);
        if (whisky != null)
        {
            publisherId = whisky.PublishedBy;
        }
        return publisherId;
    }

    public async Task ApproveWhiskyAsync(int id)
    {
        var whisky = await repo.GetByIdAsync<Whisky>(id);
        if (whisky != null)
        {
            whisky.isApproved = true;
            await repo.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<AllWhiskyModel>> GetAllWhiskiesForApproveAsync()
    {
        return await repo.AllReadOnly<Whisky>()
            .Where(w => w.isApproved == false)
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

    public async Task<List<int>> GetAllWhiskiesIdsAsync()
    {
        return await repo
            .AllReadOnly<Whisky>()
            .Where(w => w.isApproved == true)
            .Select(x => x.Id)
            .ToListAsync();
    }

    public async Task<List<AllWhiskyModel>> GetTopTenRatedWhiskiesAsync(string type)
    {
        var whiskiesQuery = repo
            .AllReadOnly<Whisky>()
            .Where(w => w.isApproved == true);

        if (type.ToLower() != "all")
        {
            whiskiesQuery = whiskiesQuery.Where(w => w.WhiskyType.Name.ToLower() == type.ToLower());
        }

        var whiskies = await whiskiesQuery
            .Select(x => new AllWhiskyModel()
            {
                Id = x.Id,
                Name = x.Name,
                Age = x.Age,
                AlcoholPercentage = x.AlcoholPercentage,
                WhiskyType = x.WhiskyType.Name,
                Reviews = x.Reviews.Count(),
                ImageURL = x.ImageURL,
                AverageRating = x.Ratings.Any() ? x.Ratings.Average(r => (r.Finish + r.Nose + r.Taste) / 3.0) : 0
            })
            .OrderByDescending(w => w.AverageRating)
            .Take(10)
            .ToListAsync();

        return whiskies;
    }

    public async Task<IEnumerable<WhiskyApiModel>> GetAllWhiskiesAsync()
    {
        return await repo.AllReadOnly<Whisky>()
            .Where(w => w.isApproved == true)
            .OrderBy(w => w.Id)
            .Select(w => new WhiskyApiModel()
            {
                Id = w.Id,
                Name = w.Name,
                Age = w.Age,
                AlcoholPercentage = w.AlcoholPercentage,
                WhiskyType = w.WhiskyType.Name,
                Description = w.Description,
                Distillery = w.Distillery.Name,
                Country = w.Distillery.Region.Country.Name,
                Region = w.Distillery.Region.Name
            })
            .ToListAsync();
    }

    public async Task<WhiskyApiModel?> GetWhiskyApiModelByIdAsync(int id)
    {
        var whisky = await repo
            .AllReadOnly<Whisky>()
            .Where(w => w.Id == id)
            .Select(w => new WhiskyApiModel
            {
                Id = w.Id,
                Name = w.Name,
                Age = w.Age,
                AlcoholPercentage = w.AlcoholPercentage,
                WhiskyType = w.WhiskyType.Name,
                Description = w.Description,
                Distillery = w.Distillery.Name,
                Country = w.Distillery.Region.Country.Name,
                Region = w.Distillery.Region.Name
            })
            .FirstOrDefaultAsync();

        return whisky;
    }
}
