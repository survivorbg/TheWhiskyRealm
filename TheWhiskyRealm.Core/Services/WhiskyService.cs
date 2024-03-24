using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models;
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
                Reviews = x.Reviews.Count()
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
                Reviews = x.Reviews.Count()
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
                AverageRating = avgRating != -1 ? avgRating.ToString("F2") : "No ratings yet"
            })
            .FirstAsync();
    }

    public async Task AddWhiskyAsync(AddWhiskyViewModel model)
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
}
