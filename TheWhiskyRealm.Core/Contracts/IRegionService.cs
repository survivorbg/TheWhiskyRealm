using TheWhiskyRealm.Core.Models.AdminArea.Region;

namespace TheWhiskyRealm.Core.Contracts;

public interface IRegionService
{
    Task AddRegionAsync(string name, int countryId);
    public Task<ICollection<RegionCountryViewModel>> GetAllRegionsByCountryIdAsync(int countryId);
    Task<bool> RegionExistsAsync(int id);
    Task<bool> RegionWithThisNameAndCountryExistsAsync(string name, int countryId);
}
