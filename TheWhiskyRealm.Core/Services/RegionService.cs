using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Whisky.Add;
using TheWhiskyRealm.Infrastructure.Data.Common;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Core.Services;

public class RegionService : IRegionService
{
    private readonly IRepository repo;

    public RegionService(IRepository repo)
    {
        this.repo = repo;
    }

    public async Task<IEnumerable<RegionViewModel>> GetAllRegionsAsync()
    {
        return await repo
            .AllReadOnly<Region>()
            .Select(r => new RegionViewModel()
            {
                RegionId = r.Id,
                Name = r.Name,
            })
            .ToListAsync();
    } 
}
