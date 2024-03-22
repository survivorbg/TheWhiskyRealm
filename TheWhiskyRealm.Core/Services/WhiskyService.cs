using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models;
using TheWhiskyRealm.Infrastructure.Data.Common;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Core.Services;

public class WhiskyService : IWhiskyService
{
    private readonly IRepository repo;

    public WhiskyService(IRepository repo)
    {
        this.repo = repo;
    }

    public async Task<IEnumerable<AllWhiskyModel>> GetPagedWhiskiesAsync(int skip, int take)
    {
        return await repo.AllReadOnly<Whisky>()
            .OrderBy(w => w.Id)
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
            .OrderBy(w => w.Id)
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
}
