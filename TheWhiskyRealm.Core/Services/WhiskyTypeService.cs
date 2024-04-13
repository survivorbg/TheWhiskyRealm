using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Whisky.Add;
using TheWhiskyRealm.Infrastructure.Data.Common;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Core.Services;

public class WhiskyTypeService : IWhiskyTypeService
{
    private readonly IRepository repo;

    public WhiskyTypeService(IRepository repo)
    {
        this.repo = repo;
    }

    public async Task<IEnumerable<WhiskyTypeViewModel>> GetAllWhiskyTypesAsync()
    {
        return await repo
            .AllReadOnly<WhiskyType>()
            .Select(wt => new WhiskyTypeViewModel
            {
                Id = wt.Id,
                Name = wt.Name
            })
            .ToListAsync();
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
            .AnyAsync(wt=>wt.Id == id);
    }

    
}
