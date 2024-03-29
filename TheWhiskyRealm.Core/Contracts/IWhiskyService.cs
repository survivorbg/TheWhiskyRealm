using TheWhiskyRealm.Core.Models.Whisky;
using TheWhiskyRealm.Core.Models.Whisky.Add;

namespace TheWhiskyRealm.Core.Contracts;

public interface IWhiskyService
{
    Task<IEnumerable<AllWhiskyModel>> GetPagedWhiskiesAsync(int skip, int take);
    Task<IEnumerable<AllWhiskyModel>> GetPagedWhiskiesAsync(int skip, int take, string sortOrder);
    Task<IEnumerable<AllWhiskyModel>> GetMoreWhiskiesAsync(int skip, int take);
    Task<bool> WhiskyExistAsync(int id);
    Task<DetailsWhiskyViewModel> GetWhiskyByIdAsync(int id);
    Task<WhiskyFormModel> GetWhiskyByIdForEditAsync(int id);
    Task AddWhiskyAsync(WhiskyFormModel model);
    Task EditWhiskyAsync(int whiskyId, WhiskyFormModel model);
    Task DeleteAsync(int id);
    Task<bool> WhiskyInFavouritesAsync(string userId, int whiskyId);
    Task AddToFavouritesAsync(string userId, int whiskyId);
    Task RemoveFromFavouritesAsync(string userId, int whiskyId);
}
