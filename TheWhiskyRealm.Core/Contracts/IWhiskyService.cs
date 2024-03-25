using TheWhiskyRealm.Core.Models;
using TheWhiskyRealm.Core.Models.Whisky.Add;

namespace TheWhiskyRealm.Core.Contracts;

public interface IWhiskyService
{
    Task<IEnumerable<AllWhiskyModel>> GetPagedWhiskiesAsync(int skip, int take);
    Task<IEnumerable<AllWhiskyModel>> GetMoreWhiskiesAsync(int skip, int take);
    Task<bool> WhiskyExistAsync(int id);
    Task<DetailsWhiskyViewModel> GetWhiskyByIdAsync(int id);
    Task<WhiskyFormModel> GetWhiskyByIdForEditAsync(int id);
    Task AddWhiskyAsync(WhiskyFormModel model);
    Task EditWhiskyAsync(int whiskyId, WhiskyFormModel model);
}
