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
            .Where(r=>r.CountryId == countryId)
            .Select(r=>new RegionCountryViewModel
            {
                Name = r.Name,
                Distilleries = r.Distilleries.Count()
            })
            .ToListAsync();

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
}
