using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models;
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
}
