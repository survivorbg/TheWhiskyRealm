using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea.Distillery;
using TheWhiskyRealm.Core.Models.Whisky.Add;
using TheWhiskyRealm.Infrastructure.Data.Common;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Core.Services;

public class DistilleryService : IDistilleryService
{
    private readonly IRepository repo;

    public DistilleryService(IRepository repo)
    {
        this.repo = repo;
    }

    public async Task<bool> DistilleryExistsAsync(int id)
    {
        return await repo
            .AllReadOnly<Distillery>()
            .AnyAsync(d=>d.Id == id);
    }

    public async Task<IEnumerable<DistilleryAddWhiskyViewModel>> GetAllDistilleriesAsync()
    {
        return await repo
            .AllReadOnly<Distillery>()
            .Select(d => new DistilleryAddWhiskyViewModel()
            {
                DistilleryId = d.Id,
                Name = d.Name,
                Country = d.Region.Country.Name
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<DistilleryRegionViewModel>> GetAllDistilleriesAsync(int regionId)
    {
        return await repo
           .AllReadOnly<Distillery>()
           .Where(d=>d.RegionId == regionId)    
           .Select(d => new DistilleryRegionViewModel()
           {
              Id = d.Id,
              Name= d.Name,
              YearFounded = d.YearFounded
           })
           .ToListAsync();
    }
}
