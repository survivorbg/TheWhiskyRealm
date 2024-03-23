using TheWhiskyRealm.Core.Models;

namespace TheWhiskyRealm.Core.Contracts;

public interface IWhiskyTypeService
{
    Task<IEnumerable<WhiskyTypeViewModel>> GetAllWhiskyTypesAsync();
}
