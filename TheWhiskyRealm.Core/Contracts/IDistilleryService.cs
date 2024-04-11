using TheWhiskyRealm.Core.Models.AdminArea.Distillery;
using TheWhiskyRealm.Core.Models.Whisky.Add;

namespace TheWhiskyRealm.Core.Contracts;

public interface IDistilleryService
{
    Task<IEnumerable<DistilleryAddWhiskyViewModel>> GetAllDistilleriesAsync();
    Task<IEnumerable<DistilleryRegionViewModel>> GetAllDistilleriesAsync(int regionId);
    Task<IEnumerable<DistilleryViewModel>> GetAllDistilleriesAsync(int currentPage, int pageSize, string sortOrder);
    Task<bool> DistilleryExistsAsync(int id);
    Task<int> GetTotalDistilleriesAsync();
    Task<DistilleryInfoModel?> GetDistilleryInfoAsync(int id);
}
