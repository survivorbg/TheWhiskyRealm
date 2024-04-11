using TheWhiskyRealm.Core.Models.AdminArea.Distillery;
using TheWhiskyRealm.Core.Models.Whisky.Add;

namespace TheWhiskyRealm.Core.Contracts;

public interface IDistilleryService
{
    Task<IEnumerable<DistilleryAddWhiskyViewModel>> GetAllDistilleriesAsync();
    Task<IEnumerable<DistilleryRegionViewModel>> GetAllDistilleriesAsync(int regionId);
    Task<bool> DistilleryExistsAsync(int id);
}
