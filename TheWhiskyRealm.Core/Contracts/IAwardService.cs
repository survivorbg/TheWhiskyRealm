using TheWhiskyRealm.Core.Models.Whisky;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Core.Contracts;

public interface IAwardService
{
    Task<ICollection<AwardViewModel>> GetAllWhiskyAwards(int id);
}
