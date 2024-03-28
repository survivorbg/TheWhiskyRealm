using TheWhiskyRealm.Core.Models.Whisky;

namespace TheWhiskyRealm.Core.Contracts;

public interface IAwardService
{
    Task<ICollection<AwardViewModel>> GetAllWhiskyAwards(int id);
    Task EditAwardAsync(AwardViewModel model);
    Task<AwardViewModel?> GetAwardByIdAsync(int id);
    Task<bool> AwardExistAsync(int id);
    Task DeleteAwardAsync(int id);
}
