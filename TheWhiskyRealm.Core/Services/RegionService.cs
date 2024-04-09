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
}
