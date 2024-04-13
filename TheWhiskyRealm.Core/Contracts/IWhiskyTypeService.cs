using TheWhiskyRealm.Core.Models.Whisky.Add;

namespace TheWhiskyRealm.Core.Contracts;

public interface IWhiskyTypeService
{
    Task<IEnumerable<WhiskyTypeViewModel>> GetAllWhiskyTypesAsync();
    Task<bool> WhiskyTypeExistsAsync(int id);
    Task<string?> GetWhiskyTypeNameAsync(int id);
}
