using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea.Distillery;
using TheWhiskyRealm.Core.Models.AdminArea.Region;
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

    public async Task<IEnumerable<DistilleryViewModel>> GetAllDistilleriesAsync(int currentPage, int pageSize, string sortOrder)
    {
        var distilleries = repo.AllReadOnly<Distillery>();

        switch (sortOrder)
        {
            case "name_desc":
                distilleries = distilleries.OrderByDescending(d => d.Name);
                break;
            case "Region":
                distilleries = distilleries.OrderBy(d => d.Region.Name);
                break;
            case "region_desc":
                distilleries = distilleries.OrderByDescending(d => d.Region.Name);
                break;
            case "Country":
                distilleries = distilleries.OrderBy(d => d.Region.Country.Name);
                break;
            case "country_desc":
                distilleries = distilleries.OrderByDescending(d => d.Region.Country.Name);
                break;
            default:
                distilleries = distilleries.OrderBy(d => d.Id);
                break;
        }

        return await distilleries
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize)
            .Select(d => new DistilleryViewModel
            {
                Id = d.Id,
                Name = d.Name,
                Country = d.Region.Country.Name,
                Region = d.Region.Name,
                YearFounded = d.YearFounded
            })
            .ToListAsync();
    }

    public async Task<int> GetTotalDistilleriesAsync()
    {
        return await repo
            .AllReadOnly<Distillery>()
            .CountAsync();
    }
}
