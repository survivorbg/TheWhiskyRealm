using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Whisky;
using TheWhiskyRealm.Infrastructure.Data.Common;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Core.Services;

public class AwardService : IAwardService
{
    private readonly IRepository repo;

    public AwardService(IRepository repo)
    {
        this.repo = repo;
    }
    public async Task<ICollection<AwardViewModel>> GetAllWhiskyAwards(int id)
    {
        return await repo
            .AllReadOnly<Award>()
            .Where(a => a.WhiskyId == id)
            .Select(a => new AwardViewModel
            {
                Id = a.Id,
                AwardsCeremony = a.AwardsCeremony,
                MedalType = a.MedalType.ToString(),
                Title = a.Title,
                Year = a.Year
            })
            .ToListAsync();
    }
}
