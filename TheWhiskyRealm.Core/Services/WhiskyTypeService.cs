using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Whisky.Add;
using TheWhiskyRealm.Infrastructure.Data.Common;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Core.Services;

public class WhiskyTypeService : IWhiskyTypeService
{
    private readonly IRepository repo;
    private readonly IMemoryCache cache;

    public WhiskyTypeService(IRepository repo, IMemoryCache cache)
    {
        this.repo = repo;
        this.cache = cache;
    }

    public async Task<IEnumerable<WhiskyTypeViewModel>> GetAllWhiskyTypesAsync()
    {
        return await cache.GetOrCreateAsync("WhiskyTypes", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
        return await repo
            .AllReadOnly<WhiskyType>()
            .Select(wt => new WhiskyTypeViewModel
            {
                Id = wt.Id,
                Name = wt.Name
            })
            .ToListAsync();
        });

    }

    public async Task<string?> GetWhiskyTypeNameAsync(int id)
    {
        var whiskyType = await repo.GetByIdAsync<WhiskyType>(id);
        if(whiskyType == null)
        {
            return null;
        }
        return whiskyType.Name;
    }

    public async Task<bool> WhiskyTypeExistsAsync(int id)
    {
        return await repo
            .AllReadOnly<WhiskyType>()
            .AnyAsync(wt => wt.Id == id);
    }

    public async Task<bool> WhiskyTypeExistsByNameAsync(string name)
    {
        return await repo
            .AllReadOnly<WhiskyType>()
            .AnyAsync(wt => wt.Name.ToLower() == name.ToLower());
    }
}
