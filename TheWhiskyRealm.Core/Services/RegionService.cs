using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea.Region;
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

    public async Task AddRegionAsync(string name, int countryId)
    {
        var region = new Region
        {
            Name = name,
            CountryId = countryId
        };

        await repo.AddAsync(region);
        await repo.SaveChangesAsync();
    }

    public async Task<ICollection<RegionCountryViewModel>> GetAllRegionsByCountryIdAsync(int countryId)
    {
        return await repo
            .AllReadOnly<Region>()
            .Where(r => r.CountryId == countryId)
            .Select(r => new RegionCountryViewModel
            {
                Name = r.Name,
                Distilleries = r.Distilleries.Count()
            })
            .ToListAsync();

    }

    public async Task<ICollection<RegionViewModel>> GetAllRegionsAsync(int currentPage, int pageSize)
    {
        return await repo
            .AllReadOnly<Region>()
            .OrderBy(r => r.CountryId)
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize)
            .Select(r => new RegionViewModel
            {
                Id = r.Id,
                Name = r.Name,
                CountryName = r.Country.Name
            })
            .ToListAsync();
    }

    public async Task<int> GetTotalRegionsAsync()
    {
        return await repo
            .AllReadOnly<Region>()
            .CountAsync();
    }

    public async Task<bool> RegionExistsAsync(int id)
    {
        return await repo
            .AllReadOnly<Region>()
            .AnyAsync(r => r.Id == id);
    }

    public async Task<bool> RegionWithThisNameAndCountryExistsAsync(string name, int countryId)
    {
        return await repo
            .AllReadOnly<Region>()
            .AnyAsync(r => r.Name == name && r.CountryId == countryId);
    }

    public async Task<EditRegionViewModel?> GetRegionByIdAsync(int id)
    {
        var region = await repo.GetByIdAsync<Region>(id);
        if (region == null)
        {
            return null;
        }

        var result = new EditRegionViewModel
        {
            Id = id,
            CountryId = region.CountryId,
            Name = region.Name,
        };

        return result;
    }

    public async Task EditRegionAsync(EditRegionViewModel model)
    {
        var region = await repo.GetByIdAsync<Region>(model.Id);
        if(region != null)
        {
            region.Name = model.Name;
            region.CountryId = model.CountryId;
            await repo.SaveChangesAsync();
        }
    }

    public async Task<ICollection<RegionViewModel>> GetAllRegionsAsync()
    {
        return await repo
            .AllReadOnly<Region>()
            .OrderBy(r => r.CountryId)
            .Select(r => new RegionViewModel
            {
                Id = r.Id,
                Name = r.Name + ", " + r.Country.Name
            })
            .ToListAsync();
    }
}
