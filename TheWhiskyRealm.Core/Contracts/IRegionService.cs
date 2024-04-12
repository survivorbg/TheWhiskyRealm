using TheWhiskyRealm.Core.Models.AdminArea.Region;

namespace TheWhiskyRealm.Core.Contracts;

public interface IRegionService
{
    Task<int> AddRegionAsync(string name, int countryId);
    Task<ICollection<RegionCountryViewModel>> GetAllRegionsByCountryIdAsync(int countryId);
    Task<ICollection<RegionViewModel>> GetAllRegionsAsync(int currentPage, int pageSize);
    Task<ICollection<RegionViewModel>> GetAllRegionsAsync();
    Task<int> GetTotalRegionsAsync();
    Task<bool> RegionExistsAsync(int id);
    Task<bool> RegionWithThisNameAndCountryExistsAsync(string name, int countryId,int regionId = 0);
    Task<EditRegionViewModel?> GetRegionByIdAsync(int id);
    Task EditRegionAsync(EditRegionViewModel model);
}
