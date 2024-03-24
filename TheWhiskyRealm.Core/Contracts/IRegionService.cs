using TheWhiskyRealm.Core.Models.Whisky.Add;

namespace TheWhiskyRealm.Core.Contracts;

public interface IRegionService
{
    Task<IEnumerable<RegionViewModel>> GetAllRegionsAsync();
}
