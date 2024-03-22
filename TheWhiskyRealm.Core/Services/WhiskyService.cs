using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Infrastructure.Data.Common;

namespace TheWhiskyRealm.Core.Services;

public class WhiskyService : IWhiskyService
{
    private readonly IRepository repo;

    public WhiskyService(IRepository repo)
    {
        this.repo = repo;
    }


}
