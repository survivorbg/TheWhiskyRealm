using TheWhiskyRealm.Core.Models.Whisky.Add;

namespace TheWhiskyRealm.Core.Contracts;

public interface IWhiskyTypeService
{
    Task<IEnumerable<WhiskyTypeViewModel>> GetAllWhiskyTypesAsync();
    Task<bool> WhiskyTypeExistsAsync(int id);
    Task<bool> WhiskyTypeExistsByNameAsync(string name);
    Task<string?> GetWhiskyTypeNameAsync(int id);
}
