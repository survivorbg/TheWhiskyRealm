using TheWhiskyRealm.Core.Models;

namespace TheWhiskyRealm.Core.Contracts;

public interface IWhiskyService
{
    Task<IEnumerable<AllWhiskyModel>> AllWhiskiesAsync();
}
