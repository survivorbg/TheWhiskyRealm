using TheWhiskyRealm.Core.Models.AdminArea.Distillery;
using TheWhiskyRealm.Core.Models.Whisky.Add;
using TheWhiskyRealm.Core.Models.Whisky.WhiskyApi;

namespace TheWhiskyRealm.Core.Contracts;

public interface IDistilleryService
{
    Task<IEnumerable<DistilleryAddWhiskyViewModel>> GetAllDistilleriesAsync();
    Task<IEnumerable<DistilleryApiModel>> GetAllDistilleriesForApi();
    Task<IEnumerable<DistilleryRegionViewModel>> GetAllDistilleriesAsync(int regionId);
    Task<IEnumerable<DistilleryViewModel>> GetAllDistilleriesAsync(int currentPage, int pageSize, string sortOrder);
    Task<bool> DistilleryExistsAsync(int id);
    Task<int> GetTotalDistilleriesAsync();
    Task<DistilleryInfoModel?> GetDistilleryInfoAsync(int id);
    Task<bool> DistilleryExistByName(string name, int id = 0);
    Task<int> AddDistilleryAsync(DistilleryFormViewModel model);
    Task<DistilleryFormViewModel?> GetDistilleryByIdAsync(int id);
    Task EditDistilleryAsync(DistilleryFormViewModel model);
    Task<DistilleryDetailsApiModel?> GetDistilleryDetailsForApi(int id);
}
