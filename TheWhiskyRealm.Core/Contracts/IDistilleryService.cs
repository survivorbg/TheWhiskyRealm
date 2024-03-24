using TheWhiskyRealm.Core.Models.Whisky.Add;

namespace TheWhiskyRealm.Core.Contracts;

public interface IDistilleryService
{
    Task<IEnumerable<DistilleryAddWhiskyViewModel>> GetAllDistilleriesAsync();
}
