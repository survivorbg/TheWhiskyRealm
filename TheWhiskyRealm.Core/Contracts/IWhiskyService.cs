using TheWhiskyRealm.Core.Models.AdminArea.Whisky;
using TheWhiskyRealm.Core.Models.Whisky;
using TheWhiskyRealm.Core.Models.Whisky.Add;

namespace TheWhiskyRealm.Core.Contracts;

public interface IWhiskyService
{
    Task<IEnumerable<AllWhiskyModel>> GetAllWhiskiesForApproveAsync();
    Task<IEnumerable<AllWhiskyModel>> GetPagedWhiskiesAsync(int skip, int take);
    Task<IEnumerable<AllWhiskyModel>> GetPagedWhiskiesAsync(int skip, int take, string sortOrder);
    Task<List<AllWhiskyModel>> GetTopTenRatedWhiskiesAsync();
    Task<bool> WhiskyExistAsync(int id);
    Task<bool> WhiskyIsApprovedAsync(int id);
    Task<DetailsWhiskyViewModel> GetWhiskyByIdAsync(int id);
    Task<WhiskyFormModel> GetWhiskyByIdForEditAsync(int id);
    Task AddWhiskyAsync(WhiskyFormModel model);
    Task EditWhiskyAsync(int whiskyId, WhiskyFormModel model);
    Task DeleteAsync(int id);
    Task<bool> WhiskyInFavouritesAsync(string userId, int whiskyId);
    Task AddToFavouritesAsync(string userId, int whiskyId);
    Task RemoveFromFavouritesAsync(string userId, int whiskyId);
    Task<ICollection<MyCollectionWhiskyModel>> MyFavouriteWhiskiesAsync(string userId);
    Task<IEnumerable<WhiskyDistilleryViewModel>> GetWhiskiesByDistilleryIdAsync(int distilleryId);
    Task<string?> GetWhiskyPublisherAsync(int id);
    Task ApproveWhiskyAsync(int id);
    Task<List<int>> GetAllWhiskiesIdsAsync();
}
