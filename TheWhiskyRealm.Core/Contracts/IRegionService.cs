using TheWhiskyRealm.Core.Models.AdminArea.Region;

namespace TheWhiskyRealm.Core.Contracts;

public interface IRegionService
{
    public Task<ICollection<RegionCountryViewModel>> GetAllRegionsByCountryIdAsync(int countryId);
}
